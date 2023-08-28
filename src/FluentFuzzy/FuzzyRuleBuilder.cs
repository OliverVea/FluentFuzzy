using FluentFuzzy.Interfaces;

namespace FluentFuzzy
{
    internal class FuzzyRuleBuilder : IRuleApplier, IRuleBuilder
    {
        private readonly ICondition _condition;

        internal FuzzyRuleBuilder(ICondition condition)
        {
            _condition = condition;
        }
    
        public IRuleApplier Then(IConsequent consequent)
        {
            consequent.Add(_condition);
            return this;
        }
    
        public IRuleApplier And(IConsequent consequent)
        {
            consequent.Add(_condition);
            return this;
        }

        public IRuleApplier Else(IConsequent consequent)
        {
            var invertedFuzzyCondition = Condition.Not(_condition);
            consequent.Add(invertedFuzzyCondition);
            return new FuzzyRuleBuilder(invertedFuzzyCondition);
        }

        public IRuleBuilder ElseIf(ICondition condition)
        {
            var invertedCondition = Condition.Not(_condition);
            var combinedCondition = Condition.And(invertedCondition, condition);
            return new FuzzyRuleBuilder(combinedCondition);
        }

        public IRuleBuilder And(ICondition condition)
        {
            var combinedCondition = Condition.And(_condition, condition);
            return new FuzzyRuleBuilder(combinedCondition);
        }

        public IRuleBuilder Or(ICondition condition)
        {
            var combinedCondition = Condition.Or(_condition, condition);
            return new FuzzyRuleBuilder(combinedCondition);
        }
    }
}
