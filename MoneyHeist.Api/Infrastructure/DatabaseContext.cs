using MoneyHeist.Api.Infrastructure.Interfaces;

namespace MoneyHeist.Api.Infrastructure
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        private const string HttpContextItemsConnectionStringKey = "ConnectionString";

        public DatabaseContext(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetDefaultConnectionString()
        {
            if (_httpContextAccessor?.HttpContext?.Items?.ContainsKey(HttpContextItemsConnectionStringKey) ?? false)
            {
                string? providedConnectionString = _httpContextAccessor?.HttpContext?.Items[HttpContextItemsConnectionStringKey]?.ToString();

                if (!string.IsNullOrWhiteSpace(providedConnectionString))
                {
                    return providedConnectionString;
                }
            }

            return _configuration.GetConnectionString("DefaultConnection");
        }

		public void SetDefaultConnectionString(string conectionString)
        {
            if (_httpContextAccessor?.HttpContext?.Items == null)
            {
                return;
            }

            _httpContextAccessor.HttpContext.Items.Add(HttpContextItemsConnectionStringKey, conectionString);
        }
    }
}
