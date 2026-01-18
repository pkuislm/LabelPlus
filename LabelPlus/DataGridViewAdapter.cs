using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LabelPlus
{
    public class DataGridViewAdapter
    {
        #region Fields

        DataGridView dgv;
        bool selectionChangedEnable = true;
        GroupDefineItemCollection group;
        ContextMenuStrip myMenuStrip;

        #endregion

        #region Events

        public EventHandler SelectedIndexChanged;

        protected void OnSelectedIndexChanged()
        {
            if (selectionChangedEnable && SelectedIndexChanged != null)
                SelectedIndexChanged(this, EventArgs.Empty);
        }

        public class UserActionEventArgs : EventArgs
        {
            public enum ActionType
            {
                setGroup,
                del,
            }

            public int[] Index { get; }
            public int Value { get; }
            public ActionType Action { get; }

            public UserActionEventArgs(int[] index, ActionType type, int value = -1)
            {
                Index = index;
                Value = value;
                Action = type;
            }
        }

        public delegate void UserActionEventHandler(object sender, UserActionEventArgs e);
        public UserActionEventHandler UserSetCategory;

        #endregion

        #region Properties

        public int Count => dgv.Rows.Count;

        public int SelectedIndex
        {
            get
            {
                return dgv.CurrentRow != null ? dgv.CurrentRow.Index : -1;
            }
            set
            {
                if (value >= 0 && value < dgv.Rows.Count)
                {
                    selectionChangedEnable = false;
                    dgv.ClearSelection();
                    dgv.Rows[value].Selected = true;
                    dgv.CurrentCell = dgv.Rows[value].Cells[1];
                    selectionChangedEnable = true;
                    SelectedIndexChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public int SelectedIndexCount => dgv.SelectedRows.Count;

        public DataGridView DataGridView => dgv;

        #endregion

        #region Methods

        string getCategoryName(int index)
        {
            return group.GetFullViewName(index);
        }

        internal void onDelItems()
        {
            var list = new List<int>();
            foreach (DataGridViewRow r in dgv.SelectedRows)
                list.Add(r.Index);

            if (list.Count > 0)
                UserSetCategory?.Invoke(this,
                    new UserActionEventArgs(list.ToArray(), UserActionEventArgs.ActionType.del));
        }

        internal void onSetCategory(int category)
        {
            var list = new List<int>();
            foreach (DataGridViewRow r in dgv.SelectedRows)
                list.Add(r.Index);

            if (list.Count > 0)
                UserSetCategory?.Invoke(this,
                    new UserActionEventArgs(list.ToArray(),
                        UserActionEventArgs.ActionType.setGroup, category));
        }

        public bool ReloadItems(List<LabelItem> items)
        {
            selectionChangedEnable = false;

            try
            {
                int selected = SelectedIndex;

                dgv.Rows.Clear();

                if (items == null)
                    return true;

                int number = 1;
                foreach (LabelItem n in items)
                {
                    int rowIndex = dgv.Rows.Add(
                        number.ToString(),
                        n.Text,
                        getCategoryName(n.Category)
                    );

                    dgv.Rows[rowIndex].Cells[2].Style.ForeColor =
                        group.GetColor(n.Category);

                    number++;
                }

                dgv.ClearSelection();
                dgv.CurrentCell = null;

                if (selected >= 0 && selected < dgv.Rows.Count)
                {
                    dgv.Rows[selected].Selected = true;
                    dgv.CurrentCell = dgv.Rows[selected].Cells[1];
                }
                
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                selectionChangedEnable = true;
            }
        }

        #endregion

        #region Event Handlers

        void dgvSelectionChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged();
        }

        void dgvKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
            {
                onSetCategory(e.KeyCode - Keys.D1 + 1);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                onDelItems();
            }
            e.SuppressKeyPress = true;
        }

        void dgvMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rebuildMenuStrip();
                myMenuStrip.Show(Control.MousePosition);
            }
        }

        void rebuildMenuStrip()
        {
            myMenuStrip.Items.Clear();
            foreach (string i in group.GetUserGroupNameArray())
                myMenuStrip.Items.Add(i);

            myMenuStrip.Items.Add(StringResources.GetValue("listview_menustrip_del"));
        }

        void myMenuStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            myMenuStrip.Close();
            int index = myMenuStrip.Items.IndexOf(e.ClickedItem);

            if (index == myMenuStrip.Items.Count - 1)
                onDelItems();
            else
                onSetCategory(index + 1);
        }

        public void SetFont(Font f)
        {
             dgv.Font = f;
        }
        #endregion

        #region Constructor

        public DataGridViewAdapter(DataGridView grid, GroupDefineItemCollection groupDefine)
        {
            dgv = grid;
            group = groupDefine;

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.ReadOnly = true;

            dgv.MultiSelect = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersVisible = true;

            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            //dgv.Columns.Clear();
            //dgv.Columns.Add(new DataGridViewTextBoxColumn { Width = 40 });
            //dgv.Columns.Add(new DataGridViewTextBoxColumn { AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            //dgv.Columns.Add(new DataGridViewTextBoxColumn { Width = 100 });

            dgv.SelectionChanged += dgvSelectionChanged;
            dgv.KeyDown += dgvKeyDown;
            dgv.MouseClick += dgvMouseClick;

            myMenuStrip = new ContextMenuStrip();
            myMenuStrip.ItemClicked += myMenuStripItemClicked;
        }

        #endregion
    }
}
