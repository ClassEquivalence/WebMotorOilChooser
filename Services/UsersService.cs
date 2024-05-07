using WebApplication1.Models.Users;
using WebApplication1.Services.Auth;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;
using System.Security.Cryptography;


namespace WebApplication1.Services
{
    public class UsersService
    {
        IPasswordHasher _passwordHasher;
        ApplicationContext _db;
        int sessionIdLength = 64;
        public static int SessionServiceDays = 30;
        public UsersService(IPasswordHasher passwordHasher, ApplicationContext db)
        {
            _passwordHasher = passwordHasher;
            _db = db;
            User._usersService = this;
        }
        User? UserExists(string login)
        {
            var users = (from user in _db.Users
                         where user.Login == login
                         select user).ToList();
            if (users.Count() == 1)
                return users.First();
            else if (users.Count() == 0)
                return null;
            else
                throw new Exception("Пользователей с указанным логином больше 1-го");
        }
        public User? Register(string username, string login, string password)
        {
            if (UserExists(login)==null)
                return new User(username, login, _passwordHasher.Generate(password));
            else
                return null;
        }
        public User? Login(string login, string password)
        {
            var users = (from user in _db.Users
                         where user.Login == login 
                         where user.PasswordHash == _passwordHasher.Generate(password)
                         select user).ToList();
            User? u = UserExists(login);
            if (u != null && u.PasswordHash == _passwordHasher.Generate(password)) 
            {
                return u;
            }
            else
                return null;
        }
        public string GenerateSessionId()
        {
            return RandomNumberGenerator.GetHexString(sessionIdLength);
        }
    }
}
