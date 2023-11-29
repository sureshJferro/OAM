using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OAM.Core.Entities;
using OAM.Core.Resolver;
using OAM_API.Middlewares;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<OamDevContext>();
builder.Services.AddCustomServices();

#region Rate Limiting Middleware 

//        /////////////////Fixed Window - You decide the time and Numbers of Requests
//        builder.Services.AddRateLimiter(options =>
//        {
//            options.RejectionStatusCode = 429;// StatusCodes.Status429TooManyRequests
//            options.AddFixedWindowLimiter(policyName: "fixed", options =>
//            {
//                options.PermitLimit = 1;
//                options.Window = TimeSpan.FromSeconds(10);
//                options.AutoReplenishment = true;
//                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//                options.QueueLimit = 0;
//            });
//        });
//        //////////////Sliding Window - Window divides into segments as we mentioned
//        builder.Services.AddRateLimiter(options =>
//        {
//            options.RejectionStatusCode = 429;
//            options.AddSlidingWindowLimiter(policyName: "sliding", options =>
//            {
//                options.PermitLimit = 30;
//                options.Window = TimeSpan.FromSeconds(60);
//                options.SegmentsPerWindow = 2;
//                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//                options.QueueLimit = 2;
//            });
//        });
//        //Token based - Each request takes one token,If token is empty requests gets rejected.Token refills after time period
//        builder.Services.AddRateLimiter(options =>
//        {
//            options.RejectionStatusCode = 429;
//            options.AddTokenBucketLimiter(policyName: "token", options =>
//            {
//                options.TokenLimit = 2;
//                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//                options.QueueLimit = 2;
//                options.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
//                options.TokensPerPeriod = 2;
//                options.AutoReplenishment = true;
//            });
//        });
//        //Concurrency - Handles concurrent 3 request and set next request,when ever the 1st req finishes it task
//        builder.Services.AddRateLimiter(options =>
//        {
//            options.RejectionStatusCode = 429;
//            options.AddConcurrencyLimiter(policyName: "concurrency", options =>
//            {
//                options.PermitLimit = 3;
//                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//                options.QueueLimit = 2;
//            });
//        });

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

//Configurimg Middleware in pipeline with Extension Method
app.UseIpFilter();
app.UseLogUrl();

app.MapControllers();

#region Minimal API Samples
app.MapGet("/GetUsersList", (OamDevContext db) => db.Users.ToList());
app.MapGet("/GetUser/{id}", (OamDevContext db, int id) => db.Users.Where(x => x.Id == id).SingleOrDefault());
#endregion

app.Run();

