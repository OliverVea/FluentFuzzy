namespace FluentFuzzy.Interfaces
{
    public interface IRuleBuilder
    {
        IRuleApplier Then(IConsequent consequent);
        IRuleBuilder And(ICondition fuzzyCondition);
        IRuleBuilder Or(ICondition fuzzyCondition);
    }
}