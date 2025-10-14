using System.ComponentModel.DataAnnotations;

namespace Webkho_20241021.Models
{
    public class phieuxuatkho
    {
        public string? MaXuatkho { get; set; }
        public string MaYeucau { get; set; }
        public string? MaDuan { get; set; }
        public string? MaNguoidung { get; set; }
        public DateTime? NgayXuatkho { get; set; }
        public string? TrangThai { get; set; }
    }
}
