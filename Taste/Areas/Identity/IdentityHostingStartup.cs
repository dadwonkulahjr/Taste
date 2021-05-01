using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(Taste.Areas.Identity.IdentityHostingStartup))]
namespace Taste.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}