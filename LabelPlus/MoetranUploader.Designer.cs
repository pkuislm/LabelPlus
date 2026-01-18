namespace LabelPlus
{
    partial class MoetranUploader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoetranUploader));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.projectNameLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.projectDescLabel = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.uploadButton = new System.Windows.Forms.Button();
            this.projectSetSelector = new System.Windows.Forms.GroupBox();
            this._treeView = new System.Windows.Forms.TreeView();
            this.uploadWarningLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.projectSetSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.uploadButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.projectSetSelector, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.uploadWarningLabel, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(518, 717);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(714, 721);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.projectNameLabel);
            this.flowLayoutPanel1.Controls.Add(this.textBox1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 658);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(351, 27);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // projectNameLabel
            // 
            this.projectNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.projectNameLabel.AutoSize = true;
            this.projectNameLabel.Location = new System.Drawing.Point(3, 7);
            this.projectNameLabel.Name = "projectNameLabel";
            this.projectNameLabel.Size = new System.Drawing.Size(71, 12);
            this.projectNameLabel.TabIndex = 0;
            this.projectNameLabel.Text = "ProjectName";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(80, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(267, 21);
            this.textBox1.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.projectDescLabel);
            this.flowLayoutPanel2.Controls.Add(this.textBox2);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 691);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(351, 27);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // projectDescLabel
            // 
            this.projectDescLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.projectDescLabel.AutoSize = true;
            this.projectDescLabel.Location = new System.Drawing.Point(3, 7);
            this.projectDescLabel.Name = "projectDescLabel";
            this.projectDescLabel.Size = new System.Drawing.Size(77, 12);
            this.projectDescLabel.TabIndex = 0;
            this.projectDescLabel.Text = "ProjectIntro";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(86, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(261, 21);
            this.textBox2.TabIndex = 1;
            // 
            // uploadButton
            // 
            this.uploadButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadButton.Location = new System.Drawing.Point(360, 691);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(351, 27);
            this.uploadButton.TabIndex = 3;
            this.uploadButton.Text = "Upload";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // projectSetSelector
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.projectSetSelector, 2);
            this.projectSetSelector.Controls.Add(this._treeView);
            this.projectSetSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectSetSelector.Location = new System.Drawing.Point(3, 3);
            this.projectSetSelector.Name = "projectSetSelector";
            this.projectSetSelector.Size = new System.Drawing.Size(708, 649);
            this.projectSetSelector.TabIndex = 4;
            this.projectSetSelector.TabStop = false;
            this.projectSetSelector.Text = "Select ProjectSet:";
            // 
            // _treeView
            // 
            this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._treeView.Location = new System.Drawing.Point(3, 17);
            this._treeView.Name = "_treeView";
            this._treeView.Size = new System.Drawing.Size(702, 629);
            this._treeView.TabIndex = 0;
            this._treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterExpand);
            // 
            // uploadWarningLabel
            // 
            this.uploadWarningLabel.AutoSize = true;
            this.uploadWarningLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadWarningLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uploadWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.uploadWarningLabel.Location = new System.Drawing.Point(360, 655);
            this.uploadWarningLabel.Name = "uploadWarningLabel";
            this.uploadWarningLabel.Size = new System.Drawing.Size(351, 33);
            this.uploadWarningLabel.TabIndex = 5;
            this.uploadWarningLabel.Text = "Warning: This operation will delete all remote labels!";
            // 
            // MoetranUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 721);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(730, 760);
            this.Name = "MoetranUploader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MoetranUploader";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.projectSetSelector.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label projectNameLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label projectDescLabel;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.GroupBox projectSetSelector;
        private System.Windows.Forms.TreeView _treeView;
        private System.Windows.Forms.Label uploadWarningLabel;
    }
}