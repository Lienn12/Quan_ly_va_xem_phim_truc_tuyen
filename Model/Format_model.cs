using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class Format_model
    {
        public int FormatID {  get; set; }
        public string FormatName { get; set; }
        public Format_model() { }
        public Format_model(int formatID, string formatName) 
        {
            FormatID = formatID;
            FormatName= formatName;
        }

        public override string ToString()
        {
            return FormatName;
        }
    }
}
