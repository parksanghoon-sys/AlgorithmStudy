using DataStructure.Lib;


// 플레이어 클래스 (간략화)
public class Player
{
    public string Name { get; private set; }
    public Vector2 Position { get; set; }
    public float Health { get; private set; }

    public Player(string name, Vector2 position)
    {
        Name = name;
        Position = position;
        Health = 100;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Console.WriteLine($"{Name}이(가) {damage} 데미지를 입음. 남은 체력: {Health}");

        if (Health <= 0)
        {
            Console.WriteLine($"{Name}이(가) 사망했습니다!");
        }
    }
}

// 게임에서의 사용 예시
public class AIGameExample
{
    private Player player;
    private List<Enemy> enemies = new List<Enemy>();
    private List<EnemyAI> enemyAIs = new List<EnemyAI>();

    public void Start()
    {
        // 플레이어 생성
        player = new Player("용사", new Vector2(50, 50));

        // 적 생성
        Enemy goblin = new Enemy("고블린", new Vector2(60, 50));
        Enemy orc = new Enemy("오크", new Vector2(40, 60));
        Enemy troll = new Enemy("트롤", new Vector2(55, 45));

        enemies.Add(goblin);
        enemies.Add(orc);
        enemies.Add(troll);

        // 각 적에 대한 AI 컨트롤러 생성
        foreach (var enemy in enemies)
        {
            enemyAIs.Add(new EnemyAI(enemy, player));
        }

        Console.WriteLine("게임 시작!");

        // 간단한 게임 루프 시뮬레이션
        float deltaTime = 0.1f;

        // 10초 동안 시뮬레이션
        for (int frame = 0; frame < 100; frame++)
        {
            if (frame % 10 == 0)
                Console.WriteLine($"\n=== 프레임 {frame} ===");

            // 플레이어가 랜덤하게 움직인다고 가정
            if (frame % 20 == 0)
            {
                player.Position = new Vector2(
                    player.Position.X + new Random().Next(-5, 6),
                    player.Position.Y + new Random().Next(-5, 6)
                );
                Console.WriteLine($"플레이어가 ({player.Position.X}, {player.Position.Y})로 이동");
            }

            // 각 적 및 AI 업데이트
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(deltaTime);
                enemyAIs[i].Update(deltaTime);
            }

            // 체력이 낮은 적은 약해지게
            if (frame == 50)
            {
                enemies[0].TakeDamage(80);
            }
        }
    }
}