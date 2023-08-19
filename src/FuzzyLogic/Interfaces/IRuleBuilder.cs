namespace FuzzyLogic.Interfaces
{
    public interface IRuleBuilder
    {
        IRuleApplier Then(Consequent consequent);
        IRuleBuilder And(FuzzyCondition fuzzyCondition);
        IRuleBuilder Or(FuzzyCondition fuzzyCondition);
    }
}