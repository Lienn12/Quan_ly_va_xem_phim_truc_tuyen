using Quan_ly_thu_vien_phim.View;
using Quan_ly_thu_vien_phim.View.View_Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new View.View_Main.FormMainUser());
            //Application.Run(new View.View_Main.FormLoginSignup());
            Application.Run(new View.FormMain());
        }
    }
}
