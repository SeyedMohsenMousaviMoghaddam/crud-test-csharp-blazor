using Mc2.CrudTest.Presentation.Client;
using Mc2.CrudTest.Presentation.Client.Services.Customers;
using Mc2.CrudTest.Presentation.Client.Shared;
using Mc2.CrudTest.Presentation.Shared.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Mc2.CrudTest.Presentation.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IAlertService, AlertService>();
            //builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IHttpService, HttpService>();
            //builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
            builder.Services.AddScoped(x => {
                var apiUrl = new Uri("http://localhost:57870");
                return new HttpClient() { BaseAddress = apiUrl };
            });
            builder.Services.AddSingleton<PageHistoryState>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}