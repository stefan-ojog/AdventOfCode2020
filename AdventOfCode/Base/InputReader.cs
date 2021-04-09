using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode.Base
{
    public static class InputReader
    {
        public static string[] GetInput(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result.Split(Environment.NewLine);
            }
        }
    }
}
