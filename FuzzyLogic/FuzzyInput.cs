using System;
using System.Collections.Generic;
using FuzzyLogic.MemberFunctions;

namespace FuzzyLogic
{
    public class FuzzyInput
    {
        private readonly Func<double> _valueFunction;
        
        private readonly Dictionary<int, IMemberFunction> _memberFunctions = new Dictionary<int, IMemberFunction>();
        public IReadOnlyDictionary<int, IMemberFunction> MemberFunctions => _memberFunctions;

        public FuzzyInput(Func<double> valueFunction)
        {
            _valueFunction = valueFunction;
        }


        public void Set(int antecedent, IMemberFunction memberFunction)
        {
            if (_memberFunctions.ContainsKey(antecedent))
                throw new ArgumentException($"Member function of {antecedent} is already set. Please use different values for different antecedents.", nameof(antecedent));
                
            _memberFunctions.Add(antecedent, memberFunction);
        }

        public FuzzyCondition Is(int antecedent)
        {
            if (_memberFunctions.TryGetValue(antecedent, out var memberFunction))
                return new FuzzyCondition(memberFunction, _valueFunction);
        
            throw new ArgumentException($"Member function of {antecedent} is not implemented.", nameof(antecedent));
        }

        public FuzzyCondition IsNot(int antecedent)
        {
            if (_memberFunctions.TryGetValue(antecedent, out var memberFunction))
                return FuzzyCondition.Not(new FuzzyCondition(memberFunction, _valueFunction));
        
            throw new ArgumentException($"Member function of {antecedent} is not implemented.", nameof(antecedent));
        }
    }
}

