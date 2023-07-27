using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest;
using System;
using Omnisearch.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IElasticClient>(provider =>
{
    var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
    return new ElasticClient(settings);
});

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Create Elasticsearch Index
var client = app.Services.GetRequiredService<IElasticClient>();
var createIndexResponse = client.Indices.Create("people", c => c
    .Map<Person>(m => m
        .AutoMap()
    )
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();