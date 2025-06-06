using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Crea un gruppo con prefisso "/api"
var api = app.MapGroup("/v1");

api.MapGet("/", () => new
{
    Status = "api works"
});

var usersGroup = api.MapGroup("/users");

usersGroup.MapPost("/signin", () => { });
usersGroup.MapPost("/login", () => { });
usersGroup.MapGet("/status", () => { });

app.MapGet("/", () =>
{
    return Results.Redirect("/v1");
});

app.Run();
