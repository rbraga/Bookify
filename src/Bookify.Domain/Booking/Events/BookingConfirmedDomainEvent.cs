using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Booking.Events;

public sealed record BookingConfirmedDomainEvent(Guid BookingId) : IDomainEvent;
