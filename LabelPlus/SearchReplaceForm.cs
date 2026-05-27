using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabelPlus
{
    public partial class SearchReplaceForm : Form
    {
        class SearchMatch
        {
            public string FileName;
            public int ItemIndex;
            public int MatchIndex;
            public int MatchLength;
        }

        Workspace workspace;
        WorkspaceControlAdpter adapter;
        SearchMatch lastMatch;
        string lastFindText = "";
        bool lastCaseSensitive;
        bool lastWholeWord;

        public SearchReplaceForm()
        {
            InitializeComponent();
        }

        public SearchReplaceForm(Workspace workspace, WorkspaceControlAdpter adapter)
            : this()
        {
            this.workspace = workspace;
            this.adapter = adapter;
            Text = "搜索/替换";

            checkBox3.Checked = true;
            findNextButton.Click += delegate { FindAndSelect(true, true); };
            findPrevButton.Click += delegate { FindAndSelect(false, true); };
            replaceButton.Click += delegate { ReplaceCurrent(); };
            button4.Click += delegate { ReplaceAll(); };
            button5.Click += delegate { Close(); };
            textBox1.TextChanged += delegate { ResetLastMatch(); };
            checkBox1.CheckedChanged += delegate { ResetLastMatch(); };
            checkBox2.CheckedChanged += delegate { ResetLastMatch(); };
        }

        void ResetLastMatch()
        {
            lastMatch = null;
        }

        List<SearchMatch> GetMatches()
        {
            List<SearchMatch> matches = new List<SearchMatch>();
            string findText = textBox1.Text;
            if (string.IsNullOrEmpty(findText) || workspace == null)
                return matches;

            StringComparison comparison = checkBox1.Checked
                ? StringComparison.CurrentCulture
                : StringComparison.CurrentCultureIgnoreCase;

            foreach (string fileName in workspace.Store.Filenames)
            {
                List<LabelItem> items = workspace.Store[fileName];
                for (int itemIndex = 0; itemIndex < items.Count; itemIndex++)
                {
                    string text = items[itemIndex].Text ?? "";
                    int startIndex = 0;
                    while (startIndex <= text.Length)
                    {
                        int index = text.IndexOf(findText, startIndex, comparison);
                        if (index < 0)
                            break;

                        if (!checkBox2.Checked || IsWholeWord(text, index, findText.Length))
                        {
                            matches.Add(new SearchMatch
                            {
                                FileName = fileName,
                                ItemIndex = itemIndex,
                                MatchIndex = index,
                                MatchLength = findText.Length
                            });
                        }

                        startIndex = index + Math.Max(1, findText.Length);
                    }
                }
            }

            return matches;
        }

        bool FindAndSelect(bool forward, bool showMessage)
        {
            List<SearchMatch> matches = GetMatches();
            if (matches.Count == 0)
            {
                if (showMessage)
                    MessageBox.Show("没有找到匹配内容。", "搜索/替换", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            int currentFileOrder = GetFileOrder(adapter == null ? "" : adapter.FileName);
            int currentItemIndex = adapter == null ? -1 : adapter.ItemIndex;
            int currentMatchIndex = forward ? 0 : int.MaxValue;

            if (IsLastMatchUsable())
            {
                currentFileOrder = GetFileOrder(lastMatch.FileName);
                currentItemIndex = lastMatch.ItemIndex;
                currentMatchIndex = forward
                    ? lastMatch.MatchIndex + Math.Max(1, lastMatch.MatchLength)
                    : lastMatch.MatchIndex - 1;
            }

            SearchMatch nextMatch = null;
            if (forward)
            {
                nextMatch = matches.FirstOrDefault(m => IsAfter(m, currentFileOrder, currentItemIndex, currentMatchIndex));
                if (nextMatch == null && checkBox3.Checked)
                    nextMatch = matches.First();
            }
            else
            {
                nextMatch = matches.LastOrDefault(m => IsBefore(m, currentFileOrder, currentItemIndex, currentMatchIndex));
                if (nextMatch == null && checkBox3.Checked)
                    nextMatch = matches.Last();
            }

            if (nextMatch == null)
            {
                if (showMessage)
                    MessageBox.Show("已经到达搜索边界。", "搜索/替换", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            SelectMatch(nextMatch);
            return true;
        }

        bool IsAfter(SearchMatch match, int fileOrder, int itemIndex, int matchIndex)
        {
            int order = GetFileOrder(match.FileName);
            if (order != fileOrder)
                return order > fileOrder;
            if (match.ItemIndex != itemIndex)
                return match.ItemIndex > itemIndex;
            return match.MatchIndex >= matchIndex;
        }

        bool IsBefore(SearchMatch match, int fileOrder, int itemIndex, int matchIndex)
        {
            int order = GetFileOrder(match.FileName);
            if (order != fileOrder)
                return order < fileOrder;
            if (match.ItemIndex != itemIndex)
                return match.ItemIndex < itemIndex;
            return match.MatchIndex <= matchIndex;
        }

        int GetFileOrder(string fileName)
        {
            if (workspace == null)
                return -1;

            string[] filenames = workspace.Store.Filenames;
            for (int i = 0; i < filenames.Length; i++)
            {
                if (filenames[i] == fileName)
                    return i;
            }
            return -1;
        }

        void SelectMatch(SearchMatch match)
        {
            lastMatch = match;
            lastFindText = textBox1.Text;
            lastCaseSensitive = checkBox1.Checked;
            lastWholeWord = checkBox2.Checked;

            if (adapter != null)
                adapter.SelectLabel(match.FileName, match.ItemIndex, match.MatchIndex, match.MatchLength);
        }

        bool IsLastMatchUsable()
        {
            if (lastMatch == null || workspace == null)
                return false;
            if (lastFindText != textBox1.Text ||
                lastCaseSensitive != checkBox1.Checked ||
                lastWholeWord != checkBox2.Checked)
            {
                return false;
            }

            LabelItem item = workspace.Store[lastMatch.FileName, lastMatch.ItemIndex];
            if (item == null || item.Text == null)
                return false;
            if (lastMatch.MatchIndex < 0 || lastMatch.MatchIndex + lastFindText.Length > item.Text.Length)
                return false;

            string current = item.Text.Substring(lastMatch.MatchIndex, lastFindText.Length);
            return string.Compare(current, lastFindText, !checkBox1.Checked) == 0;
        }

        void ReplaceCurrent()
        {
            if (!IsLastMatchUsable() && !FindAndSelect(true, false))
            {
                MessageBox.Show("没有可替换的匹配内容。", "搜索/替换", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LabelItem item = workspace.Store[lastMatch.FileName, lastMatch.ItemIndex];
            string replacement = textBox2.Text;
            string newText = item.Text.Remove(lastMatch.MatchIndex, lastMatch.MatchLength)
                .Insert(lastMatch.MatchIndex, replacement);

            workspace.Store.UpdateLabelItemText(lastMatch.FileName, lastMatch.ItemIndex, newText);
            UndoRedoManager.labelCommandPool.Clear();

            adapter.SelectLabel(lastMatch.FileName, lastMatch.ItemIndex, lastMatch.MatchIndex, replacement.Length);
            ResetLastMatch();
        }

        void ReplaceAll()
        {
            string findText = textBox1.Text;
            if (string.IsNullOrEmpty(findText) || workspace == null)
                return;

            int changedItems = 0;
            int changedOccurrences = 0;
            string replacement = textBox2.Text;
            StringComparison comparison = checkBox1.Checked
                ? StringComparison.CurrentCulture
                : StringComparison.CurrentCultureIgnoreCase;

            foreach (string fileName in workspace.Store.Filenames)
            {
                List<LabelItem> items = workspace.Store[fileName];
                foreach (LabelItem item in items)
                {
                    string text = item.Text ?? "";
                    int startIndex = 0;
                    int occurrenceCount = 0;
                    StringBuilder builder = new StringBuilder();

                    while (startIndex <= text.Length)
                    {
                        int index = text.IndexOf(findText, startIndex, comparison);
                        if (index < 0)
                            break;

                        if (checkBox2.Checked && !IsWholeWord(text, index, findText.Length))
                        {
                            builder.Append(text.Substring(startIndex, index - startIndex + findText.Length));
                            startIndex = index + findText.Length;
                            continue;
                        }

                        builder.Append(text.Substring(startIndex, index - startIndex));
                        builder.Append(replacement);
                        startIndex = index + findText.Length;
                        occurrenceCount++;
                    }

                    if (occurrenceCount > 0)
                    {
                        builder.Append(text.Substring(startIndex));
                        item.Text = builder.ToString();
                        changedItems++;
                        changedOccurrences += occurrenceCount;
                    }
                }
            }

            if (changedItems > 0)
            {
                workspace.Store.OnLabelItemTextChanged();
                UndoRedoManager.labelCommandPool.Clear();
            }
            ResetLastMatch();
            MessageBox.Show(
                "已替换 " + changedItems + " 条，" + changedOccurrences + " 处。",
                "搜索/替换",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        bool IsWholeWord(string text, int index, int length)
        {
            bool leftBoundary = index == 0 || !IsWordChar(text[index - 1]);
            int rightIndex = index + length;
            bool rightBoundary = rightIndex >= text.Length || !IsWordChar(text[rightIndex]);
            return leftBoundary && rightBoundary;
        }

        bool IsWordChar(char ch)
        {
            return char.IsLetterOrDigit(ch) || ch == '_';
        }
    }
}
