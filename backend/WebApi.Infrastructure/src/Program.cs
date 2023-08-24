using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Business.src.Services.ServiceImplementations;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Infrastructure.src.RepoImplimentations;
using WebApi.Infrastructure.src.Database;
using WebApi.Infrastructure.src.AuthorizationRequirement;
using WebApi.Domain.src.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Add Automapper DI
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Removed adding db context from the DatabaseContext.cs file due npgslp version 4 recomendations not to create new npgsqldatasourcebuilder within the scope
//builder.Services.AddDbContext<DatabaseContext>();

// Moved from DatabaseContext.cs here due npgslp version 4 recomendations not to create new npgsqldatasourcebuilder within the scope
var connectionString = builder.Configuration.GetConnectionString("Default");

var npgsqlBuilder = new NpgsqlDataSourceBuilder(connectionString);
npgsqlBuilder.MapEnum<UserRole>();
npgsqlBuilder.MapEnum<OrderStatus>();
await using var dataSource = npgsqlBuilder.Build();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.AddInterceptors(new TimeStampInterceptor());
    options.UseNpgsql(dataSource)
           .UseSnakeCaseNamingConvention();

});

// Add service DI
builder.Services
.AddScoped<IUserRepo, UserRepo>()
.AddScoped<IUserService, UserService>()
.AddScoped<IAuthService, AuthService>()
.AddScoped<IProductRepo, ProductRepo>()
.AddScoped<IProductService, ProductService>()
.AddScoped<IOrderRepo, OrderRepo>()
.AddScoped<IOrderService, OrderService>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Bearer token authentication",
        Name = "Authentication",
        In = ParameterLocation.Header
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//Config route
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

//authentication:
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
   {
       var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidIssuer = configuration.GetValue<string>("TokenSettings:Issuer"),
           ValidateAudience = false,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("TokenSettings:SecurityKey"))),
           ValidateIssuerSigningKey = true
       };
   });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Enable CORS with the default policy (allow any origin, method, and header, crendentials), this code has been reblace top of the file with other approach whily trying to fix the cors issues
//app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
