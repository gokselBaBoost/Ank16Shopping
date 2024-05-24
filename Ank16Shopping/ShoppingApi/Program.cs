using Microsoft.EntityFrameworkCore;
using Shopping.BLL.Managers.Concrete;
using Shopping.DAL.DataContext;
using Shopping.DAL.Repositories.Concrete;
using Shopping.DAL.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("MyPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7021")
              .WithMethods("GET", "POST", "PUT", "DELETE")
              .AllowAnyHeader()
              .Build();
    });
});

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors(opt =>
//{
//    opt.WithOrigins("https://localhost:7021")
//       .WithMethods("POST")
//       .Build();
//});

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
