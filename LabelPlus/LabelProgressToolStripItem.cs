using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LabelPlus
{
    internal struct LabelProgressSegment
    {
        public Color Color;
        public bool IsCompleted;

        public LabelProgressSegment(Color color, bool isCompleted)
        {
            Color = color;
            IsCompleted = isCompleted;
        }
    }

    internal sealed class LabelProgressSegmentClickedEventArgs : EventArgs
    {
        public int Index { get; private set; }

        public LabelProgressSegmentClickedEventArgs(int index)
        {
            Index = index;
        }
    }

    internal sealed class LabelProgressToolStripItem : ToolStripControlHost
    {
        private sealed class LabelProgressBar : Control
        {
            private List<LabelProgressSegment> segments = new List<LabelProgressSegment>();
            private readonly ProgressMagnifierPopup magnifier;
            private bool isMagnifierVisible;
            private int magnifierMouseX;
            private int selectedIndex = -1;
            public event EventHandler<LabelProgressSegmentClickedEventArgs> SegmentClicked;

            public LabelProgressBar()
            {
                DoubleBuffered = true;
                Size = new Size(250, 25);
                Margin = new Padding(0);

                magnifier = new ProgressMagnifierPopup();
                MouseEnter += showMagnifier;
                MouseMove += showMagnifier;
                MouseLeave += hideMagnifier;
                MouseClick += progressBar_MouseClick;
            }

            public void SetSegments(IEnumerable<LabelProgressSegment> values)
            {
                segments = values == null ? new List<LabelProgressSegment>() : new List<LabelProgressSegment>(values);
                Invalidate();
            }

            public void SetSelectedIndex(int index)
            {
                selectedIndex = index;
                magnifier.SetSelectedIndex(index);
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                Rectangle bounds = ClientRectangle;
                Rectangle barBounds = bounds;
                e.Graphics.FillRectangle(SystemBrushes.ControlLightLight, barBounds);

                if (segments.Count > 0 && barBounds.Width > 2 && barBounds.Height > 2)
                {
                    int innerWidth = barBounds.Width - 2;
                    int x = 1;
                    for (int i = 0; i < segments.Count; i++)
                    {
                        int right = 1 + (int)Math.Round((i + 1) * innerWidth / (double)segments.Count);
                        int width = right - x;
                        if (width > 0)
                        {
                            Color color = segments[i].IsCompleted
                                ? segments[i].Color
                                : Lighten(segments[i].Color);
                            using (var brush = new SolidBrush(color))
                                e.Graphics.FillRectangle(brush, x, 1, width, barBounds.Height - 2);
                        }
                        x = right;
                    }
                }

                e.Graphics.DrawRectangle(SystemPens.ControlDark, 0, 0, barBounds.Width - 1, barBounds.Height - 1);

                if (isMagnifierVisible && segments.Count > 0)
                {
                    float visibleSourceWidth = Math.Min(64f, Width);
                    float sourceLeft = magnifierMouseX - visibleSourceWidth / 2f;
                    sourceLeft = Math.Max(0, Math.Min(Width - visibleSourceWidth, sourceLeft));
                    using (var pen = new Pen(Color.FromArgb(220, Color.Black), 1.5f))
                        e.Graphics.DrawRectangle(pen, sourceLeft, 1, visibleSourceWidth, barBounds.Height - 3);
                }

                if (selectedIndex >= 0 && selectedIndex < segments.Count)
                {
                    float centerX = 1 + (selectedIndex + 0.5f) * (barBounds.Width - 2) / segments.Count;
                    float triangleHeight = Math.Max(3f, (barBounds.Height - 2) * 0.2f);
                    float triangleBaseY = barBounds.Bottom;
                    PointF[] triangle =
                    {
                        new PointF(centerX - triangleHeight, triangleBaseY),
                        new PointF(centerX + triangleHeight, triangleBaseY),
                        new PointF(centerX, triangleBaseY - triangleHeight)
                    };
                    using (var brush = new SolidBrush(Color.FromArgb(210, Color.Black)))
                        e.Graphics.FillPolygon(brush, triangle);
                }
            }

            private static Color Lighten(Color color)
            {
                const float whiteAmount = 0.68f;
                return Color.FromArgb(
                    color.A,
                    (int)(color.R + (255 - color.R) * whiteAmount),
                    (int)(color.G + (255 - color.G) * whiteAmount),
                    (int)(color.B + (255 - color.B) * whiteAmount));
            }

            private void showMagnifier(object sender, EventArgs e)
            {
                if (segments.Count == 0)
                    return;

                MouseEventArgs mouseEvent = e as MouseEventArgs;
                int mouseX = mouseEvent == null ? Width / 2 : mouseEvent.X;
                magnifierMouseX = mouseX;
                isMagnifierVisible = true;
                Invalidate();
                magnifier.Update(segments, mouseX, Width);

                if (!magnifier.Visible)
                {
                    magnifier.Location = PointToScreen(new Point(
                        Math.Max(0, (Width - magnifier.Width) / 2), Height + 5));
                    magnifier.Show(FindForm());
                }
            }

            private void hideMagnifier(object sender, EventArgs e)
            {
                isMagnifierVisible = false;
                Invalidate();
                magnifier.Hide();
            }

            private void progressBar_MouseClick(object sender, MouseEventArgs e)
            {
                if (e.Button != MouseButtons.Left || segments.Count == 0 || Width <= 0)
                    return;

                int index = Math.Min(segments.Count - 1,
                    Math.Max(0, (int)(e.X * (long)segments.Count / Width)));
                if (SegmentClicked != null)
                    SegmentClicked(this, new LabelProgressSegmentClickedEventArgs(index));
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                    magnifier.Dispose();
                base.Dispose(disposing);
            }
        }

        private sealed class ProgressMagnifierPopup : Form
        {
            private List<LabelProgressSegment> segments = new List<LabelProgressSegment>();
            private int sourceMouseX;
            private int sourceWidth;
            private int selectedIndex = -1;

            public ProgressMagnifierPopup()
            {
                FormBorderStyle = FormBorderStyle.None;
                ShowInTaskbar = false;
                StartPosition = FormStartPosition.Manual;
                Size = new Size(420, 52);
                BackColor = SystemColors.Info;
                DoubleBuffered = true;
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                    ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            }

            protected override bool ShowWithoutActivation { get { return true; } }

            protected override CreateParams CreateParams
            {
                get
                {
                    const int WS_EX_NOACTIVATE = 0x08000000;
                    const int WS_EX_TOOLWINDOW = 0x00000080;
                    CreateParams parameters = base.CreateParams;
                    parameters.ExStyle |= WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW;
                    return parameters;
                }
            }

            public void Update(IEnumerable<LabelProgressSegment> values, int mouseX, int progressWidth)
            {
                segments = new List<LabelProgressSegment>(values);
                sourceMouseX = mouseX;
                sourceWidth = Math.Max(1, progressWidth);
                Invalidate();
            }

            public void SetSelectedIndex(int index)
            {
                selectedIndex = index;
                Invalidate();
            }

            private static Color Lighten(Color color)
            {
                const float whiteAmount = 0.6f;
                return Color.FromArgb(
                    color.A,
                    (int)(color.R + (255 - color.R) * whiteAmount),
                    (int)(color.G + (255 - color.G) * whiteAmount),
                    (int)(color.B + (255 - color.B) * whiteAmount));
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.FillRectangle(SystemBrushes.Info, ClientRectangle);
                if (segments.Count == 0)
                    return;

                float visibleSourceWidth = Math.Min(64f, sourceWidth);
                float sourceLeft = sourceMouseX - visibleSourceWidth / 2f;
                sourceLeft = Math.Max(0, Math.Min(sourceWidth - visibleSourceWidth, sourceLeft));
                float sourceRight = sourceLeft + visibleSourceWidth;

                for (int i = 0; i < segments.Count; i++)
                {
                    float segmentLeft = i * sourceWidth / (float)segments.Count;
                    float segmentRight = (i + 1) * sourceWidth / (float)segments.Count;
                    float left = Math.Max(segmentLeft, sourceLeft);
                    float right = Math.Min(segmentRight, sourceRight);
                    if (right > left)
                    {
                        Color color = segments[i].IsCompleted ? segments[i].Color : Lighten(segments[i].Color);
                        using (var brush = new SolidBrush(color))
                        {
                            float outputLeft = 2 + (left - sourceLeft) / visibleSourceWidth * (Width - 4);
                            float outputRight = 2 + (right - sourceLeft) / visibleSourceWidth * (Width - 4);
                            e.Graphics.FillRectangle(brush, outputLeft, 2, outputRight - outputLeft, Height - 4);
                        }
                    }
                }

                if (selectedIndex >= 0 && selectedIndex < segments.Count)
                {
                    float selectedCenter = (selectedIndex + 0.5f) * sourceWidth / segments.Count;
                    if (selectedCenter >= sourceLeft && selectedCenter <= sourceRight)
                    {
                        float outputCenter = 2 + (selectedCenter - sourceLeft) / visibleSourceWidth * (Width - 4);
                        float triangleHeight = Math.Max(4f, (Height - 4) * 0.2f);
                        float triangleBaseY = Height;
                        PointF[] triangle =
                        {
                            new PointF(outputCenter - triangleHeight, triangleBaseY),
                            new PointF(outputCenter + triangleHeight, triangleBaseY),
                            new PointF(outputCenter, triangleBaseY - triangleHeight)
                        };
                        using (var brush = new SolidBrush(Color.FromArgb(210, Color.Black)))
                            e.Graphics.FillPolygon(brush, triangle);
                    }
                }
                e.Graphics.DrawRectangle(SystemPens.InfoText, 0, 0, Width - 1, Height - 1);
            }
        }

        private readonly LabelProgressBar progressBar;
        public event EventHandler<LabelProgressSegmentClickedEventArgs> SegmentClicked;

        public LabelProgressToolStripItem()
            : base(new LabelProgressBar())
        {
            progressBar = (LabelProgressBar)Control;
            progressBar.SegmentClicked += delegate(object sender, LabelProgressSegmentClickedEventArgs e)
            {
                if (SegmentClicked != null)
                    SegmentClicked(this, e);
            };
            AutoSize = false;
            Size = new Size(254, 25);
            Margin = new Padding(3, 0, 2, 0);
        }

        public void SetSegments(IEnumerable<LabelProgressSegment> segments)
        {
            progressBar.SetSegments(segments);
        }

        public void SetSelectedIndex(int index)
        {
            progressBar.SetSelectedIndex(index);
        }
    }
}
