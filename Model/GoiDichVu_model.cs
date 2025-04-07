using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class GoiDichVu_model
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public string Description { get; set; }
        public GoiDichVu_model() { }
        public GoiDichVu_model(int planId, string planName, decimal price, int durationDays, string description)
        {
            PlanId = planId;
            PlanName = planName;
            Price = price;
            DurationDays = durationDays;
            Description = description;
        }   
    }
}
