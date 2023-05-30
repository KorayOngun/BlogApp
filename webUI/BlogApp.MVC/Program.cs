using AutoMapper;
using BlogApp.DataAccess;
using BlogApp.Service;
using BlogApp.Service.MapProfile;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IBlogRepo, BlogRepo>();
builder.Services.AddSingleton<IBlogService, BlogService>();
builder.Services.AddSingleton<ICategoryRepo, CategoryRepo>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IUserRepo, UserRepo>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<BlogAppContext>().AddDbContextFactory<BlogAppContext>(optionsAction =>
{
    optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(configureOptions =>
{
    configureOptions.LoginPath = "/Users/login";
    configureOptions.AccessDeniedPath = "/Users/AccessDenied";
    configureOptions.ReturnUrlParameter = "page";
});
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
