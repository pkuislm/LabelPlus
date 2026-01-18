using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelPlus
{
    class ContextMenuOutsideClickFilter : IMessageFilter
    {
        private readonly ContextMenuStrip menu;

        public ContextMenuOutsideClickFilter(ContextMenuStrip menu)
        {
            this.menu = menu;
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (!menu.Visible)
                return false;

            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_RBUTTONDOWN = 0x0204;

            if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_RBUTTONDOWN)
            {
                Point p = Control.MousePosition;

                if (!menu.Bounds.Contains(p))
                {
                    menu.Close();
                    Application.RemoveMessageFilter(this);
                }
            }
            return false;
        }
    }
}
