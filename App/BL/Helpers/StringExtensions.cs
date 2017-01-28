
namespace BL.Helpers
{
    public static class StringExtensions
    {
        public static int ToInt(this string input)
        {
            int result = 0;
            int.TryParse(input, out result);

            return result;
        }
    }
}
