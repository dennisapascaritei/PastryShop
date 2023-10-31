using Microsoft.EntityFrameworkCore;
using PastryShop.Dal;

namespace PastryShop.Api.Registrars
{
    public class DbRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            //Add data context
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
        }
    }
}
