using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shopping.BLL.Managers.Concrete;
using Shopping.DAL.DataContext;
using Shopping.DAL.Repositories.Concrete;
using Shopping.DAL.Services.Concrete;
using Shopping.Entities.Concrete;
using Shopping.Services.Mail;
using System.Text;
using System.Text.Json.Serialization;

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
                ).AddDefaultTokenProviders().AddEntityFrameworkStores<ShoppingDbContext>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(opt =>
                {
                    opt.SaveToken = true;
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidAudience = builder.Configuration.GetSection("JwtToken:Audience").Value,
                        ValidIssuer = builder.Configuration.GetSection("JwtToken:Issuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtToken:SigningKey").Value))
                    };
                });

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authentication",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "ANK16 JWT Token",
};

var securityReq = new OpenApiSecurityRequirement()
{
    {
         new OpenApiSecurityScheme
        {
            Reference= new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }

        },
         new string[] { }
    }


};


var contact = new OpenApiContact()
{
    Name = "ANK16 Sınıfı",
    Email = "ank16@bilgeadam.com",
    Url = new Uri("https://www.bilgeadam.com")
};

var license = new OpenApiLicense()
{
    Name = "Free",
    Url = new Uri("https://www.bilgeadam.com")
};

var info = new OpenApiInfo()
{
    Version = "1.0",
    Title = "JWT uygulaması",
    Description = "ANK16 Grubu için JWT uygulaması",
    TermsOfService = new Uri("https://www.bilgeadam.com"),
    Contact = contact,
    License = license
};

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", info);
    opt.AddSecurityDefinition("Bearer", securityScheme);
    opt.AddSecurityRequirement(securityReq);

});

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

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
        policy.WithOrigins("https://localhost:7021", "https://localhost:7174")
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
app.UseStaticFiles();
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

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
