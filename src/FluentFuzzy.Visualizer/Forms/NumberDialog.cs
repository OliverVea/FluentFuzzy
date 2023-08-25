namespace FluentFuzzy.Visualizer.Forms;

public class NumberDialog : Form
{
    private readonly NumericUpDown _numberField = new()
    {
        DecimalPlaces = 2
    };

    public double Value => (double)_numberField.Value;
        
    public NumberDialog(double inputValue)
    {
        _numberField.Value = (decimal)inputValue;
        
        Size = new Size(300, 200);
        
        FormBorderStyle = FormBorderStyle.FixedDialog;
        AutoScaleMode = AutoScaleMode.Font;
            
        Controls.Add(_numberField);
    }
}