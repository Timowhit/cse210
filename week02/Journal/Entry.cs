using System;
using System.Collections.Generic;

public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Mode { get; set; }
    public List<string> Tags { get; set; }

    public Entry()
    {
        Tags = new List<string>();
    }

    public Entry(string date, string prompt, string response, string mode, List<string> tags)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
        Mode = mode;
        Tags = tags ?? new List<string>();
    }

    // Convert entry to CSV-safe string
    public string ToCSV()
    {
        string tagsJoined = string.Join(";", Tags);
        
        // Escape fields that might contain commas or quotes
        string safeDate = EscapeCSVField(Date);
        string safePrompt = EscapeCSVField(Prompt);
        string safeResponse = EscapeCSVField(Response);
        string safeMode = EscapeCSVField(Mode);
        string safeTags = EscapeCSVField(tagsJoined);

        return $"{safeDate},{safePrompt},{safeResponse},{safeMode},{safeTags}";
    }

    // Create entry from CSV line
    public static Entry FromCSV(string csvLine)
    {
        List<string> fields = ParseCSVLine(csvLine);
        
        if (fields.Count < 5)
        {
            return null;
        }

        Entry entry = new Entry
        {
            Date = fields[0],
            Prompt = fields[1],
            Response = fields[2],
            Mode = fields[3],
            Tags = new List<string>(fields[4].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
        };

        return entry;
    }

    // Escape a field for CSV (handle commas and quotes)
    private static string EscapeCSVField(string field)
    {
        if (string.IsNullOrEmpty(field))
        {
            return "";
        }

        // If field contains comma, quote, or newline, wrap in quotes and escape internal quotes
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }

        return field;
    }

    // Parse a CSV line respecting quoted fields
    private static List<string> ParseCSVLine(string line)
    {
        List<string> fields = new List<string>();
        bool inQuotes = false;
        string currentField = "";

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (inQuotes)
            {
                if (c == '"')
                {
                    // Check for escaped quote
                    if (i + 1 < line.Length && line[i + 1] == '"')
                    {
                        currentField += '"';
                        i++;
                    }
                    else
                    {
                        inQuotes = false;
                    }
                }
                else
                {
                    currentField += c;
                }
            }
            else
            {
                if (c == '"')
                {
                    inQuotes = true;
                }
                else if (c == ',')
                {
                    fields.Add(currentField);
                    currentField = "";
                }
                else
                {
                    currentField += c;
                }
            }
        }

        fields.Add(currentField);
        return fields;
    }

    // Display entry in a clean format
    public string Display()
    {
        string tagDisplay = Tags.Count > 0 ? string.Join(", ", Tags) : "none";
        
        return $"Date: {Date}\n" +
               $"Mode: {Mode}\n" +
               $"Tags: {tagDisplay}\n" +
               $"Prompt: {Prompt}\n" +
               $"Response: {Response}";
    }
}
