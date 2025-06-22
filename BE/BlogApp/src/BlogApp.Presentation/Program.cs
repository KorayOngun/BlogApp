using BlogApp.Persistence;
using BlogApp.Application;
using BlogApp.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();

var app = builder.Build();
 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapBlogEndpoints();

app.Run();


