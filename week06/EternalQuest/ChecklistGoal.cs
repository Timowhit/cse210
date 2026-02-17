using System;

/// <summary>
/// Represents a checklist goal that must be accomplished a certain number of times.
/// Each time it's recorded, the user receives points.
/// When the target is reached, the user receives a bonus.
/// Example: Attend the temple 10 times.
/// </summary>
public class ChecklistGoal : Goal
{
    // Private member variables
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    /// <summary>
    /// Constructor for creating a new ChecklistGoal.
    /// </summary>
    /// <param name="name">Short name of the goal</param>
    /// <param name="description">Description of the goal</param>
    /// <param name="points">Points awarded each time the goal is recorded</param>
    /// <param name="target">Number of times needed to complete the goal</param>
    /// <param name="bonus">Bonus points awarded upon completion</param>
    public ChecklistGoal(string name, string description, int points, int target, int bonus) 
        : base(name, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    /// <summary>
    /// Constructor for loading a ChecklistGoal from file.
    /// </summary>
    /// <param name="name">Short name of the goal</param>
    /// <param name="description">Description of the goal</param>
    /// <param name="points">Points awarded each time</param>
    /// <param name="target">Number of times needed to complete</param>
    /// <param name="bonus">Bonus points upon completion</param>
    /// <param name="amountCompleted">Number of times already completed</param>
    public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted) 
        : base(name, description, points)
    {
        _amountCompleted = amountCompleted;
        _target = target;
        _bonus = bonus;
    }

    /// <summary>
    /// Records an event for this checklist goal.
    /// Awards base points each time, plus bonus when target is reached.
    /// </summary>
    /// <returns>Points earned from this event</returns>
    public override int RecordEvent()
    {
        if (_amountCompleted < _target)
        {
            _amountCompleted++;
            
            // Check if we just completed the goal
            if (_amountCompleted == _target)
            {
                // Award base points plus bonus
                return _points + _bonus;
            }
            
            // Award just the base points
            return _points;
        }
        
        // Goal already complete, no more points
        return 0;
    }

    /// <summary>
    /// Checks if the goal has been completed (reached target).
    /// </summary>
    /// <returns>True if target reached, false otherwise</returns>
    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    /// <summary>
    /// Gets the details string including progress.
    /// </summary>
    /// <returns>Formatted string showing completion status and progress</returns>
    public override string GetDetailsString()
    {
        string checkbox = IsComplete() ? "[X]" : "[ ]";
        return $"{checkbox} {_shortName} ({_description}) -- Currently completed: {_amountCompleted}/{_target}";
    }

    /// <summary>
    /// Gets the string representation for saving to file.
    /// Format: ChecklistGoal:Name,Description,Points,Target,Bonus,AmountCompleted
    /// </summary>
    /// <returns>String for file storage</returns>
    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{_shortName},{_description},{_points},{_target},{_bonus},{_amountCompleted}";
    }
}
