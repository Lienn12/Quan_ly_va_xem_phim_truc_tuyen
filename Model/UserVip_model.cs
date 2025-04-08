using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class UserVip_model
    {
        public int SubscriptionId { get; set; }
        public User_model user { get; set; }
        public GoiDichVu_model plan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public UserVip_model() { }  
        public UserVip_model(int subscriptionId, User_model user, GoiDichVu_model plan, DateTime startDate, DateTime endDate, bool isActive)
        {
            SubscriptionId = subscriptionId;
            this.user = user;
            this.plan = plan;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
        }
       
    }
}
