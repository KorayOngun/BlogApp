using BlogApp.Persistence;
using BlogApp.Application;
using BlogApp.Infrastructure;
using BlogApp.Presentation.Endpoints;
using BlogApp.Core.Services;
using BlogApp.Presentation.Services;
using BlogApp.Core;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBlogAppDbContext();
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddDomainServices();

builder.Services.AddScoped<IUserHandlerService, UserHandlerService>();
builder.Services.AddScoped<IBlogService, BlogService>();

var app = builder.Build();
 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();


