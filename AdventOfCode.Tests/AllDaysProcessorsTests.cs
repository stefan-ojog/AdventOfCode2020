using NUnit.Framework;
using Autofac;
using System.Collections.Generic;
using System;
using AdventOfCode.Base;

namespace AdventOfCode.Tests
{
    public class AllDaysProcessorsTests
    {
        private IContainer _container;

        [SetUp]
        public void SetUp()
        {
            _container = new Bootstrapper().Build();
        }

        [TearDown]
        public void TearDown()
        {
            _container.Dispose();
        }

        [Test]
        public void GivenProcessors_WhenGetResultIsCalled_ThenExpectedOutputIsRetrieved()
        {
            var processors = _container.Resolve<IEnumerable<IProcessor>>();

            foreach (var processor in processors)
            {
                VerifyProcessorResult(processor);
            }
        }

        public void VerifyProcessorResult(IProcessor processor)
        {
            var result = processor.GetResultAsString();
            var expectedOutput = TestsResourceHelper.GetExpectedOutputByProcessor(processor.GetType());
            var currentProcessorName = TestsResourceHelper.GetDayAndPartByProcessorType(processor.GetType());

            Console.WriteLine($"{currentProcessorName} Expected: {expectedOutput}; Actual: {result}");
            Assert.AreEqual(expectedOutput, result);
        }
    }
}