using WebApplication1.Services;

namespace WebApplication1.Models.Users
{
    public class User : BaseEntity
    {
        public static UsersService _usersService;
        public string UserName {  get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public Permission? Permission { get; set; }
        public int PermissionId {  get; set; }
        public Role? Role { get; set; }
        public int RoleId {  get; set; }

        public event Action<string> OnSessionIdChanged;
        public HttpContext context;
        public string? SessionId 
        {
            get => SessionId;
            set 
            {
                SessionId = value;
                OnSessionIdChanged?.Invoke(value);
            }
        }
        public DateTime SessionCreatedTime { get; set; }
        //срок валидности идентификатора сессии в секундах
        public User(string userName, string login, string passwordHash)
        {
            UserName = userName;
            Login = login;
            PasswordHash = passwordHash;
            OnSessionIdChanged += (string sessionId) => SetSessionCookie(context, sessionId);
        }
        public void TryUpdateSessionId()
        {
            DateTime buf = SessionCreatedTime;
            buf.AddDays(30);
            if (buf < DateTime.Now)
            {
                SessionId = _usersService.GenerateSessionId();
            }
        }
        protected void SetSessionCookie(HttpContext context, string sessionId)
        {
            context.Response.Cookies.Append("SessionID", sessionId);
        }
    }
}
