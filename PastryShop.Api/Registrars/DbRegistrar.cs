using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PastryShop.Dal;

namespace PastryShop.Api.Registrars
{
    public class DbRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            //Add data context
            builder.Services.AddDbContext<DataContext>(options =>
                {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

            builder.Services
                .AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();
        }
    }
}
