using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowBetter_WebApp.Models;

namespace KnowBetter_WebApp.Controllers
{
    public class ContactFormController : Controller
    {

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        private EmailAddress FromAndToEmailAddress;
        private IEmailService EmailService;
        public ContactFormController(EmailAddress _fromAddress,
            IEmailService _emailService)
        {
            FromAndToEmailAddress = _fromAddress;
            EmailService = _emailService;
        }

        [HttpPost]
        public IActionResult Index(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                EmailMessage msgToSend = new EmailMessage
                {
                    FromAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                    ToAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                    Content = $"Here is your message: Name: {model.Name}, " +
                        $"Email: {model.Email}, Message: {model.Message}",
                    Subject = "Contact Form - BasicContactForm App"
                };

                EmailService.Send(msgToSend);
                return RedirectToAction("Index");
            }
            else
            {
                return Index();
            }
        }
    }
}
