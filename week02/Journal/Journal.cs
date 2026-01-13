using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Journal
{
    private List<Entry> _entries;

    public Journal()
    {
        _entries = new List<Entry>();
    }

    public int EntryCount => _entries.Count;

    // Add a new entry
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    // Get all entries
    public List<Entry> GetAllEntries()
    {
        return _entries;
    }

    // Search entries by keyword in prompt or response
    public List<Entry> Search(string keyword)
    {
        string lowerKeyword = keyword.ToLower();
        
        return _entries.Where(e => 
            e.Prompt.ToLower().Contains(lowerKeyword) || 
            e.Response.ToLower().Contains(lowerKeyword))
            .ToList();
    }

    // Filter entries by tag
    public List<Entry> FilterByTag(string tag)
    {
        string lowerTag = tag.ToLower();
        
        return _entries.Where(e => 
            e.Tags.Any(t => t.ToLower() == lowerTag))
            .ToList();
    }

    // Filter entries by date (exact match or partial like "2024-01")
    public List<Entry> FilterByDate(string dateFilter)
    {
        return _entries.Where(e => 
            e.Date.Contains(dateFilter))
            .ToList();
    }

    // Filter entries by mode
    public List<Entry> FilterByMode(string mode)
    {
        string lowerMode = mode.ToLower();
        
        return _entries.Where(e => 
            e.Mode.ToLower() == lowerMode)
            .ToList();
    }

    // Save journal to CSV file
    public bool Save(string filename)
    {
        try
        {
            // Ensure .csv extension
            if (!filename.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                filename += ".csv";
            }

            using (StreamWriter writer = new StreamWriter(filename))
            {
                // Write header
                writer.WriteLine("Date,Prompt,Response,Mode,Tags");

                // Write each entry
                foreach (Entry entry in _entries)
                {
                    writer.WriteLine(entry.ToCSV());
                }
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // Load journal from CSV file (replaces current entries)
    public bool Load(string filename)
    {
        try
        {
            // Ensure .csv extension
            if (!filename.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                filename += ".csv";
            }

            if (!File.Exists(filename))
            {
                return false;
            }

            List<Entry> loadedEntries = new List<Entry>();
            string[] lines = File.ReadAllLines(filename);

            // Skip header line, process entries
            for (int i = 1; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    Entry entry = Entry.FromCSV(lines[i]);
                    if (entry != null)
                    {
                        loadedEntries.Add(entry);
                    }
                }
            }

            // Replace current entries
            _entries = loadedEntries;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // Clear all entries
    public void Clear()
    {
        _entries.Clear();
    }

    // Display entries in a formatted way
    public void DisplayEntries(List<Entry> entries = null)
    {
        List<Entry> toDisplay = entries ?? _entries;

        if (toDisplay.Count == 0)
        {
            Console.WriteLine("\n  No entries to display.");
            return;
        }

        Console.WriteLine();
        for (int i = 0; i < toDisplay.Count; i++)
        {
            Console.WriteLine($"  ─────────────────────────────────────");
            Console.WriteLine($"  Entry {i + 1}");
            Console.WriteLine($"  ─────────────────────────────────────");
            
            string[] displayLines = toDisplay[i].Display().Split('\n');
            foreach (string line in displayLines)
            {
                Console.WriteLine($"  {line}");
            }
            Console.WriteLine();
        }
    }
}
