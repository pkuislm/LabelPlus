/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

#region Using Directives
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
#endregion

namespace LabelPlus
{
    public partial class PicView : UserControl
    {
        public class LabelUserActionEventArgs : EventArgs
        {
            int index;
            float x_percent;
            float y_percent;
            public enum ActionType
            {
                leftClick,
                rightClickAdd,
                rightClickDel,
                mouseIndexChanged,
                labelChanged
            }
            ActionType type;

            public LabelUserActionEventArgs(int n, float X_percent, float Y_percent, ActionType Type)
            {
                index = n;
                x_percent = X_percent;
                y_percent = Y_percent;
                type = Type;
            }

            public int Index { get { return index; } }
            public float X_percent { get { return x_percent; } }
            public float Y_percent { get { return y_percent; } }
            public ActionType Type { get { return type; } }
        }


        //拖拽
        LabelItem _draggingLabelItem = null;
        LabelUndo _draggingLabelUndoStart = new LabelUndo();

        public delegate void UserActionEventHandler(object sender, LabelUserActionEventArgs e);
        /*Label相关*/
        private Color[] colorList;
        private bool _hideLabel = false;
        private bool alwaysShowGroup;
        public bool AlwaysShowGroup
        {
            get { return alwaysShowGroup; }
            set
            {
                alwaysShowGroup = value;
                Invalidate();
            }
        }
        public UserActionEventHandler LabelUserAction;

        public EventHandler ZoomChanged;
        internal void OnZoomChanged()
        {
            if (ZoomChanged != null)
                ZoomChanged(this, new EventArgs());
        }

        /*图像相关*/
        Image _sourceImage;
        float _zoom = 0;
        PointF _viewOffset;

        public Image Image
        {
            set
            {
                if (_sourceImage != null)
                {
                    _sourceImage.Dispose();
                    _sourceImage = value;
                }
                else
                {
                    _sourceImage = value;
                    if (value != null)
                        Zoom = (float)(this.ClientSize.Width) / _sourceImage.Width;//首次运行 设定缩放值
                }
                Invalidate();
            }
        }

        /*共用*/

        //客户端界面坐标->百分比坐标
        public PointF ClientToPercentPoint(PointF poi)
        {
            float x, y;
            float startX = _viewOffset.X * _zoom;
            float startY = _viewOffset.Y * _zoom;

            x = (startX + poi.X) / _zoom / _sourceImage.Size.Width;
            y = (startY + poi.Y) / _zoom / _sourceImage.Size.Height;

            return new PointF(x, y);
        }


        public float Zoom
        {
            set
            {
                float newZoom = Math.Max(0.05f, Math.Min(2.0f, value));
                if (Math.Abs(newZoom - _zoom) < 0.0001f)
                    return;
                _zoom = newZoom;

                Invalidate();
                OnZoomChanged();
            }
            get
            {
                return _zoom;
            }
        }


        public float LabelSize()
        {
           return LabelSize(_zoom);
        }

        public static float LabelSize(float zoom)
        {
            //标签大小
            //return (float)(Math.Min(image.Width, image.Height) * 0.03);
            return 38.0f / zoom;
        }

        public PicView()
        {
            InitializeComponent();

            //拖拽操作相关事件
            this.MouseDown += new MouseEventHandler(this.PicView_Draging_MouseDown);
            this.MouseMove += new MouseEventHandler(this.PicView_Draging_MouseMove);
            this.MouseUp += new MouseEventHandler(this.PicView_Draging_MouseUp);
            //缩放操作相关事件
            this.MouseWheel += new MouseEventHandler(PicView_Zooming_MouseWheel);

            //标签操作相关事件
            this.MouseClick += new MouseEventHandler(this.PicView_Label_MouseClick);
            this.KeyDown += new KeyEventHandler(PicView_label_KeyDown);
            this.KeyUp += new KeyEventHandler(PicView_Label_KeyUp);

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            
            //提示标签
            toolTip.UseFading = false;
            toolTip.UseAnimation = false;
            toolTip.BackColor = Color.Black;
            toolTip.ForeColor = Color.White;

            AlwaysShowGroup = false;
        }



        #region 绘图操作
        /**
         * imageOriginal为原始图片
         * imageZoomed为缩放后图片缓存
         * image为将整幅图片缩放、标号后的缓存，
         * MakeImage函数对其进行绘制
         * PicView_Paint函数为重画事件，将image中的一部分截取出来，绘制到用户界面上
         */
        public bool LoadImage(string path)
        {
            try
            {
                Image = Image.FromFile(path);
                return true;
            }
            catch { return false; }
        }

        public bool EnableMakeImage { get; set; }

        // 获取当前的视图变换矩阵
        private Matrix GetViewMatrix()
        {
            Matrix m = new Matrix();
            // 顺序很重要：先缩放，再平移 (或者先平移到原点，再缩放，再平移回去，这里采用 ViewOffset 直接记录屏幕偏移)
            m.Translate(_viewOffset.X, _viewOffset.Y);
            m.Scale(_zoom, _zoom);
            return m;
        }

        // 坐标转换：屏幕坐标 -> 图片像素坐标
        private PointF ScreenToImage(PointF screenPoint)
        {
            // Inverse Matrix calculation manually for performance
            // ImgX = (ScreenX - OffsetX) / Zoom
            return new PointF(
                (screenPoint.X - _viewOffset.X) / _zoom,
                (screenPoint.Y - _viewOffset.Y) / _zoom
            );
        }

        // 坐标转换：图片像素坐标 -> 屏幕(控件)坐标
        private PointF ImageToScreen(PointF imgPoint)
        {
            // 公式：Screen = Image * Zoom + Offset
            return new PointF(
                imgPoint.X * _zoom + _viewOffset.X,
                imgPoint.Y * _zoom + _viewOffset.Y
            );
        }

        private void PicView_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.Clear(BackColor);

                if (_sourceImage == null) return;

                Graphics g = e.Graphics;

                using (Matrix transform = GetViewMatrix())
                {
                    g.Transform = transform;
                    g.SmoothingMode = SmoothingMode.HighSpeed;

                    if (_isDraggingImage || _isDraggingLabel)
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    }
                    else
                    {
                        g.InterpolationMode = _zoom > 1.0f ? InterpolationMode.NearestNeighbor : InterpolationMode.HighQualityBicubic;
                    }

                    g.DrawImage(_sourceImage, 0, 0, _sourceImage.Width, _sourceImage.Height);

                    if (!_hideLabel && _labels != null)
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                        DrawLabels(g);
                    }
                }
            }
            catch { }
        }

        // 将标签绘制逻辑提取出来
        private void DrawLabels(Graphics g)
        {
            int labelAlpha = 200;
            float baseLen = LabelSize();
            float labelFontSize = baseLen / 2.1f;

            using (Font myFont = new Font("Arial", labelFontSize, FontStyle.Bold))
            using (StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (Pen whitePen = new Pen(Color.FromArgb(labelAlpha, Color.White), baseLen * 0.1f))
            {
                var colorBrushes = colorList.Select(c => new SolidBrush(Color.FromArgb(labelAlpha, c))).ToArray();

                try
                {
                    for (int i = 0; i < _labels.Count; i++)
                    {
                        var geo = LabelGeometry.CalcLabelGeometry(_labels[i], _sourceImage, _zoom);
                        var brush = colorBrushes[_labels[i].Category - 1];

                        // 圆
                        g.DrawEllipse(whitePen, geo.CircleRect);
                        g.FillEllipse(brush, geo.CircleRect);

                        // 数字
                        g.DrawString((i + 1).ToString(), myFont, Brushes.White, geo.CircleRect, sf);

                        // 三角
                        g.FillPolygon(brush, geo.Triangle);
                    }
                }
                finally
                {
                    foreach (var brush in colorBrushes)
                        brush?.Dispose();
                }
            }
        }

        private void PicView_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void PicView_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.AllPaintingInWmPaint, true);
        }

        #endregion

        #region 拖拽操作
        bool _isDraggingImage = false;
        Rectangle _lastMouseDownArea;
        Point _lastMousePos;

        //标签拖拽
        bool _isDraggingLabel = false;
        int _draggingLabelIndex = -1;
        PointF _draggingLabelAnchorOffset;
        void PicView_Draging_MouseDown(object sender, MouseEventArgs e)
        {
            _lastMouseDownArea = new Rectangle(e.Location, new Size(5, 5));
            if (e.Button == MouseButtons.Left)
            {
                PointF imgPoint = ScreenToImage(e.Location);
                int clickedLabelIndex = GetLabelIndexAt(imgPoint);

                if (clickedLabelIndex != -1)
                {
                    _isDraggingLabel = true;
                    _draggingLabelIndex = clickedLabelIndex;
                    _draggingLabelItem = _labels[clickedLabelIndex];


                    // 记录 Undo 起点
                    _draggingLabelUndoStart = new LabelUndo()
                    {
                        Index = clickedLabelIndex,
                        Location = new Location() { X_percent = _draggingLabelItem.X_percent, Y_percent = _draggingLabelItem.Y_percent },
                        Category = _draggingLabelItem.Category,
                        Text = _draggingLabelItem.Text
                    };

                    var geo = LabelGeometry.CalcLabelGeometry(_draggingLabelItem, _sourceImage, _zoom);
                    float x_percent = (imgPoint.X - geo.Anchor.X) / _sourceImage.Width;
                    float y_percent = (imgPoint.Y - geo.Anchor.Y) / _sourceImage.Height;
                    _draggingLabelAnchorOffset = new PointF(x_percent, y_percent);
                }
                else
                {
                    _isDraggingImage = true;
                    _lastMousePos = e.Location;
                }
            }
        }

        void PicView_Draging_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDraggingLabel && _draggingLabelItem != null)
            {
                PointF imgPoint = ScreenToImage(e.Location);
                float newXPercent = Clamp(imgPoint.X / _sourceImage.Width - _draggingLabelAnchorOffset.X, 0f, 1f);
                float newYPercent = Clamp(imgPoint.Y / _sourceImage.Height - _draggingLabelAnchorOffset.Y, 0f, 1f);

                MoveLabelCommand(newXPercent, newYPercent);
                _isDraggingLabel = false;
                _draggingLabelIndex = -1;
            }
            else
            {
                _isDraggingImage = false;
            }
            Invalidate();
        }

        void PicView_Draging_MouseMove(object sender, MouseEventArgs e)
        {
            if(_sourceImage==null) return;

            if (_isDraggingLabel && _draggingLabelIndex >= 0)
            {
                PointF imgPoint = ScreenToImage(e.Location);
                float newXPercent = imgPoint.X / _sourceImage.Width - _draggingLabelAnchorOffset.X;
                float newYPercent = imgPoint.Y / _sourceImage.Height - _draggingLabelAnchorOffset.Y;

                _draggingLabelItem.X_percent = Clamp(newXPercent, 0f, 1f);
                _draggingLabelItem.Y_percent = Clamp(newYPercent, 0f, 1f);

                Invalidate();
                return;
            }

            if (_isDraggingImage == false) return;

            float deltaX = e.X - _lastMousePos.X;
            float deltaY = e.Y - _lastMousePos.Y;

            _viewOffset.X += deltaX;
            _viewOffset.Y += deltaY;

            _lastMousePos = e.Location;
            Invalidate();

        }
        #endregion

        #region 缩放、平移操作
        void PicView_Zooming_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_sourceImage == null) return;

            // 修饰键平移保持不变
            if (Control.ModifierKeys == Keys.Control)
            {
                _viewOffset.X -= e.Delta / _zoom;
                Invalidate();
                return;
            }
            if (Control.ModifierKeys == Keys.Alt)
            {
                _viewOffset.Y -= e.Delta / _zoom;
                Invalidate();
                return;
            }

            float oldZoom = _zoom;
            float newZoom = _zoom + (e.Delta > 0 ? 0.1f : -0.1f);
            newZoom = Math.Max(0.05f, Math.Min(3.0f, newZoom));

            if (Math.Abs(newZoom - oldZoom) < 0.0001f)
                return;

            _zoom = newZoom;

            float mouseX_rel = e.X - _viewOffset.X;
            float mouseY_rel = e.Y - _viewOffset.Y;

            // 2. 计算缩放比例变化
            float ratio = _zoom / oldZoom;

            // 3. 计算新的 ViewOffset
            _viewOffset.X = e.X - mouseX_rel * ratio;
            _viewOffset.Y = e.Y - mouseY_rel * ratio;

            Invalidate();
            OnZoomChanged();
        }

        #endregion

        #region Label操作
        struct LabelGeometry
        {
            public PointF Anchor;       // 底部锚点（image 坐标）
            public PointF Top;
            public PointF CircleCenter; // 圆心（image 坐标）
            public RectangleF CircleRect;
            public PointF[] Triangle;   // 三角形三个点（image 坐标）

            public static LabelGeometry CalcLabelGeometry(float x, float y, float zoom)
            {
                float baseLen = LabelSize(zoom);

                float r = baseLen * 0.5f;
                float triH = baseLen * 0.25f;
                float triW = baseLen * 0.4f;
                float gap = baseLen * 0.1f;

                float ax = x;
                float ay = y;

                PointF anchor = new PointF(ax, ay);

                PointF circleCenter = new PointF(
                    ax,
                    ay - triH - gap - r
                );

                RectangleF circleRect = new RectangleF(
                    circleCenter.X - r,
                    circleCenter.Y - r,
                    r * 2,
                    r * 2
                );

                PointF[] triangle =
                {
                    new PointF(ax, ay),                         // 底点
                    new PointF(ax - triW / 2, ay - triH),       // 左
                    new PointF(ax + triW / 2, ay - triH)        // 右
                };

                return new LabelGeometry
                {
                    Anchor = anchor,
                    CircleCenter = circleCenter,
                    CircleRect = circleRect,
                    Triangle = triangle,
                    Top = new PointF(ax, ay - triH - gap - r * 2)
                };
            }

            public static LabelGeometry CalcLabelGeometry(LabelItem label, Image img, float zoom)
            {
                return CalcLabelGeometry(label.X_percent * img.Width, label.Y_percent * img.Height, zoom);
            }

            public static LabelGeometry CalcLabelGeometry(LabelItem label, Image img, float zoom,float offsetX, float offsetY)
            {
                return CalcLabelGeometry(label.X_percent * img.Width - offsetX, label.Y_percent * img.Height - offsetY, zoom);
            }

            public bool HitTest(PointF realP)
            {
                float dx = realP.X - CircleCenter.X;
                float dy = realP.Y - CircleCenter.Y;
                if (dx * dx + dy * dy <= CircleRect.Width * CircleRect.Width / 4)
                    return true;
                return false;
            }
        }

        List<LabelItem> _labels = new List<LabelItem>();
        string[] groupString;
        public void ClearLabels()
        {
            _labels = new List<LabelItem>();
        }

        public void SetLabels(List<LabelItem> items, string[] groupString, Color[] colors)
        {
            this._labels = items;
            this.colorList = colors;
            this.groupString = groupString;
            Invalidate();
        }

        // 使标签可见 Input模式使用
        /// <summary>
        /// 将指定索引的标签居中显示在视图中
        /// </summary>
        /// <param name="index">标签的索引</param>
        /// <param name="targetZoom">可选：跳转时的缩放比例。如果为null，则保持当前缩放比例</param>
        public void SetLabelVisual(int index, float? targetZoom = null)
        {
            // 1. 基础校验
            if (_sourceImage == null || _labels == null) return;
            if (index < 0 || index >= _labels.Count) return;

            var label = _labels[index];

            // 2. 确定缩放比例
            // 如果传入了新的缩放比例，就更新；否则保持当前比例
            if (targetZoom.HasValue)
            {
                _zoom = targetZoom.Value;
                // 这里可以加上最大/最小缩放限制逻辑，防止缩放过头
                if (_zoom < 0.1f) _zoom = 0.1f;
                if (_zoom > 10f) _zoom = 10f;
            }

            // 3. 计算控件中心坐标
            float screenCenterX = this.Width / 2.0f;
            float screenCenterY = this.Height / 2.0f;

            // 4. 计算新的偏移量 (核心公式)
            // NewOffset = ScreenCenter - (ImagePoint * Zoom)
            float newOffsetX = screenCenterX - (label.X_percent * _sourceImage.Width * _zoom);
            float newOffsetY = screenCenterY - (label.Y_percent * _sourceImage.Height * _zoom);

            _viewOffset = new PointF(newOffsetX, newOffsetY);
            Invalidate();
        }

        private int GetLabelIndexAt(PointF imgPoint)
        {
            if (_labels == null) return -1;
            for (int i = 0; i < _labels.Count; i++)
            {
                var geo = LabelGeometry.CalcLabelGeometry(_labels[i], _sourceImage, _zoom);
                if (geo.HitTest(imgPoint)) return i;
            }
            return -1;
        }

        void PicView_Label_MouseClick(object sender, MouseEventArgs e)
        {
            if (_sourceImage == null) return;
            if (_isDraggingLabel == true) return;

            PointF imgPoint = ScreenToImage(e.Location);
            int clickedLabelIndex = GetLabelIndexAt(imgPoint);

            float x_percent = imgPoint.X / _sourceImage.Width;
            float y_percent = imgPoint.Y / _sourceImage.Height;
            if (x_percent > 1.0f || y_percent > 1.0f || x_percent < 0 || y_percent < 0) return;   //忽略超出边界的点击

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (!_lastMouseDownArea.Contains(e.Location)) return;
                if (LabelUserAction != null)
                    LabelUserAction(this, new LabelUserActionEventArgs(clickedLabelIndex, x_percent, y_percent, LabelUserActionEventArgs.ActionType.leftClick));
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //右键
                if(clickedLabelIndex != -1)
                {
                    if (LabelUserAction != null)
                        LabelUserAction(this, new LabelUserActionEventArgs(clickedLabelIndex, x_percent, y_percent, LabelUserActionEventArgs.ActionType.rightClickDel));
                }
                else
                {
                    if (LabelUserAction != null)
                        LabelUserAction(this, new LabelUserActionEventArgs(clickedLabelIndex, x_percent, y_percent, LabelUserActionEventArgs.ActionType.rightClickAdd));
                }
            }
        }

        private void MoveLabelCommand(float x_percent, float y_percent)
        {
            LabelUndo label = new LabelUndo()
            {
                Index = _draggingLabelUndoStart.Index,
                Location = new Location() { X_percent = x_percent, Y_percent = y_percent },

            };
            label.LocationPrevious = new LocationPrevious() { X_percent = _draggingLabelUndoStart.Location.X_percent, Y_percent = _draggingLabelUndoStart.Location.Y_percent };
            MoveLabelCommand moveLabelCommand = new MoveLabelCommand(MoveLabel, UndoMoveLabel, label);
            UndoRedoManager.LabelCommandPool.Register(moveLabelCommand);
            moveLabelCommand.Excute();
        }

        private void MoveLabel(LabelUndo label)
        {
            _draggingLabelItem.X_percent = label.Location.X_percent;
            _draggingLabelItem.Y_percent = label.Location.Y_percent;
            //Console.WriteLine(dragLabel.X_percent + "    " + dragLabel.Y_percent);
            //dragLabel = null;
            if (LabelUserAction != null)
                LabelUserAction(this, new LabelUserActionEventArgs(label.Index, label.Location.X_percent, label.Location.Y_percent, LabelUserActionEventArgs.ActionType.labelChanged));
        }

        private void UndoMoveLabel(LabelUndo label)
        {
            _draggingLabelItem.X_percent = label.LocationPrevious.X_percent;
            _draggingLabelItem.Y_percent = label.LocationPrevious.Y_percent;
            //Console.WriteLine(dragLabel.X_percent + "    " + dragLabel.Y_percent);
            //dragLabel = null;
            if (LabelUserAction != null)
                LabelUserAction(this, new LabelUserActionEventArgs(label.Index, label.LocationPrevious.X_percent, label.LocationPrevious.Y_percent, LabelUserActionEventArgs.ActionType.labelChanged));
        }

        bool tooltop_showing = false;
        int _lastHoverIndex = -1;
        private void PicView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                PointF imgPoint = ScreenToImage(e.Location);
                int hoverIndex = GetLabelIndexAt(imgPoint);

                if (hoverIndex != _lastHoverIndex)
                {
                    if (LabelUserAction != null)
                        LabelUserAction(this, new LabelUserActionEventArgs(hoverIndex, e.X, e.Y, LabelUserActionEventArgs.ActionType.mouseIndexChanged));
                }
                _lastHoverIndex = hoverIndex;


                //提示文本
                if (hoverIndex != -1 && !_isDraggingLabel)
                {
                    if (!tooltop_showing)
                    {
                        var geo = LabelGeometry.CalcLabelGeometry(_labels[hoverIndex], _sourceImage, _zoom);
                        var locationF = ImageToScreen(geo.Top);
                        var location = new Point((int)locationF.X, (int)locationF.Y);
                        location.X += this.Cursor.Size.Width / 2;
                        location.Y -= this.Cursor.Size.Height / 2;
                        toolTip.Show(_labels[hoverIndex].Text, this, location);
                        tooltop_showing = true;
                    }
                }
                else
                {
                    toolTip.Hide(this);
                    tooltop_showing = false;
                }
            }
            catch { }
        }

        private void PicView_label_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V)
            {
                _hideLabel = true;
                Invalidate();
            }
            else if (e.KeyCode == Keys.C)
            {
                Invalidate();
            }
        }

        private void PicView_Label_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V)
            {
                _hideLabel = false;
                Invalidate();
            }
            else if (e.KeyCode == Keys.C)
            {
                Invalidate();
            }
        }


        #endregion
        static float Clamp(float v, float min, float max)
        {
            if (v < min) return min;
            if (v > max) return max;
            return v;
        }
        private void PicView_MouseUp(object sender, MouseEventArgs e)
        {




        }

    }
}
