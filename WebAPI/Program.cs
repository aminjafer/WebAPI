using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(c =>
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
    .AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddControllersWithViews()
        .AddNewtonsoftJson( options => 
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json
        .ReferenceLoopHandling.Ignore)
        .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
        = new DefaultContractResolver());

builder.Services.AddControllers();

builder.Services.AddDbContext<DonationDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString(
        "DevConnection")));


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

app.UseCors(options => options.AllowAnyOrigin()
    .AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
