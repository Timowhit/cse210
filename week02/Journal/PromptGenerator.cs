using System;
using System.Collections.Generic;

public class PromptGenerator
{
    private static Random _random = new Random();

    // Prompt structure: prompt text mapped to its tag
    private static Dictionary<string, string> _quickPrompts = new Dictionary<string, string>
    {
        { "One word to describe today:", "reflection" },
        { "Energy level (1-10):", "health" },
        { "Today's highlight:", "gratitude" },
        { "Something I learned:", "growth" },
        { "Who made me smile:", "social" },
        { "One thing I'm grateful for:", "gratitude" },
        { "How I moved my body:", "health" },
        { "A small win today:", "gratitude" },
        { "One word for my mood:", "reflection" },
        { "Something I ate that I enjoyed:", "health" },
        { "A song that fit today:", "reflection" },
        { "One priority for tomorrow:", "growth" }
    };

    private static Dictionary<string, string> _deepPrompts = new Dictionary<string, string>
    {
        { "What challenged my perspective today and how did I respond?", "growth" },
        { "Describe a moment I felt fully present. What was I doing?", "reflection" },
        { "How did I see the Lord's hand in my life today?", "faith" },
        { "What conversation had the biggest impact on me today and why?", "social" },
        { "What am I avoiding, and what is one small step I can take toward it?", "growth" },
        { "What would I do differently if I could relive one moment from today?", "reflection" },
        { "How did I take care of my physical and mental health today?", "health" },
        { "What is one thing I'm looking forward to and why?", "gratitude" },
        { "What relationship do I want to invest more in, and how can I start?", "social" },
        { "What did I do today that aligned with my long-term goals?", "growth" },
        { "What spiritual impression or thought stayed with me today?", "faith" },
        { "What made today different from yesterday?", "reflection" },
        { "What is weighing on my mind, and what can I do about it?", "reflection" },
        { "How did I serve or help someone today?", "faith" },
        { "What am I learning about myself this week?", "growth" }
    };

    // Get a random prompt based on mode
    public static (string prompt, string tag) GetPrompt(string mode)
    {
        Dictionary<string, string> promptPool;

        switch (mode.ToLower())
        {
            case "quick":
                promptPool = _quickPrompts;
                break;
            case "deep":
                promptPool = _deepPrompts;
                break;
            case "mixed":
            default:
                // Combine both pools
                promptPool = new Dictionary<string, string>();
                foreach (var kvp in _quickPrompts)
                {
                    promptPool[kvp.Key] = kvp.Value;
                }
                foreach (var kvp in _deepPrompts)
                {
                    promptPool[kvp.Key] = kvp.Value;
                }
                break;
        }

        // Get random prompt from pool
        List<string> prompts = new List<string>(promptPool.Keys);
        int index = _random.Next(prompts.Count);
        string selectedPrompt = prompts[index];
        string tag = promptPool[selectedPrompt];

        return (selectedPrompt, tag);
    }

    // Detect tags from response text if no prompt tag available
    public static List<string> DetectTagsFromResponse(string response)
    {
        List<string> detectedTags = new List<string>();
        string lowerResponse = response.ToLower();

        // Keyword mappings for tag detection
        Dictionary<string, List<string>> tagKeywords = new Dictionary<string, List<string>>
        {
            { "faith", new List<string> { "god", "lord", "prayer", "church", "spiritual", "blessing", "scripture", "temple", "spirit", "gospel" } },
            { "gratitude", new List<string> { "thankful", "grateful", "appreciate", "blessed", "fortunate", "glad", "happy", "joy" } },
            { "growth", new List<string> { "learn", "improve", "goal", "progress", "challenge", "develop", "better", "skill", "practice" } },
            { "social", new List<string> { "friend", "family", "talk", "conversation", "people", "relationship", "connect", "together", "meeting" } },
            { "health", new List<string> { "exercise", "sleep", "eat", "run", "walk", "gym", "tired", "energy", "rest", "workout", "food" } },
            { "school", new List<string> { "class", "study", "homework", "exam", "test", "assignment", "professor", "lecture", "grade", "project" } },
            { "reflection", new List<string> { "think", "feel", "realize", "wonder", "remember", "thought", "moment", "experience" } }
        };

        foreach (var tagEntry in tagKeywords)
        {
            foreach (string keyword in tagEntry.Value)
            {
                if (lowerResponse.Contains(keyword))
                {
                    if (!detectedTags.Contains(tagEntry.Key))
                    {
                        detectedTags.Add(tagEntry.Key);
                    }
                    break;
                }
            }
        }

        // Default to reflection if no tags detected
        if (detectedTags.Count == 0)
        {
            detectedTags.Add("reflection");
        }

        return detectedTags;
    }

    // Get all available tags for display purposes
    public static List<string> GetAllTags()
    {
        return new List<string> { "faith", "gratitude", "growth", "social", "health", "school", "reflection" };
    }
}
