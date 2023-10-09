using System;
using System.Collections.Generic;

namespace AgentApp
{
    /// <summary>
    /// Represents a map containing various obstacles.
    /// </summary>
    public class Map
    {
        private List<Obstacle> _obstacles;

        /// <summary>
        /// Initializes a new instance of the Map class.
        /// </summary>
        /// <param name="obstacles">List of obstacles to be placed on the map.</param>
        public Map(List<Obstacle> obstacles)
        {
            _obstacles = obstacles;
        }

        /// <summary>
        /// Displays the map with obstacles.
        /// </summary>
        public void DisplayMap()
        {
            while (true)
            {
                Console.WriteLine("Enter the location of the top-left cell of the map (X,Y):");
                string topLeftInput = Console.ReadLine();

                Console.WriteLine("Enter the location of the bottom-right cell of the map (X,Y):");
                string bottomRightInput = Console.ReadLine();

                if (IsValidCoordinate(topLeftInput) && IsValidCoordinate(bottomRightInput))
                {
                    string[] topLeftCoordinates = topLeftInput.Split(',');
                    int topLeftX = int.Parse(topLeftCoordinates[0]);
                    int topLeftY = int.Parse(topLeftCoordinates[1]);

                    string[] bottomRightCoordinates = bottomRightInput.Split(',');
                    int bottomRightX = int.Parse(bottomRightCoordinates[0]);
                    int bottomRightY = int.Parse(bottomRightCoordinates[1]);

                    // Validate map dimensions
                    if (topLeftX <= bottomRightX && topLeftY <= bottomRightY)
                    {
                        for (int y = topLeftY; y <= bottomRightY; y++)
                        {
                            for (int x = topLeftX; x <= bottomRightX; x++)
                            {
                                Console.Write(GetMapCharacter(x, y));
                            }
                            Console.WriteLine();
                        }
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid map specification.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }

        // Validates if the input string represents valid coordinates
        private bool IsValidCoordinate(string input)
        {
            string[] coordinates = input.Split(',');
            return coordinates.Length == 2 && int.TryParse(coordinates[0], out _) && int.TryParse(coordinates[1], out _);
        }

        // Determines the character representation of an obstacle on the map
        private char GetMapCharacter(int x, int y)
        {
            foreach (var obstacle in _obstacles)
            {
                if (obstacle.IsAtLocation(x, y))
                {
                    if (obstacle is Guard) return 'g';
                    if (obstacle is Fence) return 'f';
                    if (obstacle is Sensor) return 's';
                    if (obstacle is Camera) return 'c';
                    //TODO: add new obstacle class of choice
                }
            }
            return '.';
        }
    }
}
