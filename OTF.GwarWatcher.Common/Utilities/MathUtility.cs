using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Common.Utilities
{
    public static class MathUtility
    {
        public static T Max<T>(T value1, T value2) where T : IComparable
        {
            return value1.CompareTo(value2) > 0 ? value1 : value2;
        }
    }
}
