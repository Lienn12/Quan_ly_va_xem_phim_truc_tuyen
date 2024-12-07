using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View.View_Container
{
    public partial class PhatVideo : Form
    {
        public PhatVideo()
        {
            InitializeComponent();
        }

        public void setVideo(String vidPath)
        {
            axWindowsMediaPlayer1.URL = vidPath;
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }
    }
}
