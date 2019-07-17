using DACN.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using DACN.Models.DataModel;

namespace DACN.Controllers
{
    public class QuanLyDanhMucController : Controller
    {
        DACNDbContext db = new DACNDbContext();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["mataikhoan"] != null)
            {
                int mtk = (int)Session["mataikhoan"];
                if (db.PhanQuyens.SingleOrDefault(x => x.MaTaiKhoan == mtk && x.MaQuyen != 1) != null)
                {
                    filterContext.Result = new RedirectResult(Url.Action("Index", "Home"));
                    base.OnActionExecuting(filterContext);
                }
            }
            else
            {
                filterContext.Result = new RedirectResult(Url.Action("Index", "Home"));
                base.OnActionExecuting(filterContext);
            }


        }
        // GET: QuanLyDanhMuc
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            return View(db.DanhMucs.ToList().OrderBy(n => n.MaDM).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(DanhMuc dm)
        {
            DanhMuc tdm = new DanhMuc();
            if (ModelState.IsValid)
            {
                //kiem tra ten danh muc
                var danhmuc = db.DanhMucs.FirstOrDefault(x => x.TenDM.Trim().ToLower() == dm.TenDM.Trim().ToLower());
                if (danhmuc != null)
                {
                    ModelState.AddModelError("TenDM", "Tên danh mục đã tồn tại, vui lòng nhập tên khác.");
                    return View();
                }
                else
                {
                    var danhmucdesc = db.DanhMucs.OrderByDescending(x => x.MaDM).FirstOrDefault();
                    var id = danhmucdesc != null ? danhmucdesc.MaDM : 1;
                    dm.MaDM = id++;
                    db.DanhMucs.Add(dm);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            return View();
        }
        [HttpGet]
        public ActionResult ChinhSua(int madm)
        {
            DanhMuc dm = db.DanhMucs.SingleOrDefault(n => n.MaDM == madm);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(x => x.TenDM), "MaDM", "TenDM");
            return View(dm);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(DanhMuc dm)
        {
            if (ModelState.IsValid)
            {
                //Thực hiện cập nhật trong model
                var danhmuc = db.DanhMucs.FirstOrDefault(x => (x.MaDM != dm.MaDM && x.TenDM.Trim().ToLower() == dm.TenDM.Trim().ToLower()));
                if (danhmuc != null)
                {
                    ModelState.AddModelError("TenDM", "Tên danh mục này đã tồn tại, vui lòng nhập tên khác.");
                    return View(dm);
                }
                else
                {
                    db.Entry(dm).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            // Đưa dữ liệu vào dropdownlist
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            return RedirectToAction("Index");
        }
        public ActionResult HienThi(int madm)
        {
            //Lấy ra đối tượng sách theo mã
            DanhMuc dm = db.DanhMucs.SingleOrDefault(n => n.MaDM == madm);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(dm);
        }
        [HttpGet]
        public ActionResult Xoa (int madm)
        {
            DanhMuc dm = db.DanhMucs.SingleOrDefault(n => n.MaDM == madm);
            if(dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dm);
        }
        [HttpPost,ActionName("Xoa")]
        public ActionResult XacNhanXoa(int madm)
        {
            DanhMuc dm = db.DanhMucs.SingleOrDefault(n => n.MaDM == madm);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DanhMucs.Remove(dm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}