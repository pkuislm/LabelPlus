using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LabelPlus.APIManager;

namespace LabelPlus
{
    public partial class MoetranDownloader : Form
    {
        private APIManager _apiManager;
        private Dictionary<string, Image> _thumbnailCache;

        private List<ProjectTarget> _projectTargets;
        private List<ProjectFile> _projectFiles;
        private string _currentProjectId;
        private string _currentProjectName;

        private CancellationTokenSource _loadCancellationTokenSource;
        private CancellationTokenSource _thumbnailCancellationTokenSource;

        public MoetranDownloader(APIManager mgr)
        {
            InitializeComponent();

            Language.InitFormLanguage(this, StringResources.GetValue("lang"));

            targetLanguageSelect.Enabled = false;
            downloadTranslationButton.Enabled = false;
            downloadCheckedTranslationButton.Enabled = false;
            downloadProjectButton.Enabled = false;

            _apiManager = mgr;
            _thumbnailCache = new Dictionary<string, Image>();
            LoadTeamsAsync();
        }

        private async Task<bool> LoadProjectByTargetId(string targetId, CancellationToken cancellationToken)
        {
            targetLanguageSelect.Enabled = false;
            downloadTranslationButton.Enabled = false;
            downloadCheckedTranslationButton.Enabled = false;
            downloadProjectButton.Enabled = false;

            _listView.Items.Clear();
            _imageList.Images.Clear();
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                _projectFiles = await _apiManager.GetAsync<List<ProjectFile>>(
                        $"/projects/{_currentProjectId}/files?page=1&limit=100000&word=&target={targetId}");

                cancellationToken.ThrowIfCancellationRequested();

                int imageCount = 0;
                foreach (var file in _projectFiles)
                {
                    // 检查是否已取消
                    if (cancellationToken.IsCancellationRequested)
                    {
                        _statusLabel.Text = "加载已取消";
                        return false;
                    }

                    var item = new ListViewItem
                    {
                        Text = file.name,
                        Tag = file,
                        ImageKey = file.id
                    };

                    // 为平铺视图添加子项
                    item.SubItems.Add($"标记: {file.source_count}");
                    item.SubItems.Add($"翻译: {file.translated_source_count}");
                    item.SubItems.Add($"校对: {file.checked_source_count}");
                    _listView.Items.Add(item);
                    imageCount++;
                }

                _thumbnailCancellationTokenSource?.Cancel();
                _thumbnailCancellationTokenSource = new CancellationTokenSource();

                _ = LoadThumbnailsBatchAsync(_projectFiles, _thumbnailCancellationTokenSource.Token);

                _statusLabel.Text = $"已读取 {imageCount} 张图片";
                return true;
            }
            catch (OperationCanceledException)
            {
                _statusLabel.Text = "加载已取消";
                return false;
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"加载文件失败: {ex.Message}", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                _statusLabel.Text = "加载失败";
                return false;
            }
        }

        private async Task LoadThumbnailsBatchAsync(List<ProjectFile> files, CancellationToken cancellationToken)
        {
            int total = files.Count;
            int loaded = 0;
            int concurrent = 5; // 同时加载5个缩略图

            try
            {
                var semaphore = new SemaphoreSlim(concurrent);
                var tasks = files.Select(async file =>
                {
                    await semaphore.WaitAsync(cancellationToken);
                    try
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await LoadThumbnailAsync(file, cancellationToken);

                        loaded++;
                        if (loaded % 5 == 0 || loaded == total) // 每5个更新一次状态
                        {
                            _statusLabel.Text = $"正在加载缩略图... ({loaded}/{total})";
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }).ToList();

                await Task.WhenAll(tasks);

                if (!cancellationToken.IsCancellationRequested)
                {
                    _statusLabel.Text = $"已加载 {total} 个图片文件";
                }
            }
            catch (OperationCanceledException)
            {
                _statusLabel.Text = "缩略图加载已取消";
            }
        }

        private async Task LoadProjectFilesAsync(TreeNode projectNode, CancellationToken cancellationToken)
        {
            targetLanguageSelect.Enabled = false;
            downloadTranslationButton.Enabled = false;
            downloadCheckedTranslationButton.Enabled = false;
            downloadProjectButton.Enabled = false;

            if (projectNode != null)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var project = projectNode.Tag as Project;
                    //先加载所有目标语言
                    _projectTargets = await _apiManager.GetAsync<List<ProjectTarget>>(
                        $"/projects/{project.id}/targets?page=1&limit=100000&word=");

                    cancellationToken.ThrowIfCancellationRequested();

                    targetLanguageSelect.Items.Clear();
                    targetLanguageSelect.Sorted = false;
                    foreach (var target in _projectTargets)
                    {
                        targetLanguageSelect.Items.Add(target.language.ToString());
                    }
                    targetLanguageSelect.SelectedIndex = 0;//默认第一个

                    cancellationToken.ThrowIfCancellationRequested();

                    _statusLabel.Text = $"正在加载 {project.name} 的图片...";

                    if (await LoadProjectByTargetId(_projectTargets[targetLanguageSelect.SelectedIndex].id, cancellationToken))
                    {
                        targetLanguageSelect.Enabled = true;
                        downloadTranslationButton.Enabled = true;
                        downloadCheckedTranslationButton.Enabled = true;
                        downloadProjectButton.Enabled = true;
                    }
                }
                catch (OperationCanceledException)
                {
                    _statusLabel.Text = "加载已取消";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载文件失败: {ex.Message}\n您可能没有浏览此项目的权限", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _statusLabel.Text = "加载失败";
                }
            }
        }

        private async void LoadTeamsAsync()
        {
            try
            {
                _statusLabel.Text = "正在加载团队列表...";
                _treeView.Nodes.Clear();

                var teams = await _apiManager.GetAsync<List<Team>>("/user/teams?page=1&limit=1000&word=");

                foreach (var team in teams)
                {
                    var teamNode = new TreeNode(team.name)
                    {
                        Tag = team,
                        ImageIndex = 0
                    };
                    // 添加占位节点以显示展开箭头
                    teamNode.Nodes.Add(new TreeNode("加载中..."));
                    _treeView.Nodes.Add(teamNode);
                }

                _statusLabel.Text = $"已加载 {teams.Count} 个团队";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载团队失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _statusLabel.Text = "加载失败";
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

        private async void TreeView_AfterNodeClick(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;
            // 选中项目节点时加载图片
            if (node.Tag is Project proj)
            {
                // 取消之前的加载操作
                _loadCancellationTokenSource?.Cancel();
                _loadCancellationTokenSource = new CancellationTokenSource();

                // 取消缩略图加载
                _thumbnailCancellationTokenSource?.Cancel();

                // 开始新的加载
                _currentProjectName = proj.name;
                _currentProjectId = proj.id;
                await LoadProjectFilesAsync(node, _loadCancellationTokenSource.Token);
            }
        }


        private async Task LoadProjectSetsAsync(TreeNode teamNode)
        {
            try
            {
                var team = teamNode.Tag as Team;
                _statusLabel.Text = $"正在加载 {team.name} 的项目集...";

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
                    psNode.Nodes.Add(new TreeNode("加载中..."));
                    teamNode.Nodes.Add(psNode);
                }

                _statusLabel.Text = $"已加载 {projectSets.Count} 个项目集";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载项目集失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _statusLabel.Text = "加载失败";
            }
        }

        private async Task LoadProjectsAsync(TreeNode projectSetNode)
        {
            try
            {
                var ps = projectSetNode.Tag as ProjectSet;
                _statusLabel.Text = $"正在加载 {ps.name} 的项目...";

                projectSetNode.Nodes.Clear();
                var projects = await _apiManager.GetAsync<List<Project>>(
                    $"/teams/{ps.team_id}/projects?project_set={ps.id}&page=1&limit=10000&word=&status=0");

                foreach (var project in projects)
                {
                    //// 保存项目信息以便搜索
                    //project.project_set_name = ps.name;
                    //project.team_id = ps.team_id;
                    //if (!_allProjects.Any(p => p.id == project.id))
                    //{
                    //    _allProjects.Add(project);
                    //}

                    var projectNode = new TreeNode(
                        $"{project.name} ({project.source_count} 个标签)")
                    {
                        Tag = project
                    };
                    projectSetNode.Nodes.Add(projectNode);
                }

                _statusLabel.Text = $"已加载 {projects.Count} 个项目";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载项目失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _statusLabel.Text = "加载失败";
            }
        }

        private async Task LoadThumbnailAsync(ProjectFile file, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (_thumbnailCache.ContainsKey(file.id))
                {
                    if (!_imageList.Images.ContainsKey(file.id))
                    {
                        _imageList.Images.Add(file.id, _thumbnailCache[file.id]);
                    }
                    return;
                }

                cancellationToken.ThrowIfCancellationRequested();

                var imageBytes = await _apiManager.GetByteArrayAsync(file.cover_url);

                cancellationToken.ThrowIfCancellationRequested();

                using (var ms = new MemoryStream(imageBytes))
                {
                    var thumbnail = Image.FromStream(ms);
                    cancellationToken.ThrowIfCancellationRequested();
                    _thumbnailCache[file.id] = thumbnail;

                    // 在 UI 线程上添加图片
                    if (_listView.InvokeRequired)
                    {
                        _listView.Invoke(new Action(() =>
                        {
                            if (!_imageList.Images.ContainsKey(file.id))
                            {
                                _imageList.Images.Add(file.id, thumbnail);
                                _listView.Refresh(); // 强制刷新
                            }
                        }));
                    }
                    else
                    {
                        if (!_imageList.Images.ContainsKey(file.id))
                        {
                            _imageList.Images.Add(file.id, thumbnail);
                            _listView.Refresh();
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 加载被取消，正常情况
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载缩略图失败: {ex.Message}");
                // 加载失败时使用默认图标
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 清理缓存的图片
            foreach (var img in _thumbnailCache.Values)
            {
                img?.Dispose();
            }
            _thumbnailCache.Clear();
            base.OnFormClosing(e);
        }

        private async Task DownloadTranslationsAsync(bool downloadChecked, IProgress<DownloadProgress> progress)
        {
            var targetId = _projectTargets[targetLanguageSelect.SelectedIndex].id;

            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                    saveFileDialog.FileName = SanitizeFileName(_currentProjectName + ".txt");
                    saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog.FilterIndex = 2;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        var filePath = saveFileDialog.FileName;
                        if (File.Exists(filePath))
                        {
                            if (MessageBox.Show("File exists! Do you want to overwrite it?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                        }

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        StreamWriter sr = new StreamWriter(fs, Encoding.UTF8);

                        sr.WriteLine("1,0\r\n-\r\n框内\r\n框外\r\n-\r\nDefault Comment\r\nYou can edit me\r\n");

                        int total = _projectFiles.Count;
                        int current = 0;

                        foreach (var file in _projectFiles)
                        {
                            current++;

                            progress?.Report(new DownloadProgress(
                                current,
                                total,
                                $"正在下载翻译：{file.name}"
                            ));

                            var items = await _apiManager.GetAsync<List<TranslationSource>>(
                                $"/files/{file.id}/sources?target_id={targetId}&paging=false");

                            sr.WriteLine($">>>>>>>>[{file.name}]<<<<<<<<");

                            int count = 0;
                            foreach (var n in items)
                            {
                                count++;
                                sr.WriteLine(
                                    $"----------------[{count}]----------------[{n.x:F3},{n.y:F3},{n.position_type}]");

                                var trs = n.translations.FirstOrDefault();
                                sr.WriteLine(
                                    downloadChecked && !string.IsNullOrEmpty(trs?.proofread_content)
                                        ? trs.proofread_content
                                        : trs?.content);
                            }
                        }
                        sr.Close();
                        fs.Close();
                        MessageBox.Show("Saved successfully.", "Info", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"翻译下载失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _statusLabel.Text = "加载失败";
            }
        }

        public static string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return string.Empty;

            // 替换非法字符为下划线
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in invalidChars)
            {
                fileName = fileName.Replace(c, '_');
            }

            // 处理 Windows 保留名称
            string[] reservedNames = { "CON", "PRN", "AUX", "NUL",
                "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
                "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };

            string nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);

            if (reservedNames.Contains(nameWithoutExt.ToUpper()))
            {
                fileName = "_" + fileName;
            }

            return fileName;
        }

        private async Task<string> DownloadFilesAsync(IProgress<DownloadProgress> progress)
        {
            var targetId = _projectTargets[targetLanguageSelect.SelectedIndex].id;

            try
            {
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.SelectedPath = Directory.GetCurrentDirectory();

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        var folderPath = Path.Combine(folderBrowserDialog.SelectedPath, SanitizeFileName(_currentProjectName));
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        var filePath = Path.Combine(folderPath, SanitizeFileName(_currentProjectName + ".txt"));
                        if (File.Exists(filePath))
                        {
                            if (MessageBox.Show("File exists! Do you want to overwrite it?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return null;
                            }
                        }

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        StreamWriter sr = new StreamWriter(fs, Encoding.UTF8);

                        sr.WriteLine("1,0\r\n-\r\n框内\r\n框外\r\n-\r\nDefault Comment\r\nYou can edit me\r\n");

                        int total = _projectFiles.Count;
                        int current = 0;

                        foreach (var file in _projectFiles)
                        {
                            current++;

                            progress?.Report(new DownloadProgress(
                                current,
                                total,
                                $"正在下载文件：{file.name}"
                            ));

                            File.WriteAllBytes(
                                Path.Combine(folderPath, file.name),
                                await _apiManager.GetByteArrayAsync(file.url));

                            var items = await _apiManager.GetAsync<List<TranslationSource>>(
                                $"/files/{file.id}/sources?target_id={targetId}&paging=false");

                            sr.WriteLine($">>>>>>>>[{file.name}]<<<<<<<<");

                            int count = 0;
                            foreach (var n in items)
                            {
                                count++;
                                sr.WriteLine(
                                    $"----------------[{count}]----------------[{n.x:F3},{n.y:F3},{n.position_type}]");

                                var trs = n.translations.FirstOrDefault();
                                sr.WriteLine(
                                    !string.IsNullOrEmpty(trs?.proofread_content)
                                        ? trs.proofread_content
                                        : trs?.content);
                            }
                        }
                        sr.Close();
                        fs.Close();

                        var mark = Path.Combine(folderPath, "moetran.project");
                        using (var markFs = new FileStream(mark, FileMode.Create))
                        using(var bw = new BinaryWriter(markFs))
                        {
                            bw.Write(_currentProjectId);
                            bw.Write(targetId);
                        }

                        return filePath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"翻译下载失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _statusLabel.Text = "加载失败";
            }
            return null;
        }

        private void downloadButton1_Click(object sender, EventArgs e)
        {
            var dlg = new DownloadProgressForm
            {
                Text = "正在下载翻译…",
                Work = p => DownloadTranslationsAsync(false, p)
            };

            dlg.ShowDialog(this);
        }

        private void downloadButton2_Click(object sender, EventArgs e)
        {
            var dlg = new DownloadProgressForm
            {
                Text = "正在下载翻译…",
                Work = p => DownloadTranslationsAsync(true, p)
            };

            dlg.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dlg = new DownloadProgressForm
            {
                Text = "正在下载文件…",
                Work = async p =>
                {
                    var txtPath = await DownloadFilesAsync(p);

                    if(MessageBox.Show("Do you want to open this project now?", "Download successful", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Invoke(new Action(() =>
                        {
                            var mainFrm = Application.OpenForms
                                .OfType<MainFrm>()
                                .FirstOrDefault();

                            mainFrm?.OpenWorkspace(txtPath);
                        }));
                    }
                }
            };

            dlg.ShowDialog(this);
        }
    }
}
