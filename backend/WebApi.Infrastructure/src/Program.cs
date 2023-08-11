using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Business.src.Services.ServiceImplementations;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Infrastructure.src.RepoImplimentations;
using WebApi.Infrastructure.src.Database;

var builder = WebApplication.CreateBuilder(args);

//Add automapper dependency injection
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add db context
builder.Services.AddDbContext<DatabaseContext>();

// Add service dependency injection
builder.Services
.AddScoped<IUserRepo, UserRepo>()
.AddScoped<IUserService, UserService>();

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

//Config route options
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

//authentication: TODO: !!!This is not a secure way to store the key, need to be stored in a secure place!!!
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "prackey-backend",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("prackey-backend-jsdguyfsdgcjsdbchjsdb jdhscjysdcsdj")),
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
