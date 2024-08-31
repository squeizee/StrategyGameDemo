namespace PathFind
{
    public class Node
    {
        public bool Walkable;
        public int GridX;
        public int GridY;
        public float Penalty;
        
        public int GCost;
        public int HCost;
        public Node Parent;
        
        public Node(float price, int gridX, int gridY)
        {
            Walkable = price != 0.0f;
            Penalty = price;
            GridX = gridX;
            GridY = gridY;
        }

        public int FCost => GCost + HCost;
    }
}