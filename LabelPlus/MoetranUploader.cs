using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LabelPlus.APIManager;

namespace LabelPlus
{
    public partial class MoetranUploader : Form
    {
        private APIManager _apiManager;
        private Workspace _wsp;
        private Dictionary<string, Dictionary<string, Dictionary<string, Project>>> _allProjects;

        public MoetranUploader(APIManager mgr, Workspace wsp)
        {
            InitializeComponent();
            Language.InitFormLanguage(this, StringResources.GetValue("lang"));
            _apiManager = mgr;
            _wsp = wsp;
            _allProjects = new Dictionary<string, Dictionary<string, Dictionary<string, Project>>>();
            LoadTeamsAsync();
        }

        public void UploadProject()
        {
            if (File.Exists(Path.Combine(_wsp.DirPath, "moetran.project")))
            {
                if(MessageBox.Show("检测到此项目为龙译项目，是否继续上传？\n警告：该操作将删除所有远端标签！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                using(var ms = new MemoryStream(File.ReadAllBytes(Path.Combine(_wsp.DirPath, "moetran.project"))))
                using(var br = new BinaryReader(ms))
                {
                    var dlg = new DownloadProgressForm
                    {
                        Text = "正在上传翻译…",
                        Work = p => DoUpload(_wsp.DirPath, br.ReadString(), br.ReadString(), p)
                    };

                    dlg.ShowDialog(this);
                }
            }
            else
            {
                Show();
            }
        }

        public async Task DoUpload(string path, string projectId, string targetId, IProgress<DownloadProgress> progress)
        {
            //local > online
            var files = (await _apiManager.GetAsync<List<ProjectFile>>(
                $"/projects/{projectId}/files?page=1&limit=100000&word=&target={targetId}")).ToDictionary(k => k.name, v => v);

            var current = 0;
            var total = _wsp.Store.Filenames.Count();
            foreach (var localFile in _wsp.Store.Filenames)
            {
                progress?.Report(new DownloadProgress(
                    current,
                    total,
                    $"正在上传：{localFile}"
                ));

                var fileId = "";
                if (!files.ContainsKey(localFile))
                {
                    //Upload
                    using (var content = new MultipartFormDataContent())
                    {
                        var fileBytes = File.ReadAllBytes(Path.Combine(path, localFile));
                        var fileContent = new ByteArrayContent(fileBytes);

                        content.Add(fileContent, "file", localFile);

                        var result = await _apiManager.PostAsync<ProjectFile>(
                            $"/projects/{projectId}/files", content);

                        fileId = result.id;
                    }
                }
                else
                {
                    fileId = files[localFile].id;
                }
                await ProcessLabels(fileId, targetId, _wsp.Store[localFile]);
                current++;
            }
            MessageBox.Show("Upload successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private async Task ProcessLabels(string fileId, string targetId, List<LabelItem> labels)
        {
            var ob = await _apiManager.GetAsync<List<TranslationSource>>(
                $"/files/{fileId}/sources?target_id={targetId}&paging=false");

            //delete all original translations
            foreach(var oldLabel in ob)
            {
                await _apiManager.DeleteAsync($"/sources/{oldLabel.id}");
            }

            foreach(var label in labels)
            {
                var nLabel = new TranslationSourceRequest()
                {
                    x = label.X_percent,
                    y = label.Y_percent,
                    position_type = label.Category
                };
                var translation = await _apiManager.PostAsync<TranslationSourceResponse>($"/files/{fileId}/sources", nLabel);
                
                if(!string.IsNullOrEmpty(label.Text))
                {
                    var nTrans = new TranslationSourceItemRequest()
                    {
                        content = label.Text,
                        target_id = targetId,
                    };
                    await _apiManager.PostAsync<TranslationSource>($"/sources/{translation.id}/translations", nTrans);
                }
            }
        }

        private async void LoadTeamsAsync()
        {
            try
            {
                _treeView.Nodes.Clear();
                _allProjects.Clear();

                var teams = await _apiManager.GetAsync<List<Team>>("/user/teams?page=1&limit=1000&word=");

                foreach (var team in teams)
                {
                    var teamNode = new TreeNode(team.name)
                    {
                        Tag = team,
                        ImageIndex = 0
                    };
                    _allProjects.Add(team.name, new Dictionary<string, Dictionary<string, Project>>());
                    // 添加占位节点以显示展开箭头
                    teamNode.Nodes.Add(new TreeNode("加载中..."));
                    _treeView.Nodes.Add(teamNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载团队失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void TreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;

            // 展开团队节点时加载项目集
            if (node.Tag is Team && node.Nodes.Count == 1 && node.Nodes[0].Text == "加载中...")
            {
                await LoadProjectSetsAsync(node);
            }
            // 展开项目集节点时加载项目
            else if (node.Tag is ProjectSet && node.Nodes.Count == 1 && node.Nodes[0].Text == "加载中...")
            {
                await LoadProjectsAsync(node);
            }
        }

        private async Task LoadProjectSetsAsync(TreeNode teamNode)
        {
            try
            {
                var team = teamNode.Tag as Team;
                var tsp = _allProjects[team.name];

                teamNode.Nodes.Clear();
                var projectSets = await _apiManager.GetAsync<List<ProjectSet>>($"/teams/{team.id}/project-sets?page=1&limit=1000&word=");

                foreach (var ps in projectSets)
                {
                    var psNode = new TreeNode($"{ps.name}")
                    {
                        Tag = new ProjectSet
                        {
                            id = ps.id,
                            name = ps.name,
                            team_id = team.id
                        }
                    };

                    tsp.Add(ps.name, new Dictionary<string, Project>());

                    psNode.Nodes.Add(new TreeNode("加载中..."));
                    teamNode.Nodes.Add(psNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载项目集失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadProjectsAsync(TreeNode projectSetNode)
        {
            try
            {
                var ps = projectSetNode.Tag as ProjectSet;
                var pn = projectSetNode.Parent.Tag as Team;
                var tp = _allProjects[pn.name][ps.name];

                projectSetNode.Nodes.Clear();
                var projects = await _apiManager.GetAsync<List<Project>>(
                    $"/teams/{ps.team_id}/projects?project_set={ps.id}&page=1&limit=10000&word=&status=0");

                foreach (var project in projects)
                {
                    tp.Add(project.name, project);

                    var projectNode = new TreeNode(
                        $"{project.name} ({project.source_count} 个标签)")
                    {
                        Tag = project
                    };
                    projectSetNode.Nodes.Add(projectNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载项目失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text?.Trim()))
            {
                MessageBox.Show("Please input a project name!", "Info", MessageBoxButtons.OK);
                return;
            }

            var node = _treeView.SelectedNode;

            if (node.Tag is Project)
            {
                node = node.Parent;
            }
            else if (node.Tag is Team)
            {
                MessageBox.Show("Please select a project set!", "Info", MessageBoxButtons.OK);
                return;
            }

            var ps = node.Tag as ProjectSet;
            var team = node.Parent.Tag as Team;

            //查重
            if (_allProjects[team.name][ps.name].ContainsKey(textBox1.Text.Trim()))
            {
                //是否使用已有项目
                MessageBox.Show("Project exists!", "Info", MessageBoxButtons.OK);
                return;
            }
            //创建新项目
            var req = new NewProjectRequest()
            {
                project_set = ps.id,
                name = textBox1.Text.Trim(),
                intro = textBox2.Text?.Trim(),
            };

            var newProject = await _apiManager.PostAsync<NewProjectResponse>($"/teams/{team.id}/projects", req);

            var target = (await _apiManager.GetAsync<List<ProjectTarget>>(
                    $"/projects/{newProject.project.id}/targets?page=1&limit=100000&word=")).First();

            var mark = Path.Combine(_wsp.DirPath, "moetran.project");
            using (var markFs = new FileStream(mark, FileMode.Create))
            using (var bw = new BinaryWriter(markFs))
            {
                bw.Write(newProject.project.id);
                bw.Write(target.id);
            }

            var dlg = new DownloadProgressForm
            {
                Text = "正在上传翻译…",
                Work = p => DoUpload(_wsp.DirPath, newProject.project.id, target.id, p)
            };

            dlg.ShowDialog(this);
        }
    }
}
