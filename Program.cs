using desafio.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using desafio.Repository.IRepository;
using desafio.Repository;
using desafio.Services.IServices;
using desafio.Services;
using desafio.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.\
var connection = builder.Configuration.GetConnectionString("Context");
builder.Services.AddDbContext<Context>(opt =>
    opt.UseMySql(connection, ServerVersion.AutoDetect(connection)));

//AddScoped
builder.Services.AddScoped<Context>();
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddScoped<IRepositoryStatus, RepositoryStatus>();
builder.Services.AddScoped<IRepositorySubscriptions, RepositorySubscription>();
builder.Services.AddScoped<IRepositoryEventHistory, RepositoryEventHistory>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<SubscriptionConsumer>();
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
