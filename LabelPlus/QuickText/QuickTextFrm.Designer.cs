namespace LabelPlus
{
    partial class QuickTextFrm
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
            this.QuickTextGrid = new System.Windows.Forms.DataGridView();
            this.QuickTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuickTextKeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuickTextEditColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.QuickTextRestoreColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.QuickTextDeleteColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.QuickTextValueLabel = new System.Windows.Forms.Label();
            this.QuickTextValueTextBox = new System.Windows.Forms.TextBox();
            this.QuickTextKeyLabel = new System.Windows.Forms.Label();
            this.QuickTextKeyTextBox = new System.Windows.Forms.TextBox();
            this.QuickTextAddButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.QuickTextStatusLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.QuickTextOkButton = new System.Windows.Forms.Button();
            this.QuickTextCancelButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuickTextGrid)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.QuickTextGrid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(720, 420);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // QuickTextGrid
            //
            this.QuickTextGrid.AllowUserToAddRows = false;
            this.QuickTextGrid.AllowUserToDeleteRows = false;
            this.QuickTextGrid.AllowUserToResizeRows = false;
            this.QuickTextGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.QuickTextGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.QuickTextGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.QuickTextGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.QuickTextGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QuickTextColumn,
            this.QuickTextKeyColumn,
            this.QuickTextEditColumn,
            this.QuickTextRestoreColumn,
            this.QuickTextDeleteColumn});
            this.QuickTextGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickTextGrid.Location = new System.Drawing.Point(13, 13);
            this.QuickTextGrid.MultiSelect = false;
            this.QuickTextGrid.Name = "QuickTextGrid";
            this.QuickTextGrid.RowHeadersVisible = false;
            this.QuickTextGrid.RowTemplate.Height = 23;
            this.QuickTextGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.QuickTextGrid.Size = new System.Drawing.Size(694, 316);
            this.QuickTextGrid.TabIndex = 0;
            this.QuickTextGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.QuickTextGrid_CellContentClick);
            //
            // QuickTextColumn
            //
            this.QuickTextColumn.FillWeight = 55F;
            this.QuickTextColumn.HeaderText = "快捷短语";
            this.QuickTextColumn.Name = "Text";
            this.QuickTextColumn.ReadOnly = true;
            //
            // QuickTextKeyColumn
            //
            this.QuickTextKeyColumn.FillWeight = 25F;
            this.QuickTextKeyColumn.HeaderText = "快捷键";
            this.QuickTextKeyColumn.Name = "Key";
            this.QuickTextKeyColumn.ReadOnly = true;
            //
            // QuickTextEditColumn
            //
            this.QuickTextEditColumn.FillWeight = 10F;
            this.QuickTextEditColumn.HeaderText = "修改";
            this.QuickTextEditColumn.Name = "Edit";
            this.QuickTextEditColumn.Text = "修改";
            this.QuickTextEditColumn.UseColumnTextForButtonValue = true;
            //
            // QuickTextRestoreColumn
            //
            this.QuickTextRestoreColumn.FillWeight = 15F;
            this.QuickTextRestoreColumn.HeaderText = "恢复原先值";
            this.QuickTextRestoreColumn.Name = "Restore";
            this.QuickTextRestoreColumn.Text = "恢复";
            this.QuickTextRestoreColumn.UseColumnTextForButtonValue = true;
            //
            // QuickTextDeleteColumn
            //
            this.QuickTextDeleteColumn.FillWeight = 10F;
            this.QuickTextDeleteColumn.HeaderText = "删除";
            this.QuickTextDeleteColumn.Name = "Delete";
            this.QuickTextDeleteColumn.Text = "删除";
            this.QuickTextDeleteColumn.UseColumnTextForButtonValue = true;
            //
            // tableLayoutPanel2
            //
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel2.Controls.Add(this.QuickTextValueLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.QuickTextValueTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.QuickTextKeyLabel, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.QuickTextKeyTextBox, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.QuickTextAddButton, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(13, 335);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(694, 36);
            this.tableLayoutPanel2.TabIndex = 1;
            //
            // QuickTextValueLabel
            //
            this.QuickTextValueLabel.AutoSize = true;
            this.QuickTextValueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickTextValueLabel.Location = new System.Drawing.Point(3, 4);
            this.QuickTextValueLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.QuickTextValueLabel.Name = "QuickTextValueLabel";
            this.QuickTextValueLabel.Size = new System.Drawing.Size(53, 32);
            this.QuickTextValueLabel.TabIndex = 0;
            this.QuickTextValueLabel.Text = "快捷短语";
            this.QuickTextValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // QuickTextValueTextBox
            //
            this.QuickTextValueTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickTextValueTextBox.Location = new System.Drawing.Point(62, 8);
            this.QuickTextValueTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 10, 3);
            this.QuickTextValueTextBox.Name = "QuickTextValueTextBox";
            this.QuickTextValueTextBox.Size = new System.Drawing.Size(339, 21);
            this.QuickTextValueTextBox.TabIndex = 1;
            //
            // QuickTextKeyLabel
            //
            this.QuickTextKeyLabel.AutoSize = true;
            this.QuickTextKeyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickTextKeyLabel.Location = new System.Drawing.Point(414, 4);
            this.QuickTextKeyLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.QuickTextKeyLabel.Name = "QuickTextKeyLabel";
            this.QuickTextKeyLabel.Size = new System.Drawing.Size(41, 32);
            this.QuickTextKeyLabel.TabIndex = 2;
            this.QuickTextKeyLabel.Text = "快捷键";
            this.QuickTextKeyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // QuickTextKeyTextBox
            //
            this.QuickTextKeyTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickTextKeyTextBox.Location = new System.Drawing.Point(461, 8);
            this.QuickTextKeyTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 10, 3);
            this.QuickTextKeyTextBox.Name = "QuickTextKeyTextBox";
            this.QuickTextKeyTextBox.Size = new System.Drawing.Size(117, 21);
            this.QuickTextKeyTextBox.TabIndex = 3;
            //
            // QuickTextAddButton
            //
            this.QuickTextAddButton.AutoSize = false;
            this.QuickTextAddButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickTextAddButton.Location = new System.Drawing.Point(591, 7);
            this.QuickTextAddButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.QuickTextAddButton.Name = "QuickTextAddButton";
            this.QuickTextAddButton.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.QuickTextAddButton.Size = new System.Drawing.Size(103, 26);
            this.QuickTextAddButton.TabIndex = 4;
            this.QuickTextAddButton.Text = "新增";
            this.QuickTextAddButton.UseVisualStyleBackColor = true;
            this.QuickTextAddButton.Click += new System.EventHandler(this.QuickTextAddButton_Click);
            //
            // tableLayoutPanel3
            //
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tableLayoutPanel3.Controls.Add(this.QuickTextStatusLabel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(13, 377);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(694, 30);
            this.tableLayoutPanel3.TabIndex = 2;
            //
            // QuickTextStatusLabel
            //
            this.QuickTextStatusLabel.AutoSize = false;
            this.QuickTextStatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickTextStatusLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.QuickTextStatusLabel.Location = new System.Drawing.Point(3, 0);
            this.QuickTextStatusLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.QuickTextStatusLabel.Name = "QuickTextStatusLabel";
            this.QuickTextStatusLabel.Size = new System.Drawing.Size(478, 30);
            this.QuickTextStatusLabel.TabIndex = 0;
            this.QuickTextStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // flowLayoutPanel1
            //
            this.flowLayoutPanel1.Controls.Add(this.QuickTextOkButton);
            this.flowLayoutPanel1.Controls.Add(this.QuickTextCancelButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(484, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(210, 30);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            //
            // QuickTextOkButton
            //
            this.QuickTextOkButton.AutoSize = false;
            this.QuickTextOkButton.Location = new System.Drawing.Point(103, 5);
            this.QuickTextOkButton.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.QuickTextOkButton.Name = "QuickTextOkButton";
            this.QuickTextOkButton.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.QuickTextOkButton.Size = new System.Drawing.Size(104, 28);
            this.QuickTextOkButton.TabIndex = 0;
            this.QuickTextOkButton.Text = "保存并返回";
            this.QuickTextOkButton.UseVisualStyleBackColor = true;
            this.QuickTextOkButton.Click += new System.EventHandler(this.QuickTextOkButton_Click);
            //
            // QuickTextCancelButton
            //
            this.QuickTextCancelButton.AutoSize = false;
            this.QuickTextCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.QuickTextCancelButton.Location = new System.Drawing.Point(17, 5);
            this.QuickTextCancelButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.QuickTextCancelButton.Name = "QuickTextCancelButton";
            this.QuickTextCancelButton.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.QuickTextCancelButton.Size = new System.Drawing.Size(80, 28);
            this.QuickTextCancelButton.TabIndex = 1;
            this.QuickTextCancelButton.Text = "取消";
            this.QuickTextCancelButton.UseVisualStyleBackColor = true;
            //
            // QuickTextFrm
            //
            this.AcceptButton = this.QuickTextOkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.QuickTextCancelButton;
            this.ClientSize = new System.Drawing.Size(720, 420);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(650, 340);
            this.Name = "QuickTextFrm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置快捷短语";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.QuickTextGrid)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView QuickTextGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuickTextColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuickTextKeyColumn;
        private System.Windows.Forms.DataGridViewButtonColumn QuickTextEditColumn;
        private System.Windows.Forms.DataGridViewButtonColumn QuickTextRestoreColumn;
        private System.Windows.Forms.DataGridViewButtonColumn QuickTextDeleteColumn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label QuickTextValueLabel;
        private System.Windows.Forms.TextBox QuickTextValueTextBox;
        private System.Windows.Forms.Label QuickTextKeyLabel;
        private System.Windows.Forms.TextBox QuickTextKeyTextBox;
        private System.Windows.Forms.Button QuickTextAddButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label QuickTextStatusLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button QuickTextOkButton;
        private System.Windows.Forms.Button QuickTextCancelButton;
    }
}
