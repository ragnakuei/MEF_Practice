using System;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using ILogic;

namespace MEF_Practice
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var calculator = new Calculator();
            var result     = calculator.Add(1, 2);
            Console.WriteLine(result);
        }
    }

    public class Calculator
    {
        public Calculator()
        {
            Compose();
        }

        public decimal Add(decimal a, decimal b)
        {
            return _calculatorLogic.Add(a, b);
        }

        private ICalculatorLogic _calculatorLogic;

        private void Compose()
        {
            var executableLocation = Assembly.GetEntryAssembly().Location;
            var path               = Path.Combine(Path.GetDirectoryName(executableLocation), "Plugins");
            var assemblies = Directory
                             .GetFiles(path, "*.dll", SearchOption.AllDirectories)
                             .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                             .ToList();
            var configuration = new ContainerConfiguration().WithAssemblies(assemblies);
            using (var container = configuration.CreateContainer())
            {
                //_calculatorLogic = container.GetExports<ICalculatorLogic>(); // 可以輸出為 IEnumerable<T>
                _calculatorLogic = container.GetExport<ICalculatorLogic>();
            }
        }
    }
}