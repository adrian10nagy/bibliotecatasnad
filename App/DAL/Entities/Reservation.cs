namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;

    public class Reservation
    {
        public int Id;
        public User User;
        public Book Book;
        public DateTime ReservedDate;
        public ReservationFlags Flags;
    }

    public enum ReservationFlags
    {
        Necunoscută = 0,
        Rezervată = 1,
        Completată = 2,
        Expirată = 3,
        Anulată = 4
    }
}
