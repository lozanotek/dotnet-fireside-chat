
// source: https://github.com/dotnet/aspnetcore/blob/v5.0.13/src/Identity/ApiAuthorization.IdentityServer/src/Configuration/KeyDefinition.cs
internal class CertInfo
{
    public string Password { get; set; }
    public string Name { get; set; }
    public string StoreLocation { get; set; }
    public string StoreName { get; set; }
}