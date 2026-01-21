namespace ScriptureMemorizer;

/// <summary>
/// Represents a scripture with its reference and text content.
/// Manages the collection of words and their visibility state.
/// </summary>
public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random;

    /// <summary>
    /// Creates a new Scripture with the specified reference and text.
    /// </summary>
    /// <param name="reference">The scripture reference</param>
    /// <param name="text">The text of the scripture</param>
    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = ParseText(text);
        _random = new Random();
    }

    /// <summary>
    /// Gets the scripture reference.
    /// </summary>
    public Reference Reference => _reference;

    /// <summary>
    /// Gets the total number of words in the scripture.
    /// </summary>
    public int WordCount => _words.Count;

    /// <summary>
    /// Gets the number of words that are currently hidden.
    /// </summary>
    public int HiddenWordCount => _words.Count(w => w.IsHidden);

    /// <summary>
    /// Gets the number of words that are still visible.
    /// </summary>
    public int VisibleWordCount => _words.Count(w => !w.IsHidden);

    /// <summary>
    /// Parses the scripture text into a list of Word objects.
    /// </summary>
    /// <param name="text">The text to parse</param>
    /// <returns>A list of Word objects</returns>
    private List<Word> ParseText(string text)
    {
        List<Word> words = new List<Word>();
        
        // Split by spaces while preserving punctuation with words
        string[] tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        foreach (string token in tokens)
        {
            words.Add(new Word(token));
        }
        
        return words;
    }

    /// <summary>
    /// Checks if all words in the scripture are hidden.
    /// </summary>
    /// <returns>True if all words are hidden, false otherwise</returns>
    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden);
    }

    /// <summary>
    /// Hides a specified number of random words that are not already hidden.
    /// This is the stretch challenge implementation.
    /// </summary>
    /// <param name="count">The number of words to hide</param>
    /// <returns>The number of words actually hidden</returns>
    public int HideRandomWords(int count)
    {
        // Get indices of words that are not yet hidden
        List<int> visibleIndices = new List<int>();
        for (int i = 0; i < _words.Count; i++)
        {
            if (!_words[i].IsHidden)
            {
                visibleIndices.Add(i);
            }
        }

        // If no words are visible, return 0
        if (visibleIndices.Count == 0)
        {
            return 0;
        }

        // Determine how many words to hide (can't hide more than are visible)
        int wordsToHide = Math.Min(count, visibleIndices.Count);
        int wordsHidden = 0;

        // Randomly select and hide words
        for (int i = 0; i < wordsToHide; i++)
        {
            int randomIndex = _random.Next(visibleIndices.Count);
            int wordIndex = visibleIndices[randomIndex];
            _words[wordIndex].Hide();
            visibleIndices.RemoveAt(randomIndex);
            wordsHidden++;
        }

        return wordsHidden;
    }

    /// <summary>
    /// Resets all words to visible state.
    /// </summary>
    public void Reset()
    {
        foreach (Word word in _words)
        {
            word.Show();
        }
    }

    /// <summary>
    /// Gets the display text of the scripture including reference and text.
    /// </summary>
    /// <returns>The formatted scripture display string</returns>
    public string GetDisplayText()
    {
        string referenceText = _reference.GetDisplayText();
        string scriptureText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        
        return $"{referenceText}\n{scriptureText}";
    }

    /// <summary>
    /// Gets a hint by revealing one random hidden word.
    /// </summary>
    /// <returns>True if a hint was provided, false if no words are hidden</returns>
    public bool GetHint()
    {
        // Get indices of hidden words
        List<int> hiddenIndices = new List<int>();
        for (int i = 0; i < _words.Count; i++)
        {
            if (_words[i].IsHidden)
            {
                hiddenIndices.Add(i);
            }
        }

        if (hiddenIndices.Count == 0)
        {
            return false;
        }

        // Randomly select and show one word
        int randomIndex = _random.Next(hiddenIndices.Count);
        int wordIndex = hiddenIndices[randomIndex];
        _words[wordIndex].Show();
        
        return true;
    }

    /// <summary>
    /// Returns the formatted scripture string.
    /// </summary>
    public override string ToString()
    {
        return GetDisplayText();
    }
}