
namespace AspNet5.Core.Configuration
{
    public class AppSettings
    {
        public string ConnectionStrings { get; set; }
        public string Logging { get; set; }
        public JwtIssuerOptions JwtIssuerOptions { get; set; }
    }
}
