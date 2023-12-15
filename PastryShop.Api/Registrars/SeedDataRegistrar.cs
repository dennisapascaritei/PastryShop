using Microsoft.AspNetCore.Identity;

namespace PastryShop.Api.Registrars
{
    public class SeedDataRegistrar : IWebApplicationAsyncRegistrar
    {
        public async Task RegisterPipelineComponents(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Manager", "Member" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
