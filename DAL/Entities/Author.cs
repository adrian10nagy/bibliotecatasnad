
namespace DAL.Entities
{
    public class Author
    {
        public int Id;
        public string  Name { get; set; }
        public BookAthorType BookAthorType { get; set; }
    }
}
