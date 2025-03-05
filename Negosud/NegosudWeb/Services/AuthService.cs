namespace NegosudWeb.Services
{
    public class AuthService
    {
        public bool IsLoggedIn { get; private set; }
        public int? UserId { get; private set; }

        public void Login(int userId)
        {
            UserId = userId;
            IsLoggedIn = true;
        }

        public void Logout()
        {
            UserId = null;
            IsLoggedIn = false;
        }
    }
}
