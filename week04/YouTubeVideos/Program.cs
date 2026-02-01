using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a list to store videos
        List<Video> videos = new List<Video>();

        // Create Video 1: Tech Review
        Video video1 = new Video("iPhone 15 Pro Max Review", "TechGuru", 845);
        video1.AddComment(new Comment("Sarah Johnson", "Great review! Helped me decide to upgrade."));
        video1.AddComment(new Comment("Mike Chen", "The camera comparison was really helpful."));
        video1.AddComment(new Comment("Emma Wilson", "Could you do a battery life deep dive next?"));
        video1.AddComment(new Comment("Alex Turner", "Best tech review channel on YouTube!"));
        videos.Add(video1);

        // Create Video 2: Cooking Tutorial
        Video video2 = new Video("Easy Homemade Pizza Recipe", "ChefMaria", 612);
        video2.AddComment(new Comment("David Brown", "Made this for dinner last night - amazing!"));
        video2.AddComment(new Comment("Lisa Park", "Finally a recipe that actually works. Thanks!"));
        video2.AddComment(new Comment("Tom Anderson", "My kids loved it. New family favorite!"));
        videos.Add(video2);

        // Create Video 3: Fitness Video
        Video video3 = new Video("30-Minute Full Body Workout", "FitWithJen", 1847);
        video3.AddComment(new Comment("Rachel Green", "This workout is intense but so effective!"));
        video3.AddComment(new Comment("Chris Martinez", "Day 15 of doing this daily. Seeing results!"));
        video3.AddComment(new Comment("Amy Taylor", "Love that no equipment is needed."));
        video3.AddComment(new Comment("Jordan Lee", "The modifications for beginners are super helpful."));
        videos.Add(video3);

        // Create Video 4: Travel Vlog
        Video video4 = new Video("Exploring Tokyo in 72 Hours", "WanderlustAdam", 1523);
        video4.AddComment(new Comment("Nicole White", "Adding all these spots to my Japan itinerary!"));
        video4.AddComment(new Comment("Kevin Patel", "The cinematography is stunning. What camera do you use?"));
        video4.AddComment(new Comment("Megan Scott", "That ramen shop looks incredible!"));
        videos.Add(video4);

        // Display information for each video
        foreach (Video video in videos)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLengthInSeconds()} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            Console.WriteLine();
            Console.WriteLine("Comments:");
            
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  {comment.GetCommenterName()}: \"{comment.GetText()}\"");
            }
            
            Console.WriteLine();
        }
    }
}