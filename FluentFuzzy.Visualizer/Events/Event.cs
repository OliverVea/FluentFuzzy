namespace FluentFuzzy.Visualizer.Events;

public class Event<T> where T : EventArgs
{
    private readonly object _key = new();
    
    public void Invoke(object? sender,T args)
    {
        var h = EventList.Events[_key];
        if (h is not EventHandler handler) return;

        handler.Invoke(sender, args);
    }
    
    public event EventHandler EventHandler
    {
        add => EventList.Events.AddHandler(_key, value);
        remove => EventList.Events.RemoveHandler(_key, value);
    }
}