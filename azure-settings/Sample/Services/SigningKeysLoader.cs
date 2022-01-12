using System.Security.Cryptography.X509Certificates;

// source: https://github.com/dotnet/aspnetcore/blob/v5.0.13/src/Identity/ApiAuthorization.IdentityServer/src/Configuration/SigningKeysLoader.cs
internal static class SigningKeysLoader
{
    
    public static X509Certificate2 LoadFromStoreCert(
        string subject,
        string storeName,
        StoreLocation storeLocation,
        DateTimeOffset currentTime)
    {
        using (var store = new X509Store(storeName, storeLocation))
        {
            X509Certificate2Collection? storeCertificates = null;
            X509Certificate2? foundCertificate = null;

            try
            {
                store.Open(OpenFlags.ReadOnly);
                storeCertificates = store.Certificates;
                var foundCertificates = storeCertificates
                    .Find(X509FindType.FindBySubjectDistinguishedName, subject, validOnly: false);

                foundCertificate = foundCertificates
                    .OfType<X509Certificate2>()
                    .Where(certificate => certificate.NotBefore <= currentTime && certificate.NotAfter > currentTime)
                    .OrderBy(certificate => certificate.NotAfter)
                    .FirstOrDefault();

                if (foundCertificate == null)
                {
                    throw new InvalidOperationException("Couldn't find a valid certificate with " +
                        $"subject '{subject}' on the '{storeLocation}\\{storeName}'");
                }

                return foundCertificate;
            }
            finally
            {
                DisposeCertificates(storeCertificates, except: foundCertificate);
            }
        }
    }

    private static void DisposeCertificates(X509Certificate2Collection? certificates, X509Certificate2? except)
    {
        if (certificates != null)
        {
            foreach (var certificate in certificates)
            {
                if (!certificate.Equals(except))
                {
                    certificate.Dispose();
                }
            }
        }
    }
}
