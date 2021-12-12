using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowBetter_WebApp.Models
{
    public interface IEmailService
    {
        void Send(EmailMessage message);
    }
}
