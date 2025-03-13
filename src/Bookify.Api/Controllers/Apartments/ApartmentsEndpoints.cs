using Bookify.Application.Apartments.SearchApartments;
using MediatR;

namespace Bookify.Api.Controllers.Apartments;

public static class ApartmentsEndpoints
{
    public static IEndpointRouteBuilder MapApartmentEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("apartments", SearchApartments)
            .RequireAuthorization();

        return builder;
    }

    public static async Task<IResult> SearchApartments(
        DateOnly startDate,
        DateOnly endDate,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new SearchApartmentsQuery(startDate, endDate);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result.Value);
    }
}
