using System;
using System.Collections.Generic;

namespace AgentApp
{
    /// <summary>
    /// Provides functionality to check and display safe directions.
    /// </summary>
    public class DirectionService
    {
        private List<Obstacle> _obstacles;

        /// <summary>
        /// Initializes a new instance of the DirectionService class.
        /// </summary>
        /// <param name="obstacles">List of obstacles to be considered for safe directions.</param>
        public DirectionService(List<Obstacle> obstacles)
        {
            _obstacles = obstacles;
        }

        /// <summary>
        /// Displays safe directions for the agent based on the current location.
        /// </summary>
        public void ShowSafeDirections()
        {
            Console.WriteLine("Enter your current location (X,Y): ");
            string currentLocationInput = Console.ReadLine();

            if (!IsValidCoordinate(currentLocationInput))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            string[] currentCoordinates = currentLocationInput.Split(',');
            int currentX = int.Parse(currentCoordinates[0]);
            int currentY = int.Parse(currentCoordinates[1]);

            if (IsLocationCompromised(currentX, currentY))
            {
                Console.WriteLine("Agent, your location is compromised. Abort mission.");
                return;
            }

            string safeDirections = CalculateSafeDirections(currentX, currentY);

            if (string.IsNullOrEmpty(safeDirections))
            {
                Console.WriteLine("You cannot safely move in any direction. Abort mission.");
            }
            else
            {
                Console.WriteLine($"You can safely take any of the following directions: {safeDirections}");
            }
        }

        // Validates if the input string represents valid coordinates
        private bool IsValidCoordinate(string input)
        {
            string[] coordinates = input.Split(',');
            if (coordinates.Length != 2) return false;

            return int.TryParse(coordinates[0], out _) && int.TryParse(coordinates[1], out _);
        }

        // Checks if a given location is compromised by any obstacle
        private bool IsLocationCompromised(int x, int y)
        {
            foreach (var obstacle in _obstacles)
            {
                if (obstacle.IsAtLocation(x, y))
                {
                    return true;
                }
            }
            return false;
        }

        // Calculates the safe directions based on the current location
        private string CalculateSafeDirections(int currentX, int currentY)
        {
            string directions = "";

            if (IsNorthSafe(currentX, currentY)) directions += "N";
            if (IsSouthSafe(currentX, currentY)) directions += "S";
            if (IsEastSafe(currentX, currentY)) directions += "E";
            if (IsWestSafe(currentX, currentY)) directions += "W";

            return directions;
        }

        // Checks if moving north is safe
        private bool IsNorthSafe(int x, int y)
        {
            return !IsLocationCompromised(x, y - 1);  // Decrease y for north
        }

        // Checks if moving south is safe
        private bool IsSouthSafe(int x, int y)
        {
            return !IsLocationCompromised(x, y + 1);  // Increase y for south
        }

        // Checks if moving east is safe
        private bool IsEastSafe(int x, int y)
        {
            return !IsLocationCompromised(x + 1, y);  // Increase x for east
        }

        // Checks if moving west is safe
        private bool IsWestSafe(int x, int y)
        {
            return !IsLocationCompromised(x - 1, y);  // Decrease x for west
        }
    }
}
