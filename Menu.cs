using System;
using System.Collections.Generic;

namespace AgentApp
{
    /// <summary>
    /// Represents the main menu of the application.
    /// </summary>
    public class Menu
    {
        private List<Obstacle> _obstacles = new List<Obstacle>();

        /// <summary>
        /// Displays the main menu and handles user input.
        /// </summary>
        public void Display()
        {
            while (true)
            {
                try
                {
                    ShowOptions();

                    string userInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(userInput) || userInput.Length > 1)
                    {
                        throw new ArgumentException("Invalid option.");
                    }

                    HandleInput(userInput);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Enter code: ");
                }
            }
        }

        private Map _map;

        public Menu()
        {
            _map = new Map(_obstacles);
        }

        /// <summary>
        /// Displays the available menu options to the user.
        /// </summary>
        private void ShowOptions()
        {
            Console.WriteLine("Select one of the following options:");
            Console.WriteLine("g) Add 'Guard' obstacle");
            Console.WriteLine("f) Add 'Fence' obstacle");
            Console.WriteLine("s) Add 'Sensor' obstacle");
            Console.WriteLine("c) Add 'Camera' obstacle");
            //TODO: add new obstacle class of choice (Laser (l) that acts like the fence obstacle but only diagonally (potentially adding multiple nodes.)
            Console.WriteLine("d) Show safe directions");
            Console.WriteLine("m) Display obstacle map");
            Console.WriteLine("p) Find safe path");
            Console.WriteLine("x) Exit");
            Console.WriteLine("Enter code: ");
        }

        /// <summary>
        /// Handles the user's input based on the selected menu option.
        /// </summary>
        /// <param name="input">The user's input.</param>
        private void HandleInput(string input)
        {
            switch (input.ToLower())
            {
                case "g":
                    Guard guard = Guard.CreateFromUserInput();
                    if (guard != null)
                    {
                        _obstacles.Add(guard);
                    }
                    break;

                case "f":
                    Fence fence = Fence.CreateFromUserInput();
                    if (fence != null)
                    {
                        _obstacles.Add(fence);
                    }
                    break;

                case "s":
                    Sensor sensor = Sensor.CreateFromUserInput();
                    if (sensor != null)                      
                    {                                          
                        _obstacles.Add(sensor);                
                    }                                            
                    break;
                case "c":
                    Camera camera = Camera.CreateFromUserInput();
                    if (camera != null)
                    {
                        _obstacles.Add(camera);
                    }
                    break;
                case "d":
                    DirectionService directionService = new DirectionService(_obstacles);
                    directionService.ShowSafeDirections();
                    break;
                case "m":
                    _map.DisplayMap();
                    break;
                case "p":
                    PathfindingService pathfindingService = new PathfindingService(_obstacles);
                    pathfindingService.HandleFindSafePath();
                    break;

                case "x":
                    Console.WriteLine("Exiting the program...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    Console.WriteLine("Enter code: ");
                    break;
            }
        }
    }
}
