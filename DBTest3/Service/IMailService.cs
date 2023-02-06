namespace DBTest3.Service
{
    public interface IMailService
    {
        public void SendNewUserMail(string password, string email);
    }
}
