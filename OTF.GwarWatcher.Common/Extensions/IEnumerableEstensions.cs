using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTF.GwarWatcher.Common.Extensions
{
    public static class IEnumerableEstensions
    {
        public static void ForEach<T>(this IEnumerable<T> input, Action<T> action)
        {
            if (input != null && input.Any() && action != null)
            {
                foreach (T i in input)
                {
                    action(i);
                }
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> input, Func<T, Task> func)
        {
            if (input != null && input.Any() && func != null)
            {
                foreach (T i in input)
                {
                    await func(i);
                }
            }
        }

        public static void ForEachWaitAll<T>(this IEnumerable<T> input, Action<T> action)
        {
            if (input != null && input.Any() && action != null)
            {
                Task.WaitAll(input.Select(i => Task.Run(() => action(i))).ToArray());
            }
        }
    }
}
