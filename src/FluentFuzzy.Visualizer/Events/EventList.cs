using System.ComponentModel;

namespace FluentFuzzy.Visualizer.Events;

public static class EventList
{
    private static EventHandlerList? _events;
    public static EventHandlerList Events => _events ??= new EventHandlerList();
}