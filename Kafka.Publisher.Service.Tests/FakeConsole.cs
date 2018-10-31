using System.Collections.Generic;
using System.Threading.Tasks;
using Kafka.Infrastructure;
using Kafka.Service.Interfaces;

namespace Kafka.Publisher.Service.Tests
{
    public class FakeConsole : IWriteOutput, IReadInput
    {
        private readonly Queue<string> queue = new Queue<string>();

        public void QueueInput(params string[] inputList)
        {
            foreach(var input in inputList)
            {
                queue.Enqueue(input);
            }
        }

        public Task<string> GetInput()
        {
            return queue.Dequeue().Async();
        }

        public Task WriteOutput(string output)
        {
            return 0.Async();
        }
    }
}