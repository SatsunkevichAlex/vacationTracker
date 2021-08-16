namespace API.JwtFeatures.Options
{
    public class JwtOptions
    {
        public const string Section = "JwtSettings";

        public string SecurityKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}
