/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

#region Using Directives
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
#endregion

namespace LabelPlus
{
    public partial class MainFrm : Form
    {

        #region Constants

        static readonly string APPNAME = LabelPlus.Properties.Resources.AppName;
        static readonly string APPVER = LabelPlus.Properties.Resources.AppVer;
        static readonly string FROM_TITLE = APPNAME + " " + APPVER + " ";

        #endregion

        #region Fields

        Workspace wsp = new Workspace();
        WorkspaceControlAdpter wsp_control_apt;
        ZoomAdaptor zoomAdaptor;
        LangComboxAdaptor langComboxApt;
        DataGridViewAdapter dataGridViewAdapter;
        APIManager apiManager = new APIManager();
        MoetranDownloader moetranDownloader;
        MoetranUploader moetranUploader;

        #endregion

        #region Methods

        DialogResult alter_and_save()
        {
            DialogResult result = MessageBox.Show(
                StringResources.GetValue("save_question"),
                "save",
                MessageBoxButtons.YesNoCancel);

            if (result == System.Windows.Forms.DialogResult.Yes)
                return save_file(false);

            return result;
        }
        DialogResult save_file(bool newPath)
        {
            if (!newPath && wsp.HavePath)
            {
                if (wsp.Save())
                {
                    return System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    return System.Windows.Forms.DialogResult.Cancel;
                }

            }
            else
            {
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    wsp.Path = saveFileDialog.FileName;
                    if (wsp.Save())
                    {
                        return System.Windows.Forms.DialogResult.OK;

                    }
                    else
                    {
                        return System.Windows.Forms.DialogResult.Cancel;
                    }
                }
                else return System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint, true);

            wsp.NewFile();

            this.Text = FROM_TITLE;
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (wsp.NeedSave)
            {
                DialogResult tmp = MessageBox.Show(StringResources.GetValue("save_question"), "save", MessageBoxButtons.YesNoCancel);
                if (tmp == System.Windows.Forms.DialogResult.Yes)
                {
                    if (save_file(false) == System.Windows.Forms.DialogResult.OK)
                    {
                        MessageBox.Show(StringResources.GetValue("save_complete"));
                        e.Cancel = false;
                        this.Close();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else if (tmp == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = false;
                }
                else if (tmp == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }

            Properties.Settings.Default.Save();
        }

        private void exitEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void OpenWorkspace(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return;

            if (wsp.NeedSave)
                alter_and_save();

            try
            {
                wsp.readWorkspaceFromFile(filePath);
            }
            catch (Exception exp)
            {
                MessageBox.Show(
                    StringResources.GetValue("error_openfilefail") + "\r\n" + exp,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            this.Text = FROM_TITLE + new FileInfo(filePath).Name;

            wsp_control_apt.page_right();
            toolStripComboBox_File.DroppedDown = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenWorkspace(openFileDialog.FileName);
            }
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wsp.NeedSave)
            {
                var saveResult = alter_and_save();
                if (saveResult == System.Windows.Forms.DialogResult.Yes)
                    MessageBox.Show(StringResources.GetValue("save_complete"));
                else if (saveResult == System.Windows.Forms.DialogResult.Cancel)
                    return;

            }

            wsp_control_apt.NewFile();
            wsp.NewFile();
            this.Text = FROM_TITLE;

            var dlg = new FolderPicker();
            dlg.InputPath = @"c:\";

            //folderBrowserDialog.Description = StringResources.GetValue("tip_chose_photo_dir");
            if (dlg.ShowDialog(IntPtr.Zero) == true)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dlg.ResultPath);
                if (dirInfo.Exists)
                {
                    //目录可用
                    string filename = "work.txt";
                    int order = 0;
                    do
                    {
                        filename = StringResources.GetValue("default_file_name") + "_" + order + ".txt";
                        wsp.Path = dirInfo.FullName + "\\" + filename;
                        ++order;
                    } while (File.Exists(wsp.Path));

                    if (wsp.Save())
                    {
                        //显示提示
                        string tip = StringResources.GetValue("tip_new_file_be_saved");
                        tip = String.Format(tip, wsp.Path);
                        MessageBox.Show(tip);
                        this.Text = FROM_TITLE + filename;

                        //显示管理图片窗口
                        imageToolStripMenuItem_Click(null, null);
                        wsp.Save();
                    }
                }

            }


        }
        private void saveAsDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save_file(true) == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show(StringResources.GetValue("save_complete"));
                this.Text = FROM_TITLE + new FileInfo(wsp.Path).Name;
            }
        }
        private void saveSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save_file(false) == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show(StringResources.GetValue("save_complete"));
                this.Text = FROM_TITLE + new FileInfo(wsp.Path).Name;
            }
        }
        
        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wsp.HavePath)
            {
                new ManageImageFrm(wsp).ShowDialog();
            }
            else
            {
                MessageBox.Show(StringResources.GetValue("input_images_need_save"));
            }
        }

        private void toolStripButton_Left_Click(object sender, EventArgs e)
        {
            wsp_control_apt.page_left();
        }
        private void toolStripButton_Right_Click(object sender, EventArgs e)
        {
            wsp_control_apt.page_right();
        }
        private void toolStripButton_Clear_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                StringResources.GetValue("clear_all_label_question"),
                "warning！！！",
                MessageBoxButtons.YesNo);

            if (result == System.Windows.Forms.DialogResult.Yes)
                wsp.Store.DelAllLabelInFile(wsp_control_apt.FileName);

        }
        private void toolStripButton_EditBig_Click(object sender, EventArgs e)
        {
            Font oldFont = TranslateTextBox.Font;
            Font newFont = new Font(oldFont.FontFamily, oldFont.Size + 1, oldFont.Style);

            TranslateTextBox.Font = newFont;
            //listView.Font = newFont;

        }
        private void toolStripButton_EditSmall_Click(object sender, EventArgs e)
        {
            Font oldFont = TranslateTextBox.Font;
            Font newFont = new Font(oldFont.FontFamily, oldFont.Size - 1, oldFont.Style);

            TranslateTextBox.Font = newFont;
            //listView.Font = newFont;

        }


        private void timerAutoSave_Tick(object sender, EventArgs e)
        {
            if (wsp.NeedSave && wsp.HavePath)
            {
                // 分离线程，防止使用 U 盘等低速存储时打字会卡
                var th = new Thread(() => wsp.SaveBAK());
                th.Start();
            }
        }
        private void toolStripButton_HideWindow_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = true;
            this.Visible = false;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon.Visible = false;
            this.Visible = true;
        }

        private void MainFrm_SizeChanged(object sender, EventArgs e)
        {
            SetLayout();
        }

        private void toolStripButton_FileSetting_Click(object sender, EventArgs e)
        {
            new FileSettingFrm(wsp).ShowDialog();
        }

        private void aboutAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AboutFrm().ShowDialog(this);
        }

        private void viewHelpHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://noodlefighter.com/label_plus");
        }

        #endregion

        #region Constructors
        public MainFrm()
        {
            InitializeComponent();

            Language.InitFormLanguage(this, StringResources.GetValue("lang"));

            //ToolStripButtonGroup modeBtnGroup = new ToolStripButtonGroup(toolStrip);
            //modeBtnGroup.AddButton(toolStripButton_BrowseMode);
            //modeBtnGroup.AddButton(toolStripButton_EditLabelMode);
            //modeBtnGroup.AddButton(toolStripButton_InputMode);
            //modeBtnGroup.AddButton(toolStripButton_CheckMode);

            dataGridViewAdapter = new DataGridViewAdapter(dataGridView1, wsp.GroupDefine);

            apiManager.Init();

            wsp_control_apt = new WorkspaceControlAdpter(
                toolStripComboBox_File,
                TranslateTextBox,
                TextBox_GroupBox,
                dataGridViewAdapter,
                picView,
                contextMenuStripQuickText,
                toolStrip,
                wsp,
                apiManager);

            zoomAdaptor = new ZoomAdaptor(picView,
                toolStripButton_ZoomPlus,
                toolStripButton_ZoomMinus,
                toolStripComboBox_Zoom);

            langComboxApt = new LangComboxAdaptor(langToolStripComboBox, this);

            SetLayout();

            SetFont(new Font(GlobalVar.FontName, GlobalVar.FontSize));
        }
        #endregion

        #region SetLayout
        enum LayoutStatus { Horizontal, Vertical };
        LayoutStatus CurrentLayout = LayoutStatus.Horizontal;

        private void SetLayout()
        {
            double h = this.ClientSize.Height;
            double w = this.ClientSize.Width;

            if (h == 0 || w == 0) return;

            if (h > w * 1.5) // set to Vertical
            {
                if (CurrentLayout == LayoutStatus.Horizontal) // Event: change layout
                {
                    splitContainer.Orientation = Orientation.Horizontal;
                    splitContainer1.Orientation = Orientation.Vertical;
                    splitContainer.SplitterDistance = (int)(splitContainer.ClientSize.Height * 0.85);
                    splitContainer1.SplitterDistance = (int)(splitContainer1.ClientSize.Width * 0.618);

                    CurrentLayout = LayoutStatus.Vertical;
                }
            }
            else // set to Horizontal
            {
                if (CurrentLayout == LayoutStatus.Vertical)
                {
                    splitContainer.Orientation = Orientation.Vertical;
                    splitContainer1.Orientation = Orientation.Horizontal;
                    splitContainer.SplitterDistance = (int)(splitContainer.ClientSize.Width * 0.618);
                    splitContainer1.SplitterDistance = (int)(splitContainer1.ClientSize.Height * 0.618);

                    CurrentLayout = LayoutStatus.Horizontal;
                }
            }
        }
        #endregion

        public void SetFont(Font font)
        {
            TranslateTextBox.Font = font;
            dataGridViewAdapter.SetFont(font);
        }

        private void picView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.T)
            {
                toolStripButton_HideWindow_Click(this, null);
            }
        }

        private void MainFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                saveSToolStripMenuItem_Click(sender, new EventArgs());
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog
            {
                Font = TranslateTextBox.Font
            };

            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                SetFont(fontDlg.Font);

                GlobalVar.FontName = fontDlg.Font.Name;
                GlobalVar.FontSize = fontDlg.Font.Size;
                GlobalVar.Save();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(moetranDownloader == null || moetranDownloader.IsDisposed)
            {
                moetranDownloader = new MoetranDownloader(apiManager);
            }

            if (!apiManager.IsLoggedIn())
            {
                MessageBox.Show("Please login before using this function.");
                apiManager.Login(() =>
                {
                    moetranDownloader.Show();
                });
            }
            else
            {
                moetranDownloader.Show();
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!apiManager.IsLoggedIn())
            {
                loginStatusTextBox.Text = "Not logged in";
                downloadRemoteProjectMenuItem.Enabled = false;
                uploadLocalProjectMenuItem.Enabled = false;
                loginMenuItem.Enabled = true;
                logoutMenuItem.Enabled = false;
            }
            else
            {
                loginStatusTextBox.Text = string.Format("User: {0}", apiManager.UserName);
                downloadRemoteProjectMenuItem.Enabled = true;
                uploadLocalProjectMenuItem.Enabled = true;
                loginMenuItem.Enabled = false;
                logoutMenuItem.Enabled = true;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                if (moetranUploader == null || moetranUploader.IsDisposed)
                {
                    moetranUploader = new MoetranUploader(apiManager, wsp);
                }

                if (!apiManager.IsLoggedIn())
                {
                    MessageBox.Show("Please login before using this function.");
                    apiManager.Login(() =>
                    {
                        moetranUploader.UploadProject();
                    });
                }
                else
                {
                    moetranUploader.UploadProject();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                "Upload failed：" + ex.Message,
                "Error",
                MessageBoxButtons.OK);
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (apiManager.IsLoggedIn())
            {
                apiManager.Logout();
            }
            apiManager.Login(null);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            apiManager.Logout();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(wsp.setVisualWhenIndexChanged)
            {
                wsp.setVisualWhenIndexChanged = false;
                toolStripButton2.Image = Properties.Resources.UnFollow;
            }
            else
            {
                wsp.setVisualWhenIndexChanged = true;
                toolStripButton2.Image = Properties.Resources.Follow;
            }
        }
    }
}
