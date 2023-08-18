using Mc2.CrudTest.Presentation;
using Mc2.CrudTest.Shared.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.PresentationInjection(builder.Configuration, builder.Environment);

var app = builder.Build();
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var scope = scopeFactory.CreateScope();
#region Initializing

await using (var applicationDbContextService = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
{
    await applicationDbContextService.Database.MigrateAsync();
}

#endregion
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

if (environment != "Production")
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.InjectJavascript("/SwaggerUI/custom.js");
        options.RoutePrefix = "docs";
    });
}


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");


app.Run();