using DotNetSpaAuth.Dtos;
using DotNetSpaAuth.Services;
using DotNetSpaAuth.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services
builder.Services.AddSingleton<UserService>();

//Validators
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

var usersGroup = api.MapGroup("/users");

usersGroup.MapPost("/signin", ([FromBody] SigninRequest user, IValidator<SigninRequest> validator) =>
{
    var validationResult = validator.Validate(user);

    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());
        return Results.BadRequest(new { Message = "Validation failed", Errors = errors });
    }

    return Results.Ok($"User for email: {user.Email} created successfully!");

});
usersGroup.MapPost("/login", () => { });
usersGroup.MapGet("/status", () => { });

app.MapGet("/", () =>
{
    return Results.Redirect("/v1");
});

app.Run();
