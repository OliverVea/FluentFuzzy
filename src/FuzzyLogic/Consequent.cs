using System.Collections.Generic;
using System.Linq;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic
{
    public class Consequent
    {
        private readonly List<FuzzyCondition> _fuzzyConditions = new List<FuzzyCondition>();

        private readonly IHasCentroid _memberFunction;
        
        internal Consequent(IHasCentroid memberFunction)
        {
            _memberFunction = memberFunction;
        }

        public void Add(FuzzyCondition fuzzyCondition)
        {
            _fuzzyConditions.Add(fuzzyCondition);
        }

        internal Centroid GetCentroid()
        {
            if (!_fuzzyConditions.Any()) return new Centroid(0, 0);
            
            var value = _fuzzyConditions.Select(x => x.Evaluate()).Max();
            return _memberFunction.GetCentroid(value);
        }
    }
}
