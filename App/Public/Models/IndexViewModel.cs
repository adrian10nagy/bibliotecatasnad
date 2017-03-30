
namespace Public.Models
{
    using DAL.Entities;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<Author> Items { get; set; }
        public Pager Pager { get; set; }
    }

}