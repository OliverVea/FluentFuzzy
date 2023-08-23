using System;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic
{
    internal class Condition : ICondition
    {
        private readonly Func<double> _evaluationFunc;
    
        internal Condition(IMemberFunction memberFunction, Func<double> func)
        {
            _evaluationFunc = () =>
            {
                var value = func();
                return memberFunction.Evaluate(value);
            };
        }
        
        private Condition(Func<double> evaluationFunc)
        {
            _evaluationFunc = evaluationFunc;
        }

        double ICondition.Evaluate()
        {
            return _evaluationFunc();
        }

        internal static Condition And(ICondition a, ICondition b)
        {
            return new Condition(() => Math.Min(a.Evaluate(), b.Evaluate()));
        }
        
        internal static Condition Or(ICondition a, ICondition b)
        {
            return new Condition(() => Math.Max(a.Evaluate(), b.Evaluate()));
        }
        
        internal static Condition Not(ICondition a)
        {
            return new Condition(() =>
            {
                var value = a.Evaluate();
                return 1 - value;
            });
        }
    }
}
