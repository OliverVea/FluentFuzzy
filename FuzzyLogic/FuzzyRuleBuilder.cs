namespace FuzzyLogic
{
    public class FuzzyRuleBuilder
    {
        private readonly FuzzyCondition _fuzzyCondition;

        internal FuzzyRuleBuilder(FuzzyCondition fuzzyCondition)
        {
            _fuzzyCondition = fuzzyCondition;
        }
    
        public void Then(Consequent consequent)
        {
            consequent.Add(_fuzzyCondition);
        }

        public FuzzyRuleBuilder And(FuzzyCondition fuzzyCondition)
        {
            return new FuzzyRuleBuilder(_fuzzyCondition & fuzzyCondition);
        }
    }
}
