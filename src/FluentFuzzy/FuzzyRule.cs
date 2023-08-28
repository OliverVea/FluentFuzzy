using FluentFuzzy.Interfaces;

namespace FluentFuzzy
{
    public static class FuzzyRule
    {
        public static IRuleBuilder If(ICondition condition)
        {
            return new FuzzyRuleBuilder(condition);
        }
    }
}
