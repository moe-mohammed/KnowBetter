using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public ViewResult Thanks()
        {
            return View("Thanks");
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
                        $"Email: {model.Email}, \n Message: {model.Message}",
                    Subject = "Contact Form - KnowBetter App"
                };

                EmailService.Send(msgToSend);
                return RedirectToAction("Thanks");
            }
            else
            {
                return Index();
            }
        }
    }
}
