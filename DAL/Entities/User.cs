
using System;

namespace DAL.Entities
{
    public class User
    {
        public int Id;
        public string FirstName;
        public string LastName;
        public string Username;
        public string HomeAddress;
        public DateTime Birthdate;
        public string Phone;
        public string Email;
        public string FacebookAddress;
        public DateTime JoinDate;
        public Locality Locality;
        public UserFlag Flags;
        public UserType UserType;
    }
}
