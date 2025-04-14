namespace UberEats.Core.Domain.Settings
{
    public class MailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public object Body { get; set; }
        public string EnterpriseName { get; set; }
        public string SupportEmail { get; set; }
        public string WebSite { get; set; }
    }
}
