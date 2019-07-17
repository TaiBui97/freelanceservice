namespace DACN.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DACN.Models.DataModel;

    internal sealed class Configuration : DbMigrationsConfiguration<DACN.Models.BusinessModel.DACNDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DACN.Models.BusinessModel.DACNDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //var quantri = new TaiKhoan()
            //{
            //    TenTaiKhoan = "quantri",
            //    MatKhau = "123456",
            //    Email = "nguyenmy.hutech@gmail.com",
            //    SDT = "0918765498",
            //    DiaChi = "43 DienBien Phu",
            //    SoTien = "10000"
            //};
            //context.TaiKhoans.Add(quantri);
            //var nguoidung01 = new TaiKhoan()
            //{
            //    TenTaiKhoan = "nguoidung01",
            //    MatKhau = "123456",
            //    Email = "nguyenmy.hutech@gmail.com",
            //    SDT = "0918765498",
            //    DiaChi = "43 DienBien Phu",
            //    SoTien = "10000",
            //    SoTKNganHang = "3803205050501",
            //    TenTkNganHang = "Agribank",
            //};
            //context.TaiKhoans.Add(nguoidung01);
            //context.SaveChanges();
            //var quyen = new Quyen()
            //{
            //    TenQuyen = "QuanTri"
            //};
            //context.Quyens.Add(quyen);
            //var quyen2 = new Quyen()
            //{
            //    TenQuyen = "Nguoidung"
            //};
            //context.Quyens.Add(quyen2);
            //context.SaveChanges();

            //for (int i = 1; i <= 2; i++)
            //{
            //    var phanquyen = new PhanQuyen()
            //    {
            //        MaQuyen = i,
            //        MaTaiKhoan = i
            //    };
            //    context.PhanQuyens.Add(phanquyen);
            //}  
            //var hinhanh = new HinhAnh()
            //{
            //    TenHinhAnh = "45201157_687045238348145_7760625910935453696_n.jpg",
            //    MaTaiKhoan= 1
                
            //};
            //context.HinhAnhs.Add(hinhanh);
            ////base.Seed(context);
            //context.SaveChanges();

        }
    }
}
