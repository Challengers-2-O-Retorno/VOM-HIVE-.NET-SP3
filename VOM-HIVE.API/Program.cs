using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.Services.Campaign;
using VOM_HIVE.API.Services.Company;
using VOM_HIVE.API.Services.Product;
using VOM_HIVE.API.Services.ProfileUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurando a conexão com o banco de dados Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"));
});

// Configuração de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de dependência para os serviços
builder.Services.AddScoped<IProductInterface, ProductService>();
builder.Services.AddScoped<ICompanyInterface, CompanyService>();
builder.Services.AddScoped<IProfileUserInterface, ProfileUserService>();
builder.Services.AddScoped<ICampaignInterface, CampaignService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }