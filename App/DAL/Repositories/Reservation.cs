
namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface IReservationRepository
    {
        void AddReservation(int bookId, int userId, DateTime loanDate);
        DateTime? IsReservedGetDate(int bookId);
        Reservation GetActiveReservationByBookId(int bookId);
        void UpdateReservationByBookId(int bookId, ReservationFlags flag);
    }

    public partial class Repository : IReservationRepository
    {
        #region Add

        public void AddReservation(int bookId, int userId, DateTime loanDate)
        {
            _dbRead.Execute(
              "ReservationsAdd",
          new[] { 
                new SqlParameter("@bookId", bookId),
                new SqlParameter("@userId", userId),
                new SqlParameter("@loandate", loanDate),
            });
        }

        public Reservation GetActiveReservationByBookId(int bookId)
        {
            Reservation loans = null;

            _dbRead.Execute(
                "ReservationsGetActiveByBookId",
            new[]
            {
                new SqlParameter("@bookId", bookId),
            },
                r => loans = new Reservation()
                {
                    Id = Read<int>(r, "Id"),
                    User = new User
                    {
                        Id = Read<int>(r, "UserId"),
                        FirstName = Read<string>(r, "FirstName"),
                        LastName = Read<string>(r, "LastName")
                    },

                    ReservedDate = Read<DateTime>(r, "ReservedDate"),
                    Flags = (ReservationFlags)Read<int>(r, "Flags")
                });

            return loans;
        }

        public DateTime? IsReservedGetDate(int bookId)
        {
            DateTime? reservationDate = null;

            _dbRead.Execute(
                "ReservationsGetBookSatus",
            new[]
            {
                new SqlParameter("@bookId", bookId),

            },
                r => reservationDate = Read<DateTime?>(r, "reservationDate")
                );

            return reservationDate;
        }

        #endregion

        public void UpdateReservationByBookId(int bookId, ReservationFlags flag)
        {
            _dbRead.Execute(
              "ReservationsUpdateflags",
          new[] { 
                new SqlParameter("@bookId", bookId),
                new SqlParameter("@flag", flag),
            });
        }
    }
}
