using System;

/*
 * ═══════════════════════════════════════════════════════════════════════════════
 * ETERNAL QUEST - A Goal Tracking Program with Gamification
 * ═══════════════════════════════════════════════════════════════════════════════
 * 
 * EXCEEDING CORE REQUIREMENTS - Creative Additions:
 * 
 * 1. LEVELING SYSTEM:
 *    - Players earn levels as they accumulate points
 *    - 11 different levels with unique titles (e.g., "Novice Seeker" to "Celestial Legend")
 *    - Level-up celebration messages when reaching new levels
 *    - Visual progress bar showing advancement toward next level
 * 
 * 2. NEGATIVE GOALS (New Goal Type):
 *    - Allows tracking of bad habits the user wants to break
 *    - Recording a negative goal LOSES points (penalty for slip-ups)
 *    - Special display showing slip-up count
 *    - Goal shows as "complete" only if user has zero slip-ups (perfect avoidance)
 *    - Example: "Stop eating junk food" - if you slip up, you lose points
 * 
 * 3. PROGRESS GOALS (New Goal Type):
 *    - Allows tracking incremental progress toward large accomplishments
 *    - Users specify a target amount and unit of measurement
 *    - Points are awarded proportionally based on progress made
 *    - Bonus awarded when the target is reached
 *    - Displays percentage completion and progress bar
 *    - Example: "Train for marathon - track miles run toward 100 mile goal"
 * 
 * 4. ENHANCED USER INTERFACE:
 *    - Decorative headers and borders for better visual appeal
 *    - Emoji feedback for accomplishments and slip-ups
 *    - Player information display with score, level, and title
 *    - Visual progress bar for level advancement
 *    - Infinity symbol [∞] for eternal goals to indicate never-ending nature
 *    - Star symbol [★] for perfect negative goal avoidance
 * 
 * 5. PLAYER CUSTOMIZATION:
 *    - Players can set their name which is saved and loaded with goals
 *    - Personalized welcome messages when loading saved games
 * 
 * ═══════════════════════════════════════════════════════════════════════════════
 */

/// <summary>
/// Main program class for the Eternal Quest goal tracking application.
/// </summary>
class Program
{
    /// <summary>
    /// Main entry point for the program.
    /// </summary>
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        bool running = true;

        DisplayWelcome();
        PromptForPlayerName(goalManager);

        while (running)
        {
            DisplayMenu();
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    goalManager.CreateGoal();
                    break;
                
                case "2":
                    goalManager.ListGoals();
                    break;
                
                case "3":
                    goalManager.SaveGoals();
                    break;
                
                case "4":
                    goalManager.LoadGoals();
                    break;
                
                case "5":
                    goalManager.RecordEvent();
                    break;
                
                case "6":
                    goalManager.DisplayPlayerInfo();
                    break;
                
                case "7":
                    running = false;
                    DisplayGoodbye();
                    break;
                
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    /// <summary>
    /// Displays the welcome banner.
    /// </summary>
    static void DisplayWelcome()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║              ★ ETERNAL QUEST ★                               ║");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║        Your Journey of Personal Growth Awaits!               ║");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║   Track your goals, earn points, and level up as you         ║");
        Console.WriteLine("║   progress on your eternal quest of self-improvement!        ║");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();
    }

    /// <summary>
    /// Prompts the user for their player name.
    /// </summary>
    /// <param name="goalManager">The goal manager to set the name on</param>
    static void PromptForPlayerName(GoalManager goalManager)
    {
        Console.Write("Enter your name, brave adventurer: ");
        string name = Console.ReadLine();
        
        if (!string.IsNullOrWhiteSpace(name))
        {
            goalManager.SetPlayerName(name);
        }
        
        Console.WriteLine($"\nWelcome to your Eternal Quest! May your journey be blessed.");
        Console.WriteLine("You begin as a Level 1 Novice Seeker. Set goals and earn points to level up!\n");
    }

    /// <summary>
    /// Displays the main menu.
    /// </summary>
    static void DisplayMenu()
    {
        Console.WriteLine("\n╔═══════════════════════════════╗");
        Console.WriteLine("║       MENU OPTIONS            ║");
        Console.WriteLine("╠═══════════════════════════════╣");
        Console.WriteLine("║  1. Create New Goal           ║");
        Console.WriteLine("║  2. List Goals                ║");
        Console.WriteLine("║  3. Save Goals                ║");
        Console.WriteLine("║  4. Load Goals                ║");
        Console.WriteLine("║  5. Record Event              ║");
        Console.WriteLine("║  6. Display Player Info       ║");
        Console.WriteLine("║  7. Quit                      ║");
        Console.WriteLine("╚═══════════════════════════════╝");
    }

    /// <summary>
    /// Displays a goodbye message when quitting.
    /// </summary>
    static void DisplayGoodbye()
    {
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║   Thank you for working on your Eternal Quest today!         ║");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║   Remember: Every small step brings you closer to            ║");
        Console.WriteLine("║   becoming the person you want to be.                        ║");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║   See you next time! ★                                       ║");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();
    }
}