using System;
using System.Threading.Tasks;
using Kafka.Infrastructure;
using Kafka.Service.Interfaces;

namespace Kafka.Service.Ports
{
    public class ConsoleInputOutput : IReadInput, IWriteOutput
    {
        public Task<string> GetInput()
        {
            return Console.ReadLine().Async();
        }

        public Task WriteOutput(string output)
        {
            Console.WriteLine(output);

            return 0.Async();
        }
    }
}
