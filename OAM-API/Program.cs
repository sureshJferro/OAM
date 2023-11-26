using Microsoft.Extensions.DependencyInjection.Extensions;
using OAM.Core.Entities;
using OAM.Core.Resolver;
using OAM_API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<OamDevContext>();
builder.Services.AddCustomServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<IpFilterMiddleware>();

app.MapControllers();

#region Minimal API Samples
app.MapGet("/GetUsersList", (OamDevContext db) => db.Users.ToList());
app.MapGet("/GetUser/{id}", (OamDevContext db,int id) => db.Users.Where(x=>x.Id==id).SingleOrDefault());
#endregion

app.Run();
