namespace FuzzyLogic.Interfaces
{
    public interface IRuleApplier
    {
        IRuleApplier And(Consequent consequent);
        IRuleApplier Else(Consequent consequent);
        IRuleBuilder ElseIf(FuzzyCondition fuzzyCondition);
    }
}