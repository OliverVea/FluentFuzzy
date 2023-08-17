namespace FuzzyLogic.Test;

public class TestUT
{
    private const int Critical = 0;
    private const int Low = 1;
    private const int Medium = 2;
    private const int High = 3;

    private const int Retreat = 6;
    
    private readonly HealthSense Health = new();
    private readonly Goal Goal = new Goal();

    [Test(Description = "Single Antecedent and Consequent")]
    public void Syntax_1()
    {
        Health.Set(Critical, new MemberFunction.Trapezoid(0, 0, 0.05, 0.2));
        Health.Set(Low, new MemberFunction.Triangle(0, 0.25, 0.5));
        Health.Set(Medium, new MemberFunction.Triangle(0.25, 0.5, 0.75));
        Health.Set(High, new MemberFunction.Trapezoid(0.5, 0.75, 1, 1));
        
        FuzzyRule.If(Health.Is(Low)).Then(Goal.Is(Retreat));
    }

/*
[Test(Description = "Multiple Antecedents")]
public void Syntax_2()
{
    FuzzyRule.If(Health.Is.Low).And(EnemiesNearby.Is.High).Then(Goal.Is.Flee);
}

[Test(Description = "Negation")]
public void Syntax_6()
{
    FuzzyRule.If(Health.Is.Not.Low).Then(Goal.Is.MoveForward);
}

[Test(Description = "Membership Grades")]
public void Syntax_3()
{
    FuzzyRule.If(Health.Is.Critical).And(ThreatLevel.Is.Moderate).Then(Goal.Is.Defend);
}

[Test(Description = "Fuzzy Quantifiers")]
public void Syntax_4()
{
    FuzzyRule.If(AtLeast(2, EnemiesNearby)).And(AmmoLevel.Is.High).Then(Goal.Is.Engage);
}

[Test(Description = "Hedges")]
public void Syntax_5()
{
    FuzzyRule.If(Health.Is.Somewhat.Low).And(AlliesEngaged.Is.FairlyHigh).Then(Goal.Is.Assist);
}

[Test(Description = "Compound Antecedent")]
public void Syntax_7()
{
    FuzzyRule.If((Health.Is.Low).Or(Ammo.Is.Low)).And(EnemiesNearby.Is.High).Then(Goal.Is.EvasiveManeuvers);
}

[Test(Description = "Implication Words")]
public void Syntax_8()
{
    FuzzyRule.If(ThreatLevel.Is.High).Then(Goal.Is.SeekCover);
}

[Test(Description = "Chained Rules")]
public void Syntax_9()
{
    FuzzyRule.If(AllyNeedsHelp).Then(Goal.Is.Assist).ElseIf(ThreatLevel.Is.VeryHigh).Then(Goal.Is.Retreat);

[Test(Description = "Weighted Rules")]
public void Syntax_10()
{
    FuzzyRule.If(AlliesEngaged.Is.High).Then(Goal.Is.ProvideHeavySupport).WithWeight(0.8);
    FuzzyRule.If(AlliesEngaged.Is.Low).Then(Goal.Is.ProvideLightSupport).WithWeight(0.2);
}

[Test(Description = "T-Norm and S-Norm Operators")]
public void Syntax_11()
{
    FuzzyRule.If((Health.Is.Low).And(Ammo.Is.Low)).Or(EnemiesNearby.Is.VeryHigh).Then(Goal.Is.Fallback);
}

[Test(Description = "Fuzzy Set Operators")]
public void Syntax_12()
{
    FuzzyRule.If(Health.Is.Poor).And(ThreatLevel.Is.NotLow).Then(Goal.Is.Defend);
}
*/
}

public abstract class FuzzyOutput
{
    private 
    
    public FuzzyAction Is(int consequent)
    {
        throw new NotImplementedException();
    }
    
}

public class Goal : FuzzyOutput
{
}

internal abstract class FuzzyInput
{
    private readonly Dictionary<int, MemberFunction> _memberFunctions = new();

    public void Set(int antecedent, MemberFunction memberFunction)
    {
        _memberFunctions[antecedent] = memberFunction;
    }

    public FuzzyCondition Is(int antecedent)
    {
        if (_memberFunctions.TryGetValue(antecedent, out var memberFunction))
            return new FuzzyCondition(memberFunction, () => Value);
        
        throw new ArgumentException($"Member function of {antecedent} is not implemented.", nameof(antecedent));
    }
    
    protected abstract double Value { get; }
}

internal class HealthSense : FuzzyInput
{
    private double _value;
    protected override double Value => _value;
    
    // Debugging
    public void SetValue(double value)
    {
        _value = value;
    }
}

public static class FuzzyRule
{
    public static FuzzyRuleBuilder If(FuzzyCondition fuzzyCondition)
    {
        return new FuzzyRuleBuilder(fuzzyCondition);
    }
}

public class FuzzyRuleBuilder
{
    private readonly FuzzyCondition _fuzzyCondition;

    public FuzzyRuleBuilder(FuzzyCondition fuzzyCondition)
    {
        _fuzzyCondition = fuzzyCondition;
    }
    
    public void Then(FuzzyAction fuzzyAction)
    {
        fuzzyAction.Add(_fuzzyCondition);
    }
}

public class FuzzyCondition
{
    private readonly MemberFunction _memberFunction;
    private readonly Func<double> _valueFunction;
    
    public FuzzyCondition(MemberFunction memberFunction, Func<double> func)
    {
        _memberFunction = memberFunction;
        _valueFunction = func;
    }
    
    public double Evaluate()
    {
        return _memberFunction.Evaluate(_valueFunction());
    }
}

public class FuzzyAction
{
    private readonly List<FuzzyCondition> _fuzzyConditions = new();

    public void Add(FuzzyCondition fuzzyCondition)
    {
        _fuzzyConditions.Add(fuzzyCondition);
    }
    
    public double Evaluate()
    {
        return _fuzzyConditions.Max(x => x.Evaluate());
    }
}