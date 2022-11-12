using PurchaseAPI.Extensions;
using PurchaseAPI.Interfaces;
using PurchaseAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure services
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<ModelValidationAttribute>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure global exception handler
var logger = ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<ILogger<Program>>();
app.ConfigureExceptionHandler(logger);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
