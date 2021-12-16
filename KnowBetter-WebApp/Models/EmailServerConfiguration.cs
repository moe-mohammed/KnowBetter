namespace KnowBetter_WebApp.Models
{
    public class EmailServerConfiguration
    {
        public EmailServerConfiguration(int _smtpPort = 587)
        {
            SmtpPort = _smtpPort;
        }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
