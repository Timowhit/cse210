using System;

/// <summary>
/// Base class for all goal types in the Eternal Quest program.
/// Contains shared attributes and virtual methods for derived classes.
/// </summary>
public abstract class Goal
{
    // Protected member variables using _underscoreCamelCase naming convention
    // Protected so derived classes can access them directly
    protected string _shortName;
    protected string _description;
    protected int _points;

    /// <summary>
    /// Constructor for the Goal base class.
    /// </summary>
    /// <param name="name">Short name of the goal</param>
    /// <param name="description">Description of what the goal entails</param>
    /// <param name="points">Points awarded when recording the goal</param>
    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    /// <summary>
    /// Default constructor for loading goals from file.
    /// </summary>
    public Goal()
    {
        _shortName = "";
        _description = "";
        _points = 0;
    }

    /// <summary>
    /// Records an event for the goal (user accomplished the goal).
    /// Returns the points earned from this event.
    /// </summary>
    /// <returns>Points earned from recording this event</returns>
    public abstract int RecordEvent();

    /// <summary>
    /// Checks if the goal has been completed.
    /// </summary>
    /// <returns>True if the goal is complete, false otherwise</returns>
    public abstract bool IsComplete();

    /// <summary>
    /// Gets the string representation for displaying in the goal list.
    /// Includes the checkbox status and goal details.
    /// </summary>
    /// <returns>Formatted string for display</returns>
    public virtual string GetDetailsString()
    {
        string checkbox = IsComplete() ? "[X]" : "[ ]";
        return $"{checkbox} {_shortName} ({_description})";
    }

    /// <summary>
    /// Gets the string representation for saving to a file.
    /// Each derived class will override this to include its specific data.
    /// </summary>
    /// <returns>String representation for file storage</returns>
    public abstract string GetStringRepresentation();

    /// <summary>
    /// Gets the short name of the goal.
    /// </summary>
    /// <returns>The goal's short name</returns>
    public string GetShortName()
    {
        return _shortName;
    }

    /// <summary>
    /// Gets the points value of the goal.
    /// </summary>
    /// <returns>The goal's point value</returns>
    public int GetPoints()
    {
        return _points;
    }
}
