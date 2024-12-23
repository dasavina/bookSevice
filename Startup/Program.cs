﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using FluentValidation.AspNetCore;
using Data.Configurations;
using Data.Repositories.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Services.Validation;
using Microsoft.OpenApi.Models;
using BLL.BusinessLogic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure DbContext with connection string from app settings
builder.Services.AddDbContext<ApplicationDbContext>();

// Configure repositories
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<BookPublisherRepository>();

// Configure Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configure services
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<PublisherService>();
builder.Services.AddScoped<BookPublisherService>();


// Configure AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 1024; // Ліміт у 1024 умовні одиниці
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // замініть на URL вашого Redis-сервера
    options.InstanceName = "SampleInstance";
});

// Configure Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger middleware in the request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Service API V1");
        options.RoutePrefix = string.Empty; // Swagger UI available at root: /
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


