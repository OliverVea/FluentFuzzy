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

        public static FuzzyCondition operator&(FuzzyCondition a, FuzzyCondition b)
        {
            return new FuzzyCondition(() => Math.Min(a.Evaluate(), b.Evaluate()));
        }
        
        public static FuzzyCondition operator|(FuzzyCondition a, FuzzyCondition b)
        {
            return new FuzzyCondition(() => Math.Max(a.Evaluate(), b.Evaluate()));
        }
        
        public static FuzzyCondition operator!(FuzzyCondition a)
        {
            return new FuzzyCondition(() => 1 - a.Evaluate());
        }
    }
}
