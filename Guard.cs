using System;

namespace AgentApp
{
    /// <summary>
    /// Represents a guard in the application.
    /// </summary>
    public class Guard : Obstacle
    {
        public int X { get; private set; } // Gets the x coordinate of the guard.
        public int Y { get; private set; } // Gets the y coordinate og the guard.

        /// <summary>
        /// Initializes a new instance of the <see cref="Guard"/> class.
        /// </summary>
        /// <param name="x">The X-coordinate of the guard.</param>
        /// <param name="y">The Y-coordinate of the guard.</param>
        public Guard(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Creates a guard instance based on user input.
        /// </summary>
        /// <returns>A new <see cref="Guard"/> instance.</returns>
        public static Guard CreateFromUserInput()
        {
            while (true)
            {
                Console.WriteLine("Enter the guard's location (X,Y): ");
                string input = Console.ReadLine();

                if (IsValidCoordinate(input))
                {
                    string[] coordinates = input.Split(',');
                    int x = int.Parse(coordinates[0]);
                    int y = int.Parse(coordinates[1]);

                    return new Guard(x, y);
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }

        /// <summary>
        /// Validates the input string to check if it represents valid coordinates.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <returns><c>true</c> if the input is valid; otherwise, <c>false</c>.</returns>
        private static bool IsValidCoordinate(string input)
        {
            string[] coordinates = input.Split(',');
            return coordinates.Length == 2 && int.TryParse(coordinates[0], out _) && int.TryParse(coordinates[1], out _);
        }

        /// <summary>
        /// Determines if the guard is at the specified location.
        /// </summary>
        /// <param name="x">The X-coordinate to check.</param>
        /// <param name="y">The Y-coordinate to check.</param>
        /// <returns><c>true</c> if the guard is at the specified location; otherwise, <c>false</c>.</returns>
        public override bool IsAtLocation(int x, int y)
        {
            return X == x && Y == y;
        }
    }
}
