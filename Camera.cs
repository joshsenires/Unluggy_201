using System;

namespace AgentApp
{
    /// <summary>
    /// Represents a camera in the application.
    /// </summary>
    public class Camera : Obstacle
    {
        /// <summary>
        /// Gets the X-coordinate of the camera.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets the Y-coordinate of the camera.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Gets the direction the camera is facing.
        /// </summary>
        public char Direction { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Camera class.
        /// </summary>
        public Camera(int x, int y, char direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        /// <summary>
        /// Creates a camera instance based on user input.
        /// </summary>
        public static Camera CreateFromUserInput()
        {
            while (true)
            {
                Console.WriteLine("Enter the camera's location (X,Y): ");
                string input = Console.ReadLine();

                if (IsValidCoordinate(input))
                {
                    string[] coordinates = input.Split(',');
                    int x = int.Parse(coordinates[0]);
                    int y = int.Parse(coordinates[1]);

                    Console.WriteLine("Enter the direction the camera is facing (n, s, e or w):");
                    string directionInput = Console.ReadLine();
                    char direction = directionInput.Length > 0 ? char.ToLower(directionInput[0]) : ' ';

                    // Validate camera direction
                    if (direction == 'n' || direction == 's' || direction == 'e' || direction == 'w')
                    {
                        return new Camera(x, y, direction);
                    }
                    else
                    {
                        Console.WriteLine("Invalid direction.");
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
        /// Determines if a location is within the camera's view.
        /// </summary>
        public override bool IsAtLocation(int agentX, int agentY)
        {
            // Check if the agent is at the camera's location
            if (agentX == X && agentY == Y) return true;

            // Check based on camera's direction
            switch (Direction)
            {
                case 'n':
                    if (agentX == X && agentY < Y) return true;
                    if (Math.Abs(agentX - X) <= (Y - agentY) && agentY < Y) return true;
                    break;
                case 's':
                    if (agentX == X && agentY > Y) return true;
                    if (Math.Abs(agentX - X) <= (agentY - Y) && agentY > Y) return true;
                    break;
                case 'e':
                    if (agentY == Y && agentX > X) return true;
                    if (Math.Abs(agentY - Y) <= (agentX - X) && agentX > X) return true;
                    break;
                case 'w':
                    if (agentY == Y && agentX < X) return true;
                    if (Math.Abs(agentY - Y) <= (X - agentX) && agentX < X) return true;
                    break;
            }
            return false;
        }
    }
}
