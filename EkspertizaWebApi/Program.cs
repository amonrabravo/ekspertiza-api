using EkspertizaWebApi;
using EkspertizaWebApiData;
using EkspertizaWebApiData.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(config =>
{

    var provider = builder.Configuration.GetValue<string>("Provider")!;

    switch (provider)
    {
        case "PostgreSQL":
        default:
            config.UseNpgsql(builder.Configuration.GetConnectionString(provider), x =>
            {
                x.MigrationsAssembly("MigrationsPostgreSQL");
            });
            break;
    }
});

builder.Services.AddIdentity<User, Role>(config =>
{
    var password = builder.Configuration.GetSection("Password");
    config.Password.RequiredLength = password.GetValue<int>("RequiredLength");
    config.Password.RequireDigit = password.GetValue<bool>("RequireDigit");
    config.Password.RequiredUniqueChars = password.GetValue<int>("RequiredUniqueChars");
    config.Password.RequireLowercase = password.GetValue<bool>("RequireLowercase");
    config.Password.RequireNonAlphanumeric = password.GetValue<bool>("RequireNonAlphanumeric");
    config.Password.RequireUppercase = password.GetValue<bool>("RequireUppercase");

})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddErrorDescriber<AppIdentityErrorDescriber>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

builder.Services.AddEkspertizaApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseEkspertizaWebApi();

app.Run();
