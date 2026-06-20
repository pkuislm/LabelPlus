using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LabelPlus
{
    public partial class QuickTextFrm : Form
    {
        private QuickTextItem[] items;
        private QuickTextItem[] itemsSnapshot;
        private int[] itemSnapshotIndices;
        private bool capturingQuickTextKey;
        private int capturingQuickTextKeyRowIndex = -1;

        public QuickTextFrm()
        {
            InitializeComponent();
            QuickTextKeyTextBox.KeyDown += QuickTextKeyTextBox_KeyDown;
            QuickTextGrid.KeyDown += QuickTextGrid_KeyDown;
            QuickTextGrid.Leave += QuickTextGrid_Leave;
            InitGrid();
        }

        void InitGrid()
        {
            items = CloneItems(QuickTextManager.Items);
            itemsSnapshot = CloneItems(items);
            itemSnapshotIndices = Enumerable.Range(0, items.Length).ToArray();
            SyncItems();
        }

        private void QuickTextAddButton_Click(object sender, EventArgs e)
        {
            string text = QuickTextValueTextBox.Text.Trim();
            Keys key = QuickTextManager.KeyFromText(QuickTextKeyTextBox.Text);

            QuickTextManager.QuickTextStatus status = QuickTextManager.Validate(text, key);
            SetStatus(QuickTextManager.StatusToText(status));
            if (status != QuickTextManager.QuickTextStatus.OK)
                return;

            var itemList = items.ToList();
            itemList.Add(new QuickTextItem(text, key));
            items = itemList.ToArray();
            var snapshotIndexList = itemSnapshotIndices.ToList();
            snapshotIndexList.Add(-1);
            itemSnapshotIndices = snapshotIndexList.ToArray();

            SyncItems();
            QuickTextValueTextBox.Clear();
            QuickTextKeyTextBox.Clear();
            SetStatus(null);
        }

        private void QuickTextGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (QuickTextGrid.Columns[e.ColumnIndex].Name == "Delete")
            {
                ClearQuickTextKeyCapture();
                DeleteRow(e.RowIndex);
                return;
            }

            if (QuickTextGrid.Columns[e.ColumnIndex].Name == "Edit")
            {
                BeginQuickTextKeyCapture(e.RowIndex);
                return;
            }

            if (QuickTextGrid.Columns[e.ColumnIndex].Name == "Restore")
            {
                ClearQuickTextKeyCapture();
                RestoreRow(e.RowIndex);
                return;
            }
        }

        private void QuickTextOkButton_Click(object sender, EventArgs e)
        {
            QuickTextManager.SetItems(CloneItems(items));
            GlobalVar.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void DeleteRow(int rowIndex)
        {
            var itemList = items.ToList();
            itemList.RemoveAt(rowIndex);
            items = itemList.ToArray();

            var snapshotIndexList = itemSnapshotIndices.ToList();
            snapshotIndexList.RemoveAt(rowIndex);
            itemSnapshotIndices = snapshotIndexList.ToArray();

            SyncItems();
            SetStatus(null);
        }

        private void RestoreRow(int rowIndex)
        {
            int snapshotIndex = itemSnapshotIndices[rowIndex];
            if (snapshotIndex < 0 || snapshotIndex >= itemsSnapshot.Length)
            {
                SetStatus(QuickTextManager.StatusToText(QuickTextManager.QuickTextStatus.NO_PREVIOUS_VALUE));
                return;
            }

            items[rowIndex] = new QuickTextItem(itemsSnapshot[snapshotIndex].Text, itemsSnapshot[snapshotIndex].Key);
            SyncItems();
            if (rowIndex < QuickTextGrid.Rows.Count)
                QuickTextGrid.Rows[rowIndex].Selected = true;
            SetStatus(null);
        }

        private void SyncItems()
        {
            QuickTextGrid.Rows.Clear();
            foreach (var item in items)
            {
                QuickTextGrid.Rows.Add(
                    item.Text,
                    QuickTextManager.KeyToText(item.Key)
                );
            }
            if (QuickTextGrid.Rows.Count > 0)
                QuickTextGrid.Rows[0].Selected = true;

            QuickTextManager.SetItems(CloneItems(items));
        }

        private void QuickTextKeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            QuickTextManager.QuickTextStatus status = StatusFromKeyEvent(e, out Keys key);
            SetStatus(QuickTextManager.StatusToText(status));
            if (status != QuickTextManager.QuickTextStatus.OK)
                return;

            QuickTextKeyTextBox.Text = QuickTextManager.KeyToText(key);
            QuickTextKeyTextBox.SelectionStart = QuickTextKeyTextBox.Text.Length;
            SetStatus(null);
        }

        private void BeginQuickTextKeyCapture(int rowIndex)
        {
            ClearQuickTextKeyCapture();
            capturingQuickTextKey = true;
            capturingQuickTextKeyRowIndex = rowIndex;
            QuickTextGrid.CurrentCell = QuickTextGrid.Rows[rowIndex].Cells["Key"];
            QuickTextGrid.Rows[rowIndex].Cells["Key"].Style.BackColor = Color.LightYellow;
            QuickTextGrid.Focus();
            SetStatus(null);
        }

        private void ClearQuickTextKeyCapture()
        {
            if (capturingQuickTextKeyRowIndex >= 0 && capturingQuickTextKeyRowIndex < QuickTextGrid.Rows.Count)
                QuickTextGrid.Rows[capturingQuickTextKeyRowIndex].Cells["Key"].Style.BackColor = Color.Empty;

            capturingQuickTextKey = false;
            capturingQuickTextKeyRowIndex = -1;
            SetStatus(null);
        }

        private void QuickTextGrid_Leave(object sender, EventArgs e)
        {
            ClearQuickTextKeyCapture();
        }

        private void QuickTextGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (!capturingQuickTextKey)
                return;

            e.SuppressKeyPress = true;
            e.Handled = true;

            if (e.KeyCode == Keys.Oemcomma)
            {
                ClearQuickTextKeyCapture();
                return;
            }

            QuickTextManager.QuickTextStatus status = StatusFromKeyEvent(e, out Keys key);
            if (status == QuickTextManager.QuickTextStatus.OK)
            {
                int rowIndex = capturingQuickTextKeyRowIndex;
                string text = Convert.ToString(QuickTextGrid.Rows[rowIndex].Cells["Text"].Value).Trim();
                status = QuickTextManager.Validate(text, key, true);
                if (status == QuickTextManager.QuickTextStatus.OK)
                {
                    items[rowIndex] = new QuickTextItem(text, key);
                    SyncItems();
                    ClearQuickTextKeyCapture();
                }
            }

            SetStatus(QuickTextManager.StatusToText(status));
        }

        private QuickTextManager.QuickTextStatus StatusFromKeyEvent(KeyEventArgs e, out Keys key)
        {
            key = Keys.None;
            if (e.Control || e.Alt || e.Shift)
                return QuickTextManager.QuickTextStatus.INVALID_KEY;

            if (!QuickTextManager.IsAllowedKey(e.KeyCode))
                return QuickTextManager.QuickTextStatus.INVALID_KEY_RANGE;

            key = e.KeyCode;
            return QuickTextManager.QuickTextStatus.OK;
        }

        private QuickTextItem[] CloneItems(QuickTextItem[] source)
        {
            return source.Select(item => new QuickTextItem(item.Text, item.Key)).ToArray();
        }

        private void SetStatus(string message)
        {
            if (capturingQuickTextKey)
                QuickTextStatusLabel.Text = 
                    QuickTextManager.StatusToText(QuickTextManager.QuickTextStatus.CAPTURING_KEY_MODE)
                    + "\n" + message ?? string.Empty;
            else
                QuickTextStatusLabel.Text = message ?? string.Empty;
        }
    }
}
