//using DataStructure.Lib;
///// <summary>
///// 2D 게임의 충돌 감지 시스템 예시
///// 대규모 오픈 월드 게임: 많은 개체가 있는 넓은 환경에서 충돌 검사 최적화
///// RTS 게임: 유닛 선택 및 그룹화, 적 탐지를 효율적으로 처리
///// 2D 타일 기반 게임: 타일 맵 렌더링 최적화
///// 물리 시뮬레이션: 파티클 간의 충돌 계산 가속화
///// </summary>
//public class CollisionSystem
//{
//    private QuadTree quadTree;
//    private Rectangle worldBounds;
//    private List<GameObject> gameObjects = new List<GameObject>();

//    public CollisionSystem(float worldWidth, float worldHeight)
//    {
//        worldBounds = new Rectangle(0, 0, worldWidth, worldHeight);
//        quadTree = new QuadTree(0, worldBounds);

//        Console.WriteLine($"충돌 시스템 초기화: 월드 크기 {worldWidth}x{worldHeight}");
//    }

//    // 게임 루프에서 매 프레임 호출
//    public void Update()
//    {
//        // 쿼드트리 초기화
//        quadTree.Clear();

//        // 모든 게임 오브젝트를 쿼드트리에 삽입
//        foreach (var obj in gameObjects)
//        {
//            quadTree.Insert(obj);
//        }

//        // 각 게임 오브젝트에 대해 충돌 검사
//        foreach (var obj in gameObjects)
//        {
//            // 이 오브젝트 주변의 가능한 충돌 대상 검색
//            float queryRadius = obj is Enemy ? 10f : 5f; // 적은 더 넓은 탐지 범위
//            Rectangle queryArea = new Rectangle(
//                obj.Position.X - queryRadius,
//                obj.Position.Y - queryRadius,
//                queryRadius * 2,
//                queryRadius * 2
//            );

//            List<GameObject> potentialCollisions = new List<GameObject>();
//            quadTree.Retrieve(potentialCollisions, queryArea);

//            // 잠재적 충돌 대상과의 정확한 충돌 검사
//            foreach (var other in potentialCollisions)
//            {
//                if (obj != other) // 자기 자신과는 충돌 검사 안 함
//                {
//                    CheckAndResolveCollision(obj, other);
//                }
//            }
//        }
//    }

//    private void CheckAndResolveCollision(GameObject a, GameObject b)
//    {
//        // 간단한 원-원 충돌 검사 (실제 게임에서는 더 복잡한 형태의 충돌 처리가 필요)
//        float distance = a.Position.Distance(b.Position);
//        float combinedRadius = 1.5f; // 단순화를 위한 가정

//        if (distance < combinedRadius)
//        {
//            // 충돌 처리
//            Console.WriteLine($"충돌 감지: {a.Name}와(과) {b.Name} 사이의 충돌");

//            // 오브젝트 타입에 따른 처리
//            if (a is Player && b is Enemy)
//            {
//                Console.WriteLine("플레이어가 적과 충돌했습니다! 데미지 입음");
//                // 플레이어 체력 감소 등의 처리
//            }
//            else if (a is Player && b is Item)
//            {
//                Console.WriteLine("플레이어가 아이템을 획득했습니다!");
//                RemoveGameObject(b);
//            }
//            else if (a is Projectile && b is Enemy)
//            {
//                Console.WriteLine("발사체가 적과 충돌했습니다! 적에게 데미지");
//                RemoveGameObject(a); // 발사체 제거
//                // 적 체력 감소 등의 처리
//            }
//        }
//    }

//    public void AddGameObject(GameObject obj)
//    {
//        gameObjects.Add(obj);
//        Console.WriteLine($"게임 오브젝트 추가: {obj.Name}, 현재 총 오브젝트 수: {gameObjects.Count}");
//    }

//    public void RemoveGameObject(GameObject obj)
//    {
//        gameObjects.Remove(obj);
//        Console.WriteLine($"게임 오브젝트 제거: {obj.Name}, 현재 총 오브젝트 수: {gameObjects.Count}");
//    }
//}

//// 게임 오브젝트 타입 예시
//public class Player : GameObject
//{
//    public Player(string name, Vector2 position) : base(name, position) { }
//}

//public class Enemy : GameObject
//{
//    public Enemy(string name, Vector2 position) : base(name, position) { }
//}

//public class Item : GameObject
//{
//    public Item(string name, Vector2 position) : base(name, position) { }
//}

//public class Projectile : GameObject
//{
//    public Projectile(string name, Vector2 position) : base(name, position) { }
//}

//// 게임에서의 사용 예시
//public class GameWorldExample
//{
//    private CollisionSystem collisionSystem;

//    public void Start()
//    {
//        // 2000x2000 크기의 게임 월드 생성
//        collisionSystem = new CollisionSystem(2000, 2000);

//        // 게임 오브젝트 생성 및 추가
//        Player player = new Player("플레이어", new Vector2(1000, 1000));
//        collisionSystem.AddGameObject(player);

//        // 적 생성
//        for (int i = 0; i < 100; i++)
//        {
//            float x = new Random().Next(0, 2000);
//            float y = new Random().Next(0, 2000);
//            Enemy enemy = new Enemy($"적_{i}", new Vector2(x, y));
//            collisionSystem.AddGameObject(enemy);
//        }

//        // 아이템 생성
//        for (int i = 0; i < 20; i++)
//        {
//            float x = new Random().Next(0, 2000);
//            float y = new Random().Next(0, 2000);
//            Item item = new Item($"아이템_{i}", new Vector2(x, y));
//            collisionSystem.AddGameObject(item);
//        }

//        Console.WriteLine("게임 월드 초기화 완료!");

//        // 게임 루프 시뮬레이션
//        for (int frame = 0; frame < 10; frame++)
//        {
//            Console.WriteLine($"\n프레임 {frame} 처리 중...");

//            // 플레이어 위치 업데이트 (이동했다고 가정)
//            player.Position = new Vector2(
//                player.Position.X + new Random().Next(-50, 50),
//                player.Position.Y + new Random().Next(-50, 50)
//            );

//            // 충돌 감지 및 처리
//            collisionSystem.Update();

//            // 발사체 생성 (플레이어가 공격했다고 가정)
//            if (frame % 3 == 0)
//            {
//                Projectile projectile = new Projectile("발사체", player.Position);
//                collisionSystem.AddGameObject(projectile);
//                Console.WriteLine("플레이어가 발사체를 발사했습니다!");
//            }
//        }
//    }
//}