using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Base
{
    public class ProcessorFactory : IProcessorFactory
    {
        private readonly IEnumerable<IProcessor> _processors;

        public ProcessorFactory(IEnumerable<IProcessor> processors)
        {
            _processors = processors;
        }

        public IProcessor GetProcessor(int day, int part)
        {
            return _processors.SingleOrDefault(p => p.GetType().Name.Contains($"Day{day}Part{part}"));
        }
    }
}
