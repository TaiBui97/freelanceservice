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
    public class QuanLyTranhChapController : Controller
    {
        DACNDbContext db = new DACNDbContext();
        // GET: QuanLyTranhChap
        public ActionResult Index()
        {
            var danhsanh = new DanhSachTranhChap();
            var tranhchap = db.TranhChaps.ToList();
            var donhang = new List<DonDatHang>();
            var buyer = new List<TaiKhoan>();
            var seller = new List<TaiKhoan>();
            var listSP = new List<SanPham>();
            foreach (var item in tranhchap.GroupBy(a=>a.MaDDH).Select(a=>a.FirstOrDefault()).ToList())
            {
                var temp = db.DonDatHangs.Where(a => a.MaDDH == item.MaDDH).FirstOrDefault();
                if (temp != null)
                {
                    donhang.Add(temp);
                    var tempSP = db.SanPhams.Where(a => a.MaSP == temp.MaSP).FirstOrDefault();
                    var tempBuyer = db.TaiKhoans.Where(a => a.MaTaiKhoan == temp.MaTaiKhoan).FirstOrDefault();
                    var tempSeller = db.TaiKhoans.Where(a => a.MaTaiKhoan == tempSP.MaTaiKhoan).FirstOrDefault();
                    buyer.Add(tempBuyer);
                    seller.Add(tempSeller);
                    listSP.Add(tempSP);
                }
            }
            danhsanh.donDatHangs = donhang;
            danhsanh.tranhChaps = tranhchap;
            danhsanh.Buyer = buyer;
            danhsanh.Seller = seller;
            danhsanh.listSanPham = listSP;
            return View(danhsanh);
        }
        public ActionResult ChiTiet(int maddh)
        {
            var test = Session["mataikhoan"];
            if (test != null)
            {
                if (maddh > 0)
                {
                    var tempmatk = Session["mataikhoan"];
                    ViewBag.matk = Session["mataikhoan"];
                    ViewBag.tentk = Session["tentaikhoan"];
                    var checkMa = int.Parse(tempmatk.ToString());
                    if (checkMa == 1)
                    {
                        var madh = db.DonDatHangs.Where(a => a.MaDDH == maddh && a.Delete != true).FirstOrDefault();
                        var chitietsanpham = db.CTDDHs.Where(a => a.MaDDH == maddh).ToList();
                        var temp = new List<int>();

                        foreach (var item in chitietsanpham)
                        {
                            var chitiettemp = db.CTSP.Where(a => a.MaCTSP == item.MaCTSP && a.TrangThai == true && a.Delete == false).FirstOrDefault();
                            if (chitiettemp != null)
                                temp.Add(int.Parse(chitiettemp.MaGigMetaData.ToString()));

                        }

                        List<GigMetaData> listextra = new List<GigMetaData>();
                        if (temp.Count > 0)
                        {
                            foreach (var item in temp)
                            {
                                var tempgig = db.GigMetaDatas.Where(a => a.MaGigMetaData == item).FirstOrDefault();
                                listextra.Add(tempgig);
                            }
                        }

                        var sanpham = db.SanPhams.Where(a => a.MaSP == madh.MaSP && a.Delete == false).FirstOrDefault();
                        var tkminhchung = db.TaiKhoans.Where(a => a.MaTaiKhoan == sanpham.MaTaiKhoan).FirstOrDefault();
                        var listgia = db.CTSP.Where(a => a.MaSP == madh.MaSP && a.TrangThai == true && a.Delete == false).ToList();
                        var hinh = db.HinhAnhs.Where(a => a.MaSP == sanpham.MaSP && a.Delete == false).FirstOrDefault();
                        var listHoanhThanh = db.DonHangHoanThanhs.Where(a => a.MaDDH == madh.MaDDH).ToList();
                        var listTienDo = db.TienDoCongViecs.Where(a => a.MaDDH == maddh).ToList();
                        var temp2 = db.CTSP.Where(a => a.MaSP == sanpham.MaSP && a.TrangThai == false && a.MaGigMetaData != null && a.Delete == false).Select(a => a.MaGigMetaData).ToList();
                        List<GigMetaData> listgigbd = new List<GigMetaData>();
                        var mota = db.ThemMoTaDDHs.Where(a => a.MaDDH == maddh).ToList();
                        foreach (var item in temp2)
                        {
                            var tempgigbd = db.GigMetaDatas.Where(a => a.MaGigMetaData == item).FirstOrDefault();
                            listgigbd.Add(tempgigbd);
                        }
                        List<HinhAnh> minhchung = new List<HinhAnh>();
                        foreach (var item in listHoanhThanh)
                        {
                            var tempHinh = db.HinhAnhs.Where(a => a.MaDHHT == item.MaDHHT).ToList();
                            minhchung.AddRange(tempHinh);
                        }
                        var tkBuyer = db.TaiKhoans.Where(a => a.MaTaiKhoan == madh.MaTaiKhoan).FirstOrDefault();
                        List<HinhAnh> tiendo = new List<HinhAnh>();
                        foreach (var item in listTienDo)
                        {
                            var tempHinh = db.HinhAnhs.Where(a => a.MaTienDo == item.MaTienDo).ToList();
                            tiendo.AddRange(tempHinh);
                        }
                        ChiTietTranhChap order = new ChiTietTranhChap();
                        order.tkBuyer = tkBuyer;
                        order.donDatHang = madh;
                        order.Baidang = sanpham;
                        order.tkMinhChung = tkminhchung;
                        order.listDHHT = listHoanhThanh;
                        order.listgigbd = listgigbd;
                        order.listHinhMinhChung = minhchung;
                        order.listgigkh = listextra;
                        order.listGia = listgia;
                        order.listTienDo = listTienDo;
                        order.listHinhTienDo = tiendo;
                        order.listMoTa = mota;
                        return View(order);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("DangKy", "Home");
        }
       
    }
}