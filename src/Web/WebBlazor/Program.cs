using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Refit;

using WebBlazor;
using WebBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Google", options.ProviderOptions);
});
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AuthMessageHandler>();
builder.Services.AddRefitClient<IResizeService>()
    .ConfigureHttpClient(cl => cl.BaseAddress = new Uri(builder.Configuration.GetConnectionString("Backend")!))
    .AddHttpMessageHandler<AuthMessageHandler>();
builder.Services.AddRefitClient<IImageService>()
    .ConfigureHttpClient(cl => cl.BaseAddress = new Uri(builder.Configuration.GetConnectionString("Backend")!))
    .AddHttpMessageHandler<AuthMessageHandler>();

var app = builder.Build();

await app.RunAsync();
