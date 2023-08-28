namespace FluentFuzzy.Visualizer.UserControls;

public interface ICreateOptions
{
    void Create();
    void RegisterCreateCallback(Action<object?, EventArgs> callback);
}