using System;
using System.Collections.Generic;
using Quan_ly_thu_vien_phim.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class MessageEmail_controller
    {
        public bool sendEmail(string toEmail,string code)
        {                       
            MailMessage mailMessage = new MailMessage();                      
            String from = "liennguyen4221@gmail.com";
            String pass = "reyghmgqhsnynybq";
            String messageBody = "Your code is " + code;
            mailMessage.To.Add(toEmail);
            mailMessage.From = new MailAddress(from);
            mailMessage.Body = messageBody;
            mailMessage.Subject = "Your Verification Code";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod=SmtpDeliveryMethod.Network;
            smtpClient.Credentials= new NetworkCredential(from, pass);

            try
            {
                smtpClient.Send(mailMessage);
                MessageBox.Show("Code Send Successfully"+code+toEmail);
                return true;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
