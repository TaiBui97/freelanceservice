using DACN.Models.BusinessModel;
using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DACNDbContext>
    {
        protected override void Seed(DACNDbContext context)
        {
            var quantri = new TaiKhoan()
            {
                TenTaiKhoan = "quantri",
                MatKhau = "123456",
                Email = "nguyenmy.hutech@gmail.com",
                SDT ="0918765498",
                DiaChi="43 DienBien Phu",
                SoTien= "10000",
                SoTKNganHang = "3803205050501",
                TenTkNganHang = "Agribank",
            };
            context.TaiKhoans.Add(quantri);
            context.SaveChanges();
            var nguoidung01 = new TaiKhoan()
            {
                TenTaiKhoan = "nguoidung01",
                MatKhau = "123456",
                Email = "nguyenmy.hutech@gmail.com",
                SDT = "0918765498",
                DiaChi = "43 DienBien Phu",
                SoTien = "10000",
                SoTKNganHang = "3803205050501",
                TenTkNganHang = "Agribank",
            };
            context.TaiKhoans.Add(nguoidung01);
            context.SaveChanges();
            var quyen = new Quyen()
            {
                TenQuyen = "QuanTri"
            };
            context.Quyens.Add(quyen);
            context.SaveChanges();
            var quyen2 = new Quyen()
            {
                TenQuyen = "Nguoidung"
            };
            context.Quyens.Add(quyen2);
            context.SaveChanges();
            
            for(int i=1;i<=2;i++)
            {
                var phanquyen = new PhanQuyen()
                {
                    MaQuyen = i,
                    MaTaiKhoan = i
                };
                context.PhanQuyens.Add(phanquyen);
            }
            //base.Seed(context);
            context.SaveChanges();
        }
    }
}