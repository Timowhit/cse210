namespace ScriptureMemorizer;

/// <summary>
/// Represents a scripture reference (e.g., "John 3:16" or "Proverbs 3:5-6").
/// Supports both single verses and verse ranges.
/// </summary>
public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    /// <summary>
    /// Creates a reference for a single verse.
    /// </summary>
    /// <param name="book">The book name (e.g., "John")</param>
    /// <param name="chapter">The chapter number</param>
    /// <param name="verse">The verse number</param>
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse;
    }

    /// <summary>
    /// Creates a reference for a range of verses.
    /// </summary>
    /// <param name="book">The book name (e.g., "Proverbs")</param>
    /// <param name="chapter">The chapter number</param>
    /// <param name="startVerse">The starting verse number</param>
    /// <param name="endVerse">The ending verse number</param>
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    /// <summary>
    /// Creates a reference by parsing a reference string.
    /// Supports formats like "John 3:16" or "Proverbs 3:5-6"
    /// </summary>
    /// <param name="referenceString">The reference string to parse</param>
    public Reference(string referenceString)
    {
        ParseReference(referenceString);
    }

    /// <summary>
    /// Gets the book name.
    /// </summary>
    public string Book => _book;

    /// <summary>
    /// Gets the chapter number.
    /// </summary>
    public int Chapter => _chapter;

    /// <summary>
    /// Gets the starting verse number.
    /// </summary>
    public int StartVerse => _startVerse;

    /// <summary>
    /// Gets the ending verse number.
    /// </summary>
    public int EndVerse => _endVerse;

    /// <summary>
    /// Gets whether this reference spans multiple verses.
    /// </summary>
    public bool IsVerseRange => _startVerse != _endVerse;

    /// <summary>
    /// Parses a reference string into its components.
    /// </summary>
    private void ParseReference(string referenceString)
    {
        // Find the last space to separate book from chapter:verse
        int lastSpaceIndex = referenceString.LastIndexOf(' ');
        
        if (lastSpaceIndex == -1)
        {
            throw new ArgumentException("Invalid reference format. Expected format: 'Book Chapter:Verse' or 'Book Chapter:StartVerse-EndVerse'");
        }

        _book = referenceString.Substring(0, lastSpaceIndex).Trim();
        string chapterVerse = referenceString.Substring(lastSpaceIndex + 1);

        // Split chapter and verse(s)
        string[] parts = chapterVerse.Split(':');
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid reference format. Expected format: 'Chapter:Verse' or 'Chapter:StartVerse-EndVerse'");
        }

        _chapter = int.Parse(parts[0]);
        string versePart = parts[1];

        // Check for verse range
        if (versePart.Contains('-'))
        {
            string[] verses = versePart.Split('-');
            _startVerse = int.Parse(verses[0]);
            _endVerse = int.Parse(verses[1]);
        }
        else
        {
            _startVerse = int.Parse(versePart);
            _endVerse = _startVerse;
        }
    }

    /// <summary>
    /// Gets the formatted display text of the reference.
    /// </summary>
    /// <returns>The formatted reference string</returns>
    public string GetDisplayText()
    {
        if (IsVerseRange)
        {
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
        return $"{_book} {_chapter}:{_startVerse}";
    }

    /// <summary>
    /// Returns the formatted reference string.
    /// </summary>
    public override string ToString()
    {
        return GetDisplayText();
    }
}