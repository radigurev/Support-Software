using System.Net.Mail;
using System.Net;

namespace DBTest3.Service
{
    public class MailService : IMailService
    {

        const string from = "lidiyaDiplomenProekt@outlook.com";

        const string username = "lidiyaDiplomenProekt@outlook.com";
        const string password = "aaA@1111";

        const string host = "smtp.office365.com";
        const int port = 587;

        public void SendNewUserMail(string password, string email)
        {
            string body = $"Беше ви създаден нов профил в нашата система! \n Username: {email} \n Password: {password}";
            
            SendEmail("Нова профил", body, email);
        }

        //readonly SmtpClient EmailClient = new SmtpClient(host, port)
        //{
        //    UseDefaultCredentials = false,
        //    Credentials = new NetworkCredential(username, password),
        //    EnableSsl = true
        //};


        private void SendEmail(string subject, string body,string to)
        {
            var EmailClient = new SmtpClient(host, port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            EmailClient.Send(from, to, subject, body);
        }
    }
}
