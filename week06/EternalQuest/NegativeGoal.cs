using System;

/// <summary>
/// EXCEEDING REQUIREMENTS: Represents a negative goal for breaking bad habits.
/// Recording this goal means you slipped up and LOSE points.
/// The goal is to have zero or minimal recordings.
/// Example: Stopped eating junk food, stopped procrastinating.
/// </summary>
public class NegativeGoal : Goal
{
    // Private member variables
    private int _timesRecorded;

    /// <summary>
    /// Constructor for creating a new NegativeGoal.
    /// </summary>
    /// <param name="name">Short name of the bad habit</param>
    /// <param name="description">Description of the habit to break</param>
    /// <param name="points">Points LOST each time the habit is recorded</param>
    public NegativeGoal(string name, string description, int points) 
        : base(name, description, points)
    {
        _timesRecorded = 0;
    }

    /// <summary>
    /// Constructor for loading a NegativeGoal from file.
    /// </summary>
    /// <param name="name">Short name of the bad habit</param>
    /// <param name="description">Description of the habit</param>
    /// <param name="points">Points lost each time</param>
    /// <param name="timesRecorded">Number of times already recorded</param>
    public NegativeGoal(string name, string description, int points, int timesRecorded) 
        : base(name, description, points)
    {
        _timesRecorded = timesRecorded;
    }

    /// <summary>
    /// Records a slip-up for this negative goal.
    /// Returns negative points (the user loses points).
    /// </summary>
    /// <returns>Negative points (penalty for recording)</returns>
    public override int RecordEvent()
    {
        _timesRecorded++;
        // Return negative points - this is a penalty!
        return -_points;
    }

    /// <summary>
    /// Negative goals are never "complete" - the goal is to not record them.
    /// Shows as complete only if never recorded (perfect avoidance).
    /// </summary>
    /// <returns>True if never recorded, false otherwise</returns>
    public override bool IsComplete()
    {
        return _timesRecorded == 0;
    }

    /// <summary>
    /// Gets the details string showing slip-up count.
    /// </summary>
    /// <returns>Formatted string for display</returns>
    public override string GetDetailsString()
    {
        string status = _timesRecorded == 0 ? "[★]" : "[-]";
        string message = _timesRecorded == 0 
            ? "Perfect! No slip-ups!" 
            : $"Slip-ups: {_timesRecorded} (-{_points} pts each)";
        return $"{status} {_shortName} ({_description}) -- {message}";
    }

    /// <summary>
    /// Gets the string representation for saving to file.
    /// Format: NegativeGoal:Name,Description,Points,TimesRecorded
    /// </summary>
    /// <returns>String for file storage</returns>
    public override string GetStringRepresentation()
    {
        return $"NegativeGoal:{_shortName},{_description},{_points},{_timesRecorded}";
    }
}
