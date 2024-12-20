using Txt.Ui.Services;
using Txt.Ui.Services.HttpClients;
using Txt.Ui.Services.HttpClients.Interfaces;
using Txt.Ui.Services.Interfaces;

namespace Txt.Ui.Helpers;
public static class DependencyInjectionHelper
{
    public static IServiceCollection AddLocalServices(this IServiceCollection services)
    {
        services.AddScoped<IPublicClientService, PublicClientService>();
        services.AddScoped<ITxtApiClientService, TxtApiClientService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<INotesService, NotesService>();

        return services;
    }
}