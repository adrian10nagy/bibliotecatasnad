
using DAL.Entities;
using DAL.SDK;
using System;

namespace BL.Managers
{
    public static class ReservationsManager
    {
        public static void Add(Reservation reservation)
        {
            Kit.Instance.Reservations.AddReservation(reservation.Book.Id, reservation.User.Id, reservation.ReservedDate);
        }

        public static DateTime? IsReservedGetDate(int bookId)
        {
            return Kit.Instance.Reservations.IsReservedGetDate(bookId);
        }

        public static Reservation GetActiveReservationByBookId(int bookId)
        {
            return Kit.Instance.Reservations.GetActiveReservationByBookId(bookId);
        }

        public static void UpdateReservationByBookId(int bookId, ReservationFlags flag)
        {
            Kit.Instance.Reservations.UpdateReservationByBookId(bookId, flag);
        }
    }
}
