using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class User_model
    {
        public int userId { get; set; }
        public string username { get; set; }

        private string password;
        public string email { get; set; }
        public string gender { get; set; }
        public DateTime birth {  get; set; }
        public string verifyCode {  get; set; }
        public User_model()
        {

        }
        public User_model(string usename)
        {
            username = usename;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }
        public User_model(int userId, string username, string email,string password)
        {
            this.userId = userId;
            this.username = username;
            this.email = email;
            this.password = password;    
        }
        public User_model(int userId, string username, string email, string gender, DateTime birth, string verifyCode)
        {
            this.userId = userId;
            this.username = username;
            this.email = email;
            this.gender = gender;
            this.birth = birth;
            this.verifyCode = verifyCode;
        }
        public User_model(int userId, string username, string email, string gender, DateTime birth)
        {
            this.userId = userId;
            this.username = username;
            this.email = email;
            this.gender = gender;
            this.birth = birth;
        }

    }
}
