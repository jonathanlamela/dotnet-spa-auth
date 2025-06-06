using DotNetSpaAuth.Data;
using DotNetSpaAuth.Dtos;
using DotNetSpaAuth.Models;
using DotNetSpaAuth.Services;
using DotNetSpaAuth.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default") ?? "Data Source=App.db";

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddCors();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<UserService>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<SigninRequestValidator>();

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

api.MapGet("/", () => TypedResults.Ok(new ApiStatus()
{
    Status = "api works"
}
));

app.MapGet("/", () =>
{
    return Results.Redirect("/v1");
});

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<User>();


app.Run();
