namespace AgentApp
{
    /// <summary>
    /// Represents an abstract obstacle in the application.
    /// </summary>
    public abstract class Obstacle
    {
        /// <summary>
        /// Determines if the obstacle is at the specified location.
        /// </summary>
        /// <param name="x">The X-coordinate of the location.</param>
        /// <param name="y">The Y-coordinate of the location.</param>
        /// <returns>True if the obstacle is at the specified location; otherwise, false.</returns>
        public abstract bool IsAtLocation(int x, int y);
    }
}
