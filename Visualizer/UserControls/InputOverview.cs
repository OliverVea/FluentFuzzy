using Visualizer.Collections;
using Visualizer.Forms;

namespace Visualizer.UserControls;

internal class InputOverview : BaseListView
{
    private const int TitleHeight = 40;
    private const string TitleText = "Fuzzy Inputs";
    private const int ButtonHeight = 30;
    private const string ButtonText = "Add Input";

    private readonly Dictionary<string, Control> _functions = new();
    
    public InputOverview() : base(TitleHeight, TitleText, ButtonHeight, ButtonText)
    {
        FuzzyInputCollection.InputAdded.EventHandler += InputCreated;
        FuzzyInputCollection.InputAdded.EventHandler += InputRemoved;
    }

    private void InputRemoved(object? sender, EventArgs e)
    {
        if (e is not FuzzyInputCollection.InputRemovedArgs args) return;

        if (!_functions.TryGetValue(args.RemovedInput.Name, out var element)) return;
        
        RemoveElement(element);
    }

    private void InputCreated(object? sender, EventArgs e)
    {
        if (e is not FuzzyInputCollection.InputAddedArgs args) return;

        var element = new FuzzyInputDisplay(args.AddedInput);
        InsertElement(element);
        _functions[args.AddedInput.Name] = element;
    }

    protected override void OnAddButton()
    {
        var dialog = new BaseCreateForm<CreatInputOptions>();
        dialog.Show();
    }

    protected override bool IsButtonActive()
    {
        return true;
    }
}