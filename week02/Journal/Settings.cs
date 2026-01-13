using System;
using System.IO;

public class Settings
{
    private const string SETTINGS_FILE = "settings.txt";

    public string UserName { get; set; }
    public bool IsFirstTime { get; private set; }

    public Settings()
    {
        UserName = "";
        IsFirstTime = true;
        Load();
    }

    // Load settings from file
    public void Load()
    {
        try
        {
            if (File.Exists(SETTINGS_FILE))
            {
                string[] lines = File.ReadAllLines(SETTINGS_FILE);
                
                foreach (string line in lines)
                {
                    if (line.StartsWith("UserName="))
                    {
                        UserName = line.Substring("UserName=".Length).Trim();
                    }
                }

                // If we have a username, it's not the first time
                IsFirstTime = string.IsNullOrEmpty(UserName);
            }
            else
            {
                IsFirstTime = true;
            }
        }
        catch (Exception)
        {
            IsFirstTime = true;
        }
    }

    // Save settings to file
    public bool Save()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(SETTINGS_FILE))
            {
                writer.WriteLine($"UserName={UserName}");
            }

            IsFirstTime = false;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // Get greeting based on time of day
    public string GetGreeting()
    {
        int hour = DateTime.Now.Hour;

        if (hour < 12)
        {
            return "Good morning";
        }
        else if (hour < 17)
        {
            return "Good afternoon";
        }
        else
        {
            return "Good evening";
        }
    }

    // Display welcome message
    public void DisplayWelcome()
    {
        if (IsFirstTime)
        {
            DisplayFirstTimeWelcome();
        }
        else
        {
            DisplayReturningWelcome();
        }
    }

    private void DisplayFirstTimeWelcome()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("  ╔═══════════════════════════════════════════╗");
        Console.WriteLine("  ║           Welcome to Journal              ║");
        Console.WriteLine("  ╚═══════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("  Your personal space for daily reflection.");
        Console.WriteLine("  I'll give you prompts to make journaling easier.");
        Console.WriteLine();
        Console.Write("  What's your name? ");
        
        UserName = Console.ReadLine()?.Trim() ?? "Friend";
        
        if (string.IsNullOrEmpty(UserName))
        {
            UserName = "Friend";
        }

        Save();

        Console.WriteLine();
        Console.WriteLine($"  Nice to meet you, {UserName}!");
        Console.WriteLine("  Let's get started.");
        Console.WriteLine();
        Console.Write("  Press Enter to continue...");
        Console.ReadLine();
    }

    private void DisplayReturningWelcome()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine($"  {GetGreeting()}, {UserName}.");
        Console.WriteLine();
    }
}
