using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class PhanHoiDanhGia_model
    {
        public int ReplyId { get; set; } // ID Phản hồi (nếu có)
        public int ReviewId { get; set; } // ID Đánh giá (liên kết đến Review)
        public string ReplyContent { get; set; } // Nội dung phản hồi

        public PhanHoiDanhGia_model() { }

        public PhanHoiDanhGia_model(int replyId, int reviewId, string replyContent)
        {
            ReplyId = replyId;
            ReviewId = reviewId;
            ReplyContent = replyContent; 
        }
    }
}
