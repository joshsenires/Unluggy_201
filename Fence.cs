using System;

namespace AgentApp
{
    /// <summary>
    /// Represents a fence in the application.
    /// </summary>
    public class Fence : Obstacle
    {
        /// <summary>
        /// Gets the starting location of the fence.
        /// </summary>
        public (int X, int Y) StartLocation { get; private set; }

        /// <summary>
        /// Gets the ending location of the fence.
        /// </summary>
        public (int X, int Y) EndLocation { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Fence class.
        /// </summary>
        public Fence(int startX, int startY, int endX, int endY)
        {
            StartLocation = (startX, startY);
            EndLocation = (endX, endY);
        }

        /// <summary>
        /// Creates a fence instance based on user input.
        /// </summary>
        public static Fence CreateFromUserInput()
        {
            while (true)
            {
                Console.WriteLine("Enter the location where the fence starts (X,Y): ");
                string startInput = Console.ReadLine();

                Console.WriteLine("Enter the location where the fence ends (X,Y): ");
                string endInput = Console.ReadLine();

                if (IsValidCoordinate(startInput) && IsValidCoordinate(endInput))
                {
                    string[] startCoordinates = startInput.Split(',');
                    int startX = int.Parse(startCoordinates[0]);
                    int startY = int.Parse(startCoordinates[1]);

                    string[] endCoordinates = endInput.Split(',');
                    int endX = int.Parse(endCoordinates[0]);
                    int endY = int.Parse(endCoordinates[1]);

                    // Ensure that fences are either horizontal or vertical
                    if ((startX == endX || startY == endY) && (startX != endX || startY != endY))
                    {
                        return new Fence(startX, startY, endX, endY);
                    }
                    else
                    {
                        Console.WriteLine("Fences must be horizontal or vertical.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }

        // Validates if the input string represents valid coordinates
        private static bool IsValidCoordinate(string input)
        {
            string[] coordinates = input.Split(',');
            return coordinates.Length == 2 && int.TryParse(coordinates[0], out _) && int.TryParse(coordinates[1], out _);
        }

        /// <summary>
        /// Determines if the fence is at the specified location.
        /// </summary>
        public override bool IsAtLocation(int x, int y)
        {
            // Check for vertical fence
            if (StartLocation.X == EndLocation.X)
            {
                return x == StartLocation.X && y >= Math.Min(StartLocation.Y, EndLocation.Y) && y <= Math.Max(StartLocation.Y, EndLocation.Y);
            }
            // Check for horizontal fence
            else
            {
                return y == StartLocation.Y && x >= Math.Min(StartLocation.X, EndLocation.X) && x <= Math.Max(StartLocation.X, EndLocation.X);
            }
        }
    }
}
