using System.Collections.Generic;
using System.Linq;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic
{
    internal class Consequent : IConsequent
    {
        private readonly List<ICondition> _fuzzyConditions = new List<ICondition>();

        private readonly IHasCentroid _memberFunction;
        
        internal Consequent(IHasCentroid memberFunction)
        {
            _memberFunction = memberFunction;
        }

        void IConsequent.Add(ICondition condition)
        {
            _fuzzyConditions.Add(condition);
        }

        internal Centroid GetCentroid()
        {
            if (!_fuzzyConditions.Any()) return new Centroid(0, 0);
            
            var value = _fuzzyConditions.Select(x => x.Evaluate()).Max();
            return _memberFunction.GetCentroid(value);
        }
    }
}
