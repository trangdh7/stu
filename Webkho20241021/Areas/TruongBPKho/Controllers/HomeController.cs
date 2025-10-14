using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webkho_20241021.Models;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Webkho_20241021.Areas.TruongBPKho.Controllers
{
    [Area("TruongBPKho")]
    [Authorize(Roles = "Trưởng BP-BP kho")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Tongkho()
        {
            var Tongkho = _context.khotongs.ToList();
            return View(Tongkho);
        }

        public IActionResult Trangchu()
        {
            return View();
        }

        public IActionResult Themthietbi()
        {
            return View();
        }

        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ThemvattuSQL(string[] TenSanpham, string[] MaSanpham, string[] HangSX, string[] NhaCC, int[] SL, string[] DonVi, DateTime?[] NgayBaohanh, DateTime?[] ThoiGianBH)
        {
            int count = TenSanpham.Length;
            var MakhoPrefix = "STU";

            for (int i = 0; i < count; i++)
            {
                if (string.IsNullOrWhiteSpace(TenSanpham[i]) || string.IsNullOrWhiteSpace(MaSanpham[i]) ||
                    SL[i] <= 0 || string.IsNullOrWhiteSpace(DonVi[i]))
                {
                    continue;
                }

                // Kiểm tra nếu tồn tại thiết bị với các thuộc tính giống nhau
                var existingItem = _context.khotongs
                    .FirstOrDefault(k => k.TenSanpham == TenSanpham[i] && k.MaSanpham == MaSanpham[i] && k.HangSX == HangSX[i]);

                if (existingItem != null)
                {
                    // Nếu tồn tại, cộng số lượng
                    existingItem.SL += SL[i];
                    _context.khotongs.Update(existingItem);
                }
                else
                {
                    // Nếu không tồn tại, tạo mã kho mới
                    string Makho;
                    int index = _context.khotongs.Count() + i + 1;

                    do
                    {
                        Makho = $"{MakhoPrefix}{index}";
                        index++;
                    }
                    while (_context.khotongs.Any(k => k.Makho == Makho));

                    // Tạo thiết bị mới
                    var khotongs = new khotongs
                    {
                        TenSanpham = TenSanpham[i],
                        MaSanpham = MaSanpham[i],
                        HangSX = HangSX[i],
                        NhaCC = NhaCC[i],
                        SL = SL[i],
                        DonVi = DonVi[i],
                        NgayBaohanh = NgayBaohanh[i],
                        ThoiGianBH = ThoiGianBH[i],
                        Makho = Makho,
                        NgayNhapkho = DateTime.Now,
                        TrangThai = "Tồn kho"
                    };

                    _context.khotongs.Add(khotongs);
                }
            }

            // Lưu thay đổi vào database
            _context.SaveChanges();

            return RedirectToAction("Tongkho", "Home", new { area = "TruongBPKho" });
        }

        [HttpGet]
        public IActionResult TimKiem(string timkiem)
        {
            var results = _context.khotongs
                .Where(k => k.TenSanpham.Contains(timkiem) || k.MaSanpham.Contains(timkiem))
                .ToList();
            return Json(results);
        }

        [HttpPost]
        public IActionResult ImportSQL(string[] TenSanpham, string[] MaSanpham, string[] HangSX, string[] NhaCC, int[] SL, string[] DonVi, DateTime?[] NgayBaohanh, DateTime?[] ThoiGianBH)
        {
            int count = TenSanpham.Length;
            var MakhoPrefix = "STU";

            for (int i = 0; i < count; i++)
            {
                if (string.IsNullOrWhiteSpace(TenSanpham[i]) || string.IsNullOrWhiteSpace(MaSanpham[i]) ||
                    SL[i] <= 0 || string.IsNullOrWhiteSpace(DonVi[i]))
                {
                    continue; // Bỏ qua mục không hợp lệ
                }

                // Kiểm tra xem đã có mục nào trùng trong database chưa
                var existingItem = _context.khotongs.FirstOrDefault(k =>
                    k.TenSanpham == TenSanpham[i] &&
                    k.MaSanpham == MaSanpham[i] &&
                    k.HangSX == HangSX[i]);

                if (existingItem != null)
                {
                    // Nếu đã tồn tại, cộng thêm số lượng
                    existingItem.SL += SL[i];
                    _context.khotongs.Update(existingItem);
                }
                else
                {
                    // Nếu chưa tồn tại, tạo mã kho mới
                    string Makho;
                    int index = _context.khotongs.Count() + i + 1;

                    do
                    {
                        Makho = $"{MakhoPrefix}{index}";
                        index++;
                    }
                    while (_context.khotongs.Any(k => k.Makho == Makho));

                    // Tạo mục mới
                    var newKhotong = new khotongs
                    {
                        TenSanpham = TenSanpham[i],
                        MaSanpham = MaSanpham[i],
                        HangSX = HangSX[i],
                        NhaCC = NhaCC[i],
                        SL = SL[i],
                        DonVi = DonVi[i],
                        NgayBaohanh = NgayBaohanh[i],
                        ThoiGianBH = ThoiGianBH[i],
                        Makho = Makho,
                        NgayNhapkho = DateTime.Now,
                        TrangThai = "Tồn kho"
                    };

                    _context.khotongs.Add(newKhotong);
                }
            }

            // Lưu thay đổi vào database
            _context.SaveChanges();

            return RedirectToAction("Tongkho", "Home", new { area = "TruongBPKho" });
        }

    }
}
