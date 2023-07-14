namespace AdminBaker.Services;

public static class Utils
{
    public static string ParseUserName(string email)
    {
        var userName = email.Split('@')[0];
        return userName;
    }
}