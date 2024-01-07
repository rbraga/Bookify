using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Booking.Events;

public sealed record BookingCompletedDomainEvent(Guid BookingId) : IDomainEvent;
