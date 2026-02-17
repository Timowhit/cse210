using System;

/// <summary>
/// Represents a simple goal that can be marked complete once.
/// Example: Run a marathon, complete a project.
/// </summary>
public class SimpleGoal : Goal
{
    // Private member variable for completion status
    private bool _isComplete;

    /// <summary>
    /// Constructor for creating a new SimpleGoal.
    /// </summary>
    /// <param name="name">Short name of the goal</param>
    /// <param name="description">Description of the goal</param>
    /// <param name="points">Points awarded upon completion</param>
    public SimpleGoal(string name, string description, int points) 
        : base(name, description, points)
    {
        _isComplete = false;
    }

    /// <summary>
    /// Constructor for loading a SimpleGoal from file.
    /// </summary>
    /// <param name="name">Short name of the goal</param>
    /// <param name="description">Description of the goal</param>
    /// <param name="points">Points awarded upon completion</param>
    /// <param name="isComplete">Whether the goal has been completed</param>
    public SimpleGoal(string name, string description, int points, bool isComplete) 
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    /// <summary>
    /// Records the completion of this goal.
    /// Can only be completed once.
    /// </summary>
    /// <returns>Points earned (0 if already complete)</returns>
    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }
        return 0;
    }

    /// <summary>
    /// Checks if the goal has been completed.
    /// </summary>
    /// <returns>True if complete, false otherwise</returns>
    public override bool IsComplete()
    {
        return _isComplete;
    }

    /// <summary>
    /// Gets the string representation for saving to file.
    /// Format: SimpleGoal:Name,Description,Points,IsComplete
    /// </summary>
    /// <returns>String for file storage</returns>
    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{_shortName},{_description},{_points},{_isComplete}";
    }
}
