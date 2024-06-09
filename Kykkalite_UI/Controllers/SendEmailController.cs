using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mail;

namespace Kykkalite_UI.Controllers
{
    public class SendEmailController : Controller
    {
        [HttpPost]
        public IActionResult SendEmail([FromBody] object data)
        {
            try
            {
                // Convert data to JSON string
                var jsonData = JsonConvert.SerializeObject(data);

                // Setup email
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("furkansumbul1903@gmail.com");
                mailMessage.To.Add("furkansumbul1903@gmail.com");
                mailMessage.Subject = "JSON Data Notification";
                mailMessage.Body = jsonData;
                mailMessage.IsBodyHtml = false;

                // Send email
                using (var smtpClient = new SmtpClient("smtp.example.com"))
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential("your-email@example.com", "your-password");
                    smtpClient.Port = 587; // Or other port, e.g., 25 or 465
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                }

                return Ok(new { message = "Email sent successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error sending email", details = ex.Message });
            }
        }
    }
}
