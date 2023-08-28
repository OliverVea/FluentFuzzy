namespace FluentFuzzy.Interfaces
{
    public interface IRuleApplier
    {
        IRuleApplier And(IConsequent consequent);
        IRuleApplier Else(IConsequent consequent);
        IRuleBuilder ElseIf(ICondition fuzzyCondition);
    }
}