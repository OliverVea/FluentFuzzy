using FluentFuzzy.Visualizer.Collections;

namespace FluentFuzzy.Visualizer.UserControls;

internal class RuleOverview : BaseListView
{
    private const int TitleHeight = 40;
    private const string TitleText = "Fuzzy Rules";
    private const int AddRuleHeight = 30;
    private const string AddRuleText = "Add Rule";

    public RuleOverview() : base(TitleHeight, TitleText, AddRuleHeight, AddRuleText)
    {
        FuzzyInputCollection.InputAdded.EventHandler += (_, _) => RefreshButtonActive();
        FuzzyInputCollection.InputRemoved.EventHandler += (_, _) => RefreshButtonActive();
    }

    protected override void OnAddButton()
    {
    }

    protected override bool IsButtonActive()
    {
        var hasFuzzyInputs = FuzzyInputCollection.FuzzyInputs.Any();
        return hasFuzzyInputs;
    }
}