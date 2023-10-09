namespace AgentApp
{
    /// <summary>
    /// Represents a sensor in the application.
    /// </summary>
    public class Sensor : Obstacle
    {
        /// <summary>
        /// Gets the X-coordinate of the sensor.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets the Y-coordinate of the sensor.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Gets the range of the sensor.
        /// </summary>
        public double Range { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Sensor class.
        /// </summary>
        public Sensor(int x, int y, double range)
        {
            X = x;
            Y = y;
            Range = range;
        }

        /// <summary>
        /// Creates a sensor instance based on user input.
        /// </summary>
        public static Sensor CreateFromUserInput()
        {
            while (true)
            {
                Console.WriteLine("Enter the sensor's location (X,Y): ");
                string input = Console.ReadLine();

                if (IsValidCoordinate(input))
                {
                    string[] coordinates = input.Split(',');
                    int x = int.Parse(coordinates[0]);
                    int y = int.Parse(coordinates[1]);

                    Console.WriteLine("Enter the sensor's range (in klicks): ");
                    // Validate range input
                    if (double.TryParse(Console.ReadLine(), out double range) && range > 0)
                    {
                        return new Sensor(x, y, range);
                    }
                }
                Console.WriteLine("Invalid input.");
            }
        }

        // Validates if the input string represents valid coordinates
        private static bool IsValidCoordinate(string input)
        {
            string[] coordinates = input.Split(',');
            return coordinates.Length == 2 && int.TryParse(coordinates[0], out _) && int.TryParse(coordinates[1], out _);
        }

        /// <summary>
        /// Determines if a location is within the sensor's range.
        /// </summary>
        public override bool IsAtLocation(int x, int y)
        {
            // Calculate distance from sensor to the given location
            double distance = Math.Sqrt(Math.Pow(X - x, 2) + Math.Pow(Y - y, 2));
            return distance <= Range;
        }
    }
}
