using Bookify.Application.Abstractions.Behaviors;
using Bookify.Application.Bookings.ReserveBooking;
using Bookify.Application.Users.RegisterUser;
using Bookify.Domain.Bookings;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Bookify.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

            configuration.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });

        
        //services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
        services.AddScoped<IValidator<ReserveBookingCommand>, ReserveBookingCommandValidator>();

        services.AddTransient<PricingService>();

        return services;
    }
}