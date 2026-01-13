/*
 * JOURNAL APPLICATION
 * Created by: Timothy Whitehead
 * 
 * EXCEEDING REQUIREMENTS:
 * 1. Proper CSV format with escaped quotes and commas
 * 2. Search functionality - search entries by keyword
 * 3. Filter functionality - filter by tag, date, or mode
 * 4. Auto-tagging system - prompts have assigned tags, free-write detects tags from keywords
 * 5. Three journaling modes: Quick (micro-prompts), Deep (reflective), Mixed (default)
 * 6. Persistent settings - saves username for personalized greetings
 * 7. Free-write option - skip prompts and write freely (labeled as "Open Entry")
 * 8. Time-aware greetings - Good morning/afternoon/evening
 * 9. Guided first-time setup vs concise returning user experience
 */

using System;
using System.Collections.Generic;

class Program
{
    private static Journal _journal = new Journal();
    private static Settings _settings = new Settings();
    private static string _currentMode = "mixed";

    static void Main(string[] args)
    {
        _settings.DisplayWelcome();
        RunMainMenu();
    }

    static void RunMainMenu()
    {
        bool running = true;

        while (running)
        {
            DisplayMenu();
            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    SaveJournal();
                    break;
                case "4":
                    LoadJournal();
                    break;
                case "5":
                    SearchEntries();
                    break;
                case "6":
                    FilterEntries();
                    break;
                case "7":
                    ChangeMode();
                    break;
                case "8":
                    running = false;
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine("\n  Invalid option. Try again.");
                    Pause();
                    break;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine($"  Journal  •  {_journal.EntryCount} entries  •  Mode: {_currentMode}");
        Console.WriteLine("  ─────────────────────────────────────────────");
        Console.WriteLine();
        Console.WriteLine("  1. Write new entry");
        Console.WriteLine("  2. Display journal");
        Console.WriteLine("  3. Save journal");
        Console.WriteLine("  4. Load journal");
        Console.WriteLine("  5. Search entries");
        Console.WriteLine("  6. Filter entries");
        Console.WriteLine("  7. Change mode");
        Console.WriteLine("  8. Exit");
        Console.WriteLine();
        Console.Write("  Select: ");
    }

    static void WriteNewEntry()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("  New Entry");
        Console.WriteLine("  ─────────────────────────────────────────────");
        Console.WriteLine();
        Console.WriteLine("  1. Use a prompt");
        Console.WriteLine("  2. Free write");
        Console.WriteLine("  3. Back");
        Console.WriteLine();
        Console.Write("  Select: ");

        string choice = Console.ReadLine()?.Trim() ?? "";

        switch (choice)
        {
            case "1":
                WritePromptedEntry();
                break;
            case "2":
                WriteFreeEntry();
                break;
            case "3":
                return;
            default:
                Console.WriteLine("\n  Invalid option.");
                Pause();
                break;
        }
    }

    static void WritePromptedEntry()
    {
        Console.Clear();
        Console.WriteLine();

        // Get prompt based on current mode
        var (prompt, promptTag) = PromptGenerator.GetPrompt(_currentMode);

        Console.WriteLine($"  {prompt}");
        Console.WriteLine();
        Console.Write("  > ");

        string response = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrEmpty(response))
        {
            Console.WriteLine("\n  Entry cancelled (empty response).");
            Pause();
            return;
        }

        // Build tags list - start with prompt tag, add detected tags
        List<string> tags = new List<string> { promptTag };
        List<string> detectedTags = PromptGenerator.DetectTagsFromResponse(response);
        
        foreach (string tag in detectedTags)
        {
            if (!tags.Contains(tag))
            {
                tags.Add(tag);
            }
        }

        // Create and add entry
        Entry entry = new Entry(
            DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            prompt,
            response,
            _currentMode,
            tags
        );

        _journal.AddEntry(entry);

        Console.WriteLine();
        Console.WriteLine("  Entry saved.");
        Console.WriteLine($"  Tags: {string.Join(", ", tags)}");
        Pause();
    }

    static void WriteFreeEntry()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("  Open Entry");
        Console.WriteLine("  ─────────────────────────────────────────────");
        Console.WriteLine("  Write whatever is on your mind.");
        Console.WriteLine();
        Console.Write("  > ");

        string response = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrEmpty(response))
        {
            Console.WriteLine("\n  Entry cancelled (empty response).");
            Pause();
            return;
        }

        // Detect tags from response
        List<string> tags = PromptGenerator.DetectTagsFromResponse(response);

        // Create entry with "Open Entry" as prompt
        Entry entry = new Entry(
            DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            "Open Entry",
            response,
            "free",
            tags
        );

        _journal.AddEntry(entry);

        Console.WriteLine();
        Console.WriteLine("  Entry saved.");
        Console.WriteLine($"  Tags: {string.Join(", ", tags)}");
        Pause();
    }

    static void DisplayJournal()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("  Your Journal");
        Console.WriteLine("  ─────────────────────────────────────────────");

        _journal.DisplayEntries();
        Pause();
    }

    static void SaveJournal()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("  Save Journal");
        Console.WriteLine("  ─────────────────────────────────────────────");
        Console.WriteLine();
        Console.Write("  Filename: ");

        string filename = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrEmpty(filename))
        {
            Console.WriteLine("\n  Save cancelled.");
            Pause();
            return;
        }

        if (_journal.Save(filename))
        {
            Console.WriteLine($"\n  Journal saved to {filename}.csv");
        }
        else
        {
            Console.WriteLine("\n  Error saving journal.");
        }

        Pause();
    }

    static void LoadJournal()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("  Load Journal");
        Console.WriteLine("  ─────────────────────────────────────────────");
        Console.WriteLine();
        Console.Write("  Filename: ");

        string filename = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrEmpty(filename))
        {
            Console.WriteLine("\n  Load cancelled.");
            Pause();
            return;
        }

        if (_journal.Load(filename))
        {
            Console.WriteLine($"\n  Journal loaded. {_journal.EntryCount} entries found.");
        }
        else
        {
            Console.WriteLine("\n  Error loading journal. File may not exist.");
        }

        Pause();
    }

    static void SearchEntries()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("  Search Entries");
        Console.WriteLine("  ─────────────────────────────────────────────");
        Console.WriteLine();
        Console.Write("  Search keyword: ");

        string keyword = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrEmpty(keyword))
        {
            Console.WriteLine("\n  Search cancelled.");
            Pause();
            return;
        }

        List<Entry> results = _journal.Search(keyword);

        Console.WriteLine();
        Console.WriteLine($"  Found {results.Count} matching entries:");

        _journal.DisplayEntries(results);
        Pause();
    }

    static void FilterEntries()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("  Filter Entries");
        Console.WriteLine("  ─────────────────────────────────────────────");
        Console.WriteLine();
        Console.WriteLine("  1. By tag");
        Console.WriteLine("  2. By date");
        Console.WriteLine("  3. By mode");
        Console.WriteLine("  4. Back");
        Console.WriteLine();
        Console.Write("  Select: ");

        string choice = Console.ReadLine()?.Trim() ?? "";

        List<Entry> results = new List<Entry>();

        switch (choice)
        {
            case "1":
                Console.WriteLine();
                Console.WriteLine($"  Available tags: {string.Join(", ", PromptGenerator.GetAllTags())}");
                Console.Write("  Enter tag: ");
                string tag = Console.ReadLine()?.Trim() ?? "";
                results = _journal.FilterByTag(tag);
                break;

            case "2":
                Console.Write("\n  Enter date (e.g., 2024-01-15 or 2024-01): ");
                string date = Console.ReadLine()?.Trim() ?? "";
                results = _journal.FilterByDate(date);
                break;

            case "3":
                Console.Write("\n  Enter mode (quick, deep, mixed, free): ");
                string mode = Console.ReadLine()?.Trim() ?? "";
                results = _journal.FilterByMode(mode);
                break;

            case "4":
                return;

            default:
                Console.WriteLine("\n  Invalid option.");
                Pause();
                return;
        }

        Console.WriteLine();
        Console.WriteLine($"  Found {results.Count} matching entries:");
        _journal.DisplayEntries(results);
        Pause();
    }

    static void ChangeMode()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("  Change Mode");
        Console.WriteLine("  ─────────────────────────────────────────────");
        Console.WriteLine();
        Console.WriteLine($"  Current mode: {_currentMode}");
        Console.WriteLine();
        Console.WriteLine("  1. Quick   - Short, simple prompts (1-5 words)");
        Console.WriteLine("  2. Deep    - Reflective, detailed prompts");
        Console.WriteLine("  3. Mixed   - Both types (default)");
        Console.WriteLine("  4. Back");
        Console.WriteLine();
        Console.Write("  Select: ");

        string choice = Console.ReadLine()?.Trim() ?? "";

        switch (choice)
        {
            case "1":
                _currentMode = "quick";
                Console.WriteLine("\n  Mode set to Quick.");
                break;
            case "2":
                _currentMode = "deep";
                Console.WriteLine("\n  Mode set to Deep.");
                break;
            case "3":
                _currentMode = "mixed";
                Console.WriteLine("\n  Mode set to Mixed.");
                break;
            case "4":
                return;
            default:
                Console.WriteLine("\n  Invalid option.");
                break;
        }

        Pause();
    }

    static void ExitProgram()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine($"  Goodbye, {_settings.UserName}.");
        Console.WriteLine();
    }

    static void Pause()
    {
        Console.WriteLine();
        Console.Write("  Press Enter to continue...");
        Console.ReadLine();
    }
}
