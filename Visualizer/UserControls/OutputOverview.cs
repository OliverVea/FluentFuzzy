namespace Visualizer.UserControls;

internal class OutputOverview : BaseListView
{
    private const int TitleHeight = 40;
    private const string TitleText = "Fuzzy Outputs";
    private const int AddRuleHeight = 30;
    private const string AddRuleText = "Add Output";
    
    public OutputOverview() : base(TitleHeight, TitleText, AddRuleHeight, AddRuleText)
    {
    }

    protected override void OnAddButton()
    {
    }

    protected override bool IsButtonActive()
    {
        return true;
    }
}