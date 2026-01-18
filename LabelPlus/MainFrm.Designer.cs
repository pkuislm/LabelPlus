namespace LabelPlus
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadRemoteProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadLocalProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loginStatusTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.loginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutAToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.langToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_HideWindow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelPicView = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripPicView = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Right = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Left = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox_File = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton_ZoomPlus = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_ZoomMinus = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox_Zoom = new System.Windows.Forms.ToolStripComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelLabels = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripLabels = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_EditBig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_EditSmall = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TextBox_GroupBox = new System.Windows.Forms.GroupBox();
            this.contextMenuStripQuickText = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labelCtrlEnterTip = new System.Windows.Forms.Label();
            this.TranslateTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.timerAutoSave = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.picView = new LabelPlus.PicView();
            this.menuStrip1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tableLayoutPanelPicView.SuspendLayout();
            this.toolStripPicView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanelLabels.SuspendLayout();
            this.toolStripLabels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.TextBox_GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.connectToolStripMenuItem,
            this.helpAToolStripMenuItem,
            this.langToolStripComboBox});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1193, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveProjectSToolStripMenuItem,
            this.saveAsDToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitEToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 25);
            this.fileToolStripMenuItem.Text = "File(&F)";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.newToolStripMenuItem.Text = "New(&N)";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.openToolStripMenuItem.Text = "Open(&O)";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveProjectSToolStripMenuItem
            // 
            this.saveProjectSToolStripMenuItem.Name = "saveProjectSToolStripMenuItem";
            this.saveProjectSToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveProjectSToolStripMenuItem.Text = "Save(&S)";
            this.saveProjectSToolStripMenuItem.Click += new System.EventHandler(this.saveSToolStripMenuItem_Click);
            // 
            // saveAsDToolStripMenuItem
            // 
            this.saveAsDToolStripMenuItem.Name = "saveAsDToolStripMenuItem";
            this.saveAsDToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveAsDToolStripMenuItem.Text = "Save As(&S)";
            this.saveAsDToolStripMenuItem.Click += new System.EventHandler(this.saveAsDToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 6);
            // 
            // exitEToolStripMenuItem
            // 
            this.exitEToolStripMenuItem.Name = "exitEToolStripMenuItem";
            this.exitEToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.exitEToolStripMenuItem.Text = "Exit(&E)";
            this.exitEToolStripMenuItem.Click += new System.EventHandler(this.exitEToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(126, 25);
            this.imageToolStripMenuItem.Text = "Image Manager(&I)";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.imageToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadRemoteProjectMenuItem,
            this.uploadLocalProjectMenuItem,
            this.toolStripSeparator1,
            this.loginStatusTextBox,
            this.loginMenuItem,
            this.logoutMenuItem});
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(92, 25);
            this.connectToolStripMenuItem.Text = "Connect...(&C)";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // downloadRemoteProjectMenuItem
            // 
            this.downloadRemoteProjectMenuItem.Name = "downloadRemoteProjectMenuItem";
            this.downloadRemoteProjectMenuItem.Size = new System.Drawing.Size(220, 22);
            this.downloadRemoteProjectMenuItem.Text = "Download Project Online";
            this.downloadRemoteProjectMenuItem.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // uploadLocalProjectMenuItem
            // 
            this.uploadLocalProjectMenuItem.Name = "uploadLocalProjectMenuItem";
            this.uploadLocalProjectMenuItem.Size = new System.Drawing.Size(220, 22);
            this.uploadLocalProjectMenuItem.Text = "Upload current project";
            this.uploadLocalProjectMenuItem.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // loginStatusTextBox
            // 
            this.loginStatusTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.loginStatusTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.loginStatusTextBox.Enabled = false;
            this.loginStatusTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.loginStatusTextBox.Name = "loginStatusTextBox";
            this.loginStatusTextBox.ReadOnly = true;
            this.loginStatusTextBox.Size = new System.Drawing.Size(100, 16);
            // 
            // loginMenuItem
            // 
            this.loginMenuItem.Name = "loginMenuItem";
            this.loginMenuItem.Size = new System.Drawing.Size(220, 22);
            this.loginMenuItem.Text = "Login";
            this.loginMenuItem.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // logoutMenuItem
            // 
            this.logoutMenuItem.Name = "logoutMenuItem";
            this.logoutMenuItem.Size = new System.Drawing.Size(220, 22);
            this.logoutMenuItem.Text = "Logout";
            this.logoutMenuItem.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // helpAToolStripMenuItem
            // 
            this.helpAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpHToolStripMenuItem,
            this.aboutAToolStripMenuItem1});
            this.helpAToolStripMenuItem.Name = "helpAToolStripMenuItem";
            this.helpAToolStripMenuItem.Size = new System.Drawing.Size(64, 25);
            this.helpAToolStripMenuItem.Text = "Help(&H)";
            // 
            // viewHelpHToolStripMenuItem
            // 
            this.viewHelpHToolStripMenuItem.Name = "viewHelpHToolStripMenuItem";
            this.viewHelpHToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.viewHelpHToolStripMenuItem.Text = "View Help(&H)";
            this.viewHelpHToolStripMenuItem.Click += new System.EventHandler(this.viewHelpHToolStripMenuItem_Click);
            // 
            // aboutAToolStripMenuItem1
            // 
            this.aboutAToolStripMenuItem1.Name = "aboutAToolStripMenuItem1";
            this.aboutAToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.aboutAToolStripMenuItem1.Text = "About(&A)";
            this.aboutAToolStripMenuItem1.Click += new System.EventHandler(this.aboutAToolStripMenuItem1_Click);
            // 
            // langToolStripComboBox
            // 
            this.langToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.langToolStripComboBox.Name = "langToolStripComboBox";
            this.langToolStripComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_HideWindow,
            this.toolStripSeparator3});
            this.toolStrip.Location = new System.Drawing.Point(0, 29);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1193, 27);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "text";
            // 
            // toolStripButton_HideWindow
            // 
            this.toolStripButton_HideWindow.AutoToolTip = false;
            this.toolStripButton_HideWindow.Image = global::LabelPlus.Properties.Resources.hide;
            this.toolStripButton_HideWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_HideWindow.Name = "toolStripButton_HideWindow";
            this.toolStripButton_HideWindow.Size = new System.Drawing.Size(74, 24);
            this.toolStripButton_HideWindow.Text = "Hide(&T)";
            this.toolStripButton_HideWindow.Click += new System.EventHandler(this.toolStripButton_HideWindow_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 56);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tableLayoutPanelPicView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer.Size = new System.Drawing.Size(1193, 744);
            this.splitContainer.SplitterDistance = 741;
            this.splitContainer.TabIndex = 2;
            // 
            // tableLayoutPanelPicView
            // 
            this.tableLayoutPanelPicView.ColumnCount = 1;
            this.tableLayoutPanelPicView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPicView.Controls.Add(this.toolStripPicView, 0, 1);
            this.tableLayoutPanelPicView.Controls.Add(this.picView, 0, 0);
            this.tableLayoutPanelPicView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelPicView.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelPicView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanelPicView.Name = "tableLayoutPanelPicView";
            this.tableLayoutPanelPicView.RowCount = 2;
            this.tableLayoutPanelPicView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPicView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelPicView.Size = new System.Drawing.Size(739, 742);
            this.tableLayoutPanelPicView.TabIndex = 6;
            // 
            // toolStripPicView
            // 
            this.toolStripPicView.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStripPicView.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripPicView.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripPicView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Right,
            this.toolStripButton_Left,
            this.toolStripComboBox_File,
            this.toolStripButton_ZoomPlus,
            this.toolStripButton_ZoomMinus,
            this.toolStripComboBox_Zoom});
            this.toolStripPicView.Location = new System.Drawing.Point(0, 717);
            this.toolStripPicView.Name = "toolStripPicView";
            this.toolStripPicView.Size = new System.Drawing.Size(739, 25);
            this.toolStripPicView.TabIndex = 8;
            // 
            // toolStripButton_Right
            // 
            this.toolStripButton_Right.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_Right.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Right.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Right.Image")));
            this.toolStripButton_Right.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Right.Name = "toolStripButton_Right";
            this.toolStripButton_Right.Size = new System.Drawing.Size(24, 22);
            this.toolStripButton_Right.Click += new System.EventHandler(this.toolStripButton_Right_Click);
            // 
            // toolStripButton_Left
            // 
            this.toolStripButton_Left.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_Left.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Left.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Left.Image")));
            this.toolStripButton_Left.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Left.Name = "toolStripButton_Left";
            this.toolStripButton_Left.Size = new System.Drawing.Size(24, 22);
            this.toolStripButton_Left.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripButton_Left.Click += new System.EventHandler(this.toolStripButton_Left_Click);
            // 
            // toolStripComboBox_File
            // 
            this.toolStripComboBox_File.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripComboBox_File.AutoSize = false;
            this.toolStripComboBox_File.DropDownWidth = 250;
            this.toolStripComboBox_File.MaxDropDownItems = 15;
            this.toolStripComboBox_File.Name = "toolStripComboBox_File";
            this.toolStripComboBox_File.Size = new System.Drawing.Size(200, 25);
            // 
            // toolStripButton_ZoomPlus
            // 
            this.toolStripButton_ZoomPlus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_ZoomPlus.Image = global::LabelPlus.Properties.Resources.zoom_plus;
            this.toolStripButton_ZoomPlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ZoomPlus.Name = "toolStripButton_ZoomPlus";
            this.toolStripButton_ZoomPlus.Size = new System.Drawing.Size(24, 22);
            // 
            // toolStripButton_ZoomMinus
            // 
            this.toolStripButton_ZoomMinus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_ZoomMinus.Image = global::LabelPlus.Properties.Resources.zoom_minus;
            this.toolStripButton_ZoomMinus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ZoomMinus.Name = "toolStripButton_ZoomMinus";
            this.toolStripButton_ZoomMinus.Size = new System.Drawing.Size(24, 22);
            // 
            // toolStripComboBox_Zoom
            // 
            this.toolStripComboBox_Zoom.AutoSize = false;
            this.toolStripComboBox_Zoom.DropDownHeight = 100;
            this.toolStripComboBox_Zoom.DropDownWidth = 30;
            this.toolStripComboBox_Zoom.IntegralHeight = false;
            this.toolStripComboBox_Zoom.Name = "toolStripComboBox_Zoom";
            this.toolStripComboBox_Zoom.Size = new System.Drawing.Size(60, 25);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanelLabels);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TextBox_GroupBox);
            this.splitContainer1.Size = new System.Drawing.Size(446, 742);
            this.splitContainer1.SplitterDistance = 434;
            this.splitContainer1.TabIndex = 3;
            // 
            // tableLayoutPanelLabels
            // 
            this.tableLayoutPanelLabels.ColumnCount = 1;
            this.tableLayoutPanelLabels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLabels.Controls.Add(this.toolStripLabels, 0, 0);
            this.tableLayoutPanelLabels.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanelLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLabels.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelLabels.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanelLabels.Name = "tableLayoutPanelLabels";
            this.tableLayoutPanelLabels.RowCount = 2;
            this.tableLayoutPanelLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLabels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelLabels.Size = new System.Drawing.Size(446, 434);
            this.tableLayoutPanelLabels.TabIndex = 3;
            // 
            // toolStripLabels
            // 
            this.toolStripLabels.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripLabels.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripLabels.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_EditBig,
            this.toolStripButton_EditSmall,
            this.toolStripButton1});
            this.toolStripLabels.Location = new System.Drawing.Point(0, 0);
            this.toolStripLabels.Name = "toolStripLabels";
            this.toolStripLabels.Size = new System.Drawing.Size(446, 25);
            this.toolStripLabels.TabIndex = 0;
            // 
            // toolStripButton_EditBig
            // 
            this.toolStripButton_EditBig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_EditBig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_EditBig.Image")));
            this.toolStripButton_EditBig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_EditBig.Name = "toolStripButton_EditBig";
            this.toolStripButton_EditBig.Size = new System.Drawing.Size(24, 22);
            this.toolStripButton_EditBig.Text = "text";
            this.toolStripButton_EditBig.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripButton_EditBig.Click += new System.EventHandler(this.toolStripButton_EditBig_Click);
            // 
            // toolStripButton_EditSmall
            // 
            this.toolStripButton_EditSmall.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_EditSmall.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_EditSmall.Image")));
            this.toolStripButton_EditSmall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_EditSmall.Name = "toolStripButton_EditSmall";
            this.toolStripButton_EditSmall.Size = new System.Drawing.Size(24, 22);
            this.toolStripButton_EditSmall.Text = "text";
            this.toolStripButton_EditSmall.Click += new System.EventHandler(this.toolStripButton_EditSmall_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::LabelPlus.Properties.Resources.T;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "更改字体";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CID,
            this.CText,
            this.CGroup});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("黑体", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(440, 403);
            this.dataGridView1.TabIndex = 7;
            // 
            // CID
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CID.DefaultCellStyle = dataGridViewCellStyle1;
            this.CID.HeaderText = "ID";
            this.CID.MinimumWidth = 10;
            this.CID.Name = "CID";
            this.CID.ReadOnly = true;
            this.CID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CID.Width = 25;
            // 
            // CText
            // 
            this.CText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CText.HeaderText = "Text";
            this.CText.Name = "CText";
            this.CText.ReadOnly = true;
            this.CText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CGroup
            // 
            this.CGroup.HeaderText = "Group";
            this.CGroup.Name = "CGroup";
            this.CGroup.ReadOnly = true;
            this.CGroup.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CGroup.Width = 43;
            // 
            // TextBox_GroupBox
            // 
            this.TextBox_GroupBox.BackColor = System.Drawing.SystemColors.Window;
            this.TextBox_GroupBox.ContextMenuStrip = this.contextMenuStripQuickText;
            this.TextBox_GroupBox.Controls.Add(this.labelCtrlEnterTip);
            this.TextBox_GroupBox.Controls.Add(this.TranslateTextBox);
            this.TextBox_GroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_GroupBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextBox_GroupBox.Location = new System.Drawing.Point(0, 0);
            this.TextBox_GroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextBox_GroupBox.Name = "TextBox_GroupBox";
            this.TextBox_GroupBox.Size = new System.Drawing.Size(446, 304);
            this.TextBox_GroupBox.TabIndex = 0;
            this.TextBox_GroupBox.TabStop = false;
            // 
            // contextMenuStripQuickText
            // 
            this.contextMenuStripQuickText.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripQuickText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.contextMenuStripQuickText.Name = "contextMenuStripQuickText";
            this.contextMenuStripQuickText.Size = new System.Drawing.Size(61, 4);
            // 
            // labelCtrlEnterTip
            // 
            this.labelCtrlEnterTip.AutoSize = true;
            this.labelCtrlEnterTip.BackColor = System.Drawing.SystemColors.Window;
            this.labelCtrlEnterTip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelCtrlEnterTip.Font = new System.Drawing.Font("宋体", 10F);
            this.labelCtrlEnterTip.Location = new System.Drawing.Point(3, 287);
            this.labelCtrlEnterTip.Name = "labelCtrlEnterTip";
            this.labelCtrlEnterTip.Size = new System.Drawing.Size(77, 14);
            this.labelCtrlEnterTip.TabIndex = 6;
            this.labelCtrlEnterTip.Text = "快捷键提示";
            // 
            // TranslateTextBox
            // 
            this.TranslateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TranslateTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TranslateTextBox.Font = new System.Drawing.Font("黑体", 12F);
            this.TranslateTextBox.Location = new System.Drawing.Point(3, 22);
            this.TranslateTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TranslateTextBox.Multiline = true;
            this.TranslateTextBox.Name = "TranslateTextBox";
            this.TranslateTextBox.Size = new System.Drawing.Size(440, 279);
            this.TranslateTextBox.TabIndex = 5;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "TEXT|*.txt";
            this.openFileDialog.Title = "Open";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "TEXT|*.txt";
            this.saveFileDialog.Title = "Save";
            // 
            // timerAutoSave
            // 
            this.timerAutoSave.Enabled = true;
            this.timerAutoSave.Interval = 30000;
            this.timerAutoSave.Tick += new System.EventHandler(this.timerAutoSave_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "LabelPlus";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // picView
            // 
            this.picView.AlwaysShowGroup = false;
            this.picView.BackColor = System.Drawing.Color.Gray;
            this.picView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picView.EnableMakeImage = false;
            this.picView.Location = new System.Drawing.Point(3, 3);
            this.picView.Name = "picView";
            this.picView.Size = new System.Drawing.Size(733, 711);
            this.picView.TabIndex = 5;
            this.picView.Zoom = 0.05F;
            this.picView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.picView_PreviewKeyDown);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 800);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainFrm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.SizeChanged += new System.EventHandler(this.MainFrm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFrm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tableLayoutPanelPicView.ResumeLayout(false);
            this.tableLayoutPanelPicView.PerformLayout();
            this.toolStripPicView.ResumeLayout(false);
            this.toolStripPicView.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanelLabels.ResumeLayout(false);
            this.tableLayoutPanelLabels.PerformLayout();
            this.toolStripLabels.ResumeLayout(false);
            this.toolStripLabels.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.TextBox_GroupBox.ResumeLayout(false);
            this.TextBox_GroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectSToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private PicView picView;
        private System.Windows.Forms.ToolStripMenuItem helpAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitEToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.GroupBox TextBox_GroupBox;
        private System.Windows.Forms.TextBox TranslateTextBox;
        private System.Windows.Forms.ToolStripMenuItem saveAsDToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox langToolStripComboBox;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.Timer timerAutoSave;
        private System.Windows.Forms.Label labelCtrlEnterTip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton_HideWindow;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLabels;
        private System.Windows.Forms.ToolStrip toolStripLabels;
        private System.Windows.Forms.ToolStripButton toolStripButton_EditBig;
        private System.Windows.Forms.ToolStripButton toolStripButton_EditSmall;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripQuickText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPicView;
        private System.Windows.Forms.ToolStrip toolStripPicView;
        private System.Windows.Forms.ToolStripButton toolStripButton_Right;
        private System.Windows.Forms.ToolStripButton toolStripButton_Left;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_File;
        private System.Windows.Forms.ToolStripButton toolStripButton_ZoomPlus;
        private System.Windows.Forms.ToolStripButton toolStripButton_ZoomMinus;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_Zoom;
        private System.Windows.Forms.ToolStripMenuItem viewHelpHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutAToolStripMenuItem1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CText;
        private System.Windows.Forms.DataGridViewTextBoxColumn CGroup;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadRemoteProjectMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox loginStatusTextBox;
        private System.Windows.Forms.ToolStripMenuItem uploadLocalProjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutMenuItem;
    }
}

