using FluentAssertions;
using FluentAssertions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Kafka.Infrastructure.Testing
{
    public static class Assertions
    {
        //we want to separate WriteTestOutput for each running test.
        private static readonly AsyncLocal<Guid> Id = new AsyncLocal<Guid>();

        private static readonly ConcurrentDictionary<Guid, Action<string>> WriteOutputDelegates =
            new ConcurrentDictionary<Guid, Action<string>>();

        public static Action<string> WriteTestOutput
        {
            set
            {
                Id.Value = Guid.NewGuid();

                WriteOutputDelegates.AddOrUpdate(Id.Value, value,
                    (guid, action) => { throw new Exception("Key already in use."); });
            }
            internal get
            {
                Action<string> value;
                if (!WriteOutputDelegates.TryGetValue(Id.Value, out value))
                    throw new Exception("Unable to find write-testoutput delegate for testing.");

                return value;
            }
        }
    }

    public abstract class Assertions<T, TAssertion> : ReferenceTypeAssertions<T, TAssertion>
        where TAssertion : ReferenceTypeAssertions<T, TAssertion>
    {
        private readonly Action<string> writeOutput;

        public Assertions(T subject)
        {
            writeOutput = Assertions.WriteTestOutput;
            Subject = subject;
        }

        protected override string Identifier => typeof(T).Name;

        protected void CheckedThat(string message)
        {
            var traceMessage = $"Checked that {message}.";

            writeOutput?.Invoke(traceMessage);
        }

        protected AndConstraint<TAssertion> And()
        {
            return new AndConstraint<TAssertion>(this as TAssertion);
        }

        protected AndWhichConstraint<TAssertion, TWhich> AndWhich<TWhich>(TWhich newSubject)
        {
            return new AndWhichConstraint<TAssertion, TWhich>(this as TAssertion, newSubject);
        }
    }
}