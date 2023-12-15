namespace PastryShop.Api.Registrars
{
    public interface IWebApplicationAsyncRegistrar : IRegistrar
    {
        Task RegisterPipelineComponents(WebApplication app);
    }
}
