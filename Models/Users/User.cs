using WebApplication1.Services;

namespace WebApplication1.Models.Users
{
    public class User : BaseEntity
    {
        public static UsersService _usersService;
        public static string SessionIdCookieName = "SessionId";
        public string UserName {  get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public Role? Role { get; set; }
        public int RoleId {  get; set; }

        public event Action<string> OnSessionIdChanged;

        public string? SessionId { get; set; }
        public DateTime SessionCreatedTime { get; set; }
        //срок валидности идентификатора сессии в секундах
        public User(string userName, string login, string passwordHash)
        {
            UserName = userName;
            Login = login;
            PasswordHash = passwordHash;
        }
        public void TryUpdateSessionId()
        {
            DateTime buf = SessionCreatedTime;
            if(buf!=null)
                buf.AddDays(30);
            if (buf == null || buf < DateTime.Now)
            {
                SessionId = _usersService.GenerateSessionId();
            }
        }
        public void SetSessionCookie(HttpContext context)
        {
            context.Response.Cookies.Append(SessionIdCookieName, SessionId);
        }
    }
}
