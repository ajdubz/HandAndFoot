using HandFootLib.Models;
using HandFootLib.Services;
using HandFootLib.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string constr = "database=HandFootScoring;server=adubzpc; Trusted_Connection=true;TrustServerCertificate=true;";


builder.Services.AddDbContext<Data>(op =>
{
    op.UseSqlServer(constr, op =>
    {

    });
});

// Add services to the container.
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IGameService, GameService>();


builder.Services.AddCors(op =>
{
    op.AddPolicy("corsettings", policy =>
    {
        policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("corsettings");

app.MapControllers();

app.Run();
