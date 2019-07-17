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
    public class QuanLyMetaDataController : Controller
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
        // GET: QuanLyMetaData
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            return View(db.MetaDatas.ToList().OrderBy(n => n.MaMetaData).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaCTDM = new SelectList(db.CTDMs.ToList().OrderBy(n => n.TenCTDM), "MaCTDM", "TenCTDM");
            ViewBag.MaLoaiMetaData = new SelectList(db.LoaiMetaData.ToList().OrderBy(n => n.TenLoaiMetaData), "MaLoaiMetaData", "TenLoaiMetaData");
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(MetaData md)
        {
            ViewBag.MaCTDM = new SelectList(db.CTDMs.ToList().OrderBy(n => n.TenCTDM), "MaCTDM", "TenCTDM");
            ViewBag.MaLoaiMetaData = new SelectList(db.LoaiMetaData.ToList().OrderBy(n => n.TenLoaiMetaData), "MaLoaiMetaData", "TenLoaiMetaData");
            ViewBag.MaMetaData = new SelectList(db.MetaDatas.ToList().OrderBy(n => n.TenMetaData), "MaMetaData", "TenMetaData");
            if (ModelState.IsValid)
            {
                //kiem tra ten metadata
                var metadtata = db.MetaDatas.FirstOrDefault(x => x.TenMetaData.Trim().ToLower() == md.TenMetaData.Trim().ToLower());
                if (metadtata != null)
                {
                    ModelState.AddModelError("TenMetaData", "Tên metadata này đã tồn tại, vui lòng nhập tên khác.");
                    return View();
                }
                else
                {
                    var metadtatadesc = db.MetaDatas.OrderByDescending(x => x.MaMetaData).FirstOrDefault();
                    var id = metadtatadesc != null ? metadtatadesc.MaMetaData : 1;
                    md.MaMetaData = id++;
                    db.MetaDatas.Add(md);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult ChinhSua(int mamd)
        {
            MetaData md = db.MetaDatas.SingleOrDefault(n => n.MaMetaData == mamd);
            if (md == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCTDM = new SelectList(db.CTDMs.ToList().OrderBy(n => n.TenCTDM), "MaCTDM", "TenCTDM");
            ViewBag.MaLoaiMetaData = new SelectList(db.LoaiMetaData.ToList().OrderBy(n => n.TenLoaiMetaData), "MaLoaiMetaData", "TenLoaiMetaData");
            ViewBag.MaMetaData = new SelectList(db.MetaDatas.ToList().OrderBy(n => n.TenMetaData), "MaMetaData", "TenMetaData");
            return View(md);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(MetaData md)
        {
            if (ModelState.IsValid)
            {
                var metadata = db.MetaDatas.FirstOrDefault((x => x.MaMetaData != md.MaMetaData && x.TenMetaData.Trim().ToLower() == md.TenMetaData.Trim().ToLower()));
                if (metadata != null)
                {
                    ViewBag.MaCTDM = new SelectList(db.CTDMs.ToList().OrderBy(n => n.TenCTDM), "MaCTDM", "TenCTDM");
                    ViewBag.MaLoaiMetaData = new SelectList(db.LoaiMetaData.ToList().OrderBy(n => n.TenLoaiMetaData), "MaLoaiMetaData", "TenLoaiMetaData");
                    ModelState.AddModelError("TenMetaData", "Tên metadata này đã tồn tại, vui lòng nhập tên khác.");
                    return View();
                }
                else
                {
                    db.Entry(md).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            // Đưa dữ liệu vào dropdownlist
            ViewBag.MaMetaData = new SelectList(db.MetaDatas.ToList().OrderBy(n => n.TenMetaData), "MaMetaData", "TenMetaData");
            return RedirectToAction("Index");
        }
        public ActionResult HienThi(int mamd)
        {
            //Lấy ra đối tượng sách theo mã
            MetaData md = db.MetaDatas.SingleOrDefault(n => n.MaMetaData == mamd);
            if (md == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(md);
        }
        [HttpGet]
        public ActionResult Xoa(int mamd)
        {
            MetaData md = db.MetaDatas.SingleOrDefault(n => n.MaMetaData == mamd);
            if (md == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(md);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int mamd)
        {
            MetaData md = db.MetaDatas.SingleOrDefault(n => n.MaMetaData == mamd);
            if (md == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.MetaDatas.Remove(md);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}