using EntityLayer;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Certificate.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly Context _context;

        public AdminController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult SaveManualData(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                ViewBag.Message = "Veri başarıyla kaydedildi!";
            }
            else
            {
                ViewBag.Message = "Hata: Girdiğiniz veriler geçersiz!";
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel()
        {
            var file = Request.Form.Files.FirstOrDefault();

            if (file != null && file.Length > 0)
            {
                var fileExtension = Path.GetExtension(file.FileName);

                
                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    using (var stream = new MemoryStream())
                    {
                       
                        await file.CopyToAsync(stream);

                        using (var package = new ExcelPackage(stream))
                        {
                          
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                            if (worksheet != null)
                            {
                                int rowCount = worksheet.Dimension.Rows;

                                for (int row = 2; row <= rowCount; row++)
                                {
                                    DateTime? tarih1 = worksheet.Cells[row, 4].GetValue<DateTime?>();
                                    DateTime? tarih2 = worksheet.Cells[row, 5].GetValue<DateTime?>();

                                    Student student = new Student
                                    {
                                        TC = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                                        AdSoyad = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                                        EğitimAdi = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                                        DateTime = tarih1 ?? DateTime.MinValue,
                                        DateTime2 = tarih2 ?? DateTime.MinValue
                                    };

                                    _context.Students.Add(student);
                                }

                                
                                _context.SaveChanges();
                                ViewBag.Message = "Excel'den veriler başarıyla yüklendi!";
                            }
                            else
                            {
                                ViewBag.Message = "Excel dosyasında sayfa bulunamadı!";
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "Lütfen geçerli bir Excel dosyası (.xls veya .xlsx) seçin.";
                }
            }
            else
            {
                ViewBag.Message = "Lütfen geçerli bir Excel dosyası seçin.";
            }

            return View("Index");
        }

    }
}
