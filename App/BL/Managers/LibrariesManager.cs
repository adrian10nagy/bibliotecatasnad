
namespace BL.Managers
{
    using DAL.Entities;
    using DAL.SDK;
    
    public static class LibrariesManager
    {
        public static Library GetLibraryById(int id)
        {
            var library = Kit.Instance.Libraries.GetLibraryById(id);
            return library;
        }

        public static void Add(Library library)
        {
            Kit.Instance.Libraries.Add(library);
        }
    }
}
