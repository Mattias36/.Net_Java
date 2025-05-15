using Microsoft.AspNetCore.Authentication.Certificate;
using Webapp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAuthentication(
    CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

// 2 punkt jakie� rozszerzenie jak mapa google , albo np. przesy� danych - na ocen� 5 jako� fajnie zmodyfikowa�
// albo utworzy� projekt w MVC
// 3 pokaza� chocia� pr�b� autentykacji 
// przeniesienie na chmur� -> opublikowanie si� uda ale baza danych nie
// mail studencki na zalogowaniu si�
