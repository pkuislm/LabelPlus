using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace LabelPlus
{
    public class ShortcutDefinition
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Group { get; private set; }
        public Keys DefaultKeys { get; private set; }
        public Keys Keys { get; set; }

        public ShortcutDefinition(string id, string name, string group, Keys defaultKeys)
        {
            Id = id;
            Name = name;
            Group = group;
            DefaultKeys = defaultKeys;
            Keys = defaultKeys;
        }
    }

    static class ShortcutManager
    {
        public const string NewFile = "file.new";
        public const string OpenFile = "file.open";
        public const string SaveFile = "file.save";
        public const string SaveAs = "file.saveAs";
        public const string ImageManager = "file.imageManager";
        public const string SearchReplace = "edit.searchReplace";
        public const string ShortcutSettings = "edit.shortcutSettings";
        public const string HideWindow = "view.hideWindow";
        public const string FontLarger = "edit.fontLarger";
        public const string FontSmaller = "edit.fontSmaller";
        public const string FontDialog = "edit.fontDialog";
        public const string ToggleFollow = "view.toggleFollow";
        public const string PageLeft = "nav.pageLeft";
        public const string PageRight = "nav.pageRight";
        public const string LabelPrevious = "nav.labelPrevious";
        public const string LabelNext = "nav.labelNext";
        public const string QuickText = "edit.quickText";
        public const string UndoLabel = "edit.undoLabel";
        public const string RedoLabel = "edit.redoLabel";
        public const string DeleteSelectedLabels = "edit.deleteSelectedLabels";
        public const string HideLabels = "view.hideLabels";

        static readonly List<ShortcutDefinition> definitions = new List<ShortcutDefinition>
        {
            new ShortcutDefinition(NewFile, "新建项目", "文件", Keys.Control | Keys.N),
            new ShortcutDefinition(OpenFile, "打开项目", "文件", Keys.Control | Keys.O),
            new ShortcutDefinition(SaveFile, "保存项目", "文件", Keys.Control | Keys.S),
            new ShortcutDefinition(SaveAs, "另存为", "文件", Keys.Control | Keys.Shift | Keys.S),
            new ShortcutDefinition(ImageManager, "图片管理", "文件", Keys.Control | Keys.I),
            new ShortcutDefinition(SearchReplace, "搜索/替换", "编辑", Keys.Control | Keys.F),
            new ShortcutDefinition(ShortcutSettings, "快捷键设置", "编辑", Keys.Control | Keys.K),
            new ShortcutDefinition(HideWindow, "隐藏窗口", "视图", Keys.Control | Keys.T),
            new ShortcutDefinition(FontLarger, "增大编辑字体", "编辑", Keys.Control | Keys.Shift | Keys.Oemplus),
            new ShortcutDefinition(FontSmaller, "减小编辑字体", "编辑", Keys.Control | Keys.Shift | Keys.OemMinus),
            new ShortcutDefinition(FontDialog, "更改字体", "编辑", Keys.Control | Keys.Shift | Keys.F),
            new ShortcutDefinition(ToggleFollow, "切换标签跟随", "视图", Keys.Control | Keys.Alt | Keys.F),
            new ShortcutDefinition(PageLeft, "上一页", "导航", Keys.Control | Keys.Left),
            new ShortcutDefinition(PageRight, "下一页", "导航", Keys.Control | Keys.Right),
            new ShortcutDefinition(LabelPrevious, "上一条标签", "导航", Keys.Control | Keys.Up),
            new ShortcutDefinition(LabelNext, "下一条标签", "导航", Keys.Control | Keys.Enter),
            new ShortcutDefinition(QuickText, "快捷短语", "编辑", Keys.Alt | Keys.A),
            new ShortcutDefinition(UndoLabel, "撤销标签操作", "编辑", Keys.Control | Keys.Z),
            new ShortcutDefinition(RedoLabel, "重做标签操作", "编辑", Keys.Control | Keys.Y),
            new ShortcutDefinition(DeleteSelectedLabels, "删除选中标签", "编辑", Keys.Control | Keys.Delete),
            new ShortcutDefinition(HideLabels, "按住隐藏标签", "视图", Keys.Control | Keys.Shift | Keys.V),
            new ShortcutDefinition("label.category1", "设置分类 1", "标签分类", Keys.Control | Keys.D1),
            new ShortcutDefinition("label.category2", "设置分类 2", "标签分类", Keys.Control | Keys.D2),
            new ShortcutDefinition("label.category3", "设置分类 3", "标签分类", Keys.Control | Keys.D3),
            new ShortcutDefinition("label.category4", "设置分类 4", "标签分类", Keys.Control | Keys.D4),
            new ShortcutDefinition("label.category5", "设置分类 5", "标签分类", Keys.Control | Keys.D5),
            new ShortcutDefinition("label.category6", "设置分类 6", "标签分类", Keys.Control | Keys.D6),
            new ShortcutDefinition("label.category7", "设置分类 7", "标签分类", Keys.Control | Keys.D7),
            new ShortcutDefinition("label.category8", "设置分类 8", "标签分类", Keys.Control | Keys.D8),
            new ShortcutDefinition("label.category9", "设置分类 9", "标签分类", Keys.Control | Keys.D9),
        };

        public static IEnumerable<ShortcutDefinition> Definitions
        {
            get { return definitions; }
        }

        public static ShortcutDefinition Get(string id)
        {
            return definitions.FirstOrDefault(d => d.Id == id);
        }

        public static Keys GetKeys(string id)
        {
            var definition = Get(id);
            return definition == null ? Keys.None : definition.Keys;
        }

        public static string GetText(string id)
        {
            return Format(GetKeys(id));
        }

        public static string CategoryId(int category)
        {
            return "label.category" + category.ToString();
        }

        public static bool Matches(string id, KeyEventArgs e)
        {
            return Matches(id, e.KeyCode, e.Modifiers);
        }

        public static bool Matches(string id, PreviewKeyDownEventArgs e)
        {
            return Matches(id, e.KeyCode, e.Modifiers);
        }

        public static bool Matches(string id, Keys keyCode, Keys modifiers)
        {
            Keys shortcut = GetKeys(id);
            return shortcut != Keys.None && shortcut == Normalize(keyCode, modifiers);
        }

        public static Keys Normalize(Keys keyCode, Keys modifiers)
        {
            return (keyCode & Keys.KeyCode) | (modifiers & Keys.Modifiers);
        }

        public static bool HasRequiredModifier(Keys shortcut)
        {
            Keys modifiers = shortcut & Keys.Modifiers;
            return (modifiers & (Keys.Control | Keys.Alt | Keys.Shift)) != 0;
        }

        public static string ValidateShortcut(string id, Keys shortcut)
        {
            if (shortcut == Keys.None)
                return "";

            Keys keyCode = shortcut & Keys.KeyCode;
            if (keyCode == Keys.None ||
                keyCode == Keys.ControlKey ||
                keyCode == Keys.ShiftKey ||
                keyCode == Keys.Menu)
            {
                return "快捷键需要包含一个普通按键。";
            }

            if (!HasRequiredModifier(shortcut))
                return "快捷键必须搭配 Ctrl、Alt 或 Shift。";

            var conflict = definitions.FirstOrDefault(d => d.Id != id && d.Keys == shortcut);
            if (conflict != null)
                return "与“" + conflict.Name + "”冲突。";

            return "";
        }

        public static void ResetToDefaults()
        {
            foreach (var definition in definitions)
                definition.Keys = definition.DefaultKeys;
        }

        public static void Load(XmlDocument doc)
        {
            ResetToDefaults();

            XmlNodeList nodes = doc.SelectNodes("AppConfig/Shortcuts/Shortcut");
            if (nodes == null)
                return;

            foreach (XmlNode node in nodes)
            {
                var idAttr = node.Attributes == null ? null : node.Attributes["id"];
                var keysAttr = node.Attributes == null ? null : node.Attributes["keys"];
                if (idAttr == null || keysAttr == null)
                    continue;

                var definition = Get(idAttr.Value);
                if (definition == null)
                    continue;

                Keys parsed;
                if (TryParse(keysAttr.Value, out parsed) && ValidateShortcut(definition.Id, parsed) == "")
                    definition.Keys = parsed;
            }
        }

        public static void Save(XmlDocument doc)
        {
            XmlNode root = doc.SelectSingleNode("AppConfig");
            if (root == null)
                return;

            XmlNode oldNode = doc.SelectSingleNode("AppConfig/Shortcuts");
            if (oldNode != null)
                root.RemoveChild(oldNode);

            XmlElement shortcutsNode = doc.CreateElement("Shortcuts");
            foreach (var definition in definitions)
            {
                XmlElement node = doc.CreateElement("Shortcut");
                node.SetAttribute("id", definition.Id);
                node.SetAttribute("keys", Format(definition.Keys));
                shortcutsNode.AppendChild(node);
            }
            root.AppendChild(shortcutsNode);
        }

        public static bool TryParse(string text, out Keys shortcut)
        {
            shortcut = Keys.None;
            if (string.IsNullOrWhiteSpace(text))
                return true;

            string[] parts = text.Split('+');
            foreach (string rawPart in parts)
            {
                string part = rawPart.Trim();
                if (part.Equals("Ctrl", StringComparison.OrdinalIgnoreCase) ||
                    part.Equals("Control", StringComparison.OrdinalIgnoreCase))
                {
                    shortcut |= Keys.Control;
                }
                else if (part.Equals("Alt", StringComparison.OrdinalIgnoreCase))
                {
                    shortcut |= Keys.Alt;
                }
                else if (part.Equals("Shift", StringComparison.OrdinalIgnoreCase))
                {
                    shortcut |= Keys.Shift;
                }
                else
                {
                    Keys key;
                    if (!Enum.TryParse(part, true, out key))
                        return false;
                    shortcut |= key;
                }
            }

            return true;
        }

        public static string Format(Keys shortcut)
        {
            if (shortcut == Keys.None)
                return "";

            List<string> parts = new List<string>();
            if ((shortcut & Keys.Control) == Keys.Control)
                parts.Add("Ctrl");
            if ((shortcut & Keys.Alt) == Keys.Alt)
                parts.Add("Alt");
            if ((shortcut & Keys.Shift) == Keys.Shift)
                parts.Add("Shift");

            Keys keyCode = shortcut & Keys.KeyCode;
            if (keyCode != Keys.None)
                parts.Add(KeyCodeToText(keyCode));

            return string.Join("+", parts.ToArray());
        }

        static string KeyCodeToText(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.Oemplus:
                    return "Oemplus";
                case Keys.OemMinus:
                    return "OemMinus";
                case Keys.Oemcomma:
                    return "Oemcomma";
                case Keys.OemPeriod:
                    return "OemPeriod";
                default:
                    return keyCode.ToString();
            }
        }
    }
}
