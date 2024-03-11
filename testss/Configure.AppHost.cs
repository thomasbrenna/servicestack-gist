using Funq;
using ServiceStack.Logging;
using ServiceStack.Text;
using ServiceStack.Validation;
using ServiceStack.Logging.Serilog;
using Serilog;

using testss.ServiceModel;
using testss.ServiceInterface;


[assembly: HostingStartup(typeof(testss.AppHost))]

namespace testss;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            // Configure ASP.NET Core IOC Dependencies
        });

    public AppHost() : base("testss", typeof(MyServices).Assembly) { }

    public override void Configure(Container container)
    {
        // Configure ServiceStack, Run custom logic after ASP.NET Core Startup
        if (!HostingEnvironment.IsDevelopment())
        {
            Plugins.RemoveAll(x => x is UiFeature or MetadataFeature);
        }

        // enable server-side rendering, see: https://sharpscript.net/docs/sharp-pages
        Plugins.Add(new SharpPagesFeature
        {
            EnableSpaFallback = true
        });

        Plugins.Add(new CorsFeature(
            allowCredentials: true,
            allowOriginWhitelist: new List<string> { "http://localhost:9999" },
            allowedMethods: "GET, POST, PATCH, PUT, OPTIONS"
        ));

        SetConfig(new HostConfig
        {
            AddRedirectParamsToQueryString = true,
            UseSameSiteCookies = false,
            UseSecureCookies = true
        });

        JsConfig.Init(new ServiceStack.Text.Config
        {
            DateHandler = DateHandler.ISO8601,
            AlwaysUseUtc = false,
            TextCase = TextCase.CamelCase,
            ExcludeDefaultValues = false,        // e.g. IsStartupItem=false won't be emitted unless ==true
            IncludeNullValues = false
        });

        LogManager.LogFactory = new SerilogFactory(new Serilog.LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(Path.Combine(AppSettings.Get("LogPath", "./"), "mylog.txt"),
                rollingInterval: RollingInterval.Day)
            .CreateLogger());

        container.RegisterValidators(typeof(ListEntryValidator).Assembly);
    }
}