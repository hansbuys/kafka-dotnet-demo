using System.Threading.Tasks;

namespace Kafka.Infrastructure
{
    public static class AsyncExtensions
    {
        public static Task<T> Async<T>(this T sync)
        {
            return Task.FromResult(sync);
        }
    }
}
