namespace PastryShop.Api.Registrars
{
    public interface IWebApplicationRegistrar : IRegistrar
    {
        void RegisterPipelineComponents(WebApplication app);
    }
}
