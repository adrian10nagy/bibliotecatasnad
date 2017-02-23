﻿
namespace BL.Helpers
{
    public static class StringExtensions
    {
        public static int? ToNullableInt(this string input)
        {
            int i;
            if (int.TryParse(input, out i)) return i;
            return null;
        }
    }
}
