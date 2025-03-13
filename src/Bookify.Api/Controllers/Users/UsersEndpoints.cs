using Bookify.Application.Users.GetLoggedInUser;
using Bookify.Application.Users.LogInUser;
using Bookify.Application.Users.RegisterUser;
using MediatR;

namespace Bookify.Api.Controllers.Users;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("users/me", GetLoggedInUser)
            .RequireAuthorization();

        builder.MapPost("users/register", Register)
            .AllowAnonymous();

        builder.MapPost("users/login", LogIn)
            .AllowAnonymous();

        return builder;
    }

    //[HasPermission(Permissions.UsersRead)]
    public static async Task<IResult> GetLoggedInUser(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetLoggedInUserQuery();

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> Register(
        RegisterUserRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password);

        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> LogIn(
        LogInUserRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new LogInUserCommand(request.Email, request.Password);

        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            //return Results.Unauthorized(result.Error);
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }
}
