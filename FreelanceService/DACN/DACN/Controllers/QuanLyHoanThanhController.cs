using BasicChat;
using DACN.Models.BusinessModel;
using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACN.Controllers
{
    public class QuanLyHoanThanhController : Controller
    {
        DACNDbContext db = new DACNDbContext();
        // GET: QuanLyHoanThanh
        public ActionResult Index()
        {
            DanhSachDHHT danhSach = new DanhSachDHHT();
            var list = db.DonDatHangs.ToList();
            var listDonHang = db.DonHangHoanThanhs.ToList();
            var tempListSP = new List<SanPham>();
            var tempListTK = new List<TaiKhoan>();
            foreach (var item in list)
            {
                var tempSP = db.SanPhams.Where(a => a.MaSP == item.MaSP).FirstOrDefault();
                tempListSP.Add(tempSP);
                var temp = (from s in db.SanPhams
                            join t in db.TaiKhoans on s.MaTaiKhoan equals t.MaTaiKhoan
                            where s.MaSP == item.MaSP
                            select t).FirstOrDefault();
                tempListTK.Add(temp);
            }
            danhSach.listSanPham = tempListSP;
            danhSach.listTaiKhoan = tempListTK;
            danhSach.donDatHangs = list;
            danhSach.donHangHoanThanhs = listDonHang;
            return View(danhSach);
        }
        public ActionResult HoanThanh(int MaDDH)
        {
            var donHang = db.DonDatHangs.Where(a => a.MaDDH == MaDDH).FirstOrDefault();
            donHang.TrangThai = true;
            db.SaveChanges();
            var donHangHT = db.DonHangHoanThanhs.Where(a => a.MaDDH == MaDDH).ToList();
            foreach (var item in donHangHT)
            {
                item.TrangThai = true;
            }
            db.SaveChanges();
            ChatHub.instance.HoanThanhNotification(MaDDH);
            return RedirectToAction("Index","QuanLyHoanThanh");
        }

    }
}