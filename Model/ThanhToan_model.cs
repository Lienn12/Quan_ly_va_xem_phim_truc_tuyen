using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class ThanhToan_model
    {
        public int OrderId { get; set; }
        public User_model User{ get; set; }
        public GoiDichVu_model Plan{ get; set; }
        public decimal Amount { get; set; }
        public int MethodId { get; set; }
        public string PaymentStatus { get; set; }  // "pending", "completed", "failed"
        public DateTime OrderDate { get; set; }

        public ThanhToan_model() { }
        public ThanhToan_model(int orderId, User_model user, GoiDichVu_model plan, decimal amount, int methodId, string paymentStatus, DateTime orderDate)
        {
            OrderId = orderId;
            User = user;
            Plan = plan;
            Amount = amount;
            MethodId = methodId;
            PaymentStatus = paymentStatus;
            OrderDate = orderDate;
        }
    }
}
