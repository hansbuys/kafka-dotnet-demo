using System.Collections.Generic;
using System.Threading.Tasks;
using Kafka.Infrastructure;
using Kafka.Service.Interfaces;
using Xunit.Abstractions;

namespace Kafka.Receiver.Service.Tests
{
    public class FakeConsole : IWriteOutput, IReadInput
    {
        private ITestOutputHelper output;
        public List<string> ConsoleOutput = new List<string>();
        private readonly Queue<string> queue = new Queue<string>();

        public FakeConsole(ITestOutputHelper output)
        {
            this.output = output;
        }

        public Task WriteOutput(string value)
        {
            output.WriteLine($"Output to console: {value}");
            ConsoleOutput.Add(value);

            return 0.Async();
        }

        public void QueueInput(params string[] inputList)
        {
            foreach (var input in inputList)
            {
                queue.Enqueue(input);
            }
        }

        public Task<string> GetInput()
        {
            return queue.Dequeue().Async();
        }
    }
}