using System;

namespace NeoMem2.Utils
{
    public class Math2
    {
        public static int CalculatePercentage(int count, int total)
        {
            int percentage = (int)((count / (float)total) * 100);
            return Math.Min(100, Math.Max(0, percentage));
        }
    }
}
