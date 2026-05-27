namespace LabelPlus
{
    partial class ShortcutSettingsForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.editPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.shortcutTextBox = new System.Windows.Forms.TextBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.buttons = new System.Windows.Forms.FlowLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.editPanel.SuspendLayout();
            this.buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.MultiSelect = false;
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.Columns.Add("Group", "分类");
            this.grid.Columns.Add("Name", "功能");
            this.grid.Columns.Add("Shortcut", "快捷键");
            this.grid.Columns.Add("Id", "Id");
            this.grid.Columns["Id"].Visible = false;
            this.grid.SelectionChanged += grid_SelectionChanged;
            // 
            // shortcutTextBox
            // 
            this.shortcutTextBox.ReadOnly = true;
            this.shortcutTextBox.Width = 180;
            this.shortcutTextBox.KeyDown += shortcutTextBox_KeyDown;
            // 
            // applyButton
            // 
            this.applyButton.Name = "applyButton";
            this.applyButton.Text = "应用";
            this.applyButton.Width = 75;
            this.applyButton.Click += applyButton_Click;
            // 
            // clearButton
            // 
            this.clearButton.Text = "清除";
            this.clearButton.Width = 75;
            this.clearButton.Click += clearButton_Click;
            // 
            // resetButton
            // 
            this.resetButton.Text = "恢复默认";
            this.resetButton.Width = 90;
            this.resetButton.Click += resetButton_Click;
            // 
            // editPanel
            // 
            this.editPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.editPanel.WrapContents = false;
            this.editPanel.Controls.Add(new System.Windows.Forms.Label
            {
                Text = "新快捷键:",
                AutoSize = true,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding = new System.Windows.Forms.Padding(0, 8, 0, 0)
            });
            this.editPanel.Controls.Add(shortcutTextBox);
            this.editPanel.Controls.Add(applyButton);
            this.editPanel.Controls.Add(clearButton);
            this.editPanel.Controls.Add(resetButton);
            // 
            // statusLabel
            // 
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // okButton
            // 
            this.okButton.Text = "确定";
            this.okButton.Width = 80;
            this.okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            this.cancelButton.Text = "取消";
            this.cancelButton.Width = 80;
            this.cancelButton.Click += delegate { DialogResult = System.Windows.Forms.DialogResult.Cancel; Close(); };
            // 
            // buttons
            // 
            this.buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttons.Controls.Add(okButton);
            this.buttons.Controls.Add(cancelButton);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42));
            this.tableLayoutPanel1.Controls.Add(this.grid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.editPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.statusLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttons, 0, 3);
            // 
            // ShortcutSettingsForm
            // 
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowInTaskbar = false;
            this.Size = new System.Drawing.Size(620, 420);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ShortcutSettingsForm";
            this.Text = "快捷键设置";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.editPanel.ResumeLayout(false);
            this.editPanel.PerformLayout();
            this.buttons.ResumeLayout(false);
            this.buttons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.FormClosing += ShortcutSettingsForm_FormClosing;
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel editPanel;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.TextBox shortcutTextBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.FlowLayoutPanel buttons;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}