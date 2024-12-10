using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class PhanHoiDanhGia_controller
    {
        private readonly SqlConnection conn;

        public PhanHoiDanhGia_controller()
        {
            conn = new SqlConnection();
        }
    }
}
