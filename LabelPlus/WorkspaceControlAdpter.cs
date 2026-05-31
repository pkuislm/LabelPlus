/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

#region Using Directives

using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

#endregion

namespace LabelPlus
{
    public class WorkspaceControlAdpter
    {

        #region Fields

        ToolStripComboBox combo;
        TextBox textbox;
        PicView picview;
        GroupBox textboxgroupbox;
        ContextMenuStrip menuquicktext;
        DataGridViewAdapter listviewapt;
        ToolStrip toolstrip;
        APIManager apiManager;
        Workspace wsp;
        int groupIndex = 0;
        Control quickTextTarget;
        IMessageFilter quickTextShortcutFilter;

        enum WorkMode
        {
            Browse,
            Label,
            Input,
            Check,
        }

        WorkMode workMode;
        int itemIndex = -1;
        string fileName = "";
        bool suppressSetVisualWhenIndexChanged = true;

        Point picViewMousePosition;

        #endregion

        #region Properties
        public string FileName { get { return fileName; } }
        public int ItemIndex { get { return itemIndex; } }
        //public int NewLabelCcategory { 
        //    get { return newLabelCcategory; }
        //    set { newLabelCcategory = value; } 
        //}
        #endregion

        #region Methods
        public void page_left()
        {
            try
            {
                UndoRedoManager.labelCommandPool.Clear();
                if (combo.SelectedIndex != 0)
                    combo.SelectedIndex--;
                if (wsp.setVisualWhenIndexChanged)
                {
                    listviewapt.SelectedIndex = 0;
                    picview.SetLabelVisual(listviewapt.SelectedIndex);
                }
                
            }
            catch { }
        }
        public void page_right()
        {
            try
            {
                UndoRedoManager.labelCommandPool.Clear();
                if (combo.SelectedIndex !=
                    combo.Items.Count - 1)
                    combo.SelectedIndex++;
                if (wsp.setVisualWhenIndexChanged)
                {
                    listviewapt.SelectedIndex = 0;
                    picview.SetLabelVisual(listviewapt.SelectedIndex);
                }
            }
            catch { }
        }

        public void page_to(int index)
        {
            try
            {
                UndoRedoManager.labelCommandPool.Clear();
                if (index >= 0 && index < combo.Items.Count)
                    combo.SelectedIndex = index;
                if (wsp.setVisualWhenIndexChanged)
                {
                    listviewapt.SelectedIndex = 0;
                    picview.SetLabelVisual(listviewapt.SelectedIndex);
                }
            }
            catch { }
        }

        public bool SelectLabel(string targetFileName, int targetItemIndex, int textSelectionStart = -1, int textSelectionLength = 0)
        {
            try
            {
                int fileIndex = combo.FindStringExact(targetFileName);
                if (fileIndex == -1)
                    return false;

                if (combo.SelectedIndex != fileIndex)
                    combo.SelectedIndex = fileIndex;

                listviewapt.SelectedIndex = targetItemIndex;

                if (textSelectionStart >= 0 && textSelectionStart <= textbox.TextLength)
                {
                    textbox.Focus();
                    textbox.SelectionStart = textSelectionStart;
                    textbox.SelectionLength = Math.Min(textSelectionLength, textbox.TextLength - textSelectionStart);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void NewFile()
        {
            fileName = "";
            picview.Image = null;
            picview.LoadImage(Application.StartupPath + "\\default_image.png");
            textboxgroupbox.Text = "";
            setTextboxText("");
        }

        private void refreshListViewAdaptor()
        {
            listviewapt.ReloadItems(wsp.Store[fileName]);
        }
        private void setTextboxText(string text)
        {
            textbox.TextChanged -= textbox_TextChanged;

            textbox.Text = text;
            textbox.SelectionLength = 0;
            textbox.SelectionStart = textbox.Text.Length;

            textbox.TextChanged += new EventHandler(textbox_TextChanged);
        }

        //添加和删除操作
        private void picView_UserClickAction(object sender, PicView.LabelUserActionEventArgs e)
        {
            bool ctrlBePush = workMode == WorkMode.Label || Control.ModifierKeys == Keys.Control;
            switch (e.Type)
            {
                case PicView.LabelUserActionEventArgs.ActionType.leftClick:
                    AddLabelCommand(e, 1);
                    break;
                case PicView.LabelUserActionEventArgs.ActionType.rightClickAdd:
                    //add
                    AddLabelCommand(e, 2);
                    break;
                case PicView.LabelUserActionEventArgs.ActionType.rightClickDel:
                    //del
                    DeleteLabelCommand(e);
                    break;
                case PicView.LabelUserActionEventArgs.ActionType.labelChanged:
                    {
                        wsp.Store.ChangeLabelItem();
                        if (e.Index == -1)
                            return;
                        listviewapt.SelectedIndex = e.Index;
                    }
                    break;
                case PicView.LabelUserActionEventArgs.ActionType.mouseIndexChanged:

                    if (workMode == WorkMode.Check)
                    {

                        if (e.Index == -1)
                            return;
                        listviewapt.SelectedIndex = e.Index;
                    }
                    break;
            }
        }


        private void AddLabelCommand(PicView.LabelUserActionEventArgs e, int category = 1)
        {
            LabelUndo label = new LabelUndo()
            {
                Index = listviewapt.Count,
                Location = new Location() { X_percent = e.X_percent, Y_percent = e.Y_percent },
                Category = category,
            };

            AddLabelCommand addLabelCommand = new AddLabelCommand(AddLabel, DeleteLabel, label);
            UndoRedoManager.LabelCommandPool.Register(addLabelCommand);
            addLabelCommand.Excute();
        }

        private void DeleteLabelCommand(PicView.LabelUserActionEventArgs e)
        {
            if (e.Index == -1)
                return;

            LabelUndo label = new LabelUndo()
            {
                Index = e.Index,
                Location = new Location() { X_percent = e.X_percent, Y_percent = e.Y_percent },
                Category = groupIndex + 1,
            };

            if (!string.IsNullOrEmpty(LabelFileManager.store[fileName][e.Index].Text))
            {
                label.Text = LabelFileManager.store[fileName][e.Index].Text;
                if(MessageBox.Show(
                    StringResources.GetValue("tip_sure_del_label_with_text"),
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
            }
                

            label.Category = LabelFileManager.store[fileName][e.Index].Category;
            DeleteLabelCommand deleteLabelCommand = new DeleteLabelCommand(DeleteLabel, AddLabel, label);
            UndoRedoManager.LabelCommandPool.Register(deleteLabelCommand);
            deleteLabelCommand.Excute();
        }

        private void AddLabel(LabelUndo label)
        {
            wsp.Store.AddLabelItem(FileName, new LabelItem(label.Location.X_percent, label.Location.Y_percent, label.Text, label.Category), label.Index);
            listviewapt.SelectedIndex = listviewapt.Count - 1;
        }

        private void DeleteLabel(LabelUndo label)
        {
            wsp.Store.DelLabelItem(FileName, label.Index);
            listviewapt.SelectedIndex = -1;
        }

        private void picViewKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Q)
            //{
            //    modebuttons.SelectedButtonIndex = 0;
            //}
            //else if (e.KeyCode == Keys.W)
            //{
            //    modebuttons.SelectedButtonIndex = 1;
            //}
            //else if (e.KeyCode == Keys.E)
            //{
            //    modebuttons.SelectedButtonIndex = 2;
            //}
            //else if (e.KeyCode == Keys.R)
            //{
            //    modebuttons.SelectedButtonIndex = 3;
            //}
            //else
            if (ShortcutManager.Matches(ShortcutManager.UndoLabel, e))
            {
                UndoRedoManager.UndoLabel();
                e.SuppressKeyPress = true;
            }
            else if (ShortcutManager.Matches(ShortcutManager.RedoLabel, e))
            {
                UndoRedoManager.RedoLabel();
                e.SuppressKeyPress = true;
            }
        }

        private void listViewSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listviewapt.SelectedIndex == -1)
                {
                    //did not select item
                    setTextboxText("");
                    itemIndex = -1;
                    textboxgroupbox.Text = "";
                }
                else
                {
                    itemIndex = listviewapt.SelectedIndex;
                    setTextboxText(wsp.Store[fileName][itemIndex].Text);
                    textboxgroupbox.Text = (itemIndex + 1).ToString();

                    if (listviewapt.SelectedIndexCount > 1)
                        return;

                    textbox.Focus();

                    if(sender != null)
                        picview.SetLabelVisual(listviewapt.SelectedIndex);
                    //if (!suppressSetVisualWhenIndexChanged && wsp.setVisualWhenIndexChanged)
                    //{
                    //    if (picview.Focused)
                    //        return;
                    //    if (listviewapt.SelectedIndexCount > 1)
                    //        return;
                    //    picview.SetLabelVisual(listviewapt.SelectedIndex);
                    //}
                    //else if (workMode == WorkMode.Check)
                    //{
                    //    if (listviewapt.SelectedIndexCount > 1)
                    //        return;
                    //
                    //    if (listviewapt.ListView.Focused == true)
                    //        picview.SetLabelVisual(listviewapt.SelectedIndex);
                    //}
                }
            }
            catch { }
        }

        private void labelItemTextChanged(object sender, EventArgs e)
        {
            try
            {
                listviewapt.ReloadItems(wsp.Store[fileName]);
            }
            catch { }
        }
        private void labelItemListChanged(object sender, EventArgs e)
        {
            try
            {
                if (wsp.Store.Filenames.Contains(fileName))
                {
                    listviewapt.ReloadItems(wsp.Store[fileName]);
                    picview.SetLabels(wsp.Store[fileName], wsp.GroupDefine.GetViewNames(), wsp.GroupDefine.GetColors());
                    //listviewapt.SelectedIndex = 0;
                }
                else
                {
                    listviewapt.ReloadItems(null);
                    picview.SetLabels(null, null, null);
                }
            }
            catch { }
        }
        private void fileListChanged(object sender, EventArgs e)
        {
            try
            {
                //Set comboo items

                combo.SelectedIndexChanged -= comboSelectedIndexChanged;

                string beforeFile = combo.Text;

                combo.Items.Clear();

                var keys = wsp.Store.Filenames;
                if (keys != null)
                {
                    Array.Sort(keys, LabelFileManager.StrCmpLogicalW);
                    foreach (string name in keys)
                    {
                        combo.Items.Add(name);
                    }
                }

                combo.SelectedIndexChanged += comboSelectedIndexChanged;

                int n = combo.FindStringExact(beforeFile);
                if (n != -1)
                {
                    combo.SelectedIndex = n;
                }

            }
            catch { }
        }

        private void textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (ShortcutManager.Matches(ShortcutManager.LabelNext, e))
            {
                listviewapt.SelectedIndex++;
                if (wsp.setVisualWhenIndexChanged)
                {
                    picview.SetLabelVisual(listviewapt.SelectedIndex);
                }
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (ShortcutManager.Matches(ShortcutManager.LabelPrevious, e))
            {
                listviewapt.SelectedIndex--;
                if (wsp.setVisualWhenIndexChanged)
                {
                    picview.SetLabelVisual(listviewapt.SelectedIndex);
                }
                e.SuppressKeyPress = true;
            }
            else if (ShortcutManager.Matches(ShortcutManager.PageLeft, e))
            {
                page_left();
                e.SuppressKeyPress = true;
            }
            else if (ShortcutManager.Matches(ShortcutManager.PageRight, e))
            {
                page_right();
                e.SuppressKeyPress = true;
            }

            if (ShortcutManager.Matches(ShortcutManager.QuickText, e))
            {
                var filter = new ContextMenuOutsideClickFilter(menuquicktext);
                Application.AddMessageFilter(filter);
                quickTextTarget = textbox;
                menuquicktext.Show(
                    textbox,
                    GetQuickTextMenuLocation(),
                    ToolStripDropDownDirection.BelowRight);
                e.SuppressKeyPress = true;
            }
        }

        private Point GetQuickTextMenuLocation()
        {
            int charIndex = textbox.TextLength;
            int lineIndex = textbox.GetLineFromCharIndex(charIndex);
            int firstCharIndex = textbox.GetFirstCharIndexFromLine(lineIndex);

            if (firstCharIndex < 0)
                firstCharIndex = 0;

            Point lineStart = textbox.GetPositionFromCharIndex(firstCharIndex);
            int padding = 4;
            int x = textbox.ClientSize.Width - menuquicktext.GetPreferredSize(Size.Empty).Width - padding;
            int y = lineStart.Y + textbox.Font.Height + 2;

            return new Point(Math.Max(padding, x), y);
        }

        private void textboxPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (ShortcutManager.Matches(ShortcutManager.LabelNext, e))
            {
                e.IsInputKey = true;
            }
        }
        private void textbox_TextChanged(object sender, EventArgs e)
        {
            if (itemIndex < 0)
            {
                textbox.Text = "";
                return;
            }

            //清空标签池
            UndoRedoManager.labelCommandPool.Clear();

            wsp.Store.UpdateLabelItemText(fileName, itemIndex, textbox.Text);
        }

        private void comboSelectedIndexChanged(object sender, EventArgs e)
        {
            fileName = combo.Text;
            picview.LoadImage(wsp.DirPath + @"\" + combo.Text);
            labelItemListChanged(null, null);
            listviewapt.SelectedIndex = 0;
        }



        //private void userGroupChanged(object sender, EventArgs e)
        //{
            //groupbuttons.Refresh(wsp.GroupDefine);
        //}

        private void picView_MouseMove(object sender, MouseEventArgs e)
        {
            //记录位置
            picViewMousePosition = e.Location;

            //鼠标样式
            if (Control.ModifierKeys == Keys.Control || workMode == WorkMode.Label)
            {
                picview.Cursor = Cursors.Cross;
            }
            else
            {
                picview.Cursor = Cursors.Default;
            }

        }

        private void picView_MosueClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                //中键翻页
                page_right();
            }
        }

        private void listViewUserAction(object sender, DataGridViewAdapter.UserActionEventArgs e)
        {
            if (e.Action == DataGridViewAdapter.UserActionEventArgs.ActionType.del)
            {
                if (MessageBox.Show(
                    StringResources.GetValue("tip_sure_del_label"),
                    "warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
            }
            if (e.Value <= wsp.GroupDefine.UserGroupCount)
            {
                for (int i = e.Index.Length - 1; i >= 0; i--)
                {
                    int index = e.Index[i];
                    if (e.Action == DataGridViewAdapter.UserActionEventArgs.ActionType.setGroup)
                        wsp.Store.UpdateLabelCategory(fileName, index, e.Value);
                    else if (e.Action == DataGridViewAdapter.UserActionEventArgs.ActionType.del)
                    {
                        wsp.Store.DelLabelItem(fileName, index);
                        //清空标签池
                        UndoRedoManager.labelCommandPool.Clear();
                    }
                }
            }
            picview.Invalidate();
        }


        private void quickTextItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            InsertQuickText(e.ClickedItem.ToolTipText);
        }

        private void InsertQuickText(string text)
        {
            if (quickTextTarget != textbox && !textbox.Focused)
                return;

            textbox.AppendText(text);
        }

        private void quickTextClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (quickTextShortcutFilter != null)
            {
                Application.RemoveMessageFilter(quickTextShortcutFilter);
                quickTextShortcutFilter = null;
            }

            quickTextTarget = null;
            textbox.ImeMode = ImeMode.NoControl;
        }

        private void quickTextOpened(object sender, EventArgs e)
        {
            if (quickTextShortcutFilter != null)
                Application.RemoveMessageFilter(quickTextShortcutFilter);

            quickTextShortcutFilter = new QuickTextShortcutFilter(
                menuquicktext,
                GlobalVar.QuickTextItems,
                InsertQuickText);
            Application.AddMessageFilter(quickTextShortcutFilter);
            textbox.ImeMode = ImeMode.Off;
        }

        private class QuickTextShortcutFilter : IMessageFilter
        {
            private const int WM_KEYDOWN = 0x0100;
            private readonly ContextMenuStrip menu;
            private readonly GlobalVar.QuickTextItem[] items;
            private readonly Action<string> insertText;

            public QuickTextShortcutFilter(
                ContextMenuStrip menu,
                GlobalVar.QuickTextItem[] items,
                Action<string> insertText)
            {
                this.menu = menu;
                this.items = items;
                this.insertText = insertText;
            }

            public bool PreFilterMessage(ref Message m)
            {
                if (!menu.Visible || m.Msg != WM_KEYDOWN)
                    return false;

                Keys key = (Keys)((int)m.WParam & (int)Keys.KeyCode);
                if (key == Keys.Escape)
                {
                    menu.Close();
                    return true;
                }

                string keyText = GetKeyText(key);
                if (string.IsNullOrEmpty(keyText))
                    return false;

                foreach (GlobalVar.QuickTextItem item in items)
                {
                    if (string.Equals(item.Key, keyText, StringComparison.OrdinalIgnoreCase))
                    {
                        insertText(item.Text);
                        return true;
                    }
                }

                return false;
            }

            private static string GetKeyText(Keys key)
            {
                if (key >= Keys.A && key <= Keys.Z)
                    return key.ToString();

                if (key >= Keys.D0 && key <= Keys.D9)
                    return ((char)('0' + key - Keys.D0)).ToString();

                if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
                    return ((char)('0' + key - Keys.NumPad0)).ToString();

                return "";
            }
        }
        #endregion

        #region Constructors
        public WorkspaceControlAdpter(
            ToolStripComboBox FileSelectComboBox,
            TextBox TranslateTextBox,
            GroupBox TextBoxGroupBox,
            DataGridViewAdapter LabelListViewAPT,
            PicView picView,
            ContextMenuStrip contextMenuQuictText,
            ToolStrip toolStrip,
            Workspace workspace,
            APIManager apiManager)
        {
            this.apiManager = apiManager;
            wsp = workspace;
            //wsp.UserGroupDefineChanged += new EventHandler(userGroupChanged);

            LabelFileManager.FileListChanged += new EventHandler(fileListChanged);
            LabelFileManager.LabelItemListChanged += new EventHandler(labelItemListChanged);
            LabelFileManager.LabelItemTextChanged += new EventHandler(labelItemTextChanged);
            LabelFileManager.GroupListChanged += new EventHandler(labelItemTextChanged);
            textboxgroupbox = TextBoxGroupBox;

            picview = picView;
            picview.Image = null;
            picview.Refresh();
            picview.LabelUserAction += new PicView.UserActionEventHandler(picView_UserClickAction);
            picview.MouseMove += new MouseEventHandler(picView_MouseMove);
            picview.MouseClick += new MouseEventHandler(picView_MosueClick);
            picview.KeyDown += new KeyEventHandler(picViewKeyDown);

            combo = FileSelectComboBox;
            combo.Items.Clear();
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.SelectedIndexChanged += new EventHandler(comboSelectedIndexChanged);

            textbox = TranslateTextBox;
            textbox.PreviewKeyDown += new PreviewKeyDownEventHandler(textboxPreviewKeyDown);
            textbox.KeyDown += new KeyEventHandler(textbox_KeyDown);
            textbox.TextChanged += new EventHandler(textbox_TextChanged);

            listviewapt = LabelListViewAPT;
            listviewapt.SelectedIndexChanged += new EventHandler(listViewSelectedIndexChanged);
            listviewapt.UserSetCategory += new DataGridViewAdapter.UserActionEventHandler(listViewUserAction);

            menuquicktext = contextMenuQuictText;
            foreach (GlobalVar.QuickTextItem item in GlobalVar.QuickTextItems)
            {
                string menuItemStr = item.Text + "(&" + item.Key + ")";
                menuquicktext.Items.Add(menuItemStr).ToolTipText = item.Text;

            }
            menuquicktext.ItemClicked += new ToolStripItemClickedEventHandler(quickTextItemClicked);
            menuquicktext.Opened += new EventHandler(quickTextOpened);
            menuquicktext.Closed += new ToolStripDropDownClosedEventHandler(quickTextClosed);
            menuquicktext.AutoClose = false;

            //groupbuttons = new GroupButtonAdaptor(toolStrip, wsp.GroupDefine);

            toolstrip = toolStrip;
            NewFile();
        }


        #endregion

    }
}
