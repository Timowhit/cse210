#nullable enable

namespace ScriptureMemorizer;

/// <summary>
/// Manages a collection of scriptures that can be loaded from a file or added programmatically.
/// Supports random scripture selection and library browsing.
/// </summary>
public class ScriptureLibrary
{
    private List<Scripture> _scriptures;
    private Random _random;

    /// <summary>
    /// Creates a new empty scripture library.
    /// </summary>
    public ScriptureLibrary()
    {
        _scriptures = new List<Scripture>();
        _random = new Random();
    }

    /// <summary>
    /// Gets the number of scriptures in the library.
    /// </summary>
    public int Count => _scriptures.Count;

    /// <summary>
    /// Gets a list of all scripture references in the library.
    /// </summary>
    public List<string> GetAllReferences()
    {
        return _scriptures.Select(s => s.Reference.GetDisplayText()).ToList();
    }

    /// <summary>
    /// Adds a scripture to the library.
    /// </summary>
    /// <param name="scripture">The scripture to add</param>
    public void AddScripture(Scripture scripture)
    {
        _scriptures.Add(scripture);
    }

    /// <summary>
    /// Adds a scripture to the library using reference and text.
    /// </summary>
    /// <param name="reference">The scripture reference</param>
    /// <param name="text">The scripture text</param>
    public void AddScripture(Reference reference, string text)
    {
        _scriptures.Add(new Scripture(reference, text));
    }

    /// <summary>
    /// Gets a random scripture from the library.
    /// </summary>
    /// <returns>A random scripture, or null if the library is empty</returns>
    public Scripture? GetRandomScripture()
    {
        if (_scriptures.Count == 0)
        {
            return null;
        }
        
        int index = _random.Next(_scriptures.Count);
        return _scriptures[index];
    }

    /// <summary>
    /// Gets a scripture at the specified index.
    /// </summary>
    /// <param name="index">The index of the scripture</param>
    /// <returns>The scripture at the index</returns>
    public Scripture GetScripture(int index)
    {
        if (index < 0 || index >= _scriptures.Count)
        {
            throw new IndexOutOfRangeException("Scripture index out of range");
        }
        return _scriptures[index];
    }

    /// <summary>
    /// Loads scriptures from a file.
    /// File format: Each scripture on two lines - first line is reference, second line is text.
    /// Blank lines separate scriptures.
    /// </summary>
    /// <param name="filePath">Path to the scripture file</param>
    /// <returns>The number of scriptures loaded</returns>
    public int LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Scripture file not found: {filePath}");
        }

        string[] lines = File.ReadAllLines(filePath);
        int loaded = 0;
        int i = 0;

        while (i < lines.Length)
        {
            // Skip empty lines
            while (i < lines.Length && string.IsNullOrWhiteSpace(lines[i]))
            {
                i++;
            }

            if (i >= lines.Length) break;

            // Read reference line
            string referenceLine = lines[i].Trim();
            i++;

            // Skip if no more lines
            if (i >= lines.Length) break;

            // Skip empty lines between reference and text
            while (i < lines.Length && string.IsNullOrWhiteSpace(lines[i]))
            {
                i++;
            }

            if (i >= lines.Length) break;

            // Read text line(s) - combine multiple lines until empty line or end
            List<string> textLines = new List<string>();
            while (i < lines.Length && !string.IsNullOrWhiteSpace(lines[i]))
            {
                textLines.Add(lines[i].Trim());
                i++;
            }

            if (textLines.Count > 0)
            {
                string text = string.Join(" ", textLines);
                try
                {
                    Reference reference = new Reference(referenceLine);
                    AddScripture(reference, text);
                    loaded++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Could not parse scripture '{referenceLine}': {ex.Message}");
                }
            }
        }

        return loaded;
    }

    /// <summary>
    /// Saves the library to a file.
    /// </summary>
    /// <param name="filePath">Path to save the file</param>
    public void SaveToFile(string filePath)
    {
        using StreamWriter writer = new StreamWriter(filePath);
        
        for (int i = 0; i < _scriptures.Count; i++)
        {
            Scripture scripture = _scriptures[i];
            // Reset to show all words for saving
            scripture.Reset();
            
            writer.WriteLine(scripture.Reference.GetDisplayText());
            writer.WriteLine(string.Join(" ", GetOriginalText(scripture)));
            
            if (i < _scriptures.Count - 1)
            {
                writer.WriteLine(); // Blank line between scriptures
            }
        }
    }

    /// <summary>
    /// Gets the original text of a scripture (all words visible).
    /// </summary>
    private string GetOriginalText(Scripture scripture)
    {
        scripture.Reset();
        string[] parts = scripture.GetDisplayText().Split('\n');
        return parts.Length > 1 ? parts[1] : "";
    }

    /// <summary>
    /// Initializes the library with default scriptures.
    /// </summary>
    public void LoadDefaultScriptures()
    {
        // John 3:16
        AddScripture(
            new Reference("John", 3, 16),
            "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."
        );

        // Proverbs 3:5-6
        AddScripture(
            new Reference("Proverbs", 3, 5, 6),
            "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."
        );

        // Philippians 4:13
        AddScripture(
            new Reference("Philippians", 4, 13),
            "I can do all things through Christ which strengtheneth me."
        );

        // Jeremiah 29:11
        AddScripture(
            new Reference("Jeremiah", 29, 11),
            "For I know the thoughts that I think toward you, saith the Lord, thoughts of peace, and not of evil, to give you an expected end."
        );

        // Romans 8:28
        AddScripture(
            new Reference("Romans", 8, 28),
            "And we know that all things work together for good to them that love God, to them who are the called according to his purpose."
        );

        // Psalm 23:1-3
        AddScripture(
            new Reference("Psalm", 23, 1, 3),
            "The Lord is my shepherd; I shall not want. He maketh me to lie down in green pastures: he leadeth me beside the still waters. He restoreth my soul: he leadeth me in the paths of righteousness for his name's sake."
        );

        // Isaiah 40:31
        AddScripture(
            new Reference("Isaiah", 40, 31),
            "But they that wait upon the Lord shall renew their strength; they shall mount up with wings as eagles; they shall run, and not be weary; and they shall walk, and not faint."
        );

        // Matthew 6:33
        AddScripture(
            new Reference("Matthew", 6, 33),
            "But seek ye first the kingdom of God, and his righteousness; and all these things shall be added unto you."
        );
    }
}