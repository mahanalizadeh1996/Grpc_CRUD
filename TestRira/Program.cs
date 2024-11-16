using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestRira.Data;
using TestRira.Repo;
using TestRira.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<CustomerContext>
    (options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:7163") // Change to your Blazor app URL
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});



builder.Services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapGrpcService<CustomerGrpcService>());


app.Use((context, next) =>
{
    context.Response.Headers.AccessControlAllowOrigin = "*";
    context.Response.Headers.AccessControlExposeHeaders = "*";
    return next.Invoke();
});
app.Run();
