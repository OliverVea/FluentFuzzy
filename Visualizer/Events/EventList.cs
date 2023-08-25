using System.ComponentModel;

namespace Visualizer.Events;

public static class EventList
{
    private static EventHandlerList? _events;
    public static EventHandlerList Events => _events ??= new EventHandlerList();
}