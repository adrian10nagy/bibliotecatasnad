﻿
namespace Public.Models
{
    using DAL.Entities;
    using System.Collections.Generic;
    
    public class MainPageModel
    {
        public int BookNumber { get; set; }
        public List<Book> LastAddedBooks { get; set; }
        public PageQueryParam Flags { get; set; }
    }

    public enum PageQueryParam{
        None = 0,
        NewUser = 1,
        PasswordChangeTrue = 2,
        PasswordChangeFalse = 3
    }
}