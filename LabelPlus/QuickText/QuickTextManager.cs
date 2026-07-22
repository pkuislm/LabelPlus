using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace LabelPlus
{
	public class QuickTextItem
	{
		public string Text { get; set; }
		public Keys Key { get; set; }

		public QuickTextItem(string text, Keys key)
		{
			Text = text;
			Key = key;
		}
	}

	static class QuickTextManager
	{
		public static QuickTextItem[] Items { get; private set; } = new QuickTextItem[0];
		public static event EventHandler ItemsChanged;

		public enum QuickTextStatus
		{
			DEFAULT,
			OK,
			EMPTY_TEXT,
			EMPTY_KEY,
			INVALID_KEY,
			INVALID_KEY_RANGE,
			DUPLICATE_SHORTCUT,
			DUPLICATE_TEXT,
			NO_PREVIOUS_VALUE,
			CAPTURING_KEY_MODE,
		}

		public static void SetItems(QuickTextItem[] items)
		{
			Items = items ?? new QuickTextItem[0];
			ItemsChanged?.Invoke(null, EventArgs.Empty);
		}

		public static void Load(XmlDocument doc)
		{
			// TODO: 从 xml 中加载快捷短语
			List<QuickTextItem> itemsList = new List<QuickTextItem>();
			XmlNodeList nodes = doc.SelectNodes("AppConfig/QuickText/Item");
			if (nodes == null)
                return;
			
			foreach (XmlNode node in nodes)
			{
				var textAttr = node.Attributes == null ? null : node.Attributes["Text"];
                var keyAttr = node.Attributes == null ? null : node.Attributes["Key"];
                if (textAttr == null || keyAttr == null)
                    continue;
				
				itemsList.Add(new QuickTextItem(textAttr.Value, KeyFromText(keyAttr.Value)));
			}

			SetItems(itemsList.ToArray());
		}

		public static void Save(XmlDocument doc)
		{
			// TODO: 将快捷短语保存至 xml 中
			XmlNode root = doc.SelectSingleNode("AppConfig");
            if (root == null)
                return;

            XmlNode oldNode = doc.SelectSingleNode("AppConfig/QuickText");
            if (oldNode != null)
                root.RemoveChild(oldNode);

            XmlElement quicktextNode = doc.CreateElement("QuickText");
			foreach (QuickTextItem item in Items)
			{
				XmlElement node = doc.CreateElement("Item");
                node.SetAttribute("Text", item.Text);
                node.SetAttribute("Key", KeyToText(item.Key));
                quicktextNode.AppendChild(node);
			}

			root.AppendChild(quicktextNode);
		}

		public static QuickTextStatus Validate(string value, Keys key, bool replace = false)
		{
			if (string.IsNullOrWhiteSpace(value))
				return QuickTextStatus.EMPTY_TEXT;

			if (key == Keys.None)
				return QuickTextStatus.EMPTY_KEY;

			Keys keyCode = key & Keys.KeyCode;
			if (keyCode == Keys.None || key != keyCode)
				return QuickTextStatus.INVALID_KEY;

			if (!IsAllowedKey(keyCode))
			{
				return QuickTextStatus.INVALID_KEY_RANGE;
			}
			
			foreach (QuickTextItem item in Items)
			{
				if (item.Key == key)
					return QuickTextStatus.DUPLICATE_SHORTCUT;
				if (!replace && item.Text == value)
					return QuickTextStatus.DUPLICATE_TEXT;
			}

			return QuickTextStatus.OK;
		}

		public static string StatusToText(QuickTextStatus status)
		{
			switch (status)
			{
				case QuickTextStatus.OK:
					return string.Empty;
				case QuickTextStatus.EMPTY_TEXT:
					return "快捷短语不能为空。";
				case QuickTextStatus.EMPTY_KEY:
					return "快捷键不能为空。";
				case QuickTextStatus.INVALID_KEY:
					return "快捷键只能是单个按键。";
				case QuickTextStatus.INVALID_KEY_RANGE:
					return "快捷键只能是 0-9 或 a-z。";
				case QuickTextStatus.DUPLICATE_SHORTCUT:
					return "快捷键已被使用。";
				case QuickTextStatus.DUPLICATE_TEXT:
					return "快捷短语已存在。";
				case QuickTextStatus.NO_PREVIOUS_VALUE:
					return "新增行没有原先值。";
				case QuickTextStatus.CAPTURING_KEY_MODE:
					return "正在修改快捷键：请直接按 0-9 或 a-z。按 ， 取消。";
				default:
					return "快捷短语设置无效。" + status;
			}
		}

		public static Keys KeyFromText(string keyText)
        {
            if (string.IsNullOrWhiteSpace(keyText))
                return Keys.None;

            string text = keyText.Trim();
            if (text.Length == 1)
            {
                char ch = text[0];
                if (ch >= '0' && ch <= '9')
                    return (Keys)((int)Keys.D0 + (ch - '0'));
                if (ch >= 'a' && ch <= 'z')
                    return (Keys)((int)Keys.A + (ch - 'a'));
            }

            return Keys.None;
        }

        public static string KeyToText(Keys key)
        {
            if (key >= Keys.D0 && key <= Keys.D9)
                return ((char)('0' + (key - Keys.D0))).ToString();
            if (key >= Keys.A && key <= Keys.Z)
                return ((char)('a' + (key - Keys.A))).ToString();
            return string.Empty;
        }

		public static bool IsAllowedKey(Keys key)
        {
            return (key >= Keys.D0 && key <= Keys.D9) ||
                (key >= Keys.A && key <= Keys.Z);
        }
	}
}