namespace FuzzyLogic
{
    public static class FuzzyRule
    {
        public static FuzzyRuleBuilder If(FuzzyCondition fuzzyCondition)
        {
            return new FuzzyRuleBuilder(fuzzyCondition);
        }
    }
}
