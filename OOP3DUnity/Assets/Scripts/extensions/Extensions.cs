using System;

namespace GeekBrains.Helpers
{
    public static class Extensions
    {
        public static bool TryBool(this string self)
        {
            bool result;
            return bool.TryParse(self, out result) && result;
        }
    }
}