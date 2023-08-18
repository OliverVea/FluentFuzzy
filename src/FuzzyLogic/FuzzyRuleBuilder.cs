namespace FuzzyLogic
{
    public class FuzzyRuleBuilder : IRuleApplier
    {
        private readonly FuzzyCondition _fuzzyCondition;

        internal FuzzyRuleBuilder(FuzzyCondition fuzzyCondition)
        {
            _fuzzyCondition = fuzzyCondition;
        }
    
        public IRuleApplier Then(Consequent consequent)
        {
            consequent.Add(_fuzzyCondition);
            return this;
        }
    
        public IRuleApplier And(Consequent consequent)
        {
            consequent.Add(_fuzzyCondition);
            return this;
        }

        public IRuleApplier Else(Consequent consequent)
        {
            var invertedFuzzyCondition = FuzzyCondition.Not(_fuzzyCondition);
            consequent.Add(invertedFuzzyCondition);
            return new FuzzyRuleBuilder(invertedFuzzyCondition);
        }

        public FuzzyRuleBuilder And(FuzzyCondition fuzzyCondition)
        {
            var combinedCondition = FuzzyCondition.And(_fuzzyCondition, fuzzyCondition);
            return new FuzzyRuleBuilder(combinedCondition);
        }

        public FuzzyRuleBuilder Or(FuzzyCondition fuzzyCondition)
        {
            var combinedCondition = FuzzyCondition.Or(_fuzzyCondition, fuzzyCondition);
            return new FuzzyRuleBuilder(combinedCondition);
        }
    }
}
