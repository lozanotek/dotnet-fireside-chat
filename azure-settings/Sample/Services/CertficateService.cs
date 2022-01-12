using System.Security.Cryptography.X509Certificates;

public class CertficateService : ICertificateService
{
    private readonly IConfiguration configuration;

    public CertficateService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    // borrowed from: https://github.com/dotnet/aspnetcore/blob/v5.0.13/src/Identity/ApiAuthorization.IdentityServer/src/Configuration/ConfigureSigningCredentials.cs
    public string GetDefaultPublicKey()
    {
        var certInfo = GetCertInfo();

        if (!Enum.TryParse<StoreLocation>(certInfo.StoreLocation, out var storeLocation))
        {
            throw new InvalidOperationException($"Invalid certificate store location '{certInfo.StoreLocation}'.");
        }

        var certificate = SigningKeysLoader.LoadFromStoreCert(certInfo.Name, certInfo.StoreName, storeLocation, DateTimeOffset.UtcNow);
        var publicKey = certificate.GetPublicKeyString();

        return publicKey;
    }

    private CertInfo GetCertInfo()
    {
        var info = new CertInfo();
        configuration.Bind("Sample:Cert", info);

        return info;
    }
}
