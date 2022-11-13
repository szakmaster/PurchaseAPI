using Microsoft.OpenApi.Models;
using PurchaseAPI.Extensions;
using PurchaseAPI.Interfaces;
using PurchaseAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Purchase data calculator API",
        Description = "A WebAPI that calculates the purchase data (net, gross and VAT amount) based on a given value (net/gross/VATamount) and the VAT rate."
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Configure services
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<InputValidationAttribute>();

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
