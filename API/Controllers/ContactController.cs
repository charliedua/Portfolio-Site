using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(string name, string emailTo, string comment)
        {
            return new string[] { name, emailTo, comment };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromForm] string name, [FromForm] string emailTo, [FromForm] string comment)
        {
            string emailFrom, password;

            // file not included in the repository.
            // File looks like this:
            /*
             * 1. | sample@example.com
             * 2. | passwsord
             */
            FileStream file = new FileStream(Environment.CurrentDirectory + "/url.config", FileMode.Open);

            using(StreamReader sr = new StreamReader(file))
            {
                emailFrom = sr.ReadLine();
                password = sr.ReadLine();
            }

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;

            // Credentials are stored on server and for security purpose are not shared in the repo.
            SmtpServer.Credentials =
                new System.Net.NetworkCredential(emailFrom, password);

            // mail sent via ssl
            SmtpServer.EnableSsl = true;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            MailMessage mail = new MailMessage();
             
            mail.From = new MailAddress("duaanmol2@gmail.com");
            mail.To.Add("duaanmol2@gmail.com");
            mail.Subject = "Contact Requested from your website";
            mail.Body = $"The person {name}, \n" +
                $"Email: {emailTo},\n"+
                $"Said: {comment}";

            SmtpServer.Send(mail);
        }
    }
}
