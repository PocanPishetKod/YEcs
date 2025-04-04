namespace YEcs.Interfaces.Historicity;

/// <summary>
/// History of the world.
/// </summary>
public interface IWorldHistory
{
    /// <summary>
    /// Adds an event to the history.
    /// </summary>
    /// <param name="event"></param>
    void Push(WorldEvent @event);
    
    /// <summary>
    /// Clears history.
    /// </summary>
    void Clear();
    
    /// <summary>
    /// History length - the number of events in a story.
    /// </summary>
    int Length { get; }

    /// <summary>
    /// Creates and returns history navigator.
    /// </summary>
    /// <returns></returns>
    IHistoryNavigator CreateNavigator();
}