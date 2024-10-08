using EntityLayer; 
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Concrete; 
using System.Linq;

namespace Certificate.Web.Controllers
{
    public class CertificateController : Controller
    {
        private readonly Context _context;

        public CertificateController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(string tc = null)
        {
            
            var studentsList = _context.Students.ToList();

            if (!string.IsNullOrEmpty(tc))
            {
                var studentCourses = studentsList.Where(s => s.TC == tc).ToList();
                if (studentCourses.Any())
                {
                    ViewBag.StudentCourses = studentCourses;
                    ViewBag.TC = tc;
                }
                else
                {
                    ViewBag.ErrorMessage = "Bu TC numarasıyla kayıt bulunamadı.";
                }
            }

            return View(new List<Student>());
        }

        public IActionResult ShowCertificate(string tc, string selectedCourse)
        {
            var studentsList = _context.Students.ToList();

            var selectedStudent = studentsList.FirstOrDefault(s => s.TC == tc && s.EğitimAdi == selectedCourse);
            if (selectedStudent != null)
            {
                return RedirectToAction("Index", "CertificateImage", new
                {
                    AdSoyad = selectedStudent.AdSoyad,
                    EğitimAdi = selectedStudent.EğitimAdi,
                    DateTime = selectedStudent.DateTime.ToString("dd.MM.yyyy"),
                    DateTime2 = selectedStudent.DateTime2.ToString("dd.MM.yyyy")
                });
            }
            else
            {
                ViewBag.ErrorMessage = "Aradığınız eğitime katılımınız gerçekleşmemiştir.";
                return RedirectToAction("Index", new { tc });
            }
        }
    }
}

