using DataStructure.Lib;
// 게임에서의 사용 예시
public class GraphGameExample
{
    private NavigationSystem navSystem = new NavigationSystem();

    public void Start()
    {
        // 게임 시작 시 내비게이션 시스템 초기화
        navSystem.Initialize();

        // 플레이어가 맵의 한 지점에서 다른 지점으로 이동 명령을 내릴 때
        Vector2 playerPosition = new Vector2(2, 2);
        Vector2 destination = new Vector2(8, 8);

        List<Vector2> path = navSystem.FindPath(playerPosition, destination);

        if (path != null)
        {
            Console.WriteLine("경로를 찾았습니다. 시작 이동...");
            MoveAlongPath(path);
        }
        else
        {
            Console.WriteLine("경로를 찾을 수 없습니다!");
        }
    }

    private void MoveAlongPath(List<Vector2> path)
    {
        // 실제 게임에서는 캐릭터가 부드럽게 경로를 따라가도록 구현
        Console.WriteLine("경로를 따라 이동하는 중...");
        foreach (Vector2 waypoint in path)
        {
            Console.WriteLine($"웨이포인트 ({waypoint.X}, {waypoint.Y})로 이동");
        }
        Console.WriteLine("목적지에 도착했습니다!");
    }
}
/// <summary>
/// 캐릭터 이동 스스템 예시
/// </summary>
internal class NavigationSystem
{
    private Dictionary<int, GraphNode> navMesh = new Dictionary<int, GraphNode>();

    public void Initialize()
    {
        // 게임 시작 시 내비게이션 그래프 구축
        BuildNavMesh();
    }
    // 플레이어 또는 NPC가 목적지로 이동할 때 사용
    public List<Vector2> FindPath(Vector2 start, Vector2 end)
    {
        // 시작점과 끝점에 가장 가까운 노드 찾기
        GraphNode startNode = FindClosestNode(start);
        GraphNode endNode = FindClosestNode(end);

        if (startNode == null || endNode == null)
            return null;

        // A* 알고리즘을 사용하여 경로 찾기
        AStar pathfinder = new AStar();
        Dictionary<Node, List<(Node, float)>> graph = ConvertToAStarGraph();

        List<Node> nodePath = pathfinder.FindPath(
            new Node(startNode.Id, startNode.Position),
            new Node(endNode.Id, endNode.Position),
            graph
        );

        if (nodePath == null)
            return null;

        // 노드 경로를 벡터 경로로 변환
        List<Vector2> vectorPath = new List<Vector2>();
        foreach (Node node in nodePath)
        {
            vectorPath.Add(node.Position);
        }

        Console.WriteLine($"경로를 찾았습니다: {startNode.Id}에서 {endNode.Id}까지, 경유 지점: {vectorPath.Count}개");
        return vectorPath;
    }
    private void BuildNavMesh()
    {
        // 실제 게임에서는 지형 분석을 통해 노드를 생성하지만, 여기서는 간단한 예시 제공
        GraphNode node1 = new GraphNode(1, new Vector2(0, 0));
        GraphNode node2 = new GraphNode(2, new Vector2(10, 0));
        GraphNode node3 = new GraphNode(3, new Vector2(10, 10));
        GraphNode node4 = new GraphNode(4, new Vector2(0, 10));
        GraphNode node5 = new GraphNode(5, new Vector2(5, 5));

        // 노드 간 연결 설정 (양방향)
        node1.AddNeighbor(node2, 10f); // 가중치는 거리
        node2.AddNeighbor(node1, 10f);

        node2.AddNeighbor(node3, 10f);
        node3.AddNeighbor(node2, 10f);

        node3.AddNeighbor(node4, 10f);
        node4.AddNeighbor(node3, 10f);

        node4.AddNeighbor(node1, 10f);
        node1.AddNeighbor(node4, 10f);

        // 중앙 노드는 모든 코너와 연결
        node5.AddNeighbor(node1, 7.07f); // 대각선 거리
        node1.AddNeighbor(node5, 7.07f);

        node5.AddNeighbor(node2, 7.07f);
        node2.AddNeighbor(node5, 7.07f);

        node5.AddNeighbor(node3, 7.07f);
        node3.AddNeighbor(node5, 7.07f);

        node5.AddNeighbor(node4, 7.07f);
        node4.AddNeighbor(node5, 7.07f);

        // 딕셔너리에 노드 추가
        navMesh.Add(node1.Id, node1);
        navMesh.Add(node2.Id, node2);
        navMesh.Add(node3.Id, node3);
        navMesh.Add(node4.Id, node4);
        navMesh.Add(node5.Id, node5);

        Console.WriteLine("NavMesh가 성공적으로 구축되었습니다. 총 노드 수: " + navMesh.Count);
    }
    private GraphNode FindClosestNode(Vector2 position)
    {
        // 가장 가까운 노드 찾기
        GraphNode closest = null;
        float minDistance = float.MaxValue;
        foreach (var node in navMesh.Values)
        {
            float distance = position.Distance(node.Position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = node;
            }
        }
        return closest;
    }
    // 그래프를 A* 알고리즘에 맞는 형식으로 변환
    private Dictionary<Node, List<(Node, float)>> ConvertToAStarGraph()
    {
        Dictionary<Node, List<(Node, float)>> graph = new Dictionary<Node, List<(Node, float)>>();

        foreach (GraphNode node in navMesh.Values)
        {
            Node aStarNode = new Node(node.Id, node.Position);
            List<(Node, float)> neighbors = new List<(Node, float)>();

            foreach (var neighbor in node.Neighbors)
            {
                GraphNode graphNeighbor = neighbor.node;
                float weight = neighbor.weight;

                neighbors.Add((new Node(graphNeighbor.Id, graphNeighbor.Position), weight));
            }

            graph[aStarNode] = neighbors;
        }
        return graph;
    }
}