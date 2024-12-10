using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public  class Admin_model
    {
        public string Username {  get; set; }
        private string Password;
       
        public void setPassword(string password)
        {
            this.Password = password;
        }
        public Admin_model() { }
        public Admin_model(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
}
