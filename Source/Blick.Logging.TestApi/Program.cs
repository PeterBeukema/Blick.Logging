using System.Collections;
using System.Collections.Generic;
using Blick.Logging.Abstractions;
using Blick.Logging.ConsoleLogger;
using Blick.Logging.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.ClearProviders();

builder.Logging.AddBlickLogging(options => options.BuildLoggers = BuildLoggers);

IEnumerable<ConsoleLogger> BuildLoggers(string name, LoggerOptions options)
{
    yield return new ConsoleLogger(name, options);
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();