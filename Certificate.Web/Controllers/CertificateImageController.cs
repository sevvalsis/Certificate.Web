using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Certificate.Web.Controllers
{
    public class CertificateImageController : Controller
    {
        public IActionResult Index(string AdSoyad, string EğitimAdi, string DateTime, string DateTime2)
        {
            string certificateImagePath = GetCertificateImagePath();

            if (!System.IO.File.Exists(certificateImagePath))
            {
                return NotFound($"Sertifika görseli {certificateImagePath} bulunamadı.");
            }

            ViewBag.AdSoyad = AdSoyad;
            ViewBag.EğitimAdi = EğitimAdi;
            ViewBag.DateTime = DateTime;
            ViewBag.DateTime2 = DateTime2;

            return View();
        }

        public IActionResult DownloadImage(string AdSoyad, string EğitimAdi, string DateTime, string DateTime2)
        {
            string certificateImagePath = GetCertificateImagePath();

            if (!System.IO.File.Exists(certificateImagePath))
            {
                return NotFound($"Sertifika görseli {certificateImagePath} bulunamadı.");
            }

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(certificateImagePath))
                {
                    DrawCertificateText(bitmap, AdSoyad, EğitimAdi, DateTime, DateTime2);

                    using (var stream = new MemoryStream())
                    {
                        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        stream.Seek(0, SeekOrigin.Begin);

                        return File(stream.ToArray(), "image/jpeg", "Sertifika.jpg");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Görsel işlenirken bir hata oluştu: {ex.Message}");
            }
        }

        public IActionResult DownloadPDF(string AdSoyad, string EğitimAdi, string DateTime, string DateTime2)
        {
            string certificateImagePath = GetCertificateImagePath();

            if (!System.IO.File.Exists(certificateImagePath))
            {
                return NotFound($"Sertifika görseli {certificateImagePath} bulunamadı.");
            }

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(certificateImagePath))
                {
                    DrawCertificateText(bitmap, AdSoyad, EğitimAdi, DateTime, DateTime2);

                    using (var memoryStream = new MemoryStream())
                    {
                        bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        memoryStream.Seek(0, SeekOrigin.Begin);

                        return CreatePdfFromImage(bitmap, memoryStream);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"PDF oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }

        private IActionResult CreatePdfFromImage(System.Drawing.Bitmap bitmap, MemoryStream memoryStream)
        {
            var imgSize = bitmap.Size;
            using (var pdfStream = new MemoryStream())
            {
                var pageSize = new iTextSharp.text.Rectangle(0, 0, imgSize.Width, imgSize.Height);
                var document = new Document(pageSize);
                PdfWriter.GetInstance(document, pdfStream);
                document.Open();

                var img = iTextSharp.text.Image.GetInstance(memoryStream.ToArray());
                img.SetAbsolutePosition(0, 0);
                img.ScaleToFit(pageSize.Width, pageSize.Height);
                document.Add(img);
                document.Close();

                return File(pdfStream.ToArray(), "application/pdf", "Sertifika.pdf");
            }
        }

        private void DrawCertificateText(System.Drawing.Bitmap bitmap, string AdSoyad, string EğitimAdi, string DateTime, string DateTime2)
        {
            using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
            {
                var font1 = new System.Drawing.Font("Libre Baskerville", 40, FontStyle.Bold);
                var smallFont = new System.Drawing.Font("Poppins", 22);
                var paragraphFont = new System.Drawing.Font("Poppins", 30); 

                var blackBrush = new System.Drawing.SolidBrush(System.Drawing.ColorTranslator.FromHtml("#b28c53"));
                var orangeBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

                graphics.DrawString(AdSoyad, font1, blackBrush, new System.Drawing.PointF(250, 415));

                
                string paragraphText = $"İpek Yolu Uluslararası Çocuk ve Gençlik Çalışmaları Girişimci Kuluçka Birimi tarafından {DateTime} - {DateTime2} tarihleri arasında düzenlenen {EğitimAdi} eğitimini başarıyla tamamlayarak bu belgeyi almaya hak kazanmıştır.";

             

                var rectF = new RectangleF(100, 550, 1490, 250); 
                graphics.DrawString(paragraphText, paragraphFont, orangeBrush, rectF);
            }
        }

        private string GetCertificateImagePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CerticateTemplate/Images/Sertifika.jpg");
        }
    }
}
