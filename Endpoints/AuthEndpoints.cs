using System.Security.Claims;
using DotNetSpaAuth.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetSpaAuth.Endpoints;

public static class AuthEndpoints
{

    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {

        var authGroup = app.MapGroup("/auth");

        authGroup.MapGet("/status", async Task<Results<Ok<object>, ValidationProblem, NotFound>>
         (ClaimsPrincipal claimsPrincipal, [FromServices] IServiceProvider sp) =>
        {
            var userManager = sp.GetRequiredService<UserManager<User>>();
            if (await userManager.GetUserAsync(claimsPrincipal) is not { } user)
            {
                return TypedResults.NotFound();
            }
            object data = new { user.Email, user.Firstname, user.Lastname };
            return TypedResults.Ok(data);
        });

        authGroup.MapIdentityApi<User>();

        return app;
    }


}
