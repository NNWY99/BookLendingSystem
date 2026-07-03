using Sunny.UI;
using System;
using System.Windows.Forms;

namespace BookLendingSystem.Views
{
    public class NoFlickerForm : UIForm
    {
        private const int WM_ERASEBKGND = 0x0014;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ERASEBKGND)
            {
                m.Result = (IntPtr)1;
                return;
            }
            base.WndProc(ref m);
        }
    }
}