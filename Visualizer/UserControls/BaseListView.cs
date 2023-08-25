namespace Visualizer.UserControls;

public abstract class BaseListView : UserControl
{
    protected abstract void OnAddButton();
    protected abstract bool IsButtonActive();
    
    protected void SetButtonActive(bool active) => _addButton.Enabled = active;
    
    private readonly Label _title = new()
    {
        Text = "Fuzzy Rules",
        Dock = DockStyle.Top,
        Font = Constants.Fonts.Title,
        TextAlign = ContentAlignment.MiddleCenter,
    };

    private readonly TableLayoutPanel _layout = new()
    {
        Dock = DockStyle.Fill
    };

    private readonly TableLayoutPanel _elements = new()
    {
        Dock = DockStyle.Fill,
        ColumnCount = 1,
        AutoScroll = true
    };

    private readonly Button _addButton = new()
    {
        Text = "Add Rule",
        Anchor = AnchorStyles.Left | AnchorStyles.Right
    };

    private readonly Panel _fillPanel = new()
    {
        Size = new Size(0, 0),
        Dock = DockStyle.Fill
    };

    protected BaseListView(int titleHeight, string titleText, int addButtonHeight, string addButtonText)
    {
        _title.Height = titleHeight;
        _title.Text = titleText;
        
        _addButton.Height = addButtonHeight;
        _addButton.Text = addButtonText;
        _addButton.Click += (_, _) => OnAddButton();
        
        _elements.RowStyles.Add(Constants.RowStyles.AutoSize);
        _elements.RowStyles.Add(Constants.RowStyles.AutoSize);
        _elements.Controls.Add(_addButton);
        _elements.Controls.Add(_fillPanel);

        _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, titleHeight));
        _layout.RowStyles.Add(Constants.RowStyles.AutoSize);

        _layout.Controls.Add(_title);
        _layout.Controls.Add(_elements);
        
        Controls.Add(_layout);
        
        RefreshButtonActive();
    }

    protected void RefreshButtonActive()
    {
        var active = IsButtonActive();
        SetButtonActive(active);
    }

    protected void InsertElement(Control control)
    {
        const int col = 0;
        var row = _elements.Controls.Count - 2;
        control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _elements.Controls.Add(control, col, row);
    }

    protected void RemoveElement(Control control)
    {
        _elements.Controls.Remove(control);
    }
}