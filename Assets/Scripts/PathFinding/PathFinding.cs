using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace PathFinding
{
    public class Pathfinding : MonoBehaviour
    {
        public Transform seeker, target;
        public bool allowDiagonalMovement = true; // Option to enable or disable diagonal movement
        GridController _grid;

        void Awake()
        {
            //_grid = GetComponent<GridManager>();
        }

        void Update()
        {
            //FindPath(seeker.position, target.position);
        }

        void FindPath(Vector3 startPos, Vector3 targetPos)
        {
            // Node startNode = _grid.NodeFromWorldPoint(startPos);
            // Node targetNode = _grid.NodeFromWorldPoint(targetPos);
            //
            // List<Node> openSet = new List<Node>();
            // HashSet<Node> closedSet = new HashSet<Node>();
            // openSet.Add(startNode);
            //
            // while (openSet.Count > 0)
            // {
            //     Node currentNode = openSet[0];
            //     for (int i = 1; i < openSet.Count; i++)
            //     {
            //         if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
            //         {
            //             currentNode = openSet[i];
            //         }
            //     }
            //
            //     openSet.Remove(currentNode);
            //     closedSet.Add(currentNode);
            //
            //     if (currentNode == targetNode)
            //     {
            //         RetracePath(startNode, targetNode);
            //         return;
            //     }
            //
            //     foreach (Node neighbor in GetNeighbors(currentNode))
            //     {
            //         if (!neighbor.Walkable || closedSet.Contains(neighbor))
            //             continue;
            //
            //         int newCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);
            //         if (newCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
            //         {
            //             neighbor.GCost = newCostToNeighbor;
            //             neighbor.HCost = GetDistance(neighbor, targetNode);
            //             neighbor.Parent = currentNode;
            //
            //             if (!openSet.Contains(neighbor))
            //                 openSet.Add(neighbor);
            //         }
            //     }
            // }
        }

        void RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();

            //_grid.path = path;
        }

        int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
            int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

            // Use Manhattan distance when diagonal movement is not allowed
            if (!allowDiagonalMovement)
                return 10 * (dstX + dstY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }

        List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            // Loop to check neighboring nodes
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue; // Skip the current node

                    // Skip diagonal neighbors if diagonal movement is not allowed
                    if (!allowDiagonalMovement && Mathf.Abs(x) == Mathf.Abs(y))
                        continue;

                    int checkX = node.GridX + x;
                    int checkY = node.GridY + y;

                    // if (checkX >= 0 && checkX < _grid.gridSizeX && checkY >= 0 && checkY < _grid.gridSizeY)
                    // {
                    //     neighbors.Add(_grid.grid[checkX, checkY]);
                    // }
                }
            }

            return neighbors;
        }
    }
}
