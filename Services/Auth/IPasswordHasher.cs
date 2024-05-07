namespace WebApplication1.Services.Auth
{
    public interface IPasswordHasher
    {
        public string Generate(string password);
        public void SetSalt(string salt);
    }
}
