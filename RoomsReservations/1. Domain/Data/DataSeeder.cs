using Microsoft.EntityFrameworkCore;
using RoomsReservations._1._Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoomsReservations._1._Domain.Data
{
    public static class DataSeeder
    {
        public static void SeedData(HotelDbContext dbContext)
        {
            if (dbContext.Rooms.Any() || dbContext.Guests.Any() || dbContext.Reservations.Any())
            {
                // Data already seeded, no need to proceed.
                return;
            }

            // Seed 10 Guest records
            var guests = Enumerable.Range(1, 10).Select(i => new Guest
            {
                Id = Guid.NewGuid(),
                FirstName = $"GuestFirstName{i}",
                LastName = $"GuestLastName{i}",
                BirthDate = DateTime.Now.AddYears(-i).AddDays(i),
                Phone = $"GuestPhone{i}",
                Email = $"guest{i}@example.com",
                Sex = (i % 2 == 0) ? Sex.Male : Sex.Female
            }).ToList();

            // Seed 10 Room records
            var rooms = Enumerable.Range(1, 10).Select(i => new Room
            {
                Id = Guid.NewGuid(),
                RoomName = $"Room{i}",
                RoomCapacity = 2,
                RoomPrice = 100 + (i * 10),
                RoomType = $"RoomType{i}",
                Status = RoomStatus.Free
            }).ToList();

            // Seed 10 Reservation records
            var reservations = Enumerable.Range(1, 10).Select(i => new Reservation
            {
                Id = Guid.NewGuid(),
                Guest = guests[i - 1], // Associate each reservation with a guest
                CheckInDate = DateTime.Now.AddDays(i),
                CheckOutDate = DateTime.Now.AddDays(i + 5),
                ReservationName = $"Reservation{i}",
                Room = rooms[i - 1], // Associate each reservation with a room
                WayOfPayment = Payment.CreditCard,
                PaymentStatus = PaymentStatus.Completed
            }).ToList();

            dbContext.AddRange(guests);
            dbContext.AddRange(rooms);
            dbContext.AddRange(reservations);

            dbContext.SaveChanges();
        }
    }
}
