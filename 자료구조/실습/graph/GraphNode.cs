namespace graph;

public struct Vector2
{
    public float X { get; set; }
    public float Y { get; set; }
    public Vector2(float x, float y)
    {
        this.X = x;
        this.Y = y;
    }
    public float Distance(Vector2 other)
    {
        float dx = X - other.X;
        float dy = Y - other.Y;
        return (float)Math.Sqrt(dx * dx + dy * dy);
    }
}
public class GraphNode
{
    public int Id { get; private set; }
    public Vector2 Position { get; private set; }
    public List<(GraphNode node, float weight)> Neighbors { get; private set; }
     public GraphNode(int id, Vector2 position)
    {
        Id = id;
        Position = position;
        Neighbors = new List<(GraphNode, float)>();
    }
    public void AddNeighbor(GraphNode node, float weight)
    {
        Neighbors.Add((node,weight));
    }
}
