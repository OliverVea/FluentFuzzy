namespace FuzzyLogic
{
    public interface IRuleApplier
    {
        IRuleApplier And(Consequent consequent);
        IRuleApplier Else(Consequent consequent);
    }
}