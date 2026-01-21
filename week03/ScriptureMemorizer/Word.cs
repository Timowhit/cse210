namespace ScriptureMemorizer;

/// <summary>
/// Represents a single word in a scripture that can be hidden or shown.
/// Encapsulates the word's text and its visibility state.
/// </summary>
public class Word
{
    private string _text;
    private bool _isHidden;

    /// <summary>
    /// Creates a new Word with the specified text.
    /// </summary>
    /// <param name="text">The text of the word</param>
    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    /// <summary>
    /// Gets whether this word is currently hidden.
    /// </summary>
    public bool IsHidden => _isHidden;

    /// <summary>
    /// Gets the original text of the word (for internal use).
    /// </summary>
    public string Text => _text;

    /// <summary>
    /// Gets the length of the word.
    /// </summary>
    public int Length => _text.Length;

    /// <summary>
    /// Hides the word by marking it as hidden.
    /// </summary>
    public void Hide()
    {
        _isHidden = true;
    }

    /// <summary>
    /// Shows the word by marking it as visible.
    /// </summary>
    public void Show()
    {
        _isHidden = false;
    }

    /// <summary>
    /// Gets the display text of the word.
    /// Returns underscores if hidden, otherwise returns the actual text.
    /// </summary>
    /// <returns>The text to display</returns>
    public string GetDisplayText()
    {
        if (_isHidden)
        {
            return new string('_', _text.Length);
        }
        return _text;
    }

    /// <summary>
    /// Returns the display text representation of this word.
    /// </summary>
    public override string ToString()
    {
        return GetDisplayText();
    }
}