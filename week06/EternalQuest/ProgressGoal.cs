using System;

/// <summary>
/// EXCEEDING REQUIREMENTS: Represents a progress goal for large accomplishments.
/// Allows recording incremental progress towards a target value.
/// Awards points proportional to progress made.
/// Example: Training for a marathon (track miles run), writing a book (track pages written).
/// </summary>
public class ProgressGoal : Goal
{
    // Private member variables
    private int _currentProgress;
    private int _targetProgress;
    private int _bonus;
    private string _unit;

    /// <summary>
    /// Constructor for creating a new ProgressGoal.
    /// </summary>
    /// <param name="name">Short name of the goal</param>
    /// <param name="description">Description of the goal</param>
    /// <param name="points">Points awarded per unit of progress</param>
    /// <param name="targetProgress">Target value to reach</param>
    /// <param name="bonus">Bonus points upon completion</param>
    /// <param name="unit">Unit of measurement (e.g., "miles", "pages")</param>
    public ProgressGoal(string name, string description, int points, int targetProgress, int bonus, string unit) 
        : base(name, description, points)
    {
        _currentProgress = 0;
        _targetProgress = targetProgress;
        _bonus = bonus;
        _unit = unit;
    }

    /// <summary>
    /// Constructor for loading a ProgressGoal from file.
    /// </summary>
    public ProgressGoal(string name, string description, int points, int targetProgress, int bonus, string unit, int currentProgress) 
        : base(name, description, points)
    {
        _currentProgress = currentProgress;
        _targetProgress = targetProgress;
        _bonus = bonus;
        _unit = unit;
    }

    /// <summary>
    /// Records progress for this goal.
    /// Prompts the user for amount of progress made.
    /// </summary>
    /// <returns>Points earned based on progress</returns>
    public override int RecordEvent()
    {
        if (_currentProgress >= _targetProgress)
        {
            return 0; // Already complete
        }

        Console.Write($"How many {_unit} did you complete? ");
        string input = Console.ReadLine();
        
        if (int.TryParse(input, out int progress) && progress > 0)
        {
            int previousProgress = _currentProgress;
            _currentProgress += progress;
            
            // Cap progress at target
            if (_currentProgress > _targetProgress)
            {
                progress = _targetProgress - previousProgress;
                _currentProgress = _targetProgress;
            }
            
            int pointsEarned = progress * _points;
            
            // Check if we just completed the goal
            if (_currentProgress >= _targetProgress && previousProgress < _targetProgress)
            {
                pointsEarned += _bonus;
                Console.WriteLine($"🎉 Congratulations! You completed your goal and earned a {_bonus} point bonus!");
            }
            
            return pointsEarned;
        }
        
        return 0;
    }

    /// <summary>
    /// Checks if the goal has reached its target.
    /// </summary>
    /// <returns>True if target reached, false otherwise</returns>
    public override bool IsComplete()
    {
        return _currentProgress >= _targetProgress;
    }

    /// <summary>
    /// Gets the details string showing progress.
    /// </summary>
    /// <returns>Formatted string for display</returns>
    public override string GetDetailsString()
    {
        string checkbox = IsComplete() ? "[X]" : "[ ]";
        int percentage = (int)(((double)_currentProgress / _targetProgress) * 100);
        return $"{checkbox} {_shortName} ({_description}) -- Progress: {_currentProgress}/{_targetProgress} {_unit} ({percentage}%)";
    }

    /// <summary>
    /// Gets the string representation for saving to file.
    /// Format: ProgressGoal:Name,Description,Points,TargetProgress,Bonus,Unit,CurrentProgress
    /// </summary>
    /// <returns>String for file storage</returns>
    public override string GetStringRepresentation()
    {
        return $"ProgressGoal:{_shortName},{_description},{_points},{_targetProgress},{_bonus},{_unit},{_currentProgress}";
    }
}
