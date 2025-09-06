using System;

namespace MQKJ.BSMP.Utils.Extensions
{
    public static class DoubleExtensions
    {
        public static double ToDouble(this string val, double def = 0f)
        {
            if (Double.TryParse(val, out double result))
            {
                return result;
            }

            return def;
        }

        public static int ToInt32(this string val, int def = 0)
        {
            if (Int32.TryParse(val, out int result))
            {
                return result;
            }

            return def;
        }

        public static double ToFixed(this double val, int bit)
        {
            return Math.Round(val, bit);
        }
        public static string ToHumanReadableSize(this double size)
        {
            string[] units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };
            double mod = 1024.0;
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];
        }
    }
}