using System;

/// <summary>
/// Represents an eternal goal that is never complete.
/// Each time it's recorded, the user receives points.
/// Example: Read scriptures, exercise, meditate.
/// </summary>
public class EternalGoal : Goal
{
    // Private member variable to track how many times this goal has been recorded
    private int _timesRecorded;

    /// <summary>
    /// Constructor for creating a new EternalGoal.
    /// </summary>
    /// <param name="name">Short name of the goal</param>
    /// <param name="description">Description of the goal</param>
    /// <param name="points">Points awarded each time the goal is recorded</param>
    public EternalGoal(string name, string description, int points) 
        : base(name, description, points)
    {
        _timesRecorded = 0;
    }

    /// <summary>
    /// Constructor for loading an EternalGoal from file.
    /// </summary>
    /// <param name="name">Short name of the goal</param>
    /// <param name="description">Description of the goal</param>
    /// <param name="points">Points awarded each time</param>
    /// <param name="timesRecorded">Number of times already recorded</param>
    public EternalGoal(string name, string description, int points, int timesRecorded) 
        : base(name, description, points)
    {
        _timesRecorded = timesRecorded;
    }

    /// <summary>
    /// Records an event for this eternal goal.
    /// Always awards points since it can never be complete.
    /// </summary>
    /// <returns>Points earned from this event</returns>
    public override int RecordEvent()
    {
        _timesRecorded++;
        return _points;
    }

    /// <summary>
    /// Eternal goals are never complete.
    /// </summary>
    /// <returns>Always returns false</returns>
    public override bool IsComplete()
    {
        return false;
    }

    /// <summary>
    /// Gets the details string including times recorded.
    /// </summary>
    /// <returns>Formatted string for display</returns>
    public override string GetDetailsString()
    {
        // Eternal goals show infinity symbol and times recorded
        return $"[∞] {_shortName} ({_description}) -- Recorded {_timesRecorded} time(s)";
    }

    /// <summary>
    /// Gets the string representation for saving to file.
    /// Format: EternalGoal:Name,Description,Points,TimesRecorded
    /// </summary>
    /// <returns>String for file storage</returns>
    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{_shortName},{_description},{_points},{_timesRecorded}";
    }
}
