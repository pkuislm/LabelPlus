using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LabelPlus
{
    public partial class ShortcutSettingsForm : Form
    {
        Keys pendingShortcut = Keys.None;
        Dictionary<string, Keys> originalShortcuts;
        bool saved;

        public ShortcutSettingsForm()
        {
            InitializeComponent();
            originalShortcuts = new Dictionary<string, Keys>();
            foreach (var definition in ShortcutManager.Definitions)
                originalShortcuts[definition.Id] = definition.Keys;
            LoadShortcuts();
        }

        void LoadShortcuts()
        {
            grid.Rows.Clear();
            foreach (var definition in ShortcutManager.Definitions)
            {
                grid.Rows.Add(
                    definition.Group,
                    definition.Name,
                    ShortcutManager.Format(definition.Keys),
                    definition.Id);
            }
            if (grid.Rows.Count > 0)
                grid.Rows[0].Selected = true;
        }

        string SelectedId
        {
            get
            {
                if (grid.CurrentRow == null)
                    return null;
                return Convert.ToString(grid.CurrentRow.Cells["Id"].Value);
            }
        }

        void grid_SelectionChanged(object sender, EventArgs e)
        {
            pendingShortcut = Keys.None;
            SetStatus("", false);
            shortcutTextBox.Text = "";
            if (grid.CurrentRow != null)
            {
                shortcutTextBox.Text = Convert.ToString(grid.CurrentRow.Cells["Shortcut"].Value);
                var definition = ShortcutManager.Get(SelectedId);
                if (definition != null)
                    pendingShortcut = definition.Keys;
            }
        }

        void shortcutTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            pendingShortcut = ShortcutManager.Normalize(e.KeyCode, e.Modifiers);
            shortcutTextBox.Text = ShortcutManager.Format(pendingShortcut);

            string message = ShortcutManager.ValidateShortcut(SelectedId, pendingShortcut);
            SetStatus(message, false);
        }

        void applyButton_Click(object sender, EventArgs e)
        {
            if (SelectedId == null)
                return;

            string message = ShortcutManager.ValidateShortcut(SelectedId, pendingShortcut);
            if (message != "")
            {
                SetStatus(message, false);
                return;
            }

            var definition = ShortcutManager.Get(SelectedId);
            if (definition == null)
                return;

            definition.Keys = pendingShortcut;
            grid.CurrentRow.Cells["Shortcut"].Value = ShortcutManager.Format(definition.Keys);
            SetStatus("已应用。", true);
        }

        void clearButton_Click(object sender, EventArgs e)
        {
            if (SelectedId == null)
                return;

            var definition = ShortcutManager.Get(SelectedId);
            if (definition == null)
                return;

            definition.Keys = Keys.None;
            pendingShortcut = Keys.None;
            shortcutTextBox.Text = "";
            grid.CurrentRow.Cells["Shortcut"].Value = "";
            SetStatus("已清除。", true);
        }

        void resetButton_Click(object sender, EventArgs e)
        {
            ShortcutManager.ResetToDefaults();
            LoadShortcuts();
            SetStatus("已恢复默认快捷键。", true);
        }

        void okButton_Click(object sender, EventArgs e)
        {
            GlobalVar.Save();
            saved = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        void ShortcutSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saved || originalShortcuts == null)
                return;

            foreach (var definition in ShortcutManager.Definitions)
            {
                Keys original;
                if (originalShortcuts.TryGetValue(definition.Id, out original))
                    definition.Keys = original;
            }
        }

        void SetStatus(string text, bool success)
        {
            statusLabel.ForeColor = success ? Color.DarkGreen : Color.DarkRed;
            statusLabel.Text = text;
        }
    }
}
