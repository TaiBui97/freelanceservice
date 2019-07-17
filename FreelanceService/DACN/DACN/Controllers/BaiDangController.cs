//using DACN.Hubs;
using DACN.Models.BusinessModel;
using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using BasicChat;

namespace DACN.Controllers
{
    public class BaiDangController : Controller
    {
        DACNDbContext db = new DACNDbContext();
        private static readonly string[] VietNamChar = new string[]
            {
        "aAeEoOuUiIdDyY",
        "áàạảãâấầậẩẫăắằặẳẵ",
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        "éèẹẻẽêếềệểễ",
        "ÉÈẸẺẼÊẾỀỆỂỄ",
        "óòọỏõôốồộổỗơớờợởỡ",
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        "úùụủũưứừựửữ",
        "ÚÙỤỦŨƯỨỪỰỬỮ",
        "íìịỉĩ",
        "ÍÌỊỈĨ",
        "đ",
        "Đ",
        "ýỳỵỷỹ",
        "ÝỲỴỶỸ"
            };
        public static string LocDau(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }
        [HttpPost]
        public JsonResult check(string TenDM)
        {
            BaiDang bd = new BaiDang();
            if (TenDM != null)
            {
                var ctdm = (from d in db.DanhMucs
                            join c in db.CTDMs on d.MaDM equals c.MaDM
                            where d.MaDM == c.MaDM && d.TenDM == TenDM
                            select c).ToList();

                bd.listctdm = ctdm;
            }
            BaiDang.dsbaidang.Add(bd);
            return Json(bd.listctdm, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult check2(string TenCTDM)
        {
            BaiDang bd = new BaiDang();
            if (TenCTDM != null)
            {


                var listmeta = (from c in db.CTDMs
                                join m in db.MetaDatas on c.MaCTDM equals m.MaCTDM
                                join l in db.LoaiMetaData on m.MaLoaiMetaData equals l.MaLoaiMetaData
                                where c.MaCTDM == m.MaCTDM && m.MaLoaiMetaData == l.MaLoaiMetaData && c.TenCTDM == TenCTDM
                                select m).ToList();
                var loaimeta = (from c in db.CTDMs
                                join m in db.MetaDatas on c.MaCTDM equals m.MaCTDM
                                join l in db.LoaiMetaData on m.MaLoaiMetaData equals l.MaLoaiMetaData
                                where c.MaCTDM == m.MaCTDM && m.MaLoaiMetaData == l.MaLoaiMetaData && c.TenCTDM == TenCTDM
                                select l).FirstOrDefault();
                var ma = db.CTDMs.Where(a => a.TenCTDM == TenCTDM).Select(a => a.MaCTDM).FirstOrDefault();

                var gig = db.GigMetaDatas.Where(a => a.MaCTDM == ma || a.MaCTDM == null).ToList();
                bd.GigMetadata = gig;
                bd.Metadata = listmeta;
                bd.LoaiMetadata = loaimeta;
            }
            BaiDang.dsbaidang.Add(bd);
            return Json(new { First = bd.GigMetadata, Second = bd.Metadata, Third = bd.LoaiMetadata }, JsonRequestBehavior.AllowGet);
        }
        // GET: BaiDang
        [HttpGet]
        public ActionResult Service()
        {
            var test = Session["mataikhoan"];
            if (test != null)
            {
                BaiDang bd = new BaiDang();
                var dm = (from d in db.DanhMucs select d).Distinct().ToList();
                bd.danhmuc = dm;
                BaiDang.dsbaidang.Add(bd);
                return View(bd);
            }
            return RedirectToAction("DangKy", "Home");

        }
        public bool checkTitle(string title)
        {
            var temp = db.SanPhams.Where(a => a.TenSP == title && a.Delete != true).FirstOrDefault();
            if (temp == null)
                return true;
            return false;
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Service(FormCollection collection, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            var test = Session["mataikhoan"];
            BaiDang bd = new BaiDang();
            List<BaiDang> listbd = new List<BaiDang>();
            if (test != null)
            {
                int masp = 0, matk = int.Parse(test.ToString());
                if (fileUpload.FirstOrDefault() == null)
                {
                    ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                    return this.Service();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var tieude = collection.Get("TieuDe");
                        if (checkTitle(tieude))
                        {
                            var ctdm = collection.Get("chitietDM");
                            var mota = collection.Get("MoTa");
                            var mota2 = collection.Get("MoTa2");
                            var ngaygiao = collection.Get("NgayGiao");
                            var slchinhsua = collection.Get("ChinhSua");
                            var meta = collection.Get("checkMetaData");
                            var gigmeta = collection.Get("checkGigMetadata");
                            var gigmeta2 = collection.Get("checkGigMetadata2");
                            var tien2 = collection.Get("TienMetaData2");
                            var tien = collection.Get("Tien");
                            var sl2 = collection.Get("SlMetaData2");
                            List<string> listStr = new List<string>();
                            List<string> listStr2 = new List<string>();
                            if (gigmeta != null)
                            {
                                var splitStr2 = Regex.Split(gigmeta, ",");
                                listStr2.AddRange(splitStr2);



                            }
                            if (meta != null)
                            {
                                var splitStr = Regex.Split(meta, ",");
                                listStr.AddRange(splitStr);


                            }
                            SanPham sanpham = new SanPham();
                            sanpham.MaTaiKhoan = matk;
                            matk = int.Parse(sanpham.MaTaiKhoan.ToString());
                            sanpham.NgayTao = DateTime.Now;
                            var tempTien = Regex.Split(tien, ",");
                            sanpham.GiaSP = SplitTien(tempTien);
                            sanpham.TenSP = tieude;
                            sanpham.ThoiGianGiao = int.Parse(ngaygiao.ToString());
                            sanpham.MotaSP = mota;
                            sanpham.HangDoi = 0;
                            sanpham.MotaSP2 = mota2;
                            sanpham.SoLanSua = int.Parse(slchinhsua.ToString());
                            sanpham.MaCTDM = db.CTDMs.Where(a => a.TenCTDM == ctdm).Select(a => a.MaCTDM).FirstOrDefault();
                            sanpham.Delete = false;
                            db.SanPhams.Add(sanpham);
                            db.SaveChanges();
                            masp = db.SanPhams.Where(a => a.TenSP == sanpham.TenSP && a.Delete == false).Select(a => a.MaSP).FirstOrDefault();
                            sanpham.MaSP = masp;
                            bd.Sanpham = sanpham;
                            BaiDang.dsbaidang.Add(bd);
                            List<MetaData> listmeta = new List<MetaData>();
                            var templist = (from a in BaiDang.dsbaidang where a.Metadata != null select a.Metadata).FirstOrDefault();
                            listmeta.AddRange(templist);
                            List<GigMetaData> listgigmeta = new List<GigMetaData>();
                            var templist2 = from a in BaiDang.dsbaidang where a.GigMetadata != null select a.GigMetadata;
                            listgigmeta.AddRange(templist2.FirstOrDefault());
                            CTSP chitiet = new CTSP();
                            foreach (var item2 in listStr)
                            {
                                var checkMeta = listmeta.FirstOrDefault(a => a.TenMetaData == item2);
                                if (checkMeta != null)
                                {
                                    chitiet.MaMetaData = checkMeta.MaMetaData;
                                    chitiet.TrangThai = false;
                                    chitiet.MaSP = masp;
                                    db.CTSP.Add(chitiet);
                                    db.SaveChanges();
                                }
                            }
                            CTSP chitiet2 = new CTSP();
                            foreach (var item in listStr2)
                            {
                                var checkGigMeta = listgigmeta.FirstOrDefault(a => a.TenGigMetaData == item);
                                if (checkGigMeta != null)
                                {
                                    chitiet2.MaGigMetaData = checkGigMeta.MaGigMetaData;
                                    chitiet2.TrangThai = false;
                                    chitiet2.MaSP = masp;
                                    chitiet2.Delete = false;
                                    db.CTSP.Add(chitiet2);
                                    db.SaveChanges();
                                }
                            }
                            splitString(gigmeta2, tien2, sl2, chitiet2, masp);
                            foreach (var file in fileUpload)
                            {
                                var filename = Path.GetFileName(file.FileName);
                                FileInfo fileInfo = new FileInfo(filename);
                                string newFileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                                var path = Path.Combine(Server.MapPath("~/assets/images/"), newFileName);
                                file.SaveAs(path);
                                var hinh = new HinhAnh();
                                hinh.TenHinhAnh = newFileName;
                                hinh.MaSP = masp;
                                hinh.Delete = false;
                                //Luu vao CSDL
                                db.HinhAnhs.Add(hinh);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            ViewData["TitleError"] = "Tên bài đăng đã có người đặt vui lòng đặt tên khác";
                            return this.Service();
                        }

                    }

                }

                return RedirectToAction("ServiceAccount", "BaiDang");
            }
            return RedirectToAction("DangKy", "Home");

        }
        [HttpGet]
        public ActionResult DeleteService(int id)
        {


            var DDH = db.DonDatHangs.Where(a => a.MaSP == id).FirstOrDefault();
            if (checkDDH(DDH))
            {
                SanPham sp = db.SanPhams.FirstOrDefault(a => a.MaSP == id);
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                var listcTSP = db.CTSP.Where(a => a.MaSP == id).ToList();
                foreach (var item in listcTSP)
                {
                    item.Delete = true;
                    db.SaveChanges();
                }
                var listHinhAnh = db.HinhAnhs.Where(a => a.MaSP == id).ToList();
                foreach (var item in listHinhAnh)
                {
                    item.Delete = true;
                    db.SaveChanges();
                }
                sp.Delete = true;
                db.SaveChanges();
                return RedirectToAction("ServiceAccount");
            }
            else
            {
                TempData["EditBaiDang"] = "Bài đăng bạn vừa chọn đang có người mua không thể xóa";
                return RedirectToAction("ServiceAccount");
            }
        }


        public List<string> splitArray(string[] a)
        {
            List<string> list = a.ToList();
            list.RemoveAll(b => b.Equals("0"));
            return list;
        }
        private string SplitTien(string[] str)
        {
            string temp = "";
            if (str != null)
            {
                foreach (var item in str)
                {
                    temp += item;
                }
            }
            return temp;
        }
        public void splitString(string Str1, string StrTien, string StrSL, CTSP chitiet, int masp)
        {
            if (Str1 != null)
            {
                var _Str = Regex.Split(Str1, ",");
                if (StrTien != null || StrSL != null)
                {
                    var _Str2 = Regex.Split(StrTien, ",");
                    _Str2 = splitArray(_Str2).ToArray();
                    var _Str3 = Regex.Split(StrSL, ",");
                    _Str3 = splitArray(_Str3).ToArray();
                    for (int i = 0; i < _Str.Length; i++)
                    {
                        XuLyMetaData xuLy = new XuLyMetaData();
                        xuLy.GigMetaData2 = _Str[i];
                        var test = "0";
                        var t = _Str2[i].CompareTo(test);
                        if (!_Str2[i].Equals(test) && _Str2[i] != null)
                        {
                            var temp = _Str2[i].ToString().Split('.');
                            xuLy.Tien = SplitTien(temp);
                        }
                        if (!_Str3[i].Equals(test) == true && _Str3[i] != null)
                        {
                            xuLy.SL = int.Parse(_Str3[i].ToString());
                        }
                        xuLy.TrangThai = true;
                        timMa(xuLy);
                        //timMa2(xuLy);
                        XuLyMetaData.dsXuly.Add(xuLy);
                    }
                    foreach (var item in XuLyMetaData.dsXuly)
                    {
                        chitiet.MaGigMetaData = item.Ma;
                        chitiet.MaSP = masp;
                        chitiet.GiaCTSP = item.Tien.ToString();
                        chitiet.SoLuong = item.SL;
                        chitiet.TrangThai = item.TrangThai;
                        db.CTSP.Add(chitiet);
                        db.SaveChanges();
                    }
                }
                //else
                //{
                //    var _Str2 = Regex.Split(StrTien, ",");
                //    var _Str3 = Regex.Split(StrSL, ",");
                //    for (int i = 0; i < _Str.Length; i++)
                //    {
                //        XuLyMetaData xuLy = new XuLyMetaData();
                //        xuLy.GigMetaData2 = _Str[i];
                //        if (_Str2[i].ToString() != "" || _Str2[i].ToString() != null)
                //        {
                //            xuLy.Tien = long.Parse(_Str2[i].ToString());
                //        }
                //        if (_Str3[i].ToString() != "" || _Str3[i].ToString() != null)
                //        {
                //            xuLy.SL = int.Parse(_Str3[i].ToString());
                //        }
                //        xuLy.TrangThai = true;
                //        timMa(xuLy);
                //        XuLyMetaData.dsXuly.Add(xuLy);
                //    }
                //    foreach (var item in XuLyMetaData.dsXuly)
                //    {
                //        chitiet.MaGigMetaData = item.Ma;
                //        chitiet.GiaCTSP = item.Tien.ToString();
                //        chitiet.TrangThai = item.TrangThai;
                //        db.CTSP.Add(chitiet);
                //        db.SaveChanges();
                //    }
                //}
            }



        }
        public void timMa(XuLyMetaData xuly)
        {
            var test = BaiDang.dsbaidang.Where(a => a.GigMetadata != null).Select(a => a.GigMetadata).ToList();
            foreach (var item in test)
            {
                var checkMeta = item.Where(a => LocDau(a.TenGigMetaData) == LocDau(xuly.GigMetaData2)).FirstOrDefault();
                if (checkMeta != null)
                {
                    xuly.Ma = checkMeta.MaGigMetaData;
                }

            }
        }
        private void timMa2(XuLyMetaData xuly)
        {
            var temp = db.GigMetaDatas.Where(a => a.MaCTDM == null).ToList();
            foreach (var item in temp)
            {
                if (LocDau(item.TenGigMetaData) == LocDau(xuly.GigMetaData2))
                {
                    xuly.Ma = item.MaGigMetaData;
                }
            }
        }
        [HttpGet]
        public ActionResult ServiceAccount()
        {
            var test = Session["mataikhoan"];
            if (test != null)
            {
                int ma = int.Parse(test.ToString());
                var ma2 = db.SanPhams.Where(tk => tk.MaTaiKhoan == ma).Select(tk => tk.MaSP).ToList();
                List<HienThiBaiDang> dsBaiDang = new List<HienThiBaiDang>();
                var b = db.TaiKhoans.Where(c => c.MaTaiKhoan == ma).SingleOrDefault();
                var e = (from s in db.SanPhams
                         where s.MaTaiKhoan == b.MaTaiKhoan && s.Delete == false
                         select s).ToList();
                foreach (var item in e)
                {
                    HienThiBaiDang listctsp = new HienThiBaiDang();
                    listctsp.SP = item;
                    var f = db.HinhAnhs.Where(h => h.MaSP == item.MaSP && h.Delete == false).FirstOrDefault();
                    listctsp.Hinh = f;
                    dsBaiDang.Add(listctsp);
                }
                var hinh2 = db.HinhAnhs.Where(h => h.MaTaiKhoan == ma && h.Delete != true).FirstOrDefault();
                QuanLyBaiDangModel ds = new QuanLyBaiDangModel();
                ds.taikhoan = b;
                ds.TenHinhDaiDien = hinh2;
                ds.listCTSP = dsBaiDang;
                return View(ds);
            }
            return RedirectToAction("DangKy", "Home");
        }
        [HttpGet]
        public ActionResult EditBaiDang(int masp)
        {
            var test = Session["mataikhoan"];
            var DDH = db.DonDatHangs.Where(a => a.MaSP == masp && a.Delete != true).FirstOrDefault();
            BaiDang bd = new BaiDang();

            if (test != null)
            {

                if (checkDDH(DDH))
                {
                    var sp = db.SanPhams.Where(a => a.MaSP == masp).FirstOrDefault();
                    var tempctdm = db.CTDMs.Where(a => a.MaCTDM == sp.MaCTDM).FirstOrDefault();
                    var tempdm = db.DanhMucs.Where(a => a.MaDM == tempctdm.MaDM).FirstOrDefault();
                    var ctdm = (from d in db.DanhMucs
                                join c in db.CTDMs on d.MaDM equals c.MaDM
                                where d.MaDM == c.MaDM && d.TenDM == tempdm.TenDM
                                select c).ToList();
                    var dm = (from d in db.DanhMucs select d).Distinct().ToList();
                    var check = ctdm.Select(b => b.MaCTDM).FirstOrDefault();
                    var meta = db.MetaDatas.Where(a => a.MaCTDM == check).ToList();
                    var ma = db.CTDMs.Where(a => a.MaCTDM == check).Select(a => a.MaCTDM).FirstOrDefault();
                    var hinhanh = db.HinhAnhs.Where(a => a.MaSP == masp && a.Delete != true).ToList();
                    var gig = db.GigMetaDatas.Where(a => a.MaCTDM == ma || a.TenGigMetaData == "Thời gian giao" || a.TenGigMetaData == "Số lần chỉnh sửa").ToList();
                    var listctsp = db.CTSP.Where(a => a.MaSP == masp && a.Delete != true).ToList();
                    bd.danhmuc = dm;
                    bd.listctdm = ctdm;
                    bd.Metadata = meta;
                    bd.LoaiMetadata = meta.Select(a => a.LoaiMetaData).FirstOrDefault();
                    bd.Sanpham = sp;
                    bd.GigMetadata = gig;
                    bd.hinh = hinhanh;
                    bd.listCTSP = listctsp;
                    BaiDang.dsbaidang.Add(bd);
                    return View(bd);
                }
                else
                {
                    TempData["EditBaiDang"] = "Bài đăng bạn vừa chọn đang có người mua không thể chỉnh sửa";
                    return RedirectToAction("ServiceAccount");
                }
            }
            return RedirectToAction("DangKy", "Home");
        }
        [HttpPost, ValidateInput(false)]
        [ActionName("EditBaiDang")]
        public ActionResult Edit2(int masp, FormCollection collection, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            var test = Session["mataikhoan"];
            BaiDang bd = new BaiDang();
            if (test != null)
            {
                int matk = int.Parse(test.ToString());
                if (ModelState.IsValid)
                {
                    var tieude = collection.Get("TieuDe");
                    var ctdm = collection.Get("chitietDM");
                    var mota = collection.Get("MoTa");
                    var mota2 = collection.Get("MoTa2");
                    var ngaygiao = collection.Get("NgayGiao");
                    var slchinhsua = collection.Get("ChinhSua");
                    var meta = collection.Get("checkMetaData");
                    var gigmeta = collection.Get("checkGigMetadata");
                    var gigmeta2 = collection.Get("checkGigMetadata2");
                    var tien2 = collection.Get("TienMetaData2");
                    var tien = collection.Get("Tien");
                    var sl2 = collection.Get("SlMetaData2");
                    var splitStr = Regex.Split(meta, ",");
                    var splitStr2 = Regex.Split(gigmeta, ",");
                    List<string> listStr = new List<string>();
                    List<string> listStr2 = new List<string>();
                    listStr.AddRange(splitStr);
                    listStr2.AddRange(splitStr2);
                    var sanpham = db.SanPhams.Where(a => a.MaSP == masp && a.Delete != true).FirstOrDefault();
                    sanpham.MaTaiKhoan = matk;
                    matk = int.Parse(sanpham.MaTaiKhoan.ToString());
                    sanpham.GiaSP = tien.ToString();
                    sanpham.TenSP = tieude;
                    sanpham.ThoiGianGiao = int.Parse(ngaygiao.ToString());
                    sanpham.MotaSP = mota;
                    sanpham.MotaSP2 = mota2;
                    sanpham.SoLanSua = int.Parse(slchinhsua.ToString());
                    sanpham.MaCTDM = db.CTDMs.Where(a => a.TenCTDM == ctdm).Select(a => a.MaCTDM).FirstOrDefault();
                    sanpham.NgaySua = DateTime.Now;
                    db.SaveChanges();
                    bd.Sanpham = sanpham;
                    var ctsp = db.CTSP.Where(a => a.MaSP == masp && a.Delete != true).ToList();
                    db.CTSP.RemoveRange(ctsp);
                    db.SaveChanges();
                    List<MetaData> listmeta = new List<MetaData>();
                    var templist = (from a in BaiDang.dsbaidang where a.Metadata != null select a.Metadata).FirstOrDefault();
                    listmeta.AddRange(templist);
                    List<GigMetaData> listgigmeta = new List<GigMetaData>();
                    var templist2 = from a in BaiDang.dsbaidang where a.GigMetadata != null select a.GigMetadata;
                    listgigmeta.AddRange(templist2.FirstOrDefault());
                    CTSP chitiet = new CTSP();
                    foreach (var item2 in listStr)
                    {
                        var checkMeta = listmeta.FirstOrDefault(a => a.TenMetaData == item2);
                        if (checkMeta != null)
                        {
                            chitiet.MaMetaData = checkMeta.MaMetaData;
                            chitiet.TrangThai = false;
                            chitiet.MaSP = masp;
                            db.CTSP.Add(chitiet);
                            db.SaveChanges();
                        }
                    }
                    CTSP chitiet2 = new CTSP();
                    foreach (var item in listStr2)
                    {
                        var checkGigMeta = listgigmeta.FirstOrDefault(a => a.TenGigMetaData == item);
                        if (checkGigMeta != null)
                        {
                            chitiet2.MaGigMetaData = checkGigMeta.MaGigMetaData;
                            chitiet2.TrangThai = false;
                            chitiet2.MaSP = masp;
                            db.CTSP.Add(chitiet2);
                            db.SaveChanges();
                        }
                    }
                    splitString(gigmeta2, tien2, sl2, chitiet, masp);
                    if (fileUpload.FirstOrDefault() != null)
                    {
                        foreach (var file in fileUpload)
                        {
                            var filename = Path.GetFileName(file.FileName);
                            FileInfo fileInfo = new FileInfo(filename);
                            string newFileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                            var path = Path.Combine(Server.MapPath("~/assets/images/"), newFileName);
                            file.SaveAs(path);
                            var hinh = new HinhAnh();
                            hinh.TenHinhAnh = newFileName;
                            hinh.MaSP = masp;
                            //Luu vao CSDL
                            db.HinhAnhs.Add(hinh);
                            db.SaveChanges();
                        }

                    }

                }


                return RedirectToAction("ServiceAccount", "BaiDang");
            }
            return RedirectToAction("DangKy", "Home");

        }
        private void RandomBaiDang(List<SanPham> list, List<HinhAnh> hinhAnhs,List<BinhLuan> danhgia,List<TaiKhoan> tkRandom, List<HinhAnh> hinhAnhTK)
        {
            Random rand = new Random();
            var tempLenght = db.SanPhams.Count();
            var tempSP = db.SanPhams.ToList();
            SanPham tempBD = new SanPham();
            bool check = false;
            if (tempLenght > 8)
            {
                for (int i = 0; i < 8; i++)
                {

                    int randomBD = 0;
                    do
                    {
                        randomBD = rand.Next(1, tempLenght);
                        tempBD = tempSP[randomBD];
                        var temp = list.Where(a => a.MaSP == tempBD.MaSP && a.Delete != true).FirstOrDefault();
                        if (temp == null)
                            check = true;
                    } while (check == false);
                    var tempHA = db.HinhAnhs.Where(a => a.MaSP == tempBD.MaSP && a.Delete != true).FirstOrDefault();
                    var tempTK = db.TaiKhoans.Where(a => a.MaTaiKhoan == tempBD.MaTaiKhoan).FirstOrDefault();
                    if (tempBD != null && tempHA != null && tempTK !=null)
                    {
                        var tempdanhgia = db.BinhLuans.Where(a => a.MaSP == tempBD.MaSP).ToList();
                        var tempHATK = db.HinhAnhs.Where(a => a.MaTaiKhoan == tempTK.MaTaiKhoan).FirstOrDefault();
                        list.Add(tempBD);
                        danhgia.AddRange(tempdanhgia);    
                        hinhAnhs.Add(tempHA);
                        tkRandom.Add(tempTK);
                        hinhAnhTK.Add(tempHATK);
                        check = false;
                    }

                }
            }
            else
            {
                foreach (var item in tempSP)
                {
                    var tempHA = db.HinhAnhs.Where(a => a.MaSP == item.MaSP && a.Delete != true).FirstOrDefault();
                    var tempTK = db.TaiKhoans.Where(a => a.MaTaiKhoan == item.MaTaiKhoan).FirstOrDefault();
                    var tempHATK = db.HinhAnhs.Where(a => a.MaTaiKhoan == tempTK.MaTaiKhoan).FirstOrDefault();
                    list.Add(item);
                    tkRandom.Add(tempTK);
                    hinhAnhs.Add(tempHA);
                    hinhAnhTK.Add(tempHATK);
                }
            }

        }
        public ActionResult ServiceDetails(int masp)
        {
            var sp = db.SanPhams.Where(tk => tk.MaSP == masp && tk.Delete == false && tk.Delete != true).SingleOrDefault();
            if (sp != null)
            {
                var matk = db.SanPhams.Where(a => a.MaSP == masp && a.Delete != true).Select(a => a.MaTaiKhoan).SingleOrDefault();
                var taikhoan = db.TaiKhoans.Where(tk2 => tk2.MaTaiKhoan == matk).SingleOrDefault();
                var hinh = db.HinhAnhs.Where(h => h.MaTaiKhoan == matk && h.Delete != true).FirstOrDefault();
                var hinhdau = db.HinhAnhs.Where(h => h.MaSP == sp.MaSP && h.Delete == false).Select(h => h.TenHinhAnh).FirstOrDefault();
                var hinh2 = db.HinhAnhs.Where(h => h.MaSP == sp.MaSP && h.Delete == false).Select(h => h.TenHinhAnh).ToList();
                var danhgia = db.BinhLuans.Where(a => a.MaSP == masp).ToList();
                var tkdanhgia = new List<TaiKhoan>();
                var hinhtk = new List<HinhAnh>();
                foreach(var item in danhgia)
                {
                    var temptk = db.TaiKhoans.Where(a => a.MaTaiKhoan == item.MaTaiKhoan).FirstOrDefault();
                    var temphinhtk = db.HinhAnhs.Where(a => a.MaTaiKhoan == temptk.MaTaiKhoan).FirstOrDefault();
                    tkdanhgia.Add(temptk);
                    hinhtk.Add(temphinhtk);
                }
                List<SanPham> tempBD = new List<SanPham>();
                List<HinhAnh> tempHA = new List<HinhAnh>();
                List<BinhLuan> tempDG = new List<BinhLuan>();
                List<TaiKhoan> tempTK = new List<TaiKhoan>();
                ChiTietBaiDang chitietBD = new ChiTietBaiDang();
                List<HinhAnh> tempHARandom = new List<HinhAnh>();
                chitietBD.SanPham = sp;
                chitietBD.taikhoan = taikhoan;
                chitietBD.TenHinhDaiDien = hinh;
                chitietBD.HinhSanPham = hinh2;
                chitietBD.HinhSPDauTien = hinhdau;
                RandomBaiDang(tempBD, tempHA, tempDG, tempTK, tempHARandom);
                chitietBD.randomBaiDang = tempBD;
                chitietBD.randomHinhBD = tempHA;
                chitietBD.listTaiKhoanDanhGia = tkdanhgia;
                chitietBD.listDanhGia = danhgia;
                chitietBD.listHinhTK = hinhtk;
                chitietBD.listTKRandom = tempTK;
                chitietBD.listHinhTKRandom = tempHARandom;
                var ctsp = db.CTSP.Where(ct => ct.MaSP == sp.MaSP && ct.TrangThai == true && ct.Delete != true).ToList();
                chitietBD.listCTSP = ctsp;
                List<string> temp = new List<string>();
                foreach (var item in chitietBD.listCTSP)
                {
                    if (item.TrangThai == false)
                    {
                        var ten = db.MetaDatas.Where(a => a.MaMetaData == item.MaMetaData).Select(a => a.TenMetaData).FirstOrDefault();
                        temp.Add(ten);
                    }
                }
                chitietBD.TenMetaData = temp;
                return View(chitietBD);
            }
            return RedirectToAction("DangKy", "Home");
        }
        public ActionResult OrderOffer(int masp)
        {
            var test = Session["mataikhoan"];
            if (test != null)
            {
                var sp = db.SanPhams.Where(a => a.MaSP == masp && a.Delete == false).SingleOrDefault();
                if (sp != null)
                {
                    var listctsp = db.CTSP.Where(a => a.MaSP == masp && a.Delete == false).OrderBy(a=>a.MaGigMetaData).ToList();
                    var listctsp2 = db.CTSP.Where(a => a.MaSP == masp && a.Delete != true && a.TrangThai == true).OrderBy(a => a.MaGigMetaData).ToList();
                    List<GigMetaData> listgig = new List<GigMetaData>();
                    List<GigMetaData> listgig2 = new List<GigMetaData>();
                    foreach (var item in listctsp)
                    {
                        var gig = db.GigMetaDatas.Where(a => a.MaGigMetaData == item.MaGigMetaData).SingleOrDefault();

                        listgig.Add(gig);
                    }
                    foreach (var item in listctsp2)
                    {
                        var gig = db.GigMetaDatas.Where(a => a.MaGigMetaData == item.MaGigMetaData).SingleOrDefault();

                        listgig2.Add(gig);
                    }
                    var hinh = db.HinhAnhs.Where(a => a.MaSP == masp && a.Delete == false).FirstOrDefault();
                    var ctdm = db.CTDMs.Where(a => a.MaCTDM == sp.MaCTDM).FirstOrDefault();
                    var meta = db.CTSP.Where(a => a.MaSP == masp && a.TrangThai == false && a.Delete != true).Select(a => a.MaMetaData).ToList();
                    List<MetaData> listmeta = new List<MetaData>();
                    foreach (var item2 in meta)
                    {
                        var tempmeta = db.MetaDatas.Where(a => a.MaMetaData == item2).SingleOrDefault();
                        listmeta.Add(tempmeta);
                    }
                    OderDonDatHang order = new OderDonDatHang();
                    order.Baidang = sp;
                    order.listDonHang = db.DonDatHangs.Where(a => a.MaSP == sp.MaSP).ToList();
                    order.listGigOffer = listgig;
                    order.listGigOffer2 = listgig2;
                    order.hinh = hinh;
                    order.ctdm = ctdm;
                    order.listmeta = listmeta;
                    order.listctsp = listctsp;
                    order.listctsp2 = listctsp2;
                    return View(order);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("DangKy", "Home");
        }
        [HttpPost]
        public JsonResult checkMoney(string money)
        {
            OderDonDatHang.giaddh = money;
            return Json(OderDonDatHang.giaddh, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ActionName("OrderOffer")]
        public ActionResult Offer(FormCollection collection, int masp)
        {
            var test = Session["mataikhoan"];
            if (test != null)
            {
                var soluongSP = collection.Get("SoLuongBaiDang");
                var soluongGig = collection.Get("SoLuongGig");
                var motaddh = collection.Get("MotaDDH");
                var gigmetadata = collection.Get("gigMetaData");
                var matktemp = db.SanPhams.Where(a => a.MaSP == masp && a.Delete == false).Select(a => a.MaTaiKhoan).FirstOrDefault();
                var sptemp = db.SanPhams.Where(a => a.MaSP == masp && a.Delete == false).FirstOrDefault();
                var checkMatk = int.Parse(test.ToString());
                //khi làm thanh toán thì làm
                //var temptk = db.TaiKhoans.Where(a => a.MaTaiKhoan == checkMatk).FirstOrDefault();
                if (matktemp != checkMatk)
                {
                    if (sptemp != null)
                    {
                        if (gigmetadata != null)
                        {
                            var splitStr = Regex.Split(gigmetadata, ",");
                            var splitStr2 = Regex.Split(soluongGig, ",");
                            List<string> listStr = new List<string>();
                            listStr.AddRange(splitStr);
                            DonDatHang ddh = new DonDatHang();
                            CTDDH ctddh = new CTDDH();
                            ddh.MaSP = masp;
                            ddh.MoTaDDH = motaddh;
                            var tempGia = OderDonDatHang.giaddh.Replace(".", string.Empty);
                            ddh.GiaDDH = tempGia.ToString();
                            ddh.MaTaiKhoan = int.Parse(test.ToString());
                            ddh.NgayDat = DateTime.Now;
                            ddh.SoLuong = int.Parse(soluongSP.ToString());
                            ddh.TrangThai = false;
                            db.DonDatHangs.Add(ddh);
                            db.SaveChanges();
                            var maddh = db.DonDatHangs.Where(a => a.MaTaiKhoan == ddh.MaTaiKhoan && a.MaSP == masp && a.MoTaDDH == motaddh && a.Delete != true).Select(a => a.MaDDH).FirstOrDefault();
                            for (int i = 0; i < splitStr.Length; i++)
                            {
                                var tempstr = splitStr[i];

                                var tempgig = db.GigMetaDatas.Where(a => a.TenGigMetaData == tempstr).Select(a => a.MaGigMetaData).FirstOrDefault();
                                var tempctsp = db.CTSP.Where(a => a.MaGigMetaData == tempgig && a.Delete != true).Select(a => a.MaCTSP).FirstOrDefault();
                                ctddh.MaDDH = maddh;
                                ctddh.SoLuong = int.Parse(splitStr2[i].ToString());
                                ctddh.MaCTSP = tempctsp;
                                db.CTDDHs.Add(ctddh);
                                db.SaveChanges();
                            }
                            ChatHub.instance.OfferNotification(maddh);
                            return RedirectToAction("OrderManager", "BaiDang");
                        }
                        else
                        {
                            gigmetadata = "";
                            var splitStr = Regex.Split(gigmetadata, ",");
                            List<string> listStr = new List<string>();
                            listStr.AddRange(splitStr);
                            DonDatHang ddh = new DonDatHang();
                            CTDDH ctddh = new CTDDH();
                            ddh.MaSP = masp;
                            ddh.MoTaDDH = motaddh;
                            var tempGia = OderDonDatHang.giaddh.Replace(".", string.Empty);
                            ddh.GiaDDH = tempGia.ToString();
                            ddh.SoLuong = int.Parse(soluongSP.ToString());
                            ddh.MaTaiKhoan = int.Parse(test.ToString());
                            ddh.NgayDat = DateTime.Now;
                            ddh.TrangThai = false;
                            db.DonDatHangs.Add(ddh);
                            db.SaveChanges();
                            var maddh = db.DonDatHangs.Where(a => a.MaTaiKhoan == ddh.MaTaiKhoan && a.MaSP == masp && a.MoTaDDH == motaddh && a.Delete != true).Select(a => a.MaDDH).FirstOrDefault();
                            foreach (var item in listStr)
                            {
                                var tempgig = db.GigMetaDatas.Where(a => a.TenGigMetaData == item).Select(a => a.MaGigMetaData).FirstOrDefault();
                                var tempctsp = db.CTSP.Where(a => a.MaGigMetaData == tempgig && a.Delete != true).Select(a => a.MaCTSP).FirstOrDefault();
                                ctddh.MaDDH = maddh;
                                db.CTDDHs.Add(ctddh);
                                db.SaveChanges();
                            }
                            ChatHub.instance.OfferNotification(maddh);
                            return RedirectToAction("OrderManager", "BaiDang");
                        }

                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["ErrorOffer"] = "Bạn không thể mua bài đăng của chính mình";
                    return this.OrderOffer(masp);
                }
            }
            return RedirectToAction("DangKy", "Home");
        }
        public ActionResult HuyDonHang(int ma)
        {
            var test = Session["mataikhoan"];
            if (test != null)
            {
                var donhang = db.DonDatHangs.Where(a => a.MaDDH == ma && a.Delete != true).FirstOrDefault();
                if (donhang != null)
                {
                    donhang.Delete = true;
                    //khi làm thanh toán thì làm
                    //var taikhoan = db.TaiKhoans.Where(a => a.MaTaiKhoan == donhang.MaTaiKhoan).FirstOrDefault();
                    //taikhoan.SoTien = donhang.GiaDDH;
                    db.SaveChanges();
                    ChatHub.instance.DenyNotification(ma);
                    return RedirectToAction("OrderManager", "BaiDang");
                }
            }
            return RedirectToAction("DangKy", "Home");
        }
        public ActionResult ChapNhanDonHang(int ma)
        {
            var test = Session["mataikhoan"];
            if (test != null)
            {
                var donhang = db.DonDatHangs.Where(a => a.MaDDH == ma && a.Delete != true).FirstOrDefault();
                if (donhang != null)
                {
                    var sptemp = db.SanPhams.Where(a => a.MaSP == donhang.MaSP && a.Delete == false).FirstOrDefault();
                    var listctsp = db.CTSP.Where(a => a.MaSP == donhang.MaSP && a.Delete != true && a.TrangThai == true).ToList();
                    int? dem = 0;
                    foreach (var item2 in listctsp)
                    {
                        dem += item2.SoLuong;
                    }
                    donhang.Delete = false;
                    var d = DateTime.Now;
                    donhang.NgayBatDau = d;
                    var tempThoiGianGiao = int.Parse(sptemp.ThoiGianGiao.ToString())*donhang.SoLuong + dem;
                    var tg = double.Parse(tempThoiGianGiao.ToString());
                    var temp = d.AddDays(tg);
                    donhang.NgayGiao = temp;
                    db.SaveChanges();
                    ChatHub.instance.AcceptNotification(ma);
                    return RedirectToAction("OrderDetails", "BaiDang", new { ma = donhang.MaDDH });
                }
            }
            return RedirectToAction("DangKy", "Home");
        }
        public ActionResult OrderManager()
        {

            var test = Session["mataikhoan"];
            if (test != null)
            {
                var ma = int.Parse(test.ToString());
                var myTK = db.TaiKhoans.Where(a => a.MaTaiKhoan == ma).FirstOrDefault();
                var myListDDH = db.DonDatHangs.Where(a => a.MaTaiKhoan == ma && a.Delete != true).ToList();
                var myListSP = db.SanPhams.Where(a => a.MaTaiKhoan == ma && a.Delete == false).ToList();
                List<CTSP> listctsp = new List<CTSP>();
                List<DonDatHang> customerListDDH = new List<DonDatHang>();
                List<SanPham> myBuyerListSP = new List<SanPham>();
                List<TaiKhoan> customerListTK = new List<TaiKhoan>();
                List<GigMetaData> listGig = new List<GigMetaData>();
                List<TaiKhoan> myBuyerListTK = new List<TaiKhoan>();
                foreach (var item in myListSP)
                {
                    var ddh = db.DonDatHangs.Where(a => a.MaSP == item.MaSP && a.Delete != true).ToList();
                    if (ddh.Count > 0)
                    {
                        customerListDDH.AddRange(ddh);
                    }
                }
                foreach (var item in customerListDDH)
                {

                    var customerTK = db.TaiKhoans.FirstOrDefault(a => a.MaTaiKhoan == item.MaTaiKhoan);
                    customerListTK.Add(customerTK);

                }
                foreach (var item in myListDDH)
                {
                    var buyerSP = db.SanPhams.Where(a => a.MaSP == item.MaSP && a.Delete == false).FirstOrDefault();
                    myBuyerListSP.Add(buyerSP);
                    var buyerTK = (from s in db.SanPhams
                                   join t in db.TaiKhoans on s.MaTaiKhoan equals t.MaTaiKhoan
                                   where s.MaSP == item.MaSP
                                   select t).FirstOrDefault();
                    myBuyerListTK.Add(buyerTK);
                    var myOfferGig = db.CTDDHs.Where(a => a.MaDDH == item.MaDDH && a.MaCTSP != null).ToList();
                    if (myOfferGig.Count > 0)
                    {
                        foreach (var item2 in myOfferGig)
                        {
                            var tempCTSP = db.CTSP.Where(a => a.MaCTSP == item2.MaCTSP && a.Delete == false).FirstOrDefault();
                            listctsp.Add(tempCTSP);
                        }
                    }

                }
                if (listctsp.Count > 0)
                {
                    foreach (var item in listctsp)
                    {
                        var gig = db.GigMetaDatas.Where(a => a.MaGigMetaData == item.MaGigMetaData).FirstOrDefault();
                        listGig.Add(gig);
                    }
                }
                QuanLyDDH quanly = new QuanLyDDH();
                quanly.myListDDH = myListDDH;
                quanly.customerListDDH = customerListDDH;
                quanly.myListSP = myListSP;
                quanly.customerListTK = customerListTK;
                quanly.myBuyerListSP = myBuyerListSP;
                quanly.myBuyerListTK = myBuyerListTK;
                quanly.myTk = myTK;
                quanly.listctsp = listctsp;
                quanly.listGigOffer = listGig;
                return View(quanly);
            }
            return RedirectToAction("DangKy", "Home");
        }
        public ActionResult EditDDH(int ma)
        {
            var tempddh = db.DonDatHangs.Where(a => a.MaDDH == ma).FirstOrDefault();
            var sp = db.SanPhams.Where(a => a.MaSP == tempddh.MaSP && a.Delete == false).FirstOrDefault();
            var hinhanh = db.HinhAnhs.Where(a => a.MaSP == ma).FirstOrDefault();
            var ctdm = db.CTDMs.Where(a => a.MaCTDM == sp.MaCTDM).FirstOrDefault();
            var listctsp = db.CTSP.Where(a => a.MaSP == sp.MaSP && a.Delete == false).ToList();
            List<MetaData> templist = new List<MetaData>();
            List<GigMetaData> templistgig = new List<GigMetaData>();

            foreach (var item in listctsp)
            {
                var meta = db.MetaDatas.Where(a => a.MaMetaData == item.MaMetaData).FirstOrDefault();
                templist.Add(meta);
                var gigmeta = db.GigMetaDatas.Where(a => a.MaGigMetaData == item.MaGigMetaData).FirstOrDefault();
                templistgig.Add(gigmeta);
            }
            EditOrder ddh = new EditOrder();
            ddh.Baidang = sp;
            ddh.hinh = hinhanh;
            ddh.ctdm = ctdm;
            ddh.listctsp = listctsp;
            ddh.listmeta = templist;
            ddh.listGigOffer = templistgig;
            ddh.ddh = tempddh;
            return View(ddh);
        }
        [HttpPost]
        public JsonResult checkMoney2(string money)
        {

            EditOrder.giaddh = money;
            return Json(EditOrder.giaddh, JsonRequestBehavior.AllowGet);
        }
        private bool checkDDH(DonDatHang donDatHang)
        {
            if (donDatHang == null)
            {
                return true;
            }
            else
            {
                if (donDatHang.TrangThai == true)
                    return true;
                return false;
            }

        }
        [HttpPost]
        [ActionName("EditDDH")]
        public ActionResult Edit(FormCollection collection, int ma)
        {
            var test = Session["mataikhoan"];

            if (test != null)
            {
                var motaddh = collection.Get("MotaDDH");
                var gigmetadata = collection.Get("gigMetaData");
                var matktemp = db.SanPhams.Where(a => a.MaSP == ma && a.Delete == false).Select(a => a.MaTaiKhoan).FirstOrDefault();
                var sptemp = db.SanPhams.Where(a => a.MaSP == ma && a.Delete == false).FirstOrDefault();
                var checkMatk = int.Parse(test.ToString());
                var ddh = db.DonDatHangs.Where(a => a.MaDDH == ma).FirstOrDefault();
                var ctddh = db.CTDDHs.Where(a => a.MaDDH == ma).ToList();
                var ctddh2 = new CTDDH();
                List<CTDDH> temp = new List<CTDDH>();
                if (gigmetadata != null && gigmetadata != "")
                {
                    var splitStr = Regex.Split(gigmetadata, ",");
                    List<string> listStr = new List<string>();
                    listStr.AddRange(splitStr);
                    foreach (var item in ctddh)
                    {
                        db.CTDDHs.Remove(item);
                        db.SaveChanges();
                    }
                    foreach (var item in listStr)
                    {
                        var tempgig = db.GigMetaDatas.Where(a => a.TenGigMetaData == item).Select(a => a.MaGigMetaData).FirstOrDefault();
                        var tempctsp = db.CTSP.Where(a => a.MaGigMetaData == tempgig && a.Delete == false).Select(a => a.MaCTSP).FirstOrDefault();
                        ctddh2.MaDDH = ma;
                        ctddh2.MaCTSP = tempctsp;
                        db.CTDDHs.Add(ctddh2);
                        db.SaveChanges();
                    }
                    ddh.MoTaDDH = motaddh;
                    db.SaveChanges();
                    return RedirectToAction("OrderManager", "BaiDang");
                }
                else
                {
                    ddh.MoTaDDH = motaddh;
                    ddh.GiaDDH = EditOrder.giaddh;
                    foreach (var item in ctddh)
                    {
                        db.CTDDHs.Remove(item);
                        db.SaveChanges();
                    }

                }

            }

            return RedirectToAction("DangKy", "Home");
        }
        private bool CheckInDDH(int MaDDH,int ID)
        {
            var matk2 = (from d in db.DonDatHangs
                         join s in db.SanPhams on d.MaSP equals s.MaSP
                         where d.MaSP == s.MaSP && d.MaDDH == MaDDH && s.Delete == false
                         select s).FirstOrDefault();
            var matk1 = db.DonDatHangs.Where(a => a.MaDDH == MaDDH && a.Delete != true).Select(a => a.MaTaiKhoan).FirstOrDefault();
            if (ID == matk1 || ID == matk2.MaTaiKhoan)
                return true;
            return false;
        }
        public ActionResult OrderDetails(int ma)
        {
            var test = Session["mataikhoan"];
            if (test != null)
            {
                if (ma > 0)
                {
                    var tempmatk = Session["mataikhoan"];
                    ViewBag.matk = Session["mataikhoan"];
                    ViewBag.tentk = Session["tentaikhoan"];
                    var checkMa = int.Parse(tempmatk.ToString());
                    if (CheckInDDH(ma,checkMa)==true)
                    {
                        var mess = db.TinNhans.Where(a => a.MaDDH == ma).OrderBy(a => a.ThoiGian).ToList();
                        var madh = db.DonDatHangs.Where(a => a.MaDDH == ma && a.Delete != true).FirstOrDefault();
                        var chitietsanpham = db.CTDDHs.Where(a => a.MaDDH == ma).ToList();
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
                        var listTienDo = db.TienDoCongViecs.Where(a => a.MaDDH == ma).ToList();
                        var temp2 = db.CTSP.Where(a => a.MaSP == sanpham.MaSP && a.TrangThai == false && a.MaGigMetaData != null && a.Delete == false).Select(a => a.MaGigMetaData).ToList();
                        List<GigMetaData> listgigbd = new List<GigMetaData>();
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
                        foreach(var item in listTienDo)
                        {
                            var tempHinh = db.HinhAnhs.Where(a => a.MaTienDo == item.MaTienDo).ToList();
                            tiendo.AddRange(tempHinh);
                        }
                        var listMoTa = db.ThemMoTaDDHs.Where(a => a.MaDDH == ma).ToList();
                        OrderChiTiet order = new OrderChiTiet();
                        order.tkBuyer = tkBuyer;
                        order.donDatHang = madh;
                        order.Baidang = sanpham;
                        order.hinh = hinh;
                        order.tkMinhChung = tkminhchung;
                        order.listDHHT = listHoanhThanh;
                        order.listgigbd = listgigbd;
                        order.listHinhMinhChung = minhchung;
                        order.listgigkh = listextra;
                        order.listMess = mess;
                        order.listMotaDDH = listMoTa;
                        List<TaiKhoan> listtk = new List<TaiKhoan>();
                        CheckUID(int.Parse(tempmatk.ToString()), sanpham.MaSP, madh.MaDDH);
                        foreach (var item in mess)
                        {
                            var tk = db.TaiKhoans.Where(a => a.MaTaiKhoan == item.MaTaiKhoan).FirstOrDefault();
                            listtk.Add(tk);
                        }
                        order.listTaikhoan = listtk;
                        order.listGia = listgia;
                        order.listTienDo = listTienDo;
                        order.listHinhTienDo = tiendo;
                        OrderChiTiet.listOrderChitiet = listtk.ToList();
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
        [HttpPost]
        [ActionName("OrderDetails")]
        public ActionResult Details(FormCollection collection, int madonhang, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            var test = Session["mataikhoan"];
            if (test != null)
            {
                var id = int.Parse(test.ToString());
                if (checkSeller(id, madonhang))
                {
                    var url1 = collection.Get("urlchinh");
                    var url2 = collection.Get("urlduphong");
                    var noidung = collection.Get("noidung");
                    if (url1 != null && url2 != null && noidung != null && url1 != "" && url2 != "" && noidung != "" && fileUpload.FirstOrDefault() != null)
                    {
                        DonHangHoanThanh donHang = new DonHangHoanThanh();
                        donHang.NgayHoanThanh = DateTime.Now;
                        donHang.NoiDungDHHT = noidung.ToString();
                        donHang.MaTaiKhoan = id;
                        donHang.MaDDH = madonhang;
                        donHang.DuongDanChinh = url1;
                        donHang.DuongDanPhu = url2;
                        db.DonHangHoanThanhs.Add(donHang);
                        db.SaveChanges();
                        foreach (var file in fileUpload)
                        {
                            var filename = Path.GetFileName(file.FileName);
                            FileInfo fileInfo = new FileInfo(filename);
                            string newFileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                            var path = Path.Combine(Server.MapPath("~/assets/images/"), newFileName);
                            file.SaveAs(path);
                            var hinh = new HinhAnh();
                            hinh.TenHinhAnh = newFileName;
                            hinh.MaDHHT = donHang.MaDHHT;
                            hinh.Delete = false;
                            //Luu vao CSDL
                            db.HinhAnhs.Add(hinh);
                            db.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("OrderDetails", "BaiDang", new { ma = madonhang });
            }
            return RedirectToAction("DangKy", "Home");
        }
        private bool checkSeller(int MaTK, int madonHang)
        {
            var baidang = (from d in db.DonDatHangs join s in db.SanPhams on d.MaSP equals s.MaSP where d.MaDDH == madonHang select s).FirstOrDefault();
            if (MaTK == baidang.MaTaiKhoan)
                return true;
            return false;
        }
        private void CheckUID(int MaTK, int mabaiDang, int madonHang)
        {
            var seller = db.SanPhams.Where(a => a.MaSP == mabaiDang && a.MaTaiKhoan == MaTK && a.Delete != true).FirstOrDefault();
            var buyer = db.DonDatHangs.Where(a => a.MaDDH == madonHang && a.MaTaiKhoan == MaTK && a.Delete != true).FirstOrDefault();
            if (buyer != null)
            {
                var tinNhan = db.TinNhans.Where(a => a.MaTaiKhoan == buyer.MaTaiKhoan && a.MaDDH == madonHang).ToList();
                foreach (var item in tinNhan)
                {
                    item.BuyerSeen = true;
                    db.SaveChanges();
                }
            }
            else if (seller != null)
            {
                var tinNhan = db.TinNhans.Where(a => a.MaTaiKhoan == seller.MaTaiKhoan && a.MaDDH == madonHang).ToList();
                foreach (var item in tinNhan)
                {
                    item.SellerSeen = true;
                    db.SaveChanges();
                }
            }
        }
        private List<SanPham> FilterTime(string time, List<SanPham> listSevice, int id)
        {
            if (time == "8")
            {
                listSevice = (from sv in db.SanPhams where sv.MaCTDM == id && sv.Delete == false && sv.ThoiGianGiao > 7 select sv).ToList();
            }
            else if (time == "any")
            {
                listSevice = (from sv in db.SanPhams where sv.MaCTDM == id && sv.Delete == false && sv.ThoiGianGiao > 0 select sv).ToList();
            }
            else if (time != "" || time != null)
            {
                var temp = int.Parse(time.ToString());
                listSevice = (from sv in db.SanPhams where sv.MaCTDM == id && sv.Delete == false && sv.ThoiGianGiao <= temp select sv).ToList();
            }
            return listSevice;
        }
        private List<SanPham> FilterMoney(string moneyfrom, string moneyto, List<SanPham> listSevice, int id)
        {
            var tempList = new List<SanPham>();
            if (moneyfrom != null && moneyfrom != "" && moneyfrom != "0" && moneyto != null && moneyto != "" && moneyto != "0")
            {
                foreach (var item in listSevice)
                {
                    if (int.Parse(item.GiaSP.ToString()) >= int.Parse(moneyfrom.ToString()) && int.Parse(item.GiaSP.ToString()) <= int.Parse(moneyto.ToString()))
                        tempList.Add(item);
                }
                return tempList;
            }
            return listSevice;
        }

        private List<SanPham> FilterMetadata(string[] style, List<SanPham> listSevice, int id)
        {
            var tempListMeta = new List<MetaData>();
            var tempList = new List<SanPham>();
            var ListMeta = db.MetaDatas.Where(a => a.MaCTDM == id).ToList();
            for (int i = 0; i < style.Length; i++)
            {
                foreach (var item in ListMeta)
                {
                    var temp = LocDau(item.TenMetaData);
                    if (temp == LocDau(style[i]))
                        tempListMeta.Add(item);
                }
            }
            var tempListCTSP = new List<CTSP>();
            var ListCTSP = new List<CTSP>();
            var sad = new List<CTSP>();
            foreach (var item in tempListMeta)
            {
                var temp = db.CTSP.Where(a => a.MaMetaData == item.MaMetaData).GroupBy(a => a.MaSP).Select(a => a.FirstOrDefault()).ToList();
                sad.AddRange(temp);
            }
            foreach (var item in sad)
            {
                var temp = listSevice.Where(a => a.MaSP == item.MaSP).FirstOrDefault();
                if (temp != null)
                    tempList.Add(temp);
            }
            return tempList.GroupBy(a => a.MaSP).Select(a => a.FirstOrDefault()).ToList();
        }
        private List<SanPham> FilterGigMetadata(string[] services, List<SanPham> listSevice, int id)
        {
            var tempListMeta = new List<GigMetaData>();
            var tempList = new List<SanPham>();
            var ListMeta = db.GigMetaDatas.Where(a => a.MaCTDM == id).ToList();
            for (int i = 0; i < services.Length; i++)
            {
                foreach (var item in ListMeta)
                {
                    var temp = LocDau(item.TenGigMetaData);
                    if (temp == LocDau(services[i]))
                        tempListMeta.Add(item);
                }
            }
            var tempListCTSP = new List<CTSP>();
            var ListCTSP = new List<CTSP>();
            var sad = new List<int?>();
            foreach (var item in tempListMeta)
            {
                var temp = db.CTSP.Where(a => a.MaGigMetaData == item.MaGigMetaData).Select(a => a.MaSP).Distinct().ToList();
                sad.AddRange(temp);
            }
            foreach (var item in sad)
            {
                var temp = listSevice.Where(a => a.MaSP == item).FirstOrDefault();
                tempList.Add(temp);
            }
            return tempList.Distinct().ToList();
        }
        [HttpPost]
        public JsonResult FilterByCategory(string time, string[] style, string[] service, string moneyfrom, string moneyto, int id)
        {
            DanhSachBaiDang danhSachBaiDang = new DanhSachBaiDang();
            var test2 = (from c in db.CTDMs
                         where c.MaCTDM == id
                         select c.MaDM).FirstOrDefault();
            var test = (from c in db.CTDMs
                        where c.MaDM == test2
                        select c).ToList();
            List<SanPham> listSevice = new List<SanPham>();
            listSevice = (from sv in db.SanPhams where sv.MaCTDM == id && sv.Delete == false select sv).ToList();
            listSevice = FilterTime(time, listSevice, id);
            listSevice = FilterMoney(moneyfrom, moneyto, listSevice, id);
            if (style != null)
                listSevice = FilterMetadata(style, listSevice, id);
            if (service != null)
                listSevice = FilterGigMetadata(service, listSevice, id);
            List<TaiKhoan> tk = new List<TaiKhoan>();
            List<HinhAnh> ha = new List<HinhAnh>();
            List<BinhLuan> danhgia = new List<BinhLuan>();
            int? dem = 0;
            List<double> sao = new List<double>();
            foreach (var item in listSevice)
            {
                if(item!=null)
                {
                    var temptk = db.TaiKhoans.Where(a => a.MaTaiKhoan == item.MaTaiKhoan).FirstOrDefault();
                    var tempha = db.HinhAnhs.Where(a => a.MaTaiKhoan == item.MaTaiKhoan || a.MaSP == item.MaSP && a.Delete != true).ToList();
                    var tempdg = db.BinhLuans.Where(a => a.MaSP == item.MaSP).ToList();
                    tk.Add(temptk);
                    ha.AddRange(tempha);
                    danhgia.AddRange(tempdg);
                    foreach(var item2 in danhgia)
                    {
                        dem += item2.Sao; 
                    }
                    if(danhgia.Count>0)
                    {
                        var tong = (float)dem / danhgia.Count;
                        sao.Add(Math.Round(tong, 1));
                    }
                    else
                    {
                        sao.Add(0);
                    }  
                   
                }
            }
            
            danhSachBaiDang.SanPham = listSevice;
            danhSachBaiDang.TenHinhDaiDien = ha;
            danhSachBaiDang.TaiKhoan = tk;
            danhSachBaiDang.listCTDM = test;
            danhSachBaiDang.listGig = db.GigMetaDatas.Where(a => a.MaCTDM == id).ToList();
            danhSachBaiDang.listMeta = db.MetaDatas.Where(a => a.MaCTDM == id).ToList();
            danhSachBaiDang.idFilter = id;
            danhSachBaiDang.listDanhGia = danhgia;
            return Json(new { First = danhSachBaiDang.SanPham, Second = danhSachBaiDang.TenHinhDaiDien, Third = danhSachBaiDang.TaiKhoan,Fourth = danhSachBaiDang.listDanhGia,Fifth = sao }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult HoanThanh(int MaDDH)
        {
            return View(MaDDH);
        }
        [HttpPost]
        [ActionName("HoanThanh")]
        public ActionResult DanhGia(FormCollection collection, int MaDDH)
        {
            var test = Session["mataikhoan"];
            if (test != null)
            {
                var id = int.Parse(test.ToString());
                if (CheckInDDH(MaDDH, id) == true)
                {
                    var sosao = collection.Get("reviewStars");
                    var noidungdanhgia = collection.Get("message");
                    if (noidungdanhgia != null && sosao != null && sosao != "" && noidungdanhgia != "")
                    {   
                        var donHang = db.DonDatHangs.Where(a => a.MaDDH == MaDDH && a.Delete != true).FirstOrDefault();
                        donHang.TrangThai = true;
                        db.SaveChanges();
                        var danhgia = new BinhLuan();
                        danhgia.MaSP = donHang.MaSP;
                        danhgia.MaTaiKhoan = id;
                        danhgia.NgayBinhLuan = DateTime.Now;
                        danhgia.NoiDung = noidungdanhgia;
                        danhgia.Sao = int.Parse(sosao);
                        db.BinhLuans.Add(danhgia);
                        db.SaveChanges();
                        var donHangHT = db.DonHangHoanThanhs.Where(a => a.MaDDH == MaDDH).ToList();
                        foreach (var item in donHangHT)
                        {
                            item.TrangThai = true;
                        }
                        db.SaveChanges();
                        ChatHub.instance.HoanThanhNotification(MaDDH);
                        return RedirectToAction("OrderDetails", "BaiDang", new { ma = MaDDH });
                    }
                    return RedirectToAction("OrderDetails", "BaiDang", new { ma = MaDDH });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("DangKy", "Home");
        }
        [HttpPost]
        public ActionResult BaoCao(FormCollection collection, int MaDDH, IEnumerable<HttpPostedFileBase> anhbaocao)
        {
            var test = Session["mataikhoan"];
            if (test != null)
            {
                var id = int.Parse(test.ToString());
                if (CheckInDDH(MaDDH, id) == true)
                {
                    var noidungbaocao = collection.Get("noidungbaocao");
                    var tiendo = collection.Get("tiendo");
                    if (tiendo != null && noidungbaocao != null && tiendo != "" && noidungbaocao != "" && anhbaocao.FirstOrDefault() != null)
                    {
                        TienDoCongViec baocao = new TienDoCongViec();
                        baocao.NoiDung = noidungbaocao;
                        baocao.MucTienDo = int.Parse(tiendo.ToString());
                        baocao.NgayGui = DateTime.Now;
                        baocao.MaDDH = MaDDH;
                        db.TienDoCongViecs.Add(baocao);
                        db.SaveChanges();
                        foreach (var file in anhbaocao)
                        {
                            var filename = Path.GetFileName(file.FileName);
                            FileInfo fileInfo = new FileInfo(filename);
                            string newFileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                            var path = Path.Combine(Server.MapPath("~/assets/images/"), newFileName);
                            file.SaveAs(path);
                            var hinh = new HinhAnh();
                            hinh.TenHinhAnh = newFileName;
                            hinh.MaTienDo = baocao.MaTienDo;
                            hinh.Delete = false;
                            //Luu vao CSDL
                            db.HinhAnhs.Add(hinh);
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("OrderDetails", "BaiDang", new { ma = MaDDH });    
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("DangKy", "Home");
        }
        [HttpPost]
        public JsonResult ThongBao(string UID)
        {
            var id = int.Parse(UID);
            var tempSP = db.SanPhams.Where(a => a.MaTaiKhoan == id && a.Delete != true).ToList();
            var tempDH = db.DonDatHangs.Where(a => a.MaTaiKhoan == id && a.Delete != true).ToList();
            HienThiThongBao hienThiThongBao = new HienThiThongBao();
            List<TinNhan> tempListTinNhan = new List<TinNhan>();
            List<ThongBao> tempListThongBao = new List<ThongBao>();
            List<TaiKhoan> tempListTK = new List<TaiKhoan>();
            List<HinhAnh> tempListHA = new List<HinhAnh>();
            List<DonDatHang> tempListDH = new List<DonDatHang>();
            foreach (var item in tempSP)
            {
                if (item != null)
                {
                    var donhang = db.DonDatHangs.Where(a => a.MaSP == item.MaSP && item.Delete != true).ToList();
                    foreach (var item2 in donhang)
                    {
                        var tinnhan = db.TinNhans.Where(a => a.MaDDH == item2.MaDDH && a.SellerSeen != true).GroupBy(a => a.MaDDH).Select(a => a.FirstOrDefault()).ToList();
                        if (tinnhan.Count > 0)
                        {
                            tempListTinNhan.AddRange(tinnhan);
                            foreach(var item3 in tinnhan)
                            {
                                var tempdh = db.DonDatHangs.Where(a => a.MaDDH == item3.MaDDH).FirstOrDefault();
                                tempListDH.Add(tempdh);
                            }
                        }
                        var thongbao = db.ThongBaos.Where(a => a.MaDDH == item2.MaDDH && a.SellerSeen != true).ToList();
                        if (thongbao.Count > 0)
                        {
                            tempListThongBao.AddRange(thongbao);
                            foreach (var item3 in thongbao)
                            {
                                var temptk = db.TaiKhoans.Where(a => a.MaTaiKhoan == item3.MaTaiKhoan).FirstOrDefault();
                                var tempha = db.HinhAnhs.Where(a => a.MaTaiKhoan == temptk.MaTaiKhoan).FirstOrDefault();
                                tempListTK.Add(temptk);
                                tempListHA.Add(tempha);
                            }
                        }
                    }
                }
            }
            foreach (var item in tempDH)
            {
                if (item != null)
                {
                    var tinnhan = db.TinNhans.Where(a => a.MaDDH == item.MaDDH && a.BuyerSeen != true).GroupBy(a => a.MaDDH).Select(a => a.FirstOrDefault()).ToList();
                    foreach (var item3 in tinnhan)
                    {
                        var tempdh = db.DonDatHangs.Where(a => a.MaDDH == item3.MaDDH).FirstOrDefault();
                        tempListDH.Add(tempdh);
                    }
                    var thongbao = db.ThongBaos.Where(a => a.MaDDH == item.MaDDH && a.BuyerSeen != true).ToList();
                    if (thongbao.Count > 0)
                    {
                        tempListThongBao.AddRange(thongbao);
                        foreach (var item3 in thongbao)
                        {
                            var temptk = db.TaiKhoans.Where(a => a.MaTaiKhoan == item3.MaTaiKhoan).FirstOrDefault();
                            var tempha = db.HinhAnhs.Where(a => a.MaTaiKhoan == temptk.MaTaiKhoan).FirstOrDefault();
                            tempListTK.Add(temptk);
                            tempListHA.Add(tempha);
                        }
                    }
                }
            }
            hienThiThongBao.listTinNhan = tempListTinNhan;
            hienThiThongBao.listThongBao = tempListThongBao;
            return Json(new { First = tempListTinNhan, Second = tempListThongBao, Third = tempListTK, Four = tempListHA,Five= tempListDH }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReviewServices()
        {
            return View();
        }
        public ActionResult ServiceCategory(int id)
        {

            DanhSachBaiDang danhSachBaiDang = new DanhSachBaiDang();
            var test2 = (from c in db.CTDMs
                         where c.MaCTDM == id
                         select c.MaDM).FirstOrDefault();
            var test = (from c in db.CTDMs
                        where c.MaDM == test2
                        select c).ToList();
            var listSevice = (from sv in db.SanPhams where sv.MaCTDM == id && sv.Delete == false select sv).ToList();
            List<TaiKhoan> tk = new List<TaiKhoan>();
            List<HinhAnh> ha = new List<HinhAnh>();
            foreach (var item in listSevice)
            {
                var temptk = db.TaiKhoans.Where(a => a.MaTaiKhoan == item.MaTaiKhoan).FirstOrDefault();
                var tempha = db.HinhAnhs.Where(a => a.MaTaiKhoan == item.MaTaiKhoan || a.MaSP == item.MaSP && a.Delete == false).ToList();
                tk.Add(temptk);
                ha.AddRange(tempha);
            }
            danhSachBaiDang.SanPham = listSevice;
            danhSachBaiDang.TenHinhDaiDien = ha;
            danhSachBaiDang.TaiKhoan = tk;
            danhSachBaiDang.listCTDM = test;
            danhSachBaiDang.listGig = db.GigMetaDatas.Where(a => a.MaCTDM == id).ToList();
            danhSachBaiDang.listMeta = db.MetaDatas.Where(a => a.MaCTDM == id).ToList();
            danhSachBaiDang.idFilter = id;
            return View(danhSachBaiDang);

        }
        [HttpPost]
        public void ToCao(int MaDDH, string typeData,string txtOthers,string txtContact)
        {
            if (MaDDH > 0 && typeData!=null && txtOthers != null && txtContact != null)
            {
                var tranhchap = new TranhChap();
                tranhchap.MaDDH = MaDDH;
                var noidung = typeData + txtOthers;
                tranhchap.NoiDung = noidung;
                tranhchap.ThoiGian = DateTime.Now;
                tranhchap.LienHe = txtContact;
                db.TranhChaps.Add(tranhchap);
                db.SaveChanges();
            }
        }
        [HttpPost]
        public void ThemMoTa(string noidung,int maddh)
        {
            if (maddh > 0 && noidung != null && noidung != "")
            {
                var ThemMoTa = new ThemMoTaDDH();
                ThemMoTa.MaDDH = maddh;
                ThemMoTa.MoTa = noidung;
                ThemMoTa.ThoiGian = DateTime.Now;
                db.ThemMoTaDDHs.Add(ThemMoTa);
                db.SaveChanges();
            }
        }
    }
}