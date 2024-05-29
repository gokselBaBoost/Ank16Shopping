using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping.BLL.Managers.Concrete;
using Shopping.DAL.DataContext;
using Shopping.DAL.Repositories.Concrete;
using Shopping.DAL.Services.Concrete;
using Shopping.Entities.Concrete;
using Shopping.Services.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ShoppingDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingDbStr"));
}, ServiceLifetime.Scoped);

builder.Services.AddIdentity<AppUser, IdentityRole<int>>(
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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CategoryRepo>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CategoryManager>();

builder.Services.AddScoped<ProductRepo>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductManager>();

builder.Services.AddScoped<IMailService, GmailService>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("MyPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7021")
              .WithMethods("GET")
              .AllowAnyHeader()
              .Build();
               
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors(opt =>
//{
//    //opt.AllowAnyOrigin() // * tüm siteler
//    //   .AllowAnyHeader() // * tüm header istekleri
//    //   .AllowAnyMethod() // * tüm metotlar (GET,POST,PUT,DELETE)
//    //   .Build();

//    // https://localhost:7021

//    /*
//     * .SetIsOriginAllowed(orgin => new Uri(orgin).Host == "localhost")
//       .SetIsOriginAllowed(orgin => new Uri(orgin).Scheme == "https")
//       .SetIsOriginAllowed(orgin => new Uri(orgin).Port == 7021)
//     */

//    //opt.WithOrigins("https://localhost:7021", "https://localhost:7022")
//    //   .AllowAnyHeader()
//    //   .AllowAnyMethod()
//    //   .Build();
//});

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
