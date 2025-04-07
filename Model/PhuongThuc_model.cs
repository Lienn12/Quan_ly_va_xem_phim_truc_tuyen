using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class PhuongThuc_model
    {
            public int MethodId { get; set; }
            public string MethodName { get; set; }

        public PhuongThuc_model(int methodId, string methodName)
        {
            MethodId = methodId;
            MethodName = methodName;
        }
    }
}
