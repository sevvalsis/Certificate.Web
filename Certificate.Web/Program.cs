using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Session ayarlarý
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi 30 dakika
    options.Cookie.HttpOnly = true;  // Güvenlik amacýyla
    options.Cookie.IsEssential = true; // GDPR ve benzeri kurallar için gerekli
});
builder.Services.AddDbContext<Context>();
var app = builder.Build();
// Context sýnýfýný Dependency Injection'a ekleyin

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
// Session middleware'ini ekle
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Certificate}/{action=Index}/{id?}");

app.Run();
