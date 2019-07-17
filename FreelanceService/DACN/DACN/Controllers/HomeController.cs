using DACN.Models.BusinessModel;
using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DACN.Controllers
{
    public class HomeController : Controller
    {
        DACNDbContext db = new DACNDbContext();
        public ActionResult NavigationLoad()
        {
            Navigation navigate = new Navigation();
            navigate.listDM = db.DanhMucs.ToList();
            navigate.listCTDM = db.CTDMs.ToList();
            return PartialView(navigate);
        }

        public ActionResult NavigationDrop()
        {
            Navigation navigate = new Navigation();
            navigate.listCTDM = db.CTDMs.ToList();

            return PartialView(navigate);
        }
        public ActionResult IndexAdmin()
        {
            return View();
        }
       
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult Error404Page()
        {
            return View();
        }
        
        public ActionResult OrderDetails()
        {
            return View();
        }
        public ActionResult CategoryDetails(int id)
        {
            Navigation navigate = new Navigation();
            var listCategory = (from a in db.CTDMs where a.MaDM == id select a).ToList();
            navigate.listDM = db.DanhMucs.ToList();
            navigate.listCTDM = listCategory;
            return View(navigate);
        }
        public ActionResult OrderManager()
        {
            return View();
        }
        public ActionResult ServicePage()
        {
            return View();
        }
        public ActionResult OrderOffer()
        {
            return View();
        }
        public ActionResult Messenger()
        {
            return View();
        }
        public ActionResult Messengerr()
        {
            if (Session["mataikhoan"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.matk = Session["mataikhoan"];
                ViewBag.tentk = Session["tentaikhoan"];  
                return View();
            }
        }

        public ActionResult ServiceDetails()
        {
            return View();
        }

        public ActionResult BookMarked()
        {
            return View();
        }
        public ActionResult Notifications()
        {
            return View();
        }
   
        public ActionResult Login()
        {
            if (Session["mataikhoan"] != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Logout()
        {
            if (Session["mataikhoan"] != null)
            {
                Session["mataikhoan"] = null;
                Session["tentaikhoan"] = null;
                return RedirectToAction("DangKy");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Login(string tentaikhoan, string matkhau)
        {
            var nd2 = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == tentaikhoan && x.MatKhau == matkhau);
            if (nd2 != null)
            {
                var admin = db.PhanQuyens.SingleOrDefault(x => x.MaTaiKhoan == nd2.MaTaiKhoan && x.MaQuyen == 1);
                if (admin != null)
                {
                    Session["mataikhoan"] = nd2.MaTaiKhoan;
                    Session["tentaikhoan"] = nd2.TenTaiKhoan;
                    return RedirectToAction("IndexAdmin","Home");
                }
                else
                {
                    Session["mataikhoan"] = nd2.MaTaiKhoan; 
                    Session["tentaikhoan"] = nd2.TenTaiKhoan;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ThongBao = "Đăng nhập sai hoặc bạn không có quyền vào";
            return View("DangKy");
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection)
        {
            TaiKhoan tk = new TaiKhoan();
            // Gắn các giá trị người dùng nhập dữ liệu cho các biến
            var tentk = collection["tentaikhoan"];
            var matkhau = collection["matkhau"];
            var email = collection["email"];
            var sdt = collection["sdt"];
            var diachi = collection["diachi"];
            var sotknganhang = collection["sotknganhang"];
            var tentknganhang = collection["tentknganhang"];
            if (String.IsNullOrEmpty(tentk))
            {
                ViewData["Loi1"] = "Phải nhập tên tài khoản";
            }
            else if(tentk.Trim().Length < 3 || tentk.Trim().Length > 64)
            {
                ViewData["Loi1"] = "Phải nhập trong khoảng từ 3 đến 64 ký tự";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi4"] = "Phải nhập email";
            }
            else if (String.IsNullOrEmpty(sdt))
            {
                ViewData["Loi5"] = "Phải nhập số điện thoại";
            }
            else if (!Regex.IsMatch(sdt, "^[0-9]+$", RegexOptions.Compiled))
            {
                ViewData["Loi5"] = "Số điện thoại chỉ được nhập số.";
            }
            else if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi6"] = "Phải nhập địa chỉ";
            }
           
            else
            {
                // Gắn giá trị cho đối tượng được tạo mới
                tk.TenTaiKhoan = tentk;
                tk.DiaChi = diachi;
                tk.Email = email;
                tk.SDT = sdt;
                tk.SoTKNganHang = sotknganhang;
                tk.TenTkNganHang = tentknganhang;
                tk.MatKhau = matkhau;
                var ktkh = db.TaiKhoans.Where(x => x.TenTaiKhoan == tk.TenTaiKhoan &x.Email==tk.Email && x.SDT == x.SDT).FirstOrDefault();
                if (ktkh != null)
                {
                    ViewBag.Thongbao = "Tên đăng nhập đã được đăng ký. Vui lòng nhập lại thông tin";
                }
                else
                {
                    db.TaiKhoans.Add(tk);
                    db.SaveChanges();
                    var ma = db.TaiKhoans.Where(a => a.TenTaiKhoan == tentk && a.MatKhau == matkhau).FirstOrDefault();
                    HinhAnh hinh = new HinhAnh();
                    hinh.MaTaiKhoan = ma.MaTaiKhoan;
                    hinh.TenHinhAnh = "abc.jpg";
                    db.HinhAnhs.Add(hinh);
                    db.SaveChanges();
                    return this.DangKy();
                }
            }
            return this.DangKy();
        }
        public ActionResult QuenMatKhau()
        {
            return View();
        }
        [NonAction]
        public void SendVerificationLinkEmail(string email, string makichhoat, string emailFor = "XacNhanTaiKhoan")
        {
            var verifyUrl = "/Home/" + emailFor + "/" + makichhoat;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("dacnttm@gmail.com", "CN DA"); 
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "dacn12345"; 

            string subject = "";
            string body = "";
            if (emailFor == "XacNhanTaiKhoan")
            {
                subject = "Tài khoản của bạn đã được tạo thành công!";
                body = "<br/><br/>Chúng tôi rất vui được thông báo với bạn rằng tài khoản DACN của bạn là" +
                    "Vui lòng nhấp vào liên kết bên dưới để xác minh tài khoản của bạn" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "DatLaiMatKhau")
            {
                subject = "Đặt lại mật khẩu thành công";
                body = "Hi,<br/>br/>Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu tài khoản của bạn. Vui lòng nhấp vào liên kết bên dưới để đặt lại mật khẩu của bạn" +
                    "<br/><br/><a href=" + link + ">link đặt lại mật khẩu</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        [HttpPost]
        public ActionResult QuenMatKhau(string email)
        {
            string message = "";

            using (DACNDbContext dc = new DACNDbContext())
            {
                var tk = dc.TaiKhoans.Where(a => a.Email == email).FirstOrDefault();
                if (tk != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(tk.Email, resetCode, "DatLaiMatKhau");
                    tk.MaMatKhau = resetCode;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                    //in our model class in part 1
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();
                    message = "Đặt lại liên kết mật khẩu đã được gửi đến id email của bạn";
                }
                else
                {
                    message = "Tài khoản không được tìm thấy";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        //////////////////

        public ActionResult DatLaiMatKhau(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (DACNDbContext dc = new DACNDbContext())
            {
                var user = dc.TaiKhoans.Where(a => a.MaMatKhau == id).FirstOrDefault();
                if (user != null)
                {
                    DatLaiMKModel model = new DatLaiMKModel();
                    model.DatLaiMa = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatLaiMatKhau(DatLaiMKModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (DACNDbContext dc = new DACNDbContext())
                {
                    var user = dc.TaiKhoans.Where(a => a.MaMatKhau == model.DatLaiMa).FirstOrDefault();
                    if (user != null)
                    {
                        user.MatKhau = model.MatKhauMoi;
                        user.MaMatKhau = "";
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "Đã cập nhật thành công mật khẩu mới";
                    }
                }
            }
            else
            {
                message = "Không hợp lệ";
            }
            ViewBag.Message = message;
            return View(model);
        }
        public ActionResult Suathongtin()
        {
            var test = Session["mataikhoan"];
            int id = int.Parse(test.ToString());

            TaiKhoan kh = db.TaiKhoans.SingleOrDefault(n => n.MaTaiKhoan == id && n.MaTaiKhoan != 1);

            if (kh == null)
            {
                return RedirectToAction("DangKy", "Home");
            }
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Suathongtin(TaiKhoan kh)
        {

            TaiKhoan khm = db.TaiKhoans.SingleOrDefault(n => n.MaTaiKhoan == kh.MaTaiKhoan & n.MaTaiKhoan != 1);
            ViewBag.Makh = kh.MaTaiKhoan;

            var dc = kh.DiaChi;
            var sdt = kh.SDT;
            var email = kh.Email;
            var stk = kh.SoTKNganHang;
            var tentk = kh.TenTkNganHang;
            var hoten = kh.HoTen;
            var mota = kh.MoTaTaiKhoan;

            khm.MoTaTaiKhoan = mota;
            khm.HoTen = hoten;
            khm.SoTKNganHang = stk;
            khm.DiaChi = dc;
            khm.SDT = sdt;
            khm.Email = email;
            khm.TenTkNganHang = tentk;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChanglePassword()
        {
            var test = Session["mataikhoan"];
            int id = int.Parse(test.ToString());

            TaiKhoan kh = db.TaiKhoans.SingleOrDefault(n => n.MaTaiKhoan == id && n.MaTaiKhoan != 1);

            if (kh == null)
            {
                return RedirectToAction("DangKy", "Home");
            }
            return View(kh);
        }
        public ActionResult SuaMoTa()
        {
            var test = Session["mataikhoan"];
            int id = int.Parse(test.ToString());

            TaiKhoan kh = db.TaiKhoans.SingleOrDefault(n => n.MaTaiKhoan == id && n.MaTaiKhoan != 1);

            if (kh == null)
            {
                return RedirectToAction("DangKy", "Home");
            }
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult SuaMoTa(TaiKhoan kh)
        {

            TaiKhoan khm = db.TaiKhoans.SingleOrDefault(n => n.MaTaiKhoan == kh.MaTaiKhoan & n.MaTaiKhoan != 1);
            ViewBag.Makh = kh.MaTaiKhoan;
            var mota = kh.MoTaTaiKhoan;
            khm.MoTaTaiKhoan = mota;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult ChanglePassword(FormCollection collection)
        {
            var test = Session["mataikhoan"];
            int id = int.Parse(test.ToString());
            var PwdOld = collection["PwdOld"];
            var kh = db.TaiKhoans.Where(a => a.MaTaiKhoan == id).FirstOrDefault();
            var OldPwd = kh.MatKhau;
            var NewPwd = collection["NewPwd"];
            var ComfirmPwd = collection["ComfirmPwd"];
            if (PwdOld == OldPwd && NewPwd == ComfirmPwd)
            {
                kh.MatKhau = NewPwd;
                db.SaveChanges();
            }
            else
            {
                if (PwdOld != OldPwd)
                {
                    ViewBag.ThongBao = "Mật khẩu cũ không đúng!!!";
                    return View("ChanglePassword");
                }
                else
                {
                    ViewBag.ThongBao = "Nhập mật khẩu lại chưa chính xác";
                    return View("ChanglePassword");
                }
            }
            return View("Index");
        }

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

        public List<SanPham> CheckSanPham(string search)
        {
            List<SanPham> temp = new List<SanPham>();
            var listSevice = (from sv in db.SanPhams select sv).ToList();
            foreach(var item in listSevice)
            {
                var tempMota = LocDau(item.MotaSP);
                var tempTen = LocDau(item.TenSP);
                var tempSearch = LocDau(search);
                if(tempSearch!=null && tempSearch!="")
                {
                    if (tempMota.Contains(tempSearch) || tempTen.Contains(tempSearch))
                        temp.Add(item);
                }
                
            }
            return temp;
        }
        public ActionResult Search(int id = 0, String search = "")
        {
            
            DanhSachBaiDang danhSachBaiDang = new DanhSachBaiDang();

            var listSevice = CheckSanPham(search);
            List<TaiKhoan> tk = new List<TaiKhoan>();
            List<HinhAnh> ha = new List<HinhAnh>();
            foreach (var item in listSevice)
            {
                var temptk = db.TaiKhoans.Where(a => a.MaTaiKhoan == item.MaTaiKhoan).FirstOrDefault();
                var tempha = db.HinhAnhs.Where(a => a.MaTaiKhoan == item.MaTaiKhoan || a.MaSP == item.MaSP).ToList();
                tk.Add(temptk);
                ha.AddRange(tempha);
            }
            danhSachBaiDang.SanPham = listSevice;
            danhSachBaiDang.TenHinhDaiDien = ha;
            danhSachBaiDang.TaiKhoan = tk;
            return View(danhSachBaiDang);

        }
        public ActionResult SearchDrop(String cateID, String search = "")
        {
            DanhSachBaiDang danhSachBaiDang = new DanhSachBaiDang();
            int id = int.Parse(cateID);
            var test2 = (from c in db.CTDMs
                                where c.MaCTDM == id
                                select c.MaDM).FirstOrDefault();
            var test = (from c in db.CTDMs
                        where c.MaDM == test2
                        select c).ToList();
            var listSevice = (from sv in db.SanPhams where sv.MotaSP.Contains(search) || sv.MaCTDM ==id select sv).ToList();
            List<TaiKhoan> tk = new List<TaiKhoan>();
            List<HinhAnh> ha = new List<HinhAnh>();
            foreach (var item in listSevice)
            {
                var temptk = db.TaiKhoans.Where(a => a.MaTaiKhoan == item.MaTaiKhoan).FirstOrDefault();
                var tempha = db.HinhAnhs.Where(a => a.MaTaiKhoan == item.MaTaiKhoan || a.MaSP == item.MaSP).ToList();
                tk.Add(temptk);
                ha.AddRange(tempha);
            }
            danhSachBaiDang.SanPham = listSevice;
            danhSachBaiDang.TenHinhDaiDien = ha;
            danhSachBaiDang.TaiKhoan = tk;
            return View(danhSachBaiDang);

        }

    }    
}
