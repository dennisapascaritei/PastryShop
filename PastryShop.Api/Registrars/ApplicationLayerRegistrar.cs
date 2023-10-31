using PastryShop.Application.Services;

namespace PastryShop.Api.Registrars
{
    public class ApplicationLayerRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IdentityService>();
        }
    }
}
