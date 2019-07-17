using DACN.Models.BusinessModel;
using DACN.Models.DataModel;
using PagedList;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace DACN.Controllers
{
    public class ChiTietDanhMucController : Controller
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
        // GET: ChiTietDanhMuc
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 15;
            return View(db.CTDMs.ToList().OrderBy(n => n.MaCTDM).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(CTDM ctdm)
        {
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            ViewBag.MaCTDM = new SelectList(db.CTDMs.ToList().OrderBy(n => n.TenCTDM), "MaCTDM", "TenCTDM");
            if (ModelState.IsValid)
            {
                //kiem tra ten chi tiet danh muc
                var chitietdm = db.CTDMs.FirstOrDefault(x => x.TenCTDM.Trim().ToLower() == ctdm.TenCTDM.Trim().ToLower());
                if (chitietdm != null)
                {
                    ModelState.AddModelError("TenCTDM", "Tên chi tiết danh mục đã tồn tại, vui lòng nhập tên khác.");
                    return View();
                }
                else
                {
                    var chitietdmdesc = db.CTDMs.OrderByDescending(x => x.MaCTDM).FirstOrDefault();
                    var id =chitietdmdesc != null ? chitietdmdesc.MaCTDM : 1;
                    ctdm.MaCTDM = id++;
                    db.CTDMs.Add(ctdm);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult ChinhSua(int mactdm)
        {
            CTDM ctdm = db.CTDMs.SingleOrDefault(n => n.MaCTDM == mactdm);
            if (ctdm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCTDM = new SelectList(db.CTDMs.ToList().OrderBy(x => x.TenCTDM), "MaCTDM", "TenCTDM");
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            return View(ctdm);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(CTDM ctdm)
        {
            if (ModelState.IsValid)
            {
                var chitietdm = db.CTDMs.FirstOrDefault((x => x.MaCTDM != ctdm.MaCTDM && x.TenCTDM.Trim().ToLower() == ctdm.TenCTDM.Trim().ToLower()));
                if (chitietdm != null)
                {
                    ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
                    ModelState.AddModelError("TenCTDM", "Tên chi tiết danh mục đã tồn tại, vui lòng nhập tên khác.");
                    return View();
                }
                else
                {
                    db.Entry(ctdm).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            // Đưa dữ liệu vào dropdownlist
            ViewBag.MaCTDM = new SelectList(db.CTDMs.ToList().OrderBy(n => n.TenCTDM), "MaCTDM", "TenCTDM");
            return RedirectToAction("Index");
        }       
        public ActionResult HienThi(int mactdm)
        {
            //Lấy ra đối tượng sách theo mã
            CTDM ctdm = db.CTDMs.SingleOrDefault(n => n.MaCTDM == mactdm);
            if (ctdm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ctdm);
        }
        [HttpGet]
        public ActionResult Xoa(int mactdm)
        {
            CTDM ctdm = db.CTDMs.SingleOrDefault(n => n.MaCTDM == mactdm);
            if (ctdm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ctdm);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int mactdm)
        {
            CTDM ctdm = db.CTDMs.SingleOrDefault(n => n.MaCTDM == mactdm);
            if (ctdm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.CTDMs.Remove(ctdm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}