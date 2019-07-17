using DACN.Models.BusinessModel;
using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACN.Controllers
{
    public class QuanLyChuyenTienController : Controller
    {
        DACNDbContext db = new DACNDbContext();
        // GET: QuanLyChuyenTien
        public ActionResult Index()
        {
            var test = Session["mataikhoan"];
            if (test != null)
            {
                DanhSachChuyenTien list = new DanhSachChuyenTien();
                var tempListSP = new List<SanPham>();
                var tempListTK = new List<TaiKhoan>();
                list.listDonDatHang = db.DonDatHangs.Where(a => a.TrangThai == true).ToList();
                foreach (var item in list.listDonDatHang)
                {
                    var tempSP = db.SanPhams.Where(a => a.MaSP == item.MaSP).FirstOrDefault();
                    tempListSP.Add(tempSP);
                    var temp = (from s in db.SanPhams
                                join t in db.TaiKhoans on s.MaTaiKhoan equals t.MaTaiKhoan
                                where s.MaSP == item.MaSP
                                select t).FirstOrDefault();
                    tempListTK.Add(temp);
                }
                list.listSanPham = tempListSP;
                list.listTaiKhoan = tempListTK;
                return View(list);
            }
            return RedirectToAction("DangKy", "Home");
        }
        public ActionResult ChuyenTien(int MaDDH)
        {
            var donHang = db.DonDatHangs.FirstOrDefault(a => a.MaDDH == MaDDH);
            if (donHang !=null && donHang.NgayNhanTien ==null && donHang.SoTienNhan == null)
            {
                donHang.NgayNhanTien = DateTime.Now;
                donHang.SoTienNhan = (double.Parse(donHang.GiaDDH.Trim().ToString()) - (double.Parse(donHang.GiaDDH.Trim().ToString()) * 0.1)).ToString();
                db.SaveChanges();
            }
            return RedirectToAction("Index","QuanLyChuyenTien");
        }
    }
}