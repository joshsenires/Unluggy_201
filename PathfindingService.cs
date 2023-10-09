using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentApp
{
    /// <summary>
    /// Provides pathfinding functionality considering obstacles.
    /// </summary>
    public class PathfindingService
    {
        private List<Obstacle> _obstacles;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathfindingService"/> class.
        /// </summary>
        /// <param name="obstacles">List of obstacles to consider during pathfinding.</param>
        public PathfindingService(List<Obstacle> obstacles)
        {
            _obstacles = obstacles;
        }

        /// <summary>
        /// Parses user input to extract coordinates.
        /// </summary>
        /// <param name="message">Message to display to the user.</param>
        /// <returns>A tuple representing the parsed coordinates.</returns>
        private (int, int) ParseInput(string message)
        {
            Console.WriteLine(message);
            var parts = Console.ReadLine().Split(',');
            return (int.Parse(parts[0]), int.Parse(parts[1]));
        }

        /// <summary>
        /// Handles user input and displays the safe path to the objective.
        /// </summary>
        public void HandleFindSafePath()
        {
            var (agentX, agentY) = ParseInput("Enter your current location (X,Y):");
            var (objectiveX, objectiveY) = ParseInput("Enter the location of the mission objective (X,Y):");

            if (agentX == objectiveX && agentY == objectiveY)
            {
                Console.WriteLine("Agent, you are already at the objective.");
                return;
            }

            if (_obstacles.Any(o => o.IsAtLocation(objectiveX, objectiveY)))
            {
                Console.WriteLine("The objective is blocked by an obstacle and cannot be reached.");
                return;
            }

            string path = FindPath(agentX, agentY, objectiveX, objectiveY);

            Console.WriteLine(string.IsNullOrEmpty(path)
                ? "There is no safe path to the objective."
                : $"The following path will take you to the objective:\n{path}");
        }

        /// <summary>
        /// Finds the safest path from a start location to an end location.
        /// </summary>
        /// <param name="startX">Starting X coordinate.</param>
        /// <param name="startY">Starting Y coordinate.</param>
        /// <param name="endX">Ending X coordinate.</param>
        /// <param name="endY">Ending Y coordinate.</param>
        /// <returns>A string representing the path directions.</returns>
        public string FindPath(int startX, int startY, int endX, int endY)
        {
            Node startNode = new Node(startX, startY);
            Node endNode = new Node(endX, endY);

            List<Node> openList = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node currentNode = openList.OrderBy(n => n.FCost).ThenBy(n => n.HCost).First();

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode.Equals(endNode))
                {
                    StringBuilder pathDirections = new StringBuilder();
                    while (currentNode != null)
                    {
                        if (currentNode.Direction != null)
                            pathDirections.Insert(0, currentNode.Direction); // prepend the direction
                        currentNode = currentNode.Parent;
                    }
                    return pathDirections.ToString();
                }

                foreach (Node neighbor in GetAdjacentNodes(currentNode))
                {
                    if (_obstacles.Any(o => o.IsAtLocation(neighbor.X, neighbor.Y)) || closedList.Contains(neighbor))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);
                    if (newMovementCostToNeighbor < neighbor.GCost || !openList.Contains(neighbor))
                    {
                        neighbor.GCost = newMovementCostToNeighbor;
                        neighbor.HCost = GetDistance(neighbor, endNode);
                        neighbor.Parent = currentNode;

                        if (!openList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                        }
                    }
                }
            }

            return string.Empty; // No path found
        }

        private int GetDistance(Node a, Node b)
        {
            int distX = Math.Abs(a.X - b.X);
            int distY = Math.Abs(a.Y - b.Y);

            return distX + distY;
        }

        private List<Node> GetAdjacentNodes(Node node)
        {
            return new List<Node>
            {
                new Node(node.X - 1, node.Y) { Direction = "W" },
                new Node(node.X + 1, node.Y) { Direction = "E" },
                new Node(node.X, node.Y - 1) { Direction = "N" },
                new Node(node.X, node.Y + 1) { Direction = "S" }
            };
        }

        /// <summary>
        /// Represents a node in the pathfinding algorithm.
        /// </summary>
        public class Node
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int GCost { get; set; }
            public int HCost { get; set; }
            public Node Parent { get; set; }
            public string Direction { get; set; } // N, S, E, W

            public int FCost => GCost + HCost;

            public Node(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;

                Node node = (Node)obj;
                return X == node.X && Y == node.Y;
            }

            public override int GetHashCode()
            {
                return X.GetHashCode() ^ Y.GetHashCode();
            }
        }
    }
}
