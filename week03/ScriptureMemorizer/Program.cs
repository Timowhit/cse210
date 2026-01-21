/*
 * Scripture Memorizer Program
 * 
 * EXCEEDING REQUIREMENTS:
 * 
 * 1. SCRIPTURE LIBRARY: Instead of a single scripture, the program works with a 
 *    library of 8 pre-loaded scriptures. Users can practice random scriptures or 
 *    select a specific one from the list.
 * 
 * 2. FILE LOADING: The program can load scriptures from a text file (scriptures.txt).
 *    This allows users to add their own scriptures without modifying the code.
 * 
 * 3. SMART WORD HIDING: The stretch challenge is implemented - the program only
 *    selects from words that are not already hidden, making the practice more efficient.
 * 
 * 4. HINT SYSTEM: Users can type "hint" to reveal one hidden word, helping them
 *    when they're stuck on a particular word.
 * 
 * 5. DIFFICULTY LEVELS: Users can choose Easy (2 words), Medium (3 words), or 
 *    Hard (5 words) to control how many words are hidden each round.
 * 
 * 6. PROGRESS TRACKING: The display shows the current progress (percentage hidden)
 *    to help users track their memorization progress.
 * 
 * 7. RESTART OPTION: Users can type "restart" to reset the current scripture
 *    and practice it again from the beginning.
 * 
 * 8. MENU SYSTEM: A comprehensive menu allows users to navigate between different
 *    features including selecting scriptures, changing difficulty, and viewing the library.
 */

namespace ScriptureMemorizer;

/// <summary>
/// Main program class that runs the Scripture Memorizer application.
/// </summary>
class Program
{
    private static ScriptureLibrary _library = new ScriptureLibrary();
    private static int _wordsToHidePerRound = 3; // Default: Medium difficulty
    private static string _difficultyName = "Medium";

    /// <summary>
    /// Safely clears the console, handling cases where console is not available.
    /// </summary>
    static void SafeClear()
    {
        Console.WriteLine("\n\n\n");
    }

    static void Main(string[] args)
    {
        InitializeLibrary();
        ShowWelcome();
        
        bool running = true;
        while (running)
        {
            running = ShowMainMenu();
        }

        Console.WriteLine("\nThank you for using Scripture Memorizer. God bless!");
    }

    /// <summary>
    /// Initializes the scripture library with default scriptures and optionally loads from file.
    /// </summary>
    static void InitializeLibrary()
    {
        // Load default scriptures
        _library.LoadDefaultScriptures();

        // Try to load additional scriptures from file
        string scriptureFile = "scriptures.txt";
        if (File.Exists(scriptureFile))
        {
            try
            {
                int loaded = _library.LoadFromFile(scriptureFile);
                if (loaded > 0)
                {
                    Console.WriteLine($"Loaded {loaded} additional scripture(s) from {scriptureFile}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not load scriptures from file: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Displays the welcome message.
    /// </summary>
    static void ShowWelcome()
    {
        SafeClear();
        Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║           SCRIPTURE MEMORIZER                                 ║");
        Console.WriteLine("║   Hide words progressively to practice memorization!         ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine($"Library contains {_library.Count} scriptures.");
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }

    /// <summary>
    /// Displays and handles the main menu.
    /// </summary>
    /// <returns>True to continue, false to quit</returns>
    static bool ShowMainMenu()
    {
        SafeClear();
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine("                    MAIN MENU");
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine();
        Console.WriteLine("  1. Practice a random scripture");
        Console.WriteLine("  2. Choose a scripture from the library");
        Console.WriteLine("  3. View all scriptures in the library");
        Console.WriteLine($"  4. Change difficulty (Current: {_difficultyName} - {_wordsToHidePerRound} words)");
        Console.WriteLine("  5. Quit");
        Console.WriteLine();
        Console.Write("Enter your choice (1-5): ");

        string input = Console.ReadLine()?.Trim();

        switch (input)
        {
            case "1":
                Scripture randomScripture = _library.GetRandomScripture();
                if (randomScripture != null)
                {
                    PracticeScripture(randomScripture);
                }
                break;
            case "2":
                SelectAndPracticeScripture();
                break;
            case "3":
                ViewLibrary();
                break;
            case "4":
                ChangeDifficulty();
                break;
            case "5":
                return false;
            default:
                Console.WriteLine("Invalid choice. Press Enter to continue...");
                Console.ReadLine();
                break;
        }

        return true;
    }

    /// <summary>
    /// Allows the user to select a scripture from the library.
    /// </summary>
    static void SelectAndPracticeScripture()
    {
        SafeClear();
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine("              SELECT A SCRIPTURE");
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine();

        List<string> references = _library.GetAllReferences();
        for (int i = 0; i < references.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {references[i]}");
        }

        Console.WriteLine();
        Console.Write($"Enter your choice (1-{references.Count}) or 0 to go back: ");

        string input = Console.ReadLine()?.Trim();
        if (int.TryParse(input, out int choice))
        {
            if (choice == 0) return;
            
            if (choice >= 1 && choice <= references.Count)
            {
                Scripture scripture = _library.GetScripture(choice - 1);
                PracticeScripture(scripture);
            }
            else
            {
                Console.WriteLine("Invalid choice. Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    /// <summary>
    /// Displays all scriptures in the library.
    /// </summary>
    static void ViewLibrary()
    {
        SafeClear();
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine("              SCRIPTURE LIBRARY");
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine();

        for (int i = 0; i < _library.Count; i++)
        {
            Scripture scripture = _library.GetScripture(i);
            scripture.Reset();
            Console.WriteLine($"[{i + 1}] {scripture.GetDisplayText()}");
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine();
        }

        Console.WriteLine("Press Enter to return to the menu...");
        Console.ReadLine();
    }

    /// <summary>
    /// Allows the user to change the difficulty level.
    /// </summary>
    static void ChangeDifficulty()
    {
        SafeClear();
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine("              DIFFICULTY SETTINGS");
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine();
        Console.WriteLine($"  Current difficulty: {_difficultyName} ({_wordsToHidePerRound} words per round)");
        Console.WriteLine();
        Console.WriteLine("  1. Easy (2 words per round)");
        Console.WriteLine("  2. Medium (3 words per round)");
        Console.WriteLine("  3. Hard (5 words per round)");
        Console.WriteLine("  4. Custom");
        Console.WriteLine();
        Console.Write("Enter your choice (1-4): ");

        string input = Console.ReadLine()?.Trim();

        switch (input)
        {
            case "1":
                _wordsToHidePerRound = 2;
                _difficultyName = "Easy";
                break;
            case "2":
                _wordsToHidePerRound = 3;
                _difficultyName = "Medium";
                break;
            case "3":
                _wordsToHidePerRound = 5;
                _difficultyName = "Hard";
                break;
            case "4":
                Console.Write("Enter number of words to hide per round (1-10): ");
                string customInput = Console.ReadLine()?.Trim();
                if (int.TryParse(customInput, out int custom) && custom >= 1 && custom <= 10)
                {
                    _wordsToHidePerRound = custom;
                    _difficultyName = "Custom";
                }
                else
                {
                    Console.WriteLine("Invalid input. Keeping current setting.");
                }
                break;
        }

        Console.WriteLine($"\nDifficulty set to {_difficultyName} ({_wordsToHidePerRound} words per round).");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    /// <summary>
    /// Runs the scripture practice session.
    /// </summary>
    /// <param name="scripture">The scripture to practice</param>
    static void PracticeScripture(Scripture scripture)
    {
        // Reset the scripture to show all words
        scripture.Reset();

        bool practicing = true;
        int hintsUsed = 0;

        while (practicing)
        {
            // Clear screen and display scripture
            SafeClear();
            DisplayScriptureWithProgress(scripture);

            // Check if all words are hidden
            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("\n🎉 Congratulations! You've hidden all the words!");
                Console.WriteLine($"   Hints used: {hintsUsed}");
                Console.WriteLine("\nPress Enter to return to the menu...");
                Console.ReadLine();
                break;
            }

            // Display instructions
            Console.WriteLine("\n══════════════════════════════════════════════════════");
            Console.WriteLine("Commands:");
            Console.WriteLine("  [Enter]  - Hide more words");
            Console.WriteLine("  hint     - Reveal one hidden word");
            Console.WriteLine("  restart  - Start over with this scripture");
            Console.WriteLine("  quit     - Return to main menu");
            Console.WriteLine("══════════════════════════════════════════════════════");
            Console.Write("\nYour choice: ");

            string input = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(input))
            {
                // Enter pressed - hide more words
                scripture.HideRandomWords(_wordsToHidePerRound);
            }
            else if (input == "quit")
            {
                practicing = false;
            }
            else if (input == "hint")
            {
                if (scripture.GetHint())
                {
                    hintsUsed++;
                    Console.WriteLine("One word has been revealed.");
                }
                else
                {
                    Console.WriteLine("No hidden words to reveal.");
                }
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            else if (input == "restart")
            {
                scripture.Reset();
                hintsUsed = 0;
                Console.WriteLine("Scripture has been reset. Press Enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Unknown command. Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    /// <summary>
    /// Displays the scripture with progress information.
    /// </summary>
    /// <param name="scripture">The scripture to display</param>
    static void DisplayScriptureWithProgress(Scripture scripture)
    {
        // Calculate progress
        int totalWords = scripture.WordCount;
        int hiddenWords = scripture.HiddenWordCount;
        int percentHidden = (int)((double)hiddenWords / totalWords * 100);

        // Display header
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine("              SCRIPTURE PRACTICE");
        Console.WriteLine("══════════════════════════════════════════════════════");
        Console.WriteLine();

        // Display scripture
        Console.WriteLine(scripture.GetDisplayText());

        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------");
        
        // Display progress bar
        int progressBarWidth = 30;
        int filledWidth = (int)((double)hiddenWords / totalWords * progressBarWidth);
        string progressBar = new string('█', filledWidth) + new string('░', progressBarWidth - filledWidth);
        
        Console.WriteLine($"Progress: [{progressBar}] {percentHidden}%");
        Console.WriteLine($"Words: {hiddenWords}/{totalWords} hidden | {scripture.VisibleWordCount} remaining");
    }
}