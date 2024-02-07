using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text;
using Microsoft.OpenApi.Models;
using FulltechAPI.Core.Interfaces;
using FulltechAPI.Core.Repository;
using FulltechAPI.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<HolidayCheckerService>();
builder.Services.AddScoped<TransferService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();


