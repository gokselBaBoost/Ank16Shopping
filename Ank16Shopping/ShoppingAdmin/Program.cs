using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping.BLL.Managers.Concrete;
using Shopping.DAL.DataContext;
using Shopping.DAL.Repositories.Concrete;
using Shopping.DAL.Services.Concrete;
using Shopping.Entities.Concrete;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

builder.Services.AddDbContext<ShoppingDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingDbStr"));
}, ServiceLifetime.Scoped);

builder.Services.AddIdentity<AppUser,IdentityRole<int>>(
    opt =>
    {
        opt.SignIn.RequireConfirmedEmail = true;

        opt.User.RequireUniqueEmail = true;

        opt.Password.RequireDigit = true;
        opt.Password.RequireLowercase = true;
        opt.Password.RequireUppercase = true;
        opt.Password.RequireNonAlphanumeric = true;
        opt.Password.RequiredUniqueChars = 1;
        opt.Password.RequiredLength = 8;
    }
                )
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ShoppingDbContext>();

builder.Services.AddScoped<CategoryRepo>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CategoryManager>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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
