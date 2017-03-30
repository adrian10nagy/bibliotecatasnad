
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;
    using System;

    public class Reservations
    {
        private static IReservationRepository _repository;

        static Reservations()
        {
            _repository = new Repository();
        }

        public void AddReservation(int bookId, int UserId, System.DateTime loanDate)
        {
            _repository.AddReservation(bookId, UserId, loanDate);
        }

        public DateTime? IsReservedGetDate(int bookId)
        {
            return _repository.IsReservedGetDate(bookId);
        }

        public Reservation GetActiveReservationByBookId(int bookId)
        {
            return _repository.GetActiveReservationByBookId(bookId);

        }

        public void UpdateReservationByBookId(int bookId, ReservationFlags flag)
        {
            _repository.UpdateReservationByBookId(bookId, flag);
        }
    }
}
