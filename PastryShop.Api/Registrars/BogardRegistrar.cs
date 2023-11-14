
namespace PastryShop.Api.Registrars
{
    public class BogardRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program), typeof(GetAllUserProfilesQuery));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllUserProfilesQuery).Assembly));
        }
    }
}
