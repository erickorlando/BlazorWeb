namespace AdminBaker.Entities.Configuration;

public class AppConfig
{
    public Storageconfiguration StorageConfiguration { get; set; } = null!;

    public Jwt Jwt { get; set; } = null!;

    public Smtpconfiguration SmtpConfiguration { get; set; } = null!;

}

public class Storageconfiguration
{
    public string PublicUrl { get; set; } = null!;
    public string Path { get; set; } = null!;
}


public class Jwt
{
    public string SecretKey { get; set; } = default!;

    public string Audiencia { get; set; } = default!;

    public string Emisor { get; set; } = default!;
}

public class Smtpconfiguration
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public int PortNumber { get; set; }
    public string FromName { get; set; } = default!;
    public string Server { get; set; } = default!;
    public bool EnableSsl { get; set; }
}
