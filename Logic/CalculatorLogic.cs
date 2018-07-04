using ILogic;
using System.Composition;

namespace Logic
{
    [Export(typeof(ICalculatorLogic))]
    public class CalculatorLogic : ICalculatorLogic
    {
        public decimal Add(decimal a, decimal b)
        {
            return a + b;
        }
    }
}
