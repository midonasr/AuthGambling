using AuthGambling.interfaces;
using AuthGambling.models;
using AuthGambling.repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContextPool<db_tables>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("accounts")));
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
