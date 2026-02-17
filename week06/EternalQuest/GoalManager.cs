using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Manages the collection of goals, user score, level system, and save/load functionality.
/// </summary>
public class GoalManager
{
    // Private member variables
    private List<Goal> _goals;
    private int _score;
    private string _playerName;

    // Level thresholds for gamification (EXCEEDING REQUIREMENTS)
    private static readonly int[] _levelThresholds = { 0, 500, 1000, 2000, 3500, 5000, 7500, 10000, 15000, 20000, 30000 };
    private static readonly string[] _levelTitles = 
    { 
        "Novice Seeker", 
        "Aspiring Disciple", 
        "Dedicated Learner",
        "Faithful Steward",
        "Devoted Warrior",
        "Noble Champion",
        "Valiant Hero",
        "Legendary Saint",
        "Eternal Guardian",
        "Divine Master",
        "Celestial Legend"
    };

    /// <summary>
    /// Constructor for GoalManager.
    /// </summary>
    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _playerName = "Hero";
    }

    /// <summary>
    /// Sets the player's name.
    /// </summary>
    /// <param name="name">Player name to set</param>
    public void SetPlayerName(string name)
    {
        _playerName = name;
    }

    /// <summary>
    /// Gets the current score.
    /// </summary>
    /// <returns>Current score</returns>
    public int GetScore()
    {
        return _score;
    }

    /// <summary>
    /// Gets the current level based on score (EXCEEDING REQUIREMENTS).
    /// </summary>
    /// <returns>Current level number</returns>
    public int GetLevel()
    {
        for (int i = _levelThresholds.Length - 1; i >= 0; i--)
        {
            if (_score >= _levelThresholds[i])
            {
                return i + 1;
            }
        }
        return 1;
    }

    /// <summary>
    /// Gets the title for the current level (EXCEEDING REQUIREMENTS).
    /// </summary>
    /// <returns>Level title string</returns>
    public string GetLevelTitle()
    {
        int level = GetLevel();
        if (level <= _levelTitles.Length)
        {
            return _levelTitles[level - 1];
        }
        return _levelTitles[_levelTitles.Length - 1];
    }

    /// <summary>
    /// Gets points needed for next level (EXCEEDING REQUIREMENTS).
    /// </summary>
    /// <returns>Points needed for next level, or 0 if at max</returns>
    public int GetPointsToNextLevel()
    {
        int level = GetLevel();
        if (level < _levelThresholds.Length)
        {
            return _levelThresholds[level] - _score;
        }
        return 0;
    }

    /// <summary>
    /// Displays player info including score, level, and title.
    /// </summary>
    public void DisplayPlayerInfo()
    {
        Console.WriteLine();
        Console.WriteLine("═══════════════════════════════════════════════════════════");
        Console.WriteLine($"  Player: {_playerName}");
        Console.WriteLine($"  Score: {_score} points");
        Console.WriteLine($"  Level: {GetLevel()} - {GetLevelTitle()}");
        
        int pointsToNext = GetPointsToNextLevel();
        if (pointsToNext > 0)
        {
            Console.WriteLine($"  Points to next level: {pointsToNext}");
            DisplayProgressBar();
        }
        else
        {
            Console.WriteLine("  ★ MAX LEVEL ACHIEVED! ★");
        }
        Console.WriteLine("═══════════════════════════════════════════════════════════");
        Console.WriteLine();
    }

    /// <summary>
    /// Displays a visual progress bar for level progression (EXCEEDING REQUIREMENTS).
    /// </summary>
    private void DisplayProgressBar()
    {
        int level = GetLevel();
        if (level < _levelThresholds.Length)
        {
            int currentLevelStart = _levelThresholds[level - 1];
            int nextLevelStart = _levelThresholds[level];
            int progressInLevel = _score - currentLevelStart;
            int levelRange = nextLevelStart - currentLevelStart;
            
            int barWidth = 30;
            int filledWidth = (int)((double)progressInLevel / levelRange * barWidth);
            
            Console.Write("  Progress: [");
            Console.Write(new string('█', filledWidth));
            Console.Write(new string('░', barWidth - filledWidth));
            Console.WriteLine("]");
        }
    }

    /// <summary>
    /// Displays all goals in the list.
    /// </summary>
    public void ListGoals()
    {
        Console.WriteLine("\nThe goals are:");
        
        if (_goals.Count == 0)
        {
            Console.WriteLine("  No goals yet. Create some goals to get started!");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_goals[i].GetDetailsString()}");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Creates a new goal based on user input.
    /// </summary>
    public void CreateGoal()
    {
        Console.WriteLine("\nThe types of goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.WriteLine("  4. Negative Goal (Break bad habits)");
        Console.WriteLine("  5. Progress Goal (Large accomplishments)");
        Console.Write("Which type of goal would you like to create? ");

        string choice = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();

        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, description, points));
                Console.WriteLine("✓ Simple goal created successfully!");
                break;
            
            case "2":
                _goals.Add(new EternalGoal(name, description, points));
                Console.WriteLine("✓ Eternal goal created successfully!");
                break;
            
            case "3":
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("What is the bonus for accomplishing it that many times? ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                Console.WriteLine("✓ Checklist goal created successfully!");
                break;
            
            case "4":
                _goals.Add(new NegativeGoal(name, description, points));
                Console.WriteLine("✓ Negative goal created successfully! Try not to record this one!");
                break;
            
            case "5":
                Console.Write("What is the target amount to reach? ");
                int progressTarget = int.Parse(Console.ReadLine());
                Console.Write("What is the unit of measurement (e.g., miles, pages, hours)? ");
                string unit = Console.ReadLine();
                Console.Write("What is the bonus for completing the goal? ");
                int progressBonus = int.Parse(Console.ReadLine());
                _goals.Add(new ProgressGoal(name, description, points, progressTarget, progressBonus, unit));
                Console.WriteLine("✓ Progress goal created successfully!");
                break;
            
            default:
                Console.WriteLine("Invalid choice. Goal not created.");
                break;
        }
    }

    /// <summary>
    /// Records an event for a selected goal.
    /// </summary>
    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("\nNo goals to record. Create some goals first!");
            return;
        }

        Console.WriteLine("\nThe goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_goals[i].GetShortName()}");
        }

        Console.Write("Which goal did you accomplish? ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int goalIndex) && goalIndex >= 1 && goalIndex <= _goals.Count)
        {
            Goal selectedGoal = _goals[goalIndex - 1];
            int previousLevel = GetLevel();
            int pointsEarned = selectedGoal.RecordEvent();
            _score += pointsEarned;

            // Ensure score doesn't go below 0
            if (_score < 0)
            {
                _score = 0;
            }

            if (pointsEarned > 0)
            {
                Console.WriteLine($"\n🎉 Congratulations! You have earned {pointsEarned} points!");
            }
            else if (pointsEarned < 0)
            {
                Console.WriteLine($"\n😔 You lost {Math.Abs(pointsEarned)} points. Keep trying!");
            }
            else
            {
                Console.WriteLine("\nNo points earned (goal may already be complete).");
            }

            Console.WriteLine($"You now have {_score} points.");

            // Check for level up (EXCEEDING REQUIREMENTS)
            int newLevel = GetLevel();
            if (newLevel > previousLevel)
            {
                Console.WriteLine();
                Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★");
                Console.WriteLine($"   LEVEL UP! You are now Level {newLevel}!");
                Console.WriteLine($"   New Title: {GetLevelTitle()}");
                Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★");
            }
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }

    /// <summary>
    /// Saves goals and score to a file.
    /// </summary>
    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            // Save player name and score first
            outputFile.WriteLine($"{_playerName},{_score}");

            // Save each goal
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine($"✓ Goals saved to {filename}");
    }

    /// <summary>
    /// Loads goals and score from a file.
    /// </summary>
    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found!");
            return;
        }

        _goals.Clear();
        string[] lines = File.ReadAllLines(filename);

        // First line contains player name and score
        string[] headerParts = lines[0].Split(',');
        _playerName = headerParts[0];
        _score = int.Parse(headerParts[1]);

        // Remaining lines contain goals
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] mainParts = line.Split(':');
            string goalType = mainParts[0];
            string[] parts = mainParts[1].Split(',');

            switch (goalType)
            {
                case "SimpleGoal":
                    _goals.Add(new SimpleGoal(
                        parts[0],                    // name
                        parts[1],                    // description
                        int.Parse(parts[2]),         // points
                        bool.Parse(parts[3])         // isComplete
                    ));
                    break;

                case "EternalGoal":
                    _goals.Add(new EternalGoal(
                        parts[0],                    // name
                        parts[1],                    // description
                        int.Parse(parts[2]),         // points
                        int.Parse(parts[3])          // timesRecorded
                    ));
                    break;

                case "ChecklistGoal":
                    _goals.Add(new ChecklistGoal(
                        parts[0],                    // name
                        parts[1],                    // description
                        int.Parse(parts[2]),         // points
                        int.Parse(parts[3]),         // target
                        int.Parse(parts[4]),         // bonus
                        int.Parse(parts[5])          // amountCompleted
                    ));
                    break;

                case "NegativeGoal":
                    _goals.Add(new NegativeGoal(
                        parts[0],                    // name
                        parts[1],                    // description
                        int.Parse(parts[2]),         // points
                        int.Parse(parts[3])          // timesRecorded
                    ));
                    break;

                case "ProgressGoal":
                    _goals.Add(new ProgressGoal(
                        parts[0],                    // name
                        parts[1],                    // description
                        int.Parse(parts[2]),         // points
                        int.Parse(parts[3]),         // targetProgress
                        int.Parse(parts[4]),         // bonus
                        parts[5],                    // unit
                        int.Parse(parts[6])          // currentProgress
                    ));
                    break;
            }
        }

        Console.WriteLine($"✓ Goals loaded from {filename}");
        Console.WriteLine($"  Welcome back, {_playerName}!");
    }
}
