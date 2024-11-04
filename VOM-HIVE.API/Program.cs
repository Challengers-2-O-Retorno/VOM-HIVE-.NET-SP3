using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;
using VOM_HIVE.AI.Model;
using VOM_HIVE.API.Auth;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.Dependecy;
using VOM_HIVE.API.Services.Authenticate;
using VOM_HIVE.API.Services.Campaign;
using VOM_HIVE.API.Services.Company;
using VOM_HIVE.API.Services.Configuration;
using VOM_HIVE.API.Services.Product;
using VOM_HIVE.API.Services.ProductDescription;
using VOM_HIVE.API.Services.ProfileUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurando a conexão com o banco de dados Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"));
});

//Configurando dados do Auth0
builder.Services.Configure<ConfigurationService>(builder.Configuration.GetSection("Auth0"));
builder.Services.AddSingleton<IConfigurationInterface>(sp =>
    sp.GetRequiredService<IOptions<ConfigurationService>>().Value
);

//Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "https://dev-cwex01rb05coo3fo.us.auth0.com/";
    options.Audience = "VOM-HIVE.API";
});

// Configuração de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructureSwagger();
//builder.Services.AddSwaggerGen();

// Injeção de dependência para os serviços
builder.Services.AddScoped<IProductInterface, ProductService>();
builder.Services.AddScoped<ICompanyInterface, CompanyService>();
builder.Services.AddScoped<IProfileUserInterface, ProfileUserService>();
builder.Services.AddScoped<ICampaignInterface, CampaignService>();

builder.Services.AddScoped<IAuthenticateInterface, AuthenticateService>();

//IA
var trainingData = LoadTrainingData();
builder.Services.AddSingleton(new ProductDescriptionService(trainingData));

//Log de erro
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error != null)
        {
            var exception = exceptionHandlerPathFeature.Error;
            var response = new
            {
                error = exception.Message
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    });
});

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

IEnumerable<ProductDescriptionData> LoadTrainingData()
{
    var json = System.IO.File.ReadAllText("D:/FIAP/SP3.NET/VOM-HIVE/VOM-HIVE.AI/ProductDescriptionData.json");
    var trainingData = JsonSerializer.Deserialize<IEnumerable<ProductDescriptionData>>(json);
    return trainingData;
}

public partial class Program { }