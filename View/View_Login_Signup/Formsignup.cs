using Quan_ly_thu_vien_phim.View.View_Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Quan_ly_thu_vien_phim.View.View_Login_Signup
{
    public partial class Formsignup : Form
    {
        private View_Main.FormLoginSignup frmLoginSignup;
        public Formsignup(FormLoginSignup frmLoginSignup)
        {
            InitializeComponent();
            this.frmLoginSignup = frmLoginSignup;
        }

        private void lbLogin_Click(object sender, EventArgs e)
        {
            frmLoginSignup.OpenChidForm(new View.View_Login_Signup.FormLogin(frmLoginSignup));
            Close();
        }
    }
}
