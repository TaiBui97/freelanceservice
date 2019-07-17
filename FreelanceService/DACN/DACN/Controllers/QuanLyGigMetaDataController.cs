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
    public class QuanLyGigMetaDataController : Controller
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

        public ActionResult Index(int ? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            return View(db.GigMetaDatas.ToList().OrderBy(n => n.MaGigMetaData).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaCTDM = new SelectList(db.CTDMs.ToList().OrderBy(n => n.TenCTDM), "MaCTDM", "TenCTDM");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(GigMetaData gigmeta)
        {
            GigMetaData tengigmeta = new GigMetaData();
            if (ModelState.IsValid)
            {
                //kiem tra ten loai metadata
                var gigmetadata = db.GigMetaDatas.FirstOrDefault(x => x.TenGigMetaData.Trim().ToLower() == gigmeta.TenGigMetaData.Trim().ToLower());
                if (gigmetadata != null)
                {
                    ModelState.AddModelError("GigMetaData", "Tên GigMetaData này đã tồn tại, vui lòng nhập tên khác.");
                    return View();
                }
                else
                {
                    var gigmetadesc = db.GigMetaDatas.OrderByDescending(x => x.MaGigMetaData).FirstOrDefault();
                    var id = gigmetadesc != null ? gigmetadesc.MaGigMetaData : 1;
                    gigmeta.MaGigMetaData = id++;
                    db.GigMetaDatas.Add(gigmeta);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MaGigMetaData = new SelectList(db.GigMetaDatas.ToList().OrderBy(n => n.TenGigMetaData), "MaGigMetaData", "TenGigMetaData");
            return View();
        }
        [HttpGet]
        public ActionResult ChinhSua(int magigmeta)
        {
            GigMetaData lmt = db.GigMetaDatas.SingleOrDefault(n => n.MaGigMetaData == magigmeta);
            if (lmt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCTDM = new SelectList(db.CTDMs.ToList().OrderBy(x => x.TenCTDM), "MaCTDM", "TenCTDM");
            ViewBag.MaGigMetaData = new SelectList(db.GigMetaDatas.ToList().OrderBy(x => x.TenGigMetaData), "MaGigMetaData", "TenGigMetaData");
            return View(lmt);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(GigMetaData gigmeta)
        {
            if (ModelState.IsValid)
            {
                //Thực hiện cập nhật trong model
                var gigmetadata = db.GigMetaDatas.FirstOrDefault(x => (x.MaGigMetaData != gigmeta.MaGigMetaData && x.TenGigMetaData.Trim().ToLower() == gigmeta.TenGigMetaData.Trim().ToLower()));
                if (gigmetadata != null)
                {
                    ModelState.AddModelError("TenGigMetaData", "Tên loại metadata này đã tồn tại, vui lòng nhập tên khác.");
                    return View(gigmeta);
                }
                else
                {
                    db.Entry(gigmeta).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            // Đưa dữ liệu vào dropdownlist
            ViewBag.MaGigMetaData = new SelectList(db.GigMetaDatas.ToList().OrderBy(n => n.TenGigMetaData), "MaGigMetaData", "TenGigMetaData");
            return RedirectToAction("Index");
        }
        public ActionResult HienThi(int magigmeta)
        {
            //Lấy ra đối tượng sách theo mã
            GigMetaData lmt = db.GigMetaDatas.SingleOrDefault(n => n.MaGigMetaData == magigmeta);
            if (lmt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lmt);
        }
        [HttpGet]
        public ActionResult Xoa(int magigmeta)
        {
            GigMetaData lmt = db.GigMetaDatas.SingleOrDefault(n => n.MaGigMetaData == magigmeta);
            if (lmt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lmt);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int magigmeta)
        {
            GigMetaData lmt = db.GigMetaDatas.SingleOrDefault(n => n.MaGigMetaData == magigmeta);
            if (lmt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.GigMetaDatas.Remove(lmt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}