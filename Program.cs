using Microsoft.EntityFrameworkCore;
using db;
using Application2.DATA;
using System.Net;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
                options.UseMySql("server=localhost;user=root;password=fylhtq9049791;database=Application2", ServerVersion.AutoDetect("server=localhost;user=root;password=fylhtq9049791;database=Application2;")));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles("/home/user/Source/test_drive/Application2/wwwroot");
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var contextOptions = new DbContextOptionsBuilder<Context>()
        .UseMySql("server=localhost;user=root;password=fylhtq9049791;database=Application2", ServerVersion.AutoDetect("server=localhost;user=root;password=fylhtq9049791;database=Application2;"))
        .Options;
 

Context context = new Context(contextOptions);
SampleData.InitData(context);

app.Run();
