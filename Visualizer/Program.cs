using Visualizer.Forms;

namespace Visualizer;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        
        var form = new MainForm();
        
        Application.Run(form);
    }
}