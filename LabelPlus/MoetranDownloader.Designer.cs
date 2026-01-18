namespace LabelPlus
{
    partial class MoetranDownloader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("本子1");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("本子2");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("杂志", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("白杨", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoetranDownloader));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._treeView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.downloadCheckedTranslationButton = new System.Windows.Forms.Button();
            this.downloadTranslationButton = new System.Windows.Forms.Button();
            this.downloadProjectButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.targetLngLabel = new System.Windows.Forms.Label();
            this.targetLanguageSelect = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._treeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(1077, 645);
            this.splitContainer1.SplitterDistance = 342;
            this.splitContainer1.TabIndex = 0;
            // 
            // _treeView
            // 
            this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._treeView.Location = new System.Drawing.Point(0, 0);
            this._treeView.Name = "_treeView";
            treeNode5.Name = "节点2";
            treeNode5.Text = "本子1";
            treeNode6.Name = "节点3";
            treeNode6.Text = "本子2";
            treeNode7.Name = "节点1";
            treeNode7.Text = "杂志";
            treeNode8.Name = "节点0";
            treeNode8.Text = "白杨";
            this._treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8});
            this._treeView.Size = new System.Drawing.Size(342, 645);
            this._treeView.TabIndex = 0;
            this._treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterExpand);
            this._treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterNodeClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._listView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(731, 645);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // _listView
            // 
            this._listView.BackColor = System.Drawing.SystemColors.Window;
            this._listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this._listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listView.FullRowSelect = true;
            this._listView.GridLines = true;
            this._listView.HideSelection = false;
            this._listView.LargeImageList = this._imageList;
            this._listView.Location = new System.Drawing.Point(3, 43);
            this._listView.Name = "_listView";
            this._listView.Size = new System.Drawing.Size(725, 599);
            this._listView.SmallImageList = this._imageList;
            this._listView.TabIndex = 0;
            this._listView.TileSize = new System.Drawing.Size(310, 155);
            this._listView.UseCompatibleStateImageBehavior = false;
            this._listView.View = System.Windows.Forms.View.Tile;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "文件名";
            this.columnHeader1.Width = 452;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "标签";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "翻译";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "校对";
            // 
            // _imageList
            // 
            this._imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this._imageList.ImageSize = new System.Drawing.Size(180, 140);
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(725, 34);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.downloadCheckedTranslationButton);
            this.flowLayoutPanel1.Controls.Add(this.downloadTranslationButton);
            this.flowLayoutPanel1.Controls.Add(this.downloadProjectButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(254, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(468, 28);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // downloadCheckedTranslationButton
            // 
            this.downloadCheckedTranslationButton.AutoSize = true;
            this.downloadCheckedTranslationButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.downloadCheckedTranslationButton.Location = new System.Drawing.Point(282, 3);
            this.downloadCheckedTranslationButton.Name = "downloadCheckedTranslationButton";
            this.downloadCheckedTranslationButton.Size = new System.Drawing.Size(183, 22);
            this.downloadCheckedTranslationButton.TabIndex = 3;
            this.downloadCheckedTranslationButton.Text = "Download checked translation";
            this.downloadCheckedTranslationButton.UseVisualStyleBackColor = true;
            this.downloadCheckedTranslationButton.Click += new System.EventHandler(this.downloadButton2_Click);
            // 
            // downloadTranslationButton
            // 
            this.downloadTranslationButton.AutoSize = true;
            this.downloadTranslationButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.downloadTranslationButton.Location = new System.Drawing.Point(141, 3);
            this.downloadTranslationButton.Name = "downloadTranslationButton";
            this.downloadTranslationButton.Size = new System.Drawing.Size(135, 22);
            this.downloadTranslationButton.TabIndex = 2;
            this.downloadTranslationButton.Text = "Download translation";
            this.downloadTranslationButton.UseVisualStyleBackColor = true;
            this.downloadTranslationButton.Click += new System.EventHandler(this.downloadButton1_Click);
            // 
            // downloadProjectButton
            // 
            this.downloadProjectButton.AutoSize = true;
            this.downloadProjectButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.downloadProjectButton.Location = new System.Drawing.Point(24, 3);
            this.downloadProjectButton.Name = "downloadProjectButton";
            this.downloadProjectButton.Size = new System.Drawing.Size(111, 22);
            this.downloadProjectButton.TabIndex = 4;
            this.downloadProjectButton.Text = "Download Project";
            this.downloadProjectButton.UseVisualStyleBackColor = true;
            this.downloadProjectButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.targetLngLabel);
            this.flowLayoutPanel2.Controls.Add(this.targetLanguageSelect);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(245, 28);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // targetLngLabel
            // 
            this.targetLngLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.targetLngLabel.AutoSize = true;
            this.targetLngLabel.Location = new System.Drawing.Point(3, 7);
            this.targetLngLabel.Name = "targetLngLabel";
            this.targetLngLabel.Size = new System.Drawing.Size(41, 12);
            this.targetLngLabel.TabIndex = 0;
            this.targetLngLabel.Text = "Target";
            // 
            // targetLanguageSelect
            // 
            this.targetLanguageSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetLanguageSelect.FormattingEnabled = true;
            this.targetLanguageSelect.Location = new System.Drawing.Point(50, 3);
            this.targetLanguageSelect.Name = "targetLanguageSelect";
            this.targetLanguageSelect.Size = new System.Drawing.Size(192, 20);
            this.targetLanguageSelect.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 645);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1077, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _statusLabel
            // 
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(131, 17);
            this._statusLabel.Text = "toolStripStatusLabel1";
            // 
            // MoetranDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 667);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MoetranDownloader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Moetran";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView _treeView;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _statusLabel;
        private System.Windows.Forms.ListView _listView;
        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox targetLanguageSelect;
        private System.Windows.Forms.Label targetLngLabel;
        private System.Windows.Forms.Button downloadCheckedTranslationButton;
        private System.Windows.Forms.Button downloadTranslationButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button downloadProjectButton;
    }
}