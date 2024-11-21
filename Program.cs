using ClinicaAPI.Data;
using Microsoft.EntityFrameworkCore;
using ClinicaAPI.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddDbContext<ClinicaDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ClinicaDB")));


builder.Services.AddScoped<UserService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); 

app.Run();
