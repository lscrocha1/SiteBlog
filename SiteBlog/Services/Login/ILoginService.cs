namespace SiteBlog.Services.Login;

public interface ILoginService
{
    string Login(string username, string password);
}