using System;
using System.Collections.Generic;
using System.Linq;
using FuzzyLogic.MemberFunctions;

namespace FuzzyLogic
{
    public class FuzzyOutput
    {
        private readonly Dictionary<int, Consequent> _fuzzyActions = new Dictionary<int, Consequent>();
        
        public Consequent Is(int consequent)
        {
            return _fuzzyActions[consequent];
        }
        
        public double Evaluate()
        { 
            var centroids = _fuzzyActions.Values.Select(x => x.GetCentroid());
            var centroid = centroids.Sum();
            return centroid.Value;
        }
        
        public void Set(int consequent, IHasCentroid memberFunction)
        {
            if (_fuzzyActions.ContainsKey(consequent))
                throw new ArgumentException($"Member function of {consequent} is already set. Please use different values for different consequents.", nameof(consequent));
            
            var action = new Consequent(memberFunction);
            _fuzzyActions[consequent] = action;
        }
    }
}

