using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webkho_20241021.Models;

namespace Webkho_20241021.Areas.NhanvienKythuat.Controllers
{
    [Area("NhanvienKythuat")]
    [Authorize(Roles = "Nhân viên-BP kỹ thuật")]
    public class ThongtincanhanController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        public ThongtincanhanController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Thongtincanhan()
        {
            var KhoNguoidung = _context.khonguoidungs.ToList();
            return View(KhoNguoidung);
        }
    }
}
