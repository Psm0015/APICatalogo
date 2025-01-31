using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddOpenApi();

string mySqlConnection = builder.Configuration.GetConnectionString("senai09a8g");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "weather api"));
    //app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
