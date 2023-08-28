using FluentFuzzy.Visualizer.Collections;
using FluentFuzzy.Visualizer.Forms;

namespace FluentFuzzy.Visualizer.UserControls;

internal class OutputOverview : BaseListView
{
    private const int TitleHeight = 40;
    private const string TitleText = "Fuzzy Outputs";
    private const int ButtonHeight = 30;
    private const string ButtonText = "Add Output";

    private readonly Dictionary<string, Control> _functions = new();
    
    public OutputOverview() : base(TitleHeight, TitleText, ButtonHeight, ButtonText)
    {
        FuzzyOutputCollection.OutputAdded.EventHandler += OutputCreated;
        FuzzyOutputCollection.OutputAdded.EventHandler += OutputRemoved;
    }

    private void OutputRemoved(object? sender, EventArgs e)
    {
        if (e is not FuzzyOutputCollection.OutputRemovedArgs args) return;

        if (!_functions.TryGetValue(args.RemovedOutput.Name, out var element)) return;
        
        RemoveElement(element);
    }

    private void OutputCreated(object? sender, EventArgs e)
    {
        if (e is not FuzzyOutputCollection.OutputAddedArgs args) return;

        var element = new OutputDisplay(args.AddedOutput);
        InsertElement(element);
        _functions[args.AddedOutput.Name] = element;
    }

    protected override void OnAddButton()
    {
        new BaseCreateForm<CreateOutputOptions>("Create Fuzzy Output").Show();
    }

    protected override bool IsButtonActive()
    {
        return true;
    }
}