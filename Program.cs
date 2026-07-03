using Sunny.UI;
using System;
using System.Windows.Forms;

namespace BookLendingSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new Views.LoginForm());
        }
    }
}