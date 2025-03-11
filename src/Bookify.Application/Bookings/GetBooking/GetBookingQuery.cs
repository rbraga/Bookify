﻿using Bookify.Application.Abstractions.Cache;

namespace Bookify.Application.Bookings.GetBooking;

public sealed record GetBookingQuery(Guid BookingId) : ICacheQuery<BookingResponse>
{
    public string CacheKey => $"bookings-{BookingId}";

    public TimeSpan? CacheExpiration => null;
}
