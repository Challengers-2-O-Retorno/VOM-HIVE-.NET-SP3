using Microsoft.EntityFrameworkCore;
using VOM_HIVE.API.Data;
using VOM_HIVE.API.Services.Company;
using VOM_HIVE.API.Services.Product;
using VOM_HIVE.API.Services.ProfileUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"));
});

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

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

app.UseCors("AllowAllOrigins");  // Aplicando a política de CORS configurada

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
