using ApplicationCore.Contracts;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<OrdersDbContext>(Options=>
    Options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    );

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
