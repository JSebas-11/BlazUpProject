using MudBlazor.Services;
using Presentation.Components;
using Application;
using Infrastructure;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Infrastructure Layer.
string? connStr = builder.Configuration.GetConnectionString("DefaultConnection");
if (String.IsNullOrWhiteSpace(connStr)) { throw new InvalidOperationException("Connection could not be found"); }
builder.Services.AddInfrastructure(connStr);

// Add Application Layer.
builder.Services.AddApplication();

// Add Presentation Layer.
builder.Services.AddPresentation();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
