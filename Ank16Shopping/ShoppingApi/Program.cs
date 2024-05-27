using Microsoft.EntityFrameworkCore;
using Shopping.BLL.Managers.Concrete;
using Shopping.DAL.DataContext;
using Shopping.DAL.Repositories.Concrete;
using Shopping.DAL.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ShoppingDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingDbStr"));
}, ServiceLifetime.Scoped);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CategoryRepo>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CategoryManager>();

builder.Services.AddScoped<ProductRepo>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductManager>();

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
