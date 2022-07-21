using Dotnet.Data;
using Dotnet.Repository;
using Dotnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var services = builder.Services;
var Configuration = builder.Configuration;



services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefualtConnection")));
services.AddIdentity<IdentityUser,IdentityRole>(options => 
{
}).AddEntityFrameworkStores<AppDbContext>();

services.AddAuthorization(options => 
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});
services.AddScoped<IStudentRepository,StudentRepository>();
services.AddTransient<IEmailSender,MessageServices>();
services.AddTransient<ISmsSender,MessageServices>();

var app = builder.Build();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=GetStudents}/{id?}");

app.Run();
