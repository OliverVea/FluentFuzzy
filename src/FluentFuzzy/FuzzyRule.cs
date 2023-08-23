using FuzzyLogic.Interfaces;

namespace FuzzyLogic
{
    public static class FuzzyRule
    {
        public static IRuleBuilder If(ICondition condition)
        {
            return new FuzzyRuleBuilder(condition);
        }
    }
}
