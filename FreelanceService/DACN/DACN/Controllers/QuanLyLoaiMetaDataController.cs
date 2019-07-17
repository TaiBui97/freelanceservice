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
    public class QuanLyLoaiMetaDataController : Controller
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
        // GET:  QuanLyLoaiMetaData
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            return View(db.LoaiMetaData.ToList().OrderBy(n => n.MaLoaiMetaData).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(LoaiMetaData loaimd)
        {
            LoaiMetaData tloaimetadata = new LoaiMetaData();
            if (ModelState.IsValid)
            {
                //kiem tra ten loai metadata
                var loaimetadata = db.LoaiMetaData.FirstOrDefault(x => x.TenLoaiMetaData.Trim().ToLower() == loaimd.TenLoaiMetaData.Trim().ToLower());
                if (loaimetadata != null)
                {
                    ModelState.AddModelError("TenLoaiMetaData", "Tên loại metadata này đã tồn tại, vui lòng nhập tên khác.");
                    return View();
                }
                else
                {
                    var loaimetadatadesc = db.LoaiMetaData.OrderByDescending(x => x.MaLoaiMetaData).FirstOrDefault();
                    var id = loaimetadatadesc != null ? loaimetadatadesc.MaLoaiMetaData : 1;
                    loaimd.MaLoaiMetaData = id++;
                    db.LoaiMetaData.Add(loaimd);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MaLoaiMetaData = new SelectList(db.LoaiMetaData.ToList().OrderBy(n => n.TenLoaiMetaData), "MaLoaiMetaData", "TenLoaiMetaData");
            return View();
        }
        [HttpGet]
        public ActionResult ChinhSua(int maloaimetadata)
        {
            LoaiMetaData lmt = db.LoaiMetaData.SingleOrDefault(n => n.MaLoaiMetaData == maloaimetadata);
            if (lmt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoaiMetaData = new SelectList(db.LoaiMetaData.ToList().OrderBy(x => x.TenLoaiMetaData), "MaLoaiMetaData", "TenLoaiMetaData");
            return View(lmt);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(LoaiMetaData loaimd)
        {
            if (ModelState.IsValid) 
            {
                //Thực hiện cập nhật trong model
                var loaimetadata = db.LoaiMetaData.FirstOrDefault(x => (x.MaLoaiMetaData != loaimd.MaLoaiMetaData && x.TenLoaiMetaData.Trim().ToLower() == loaimd.TenLoaiMetaData.Trim().ToLower()));
                if (loaimetadata != null)
                {
                    ModelState.AddModelError("TenLoaiMetaData", "Tên loại metadata này đã tồn tại, vui lòng nhập tên khác.");
                    return View(loaimd);
                }
                else
                {
                    db.Entry(loaimd).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            // Đưa dữ liệu vào dropdownlist
            ViewBag.MaLoaiMetaData = new SelectList(db.LoaiMetaData.ToList().OrderBy(n => n.TenLoaiMetaData), "MaLoaiMetaData", "TenLoaiMetaData");
            return RedirectToAction("Index");
        }
        public ActionResult HienThi(int maloaimetadata)
        {
            //Lấy ra đối tượng sách theo mã
            LoaiMetaData lmt = db.LoaiMetaData.SingleOrDefault(n => n.MaLoaiMetaData == maloaimetadata);
            if (lmt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lmt);
        }
        [HttpGet]
        public ActionResult Xoa(int maloaimetadata)
        {
            LoaiMetaData lmt = db.LoaiMetaData.SingleOrDefault(n => n.MaLoaiMetaData == maloaimetadata);
            if (lmt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lmt);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int maloaimetadata)
        {
            LoaiMetaData lmt = db.LoaiMetaData.SingleOrDefault(n => n.MaLoaiMetaData == maloaimetadata);
            if (lmt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.LoaiMetaData.Remove(lmt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}