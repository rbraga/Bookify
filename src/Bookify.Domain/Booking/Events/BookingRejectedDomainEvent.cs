using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Booking.Events;

public sealed record BookingRejectedDomainEvent(Guid BookingId) : IDomainEvent;
