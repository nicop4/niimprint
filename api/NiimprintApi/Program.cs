using System.Net;
using NiimprintApi.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<NiimbotB1_50x30>();
builder.Services.AddSingleton<NiimbotB1_60x40>();
builder.Services.AddSingleton<NiimbotB1_Product>();
builder.Services.AddSingleton<NiimbotB1_Stamp>();
builder.Services.AddSingleton<NiimbotB1_Address>();
builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.Listen(IPAddress.Any, 8080);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

//app.UseAuthorization();

// listens on port 8080
app.UseRouting();

// add cors policy
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.Run();
