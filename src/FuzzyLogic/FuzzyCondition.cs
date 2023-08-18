using System;
using FuzzyLogic.MemberFunctions;

namespace FuzzyLogic
{
    public class FuzzyCondition
    {
        private readonly Func<double> _evaluationFunc;
    
        internal FuzzyCondition(IMemberFunction memberFunction, Func<double> func)
        {
            _evaluationFunc = () =>
            {
                var value = func();
                return memberFunction.Evaluate(value);
            };
        }
        
        private FuzzyCondition(Func<double> evaluationFunc)
        {
            _evaluationFunc = evaluationFunc;
        }
    
        internal double Evaluate()
        {
            return _evaluationFunc();
        }

        internal static FuzzyCondition And(FuzzyCondition a, FuzzyCondition b)
        {
            return new FuzzyCondition(() => Math.Min(a.Evaluate(), b.Evaluate()));
        }
        
        internal static FuzzyCondition Or(FuzzyCondition a, FuzzyCondition b)
        {
            return new FuzzyCondition(() => Math.Max(a.Evaluate(), b.Evaluate()));
        }
        
        internal static FuzzyCondition Not(FuzzyCondition a)
        {
            return new FuzzyCondition(() =>
            {
                var value = a.Evaluate();
                return 1 - value;
            });
        }
    }
}
