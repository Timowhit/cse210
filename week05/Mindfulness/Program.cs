// Mindfulness Program
// This program provides three mindfulness activities: Breathing, Reflection, and Listing.
// 
// EXCEEDING REQUIREMENTS:
// [Add your creativity items here, for example:]
// - Added a Gratitude Journal activity
// - Tracks how many times each activity was performed in the session
// - etc.

using System;

class Program
{
    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflection activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BreathingActivity breathing = new BreathingActivity();
                    breathing.Run();
                    break;

                case "2":
                    ReflectionActivity reflection = new ReflectionActivity();
                    reflection.Run();
                    break;

                case "3":
                    ListingActivity listing = new ListingActivity();
                    listing.Run();
                    break;

                case "4":
                    running = false;
                    Console.WriteLine("\nThank you for using the Mindfulness Program. Goodbye!");
                    break;

                default:
                    Console.WriteLine("\nInvalid option. Please try again.");
                    Thread.Sleep(2000);
                    break;
            }
        }
    }
}