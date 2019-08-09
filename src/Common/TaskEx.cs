using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class TaskEx
    {
        public static Task<T> FromResult<T>(T result)
        {
            //hack for less than .net fx 4.5
            //return Task.FromResult(result);
            var tcs = new TaskCompletionSource<T>();
            tcs.SetResult(result);
            return tcs.Task;
        }

        public static Task Delay(TimeSpan span)
        {
            //hack for less than .net fx 4.5
            //return Task.Delay(span);
            return Delay(span, CancellationToken.None);
        }
        
        public static Task Delay(TimeSpan span, CancellationToken token)
        {
            //hack for less than .net fx 4.5
            //return Task.Delay(span);
            return Delay((int)span.TotalMilliseconds, token);
        }

        private static Task Delay(int milliseconds, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<object>();
            token.Register(() => tcs.TrySetCanceled());
            var timer = new Timer(_ => tcs.TrySetResult(null));
            timer.Change(milliseconds, -1);
            return tcs.Task;
        }
    }
}
