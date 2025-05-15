namespace UberEats.Core.Domain.Settings
{
    public class MailModel
    {
        public string To { get; set; }
        public string Subject = "Código de Verificación - Completa tu Solicitud";
        public string Body { get; set; }
        public string EnterpriseName  = "DDA Company";
        public string SupportEmail { get; set; }
        public string WebSite { get; set; } = "https://www.DDACOMPANY.net";
    }
}
