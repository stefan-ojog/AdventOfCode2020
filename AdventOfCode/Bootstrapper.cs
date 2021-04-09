using AdventOfCode.Base;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Bootstrapper
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<InputReaderWrapper>().As<IInputReader>().SingleInstance();
            builder.RegisterType<ProcessorFactory>().As<IProcessorFactory>().SingleInstance();

            RegisterInputTransformTypes(builder);
            RegisterProcessorTypes(builder);

            return builder.Build();
        }

        private void RegisterInputTransformTypes(ContainerBuilder containerBuilder)
        {
            var inputTransformTypes = GetTypesWithInterface(typeof(IInputTransform));
            foreach (var inputTransformType in inputTransformTypes)
                containerBuilder.RegisterType(inputTransformType).SingleInstance();
        }

        private void RegisterProcessorTypes(ContainerBuilder containerBuilder)
        {
            var processorTypes = GetTypesWithInterface(typeof(IProcessor));

            foreach (var processorType in processorTypes)
                containerBuilder.RegisterType(processorType)
                    .WithParameter("inputResourceName", GetInputResourceNameFromType(processorType))
                    .AsSelf()
                    .As<IProcessor>()
                    .SingleInstance();
        }

        private string GetInputResourceNameFromType(Type type)
        {
            var namePreffix = "AdventOfCode.Input";
            var regexString = @"Day\d+";

            var regex = new Regex(regexString);
            var match = regex.Match(type.Name);

            var resourceName = $"{namePreffix}.{match.Value}.txt";

            return resourceName;
        }

        private IEnumerable<Type> GetTypesWithInterface(Type interfaceType)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            return currentAssembly.GetLoadableTypes().Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList();
        }
    }
}
