using DataStructure.Lib;

internal partial class Program
{
    private static void Main(string[] args)
    {
        // Node 구조의 위치 찾기
        //GraphGameExample graphGameExample = new();
        //graphGameExample.Start();

        // TreeNode 예제
        //RPGGameExample rPGGameExample = new RPGGameExample();
        //rPGGameExample.Start();

        // 우선순위 큐 (시간)
        //GameLoopExample gameEventSystem = new GameLoopExample();
        //gameEventSystem.StartGame();

        // 출돌 확인 알고리즘
        //GameWorldExample gameWorldExample = new GameWorldExample();
        //gameWorldExample.Start();

        BallisticsGameExample ballisticsGameExample = new BallisticsGameExample();
        ballisticsGameExample.Start();
        Console.WriteLine("Hello, World!");
    }
}
// 게임 오브젝트 풀링 시스템 예시
public class ObjectPool<T> where T : IPoolable, new()
{
    private List<T> activeObjects = new List<T>();
    private Queue<T> pooledObjects = new Queue<T>();
    private int maxPoolSize;
    private int defaultPoolSize;

    public ObjectPool(int defaultPoolSize = 10, int maxPoolSize = 100)
    {
        this.defaultPoolSize = defaultPoolSize;
        this.maxPoolSize = maxPoolSize;

        // 초기 풀 생성
        for (int i = 0; i < defaultPoolSize; i++)
        {
            T newObj = new T();
            newObj.Reset();
            pooledObjects.Enqueue(newObj);
        }

        Console.WriteLine($"오브젝트 풀 생성 완료: 초기 크기 {defaultPoolSize}, 최대 크기 {maxPoolSize}");
    }

    public T Get()
    {
        T obj;

        // 풀에 사용 가능한 객체가 있는지 확인
        if (pooledObjects.Count > 0)
        {
            obj = pooledObjects.Dequeue();
        }
        else
        {
            // 풀이 비었으면 새 객체 생성
            obj = new T();
            Console.WriteLine("풀이 비어 있어 새 객체 생성");
        }

        obj.Reset();
        activeObjects.Add(obj);

        return obj;
    }

    public void Return(T obj)
    {
        if (activeObjects.Contains(obj))
        {
            activeObjects.Remove(obj);

            // 최대 풀 크기를 초과하지 않으면 객체 반환
            if (pooledObjects.Count < maxPoolSize)
            {
                pooledObjects.Enqueue(obj);
            }
            else
            {
                Console.WriteLine("풀이 가득 차서 객체를 버림");
                // 실제 게임에서는 여기서 객체를 파괴하는 로직 추가
            }
        }
    }

    public void ReturnAll()
    {
        while (activeObjects.Count > 0)
        {
            Return(activeObjects[0]);
        }

        Console.WriteLine("모든 활성 객체가 풀로 반환됨");
    }

    public int ActiveCount => activeObjects.Count;
    public int PooledCount => pooledObjects.Count;
}

// 풀링 가능한 객체 인터페이스
public interface IPoolable
{
    void Reset();
}

// 발사체 예시
public class Bullet : IPoolable
{
    public bool Active { get; private set; }
    public Vector2 Position { get; set; }
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }
    public float Damage { get; set; }
    public float LifeTime { get; set; }
    private float currentLifeTime;

    public Bullet()
    {
        Reset();
    }

    public void Reset()
    {
        Active = false;
        Position = new Vector2(0, 0);
        Direction = new Vector2(0, 0);
        Speed = 10.0f;
        Damage = 10.0f;
        LifeTime = 3.0f;
        currentLifeTime = 0;
    }

    public void Activate(Vector2 position, Vector2 direction)
    {
        Active = true;
        Position = position;
        Direction = direction;
        currentLifeTime = 0;
    }

    public bool Update(float deltaTime)
    {
        if (!Active) return false;

        // 시간 업데이트
        currentLifeTime += deltaTime;
        if (currentLifeTime >= LifeTime)
        {
            Active = false;
            return true; // 수명 종료, 풀로 반환
        }

        // 이동 처리
        Position = new Vector2(
            Position.X + Direction.X * Speed * deltaTime,
            Position.Y + Direction.Y * Speed * deltaTime
        );

        return false;
    }
}

// 파티클 예시
public class Particle : IPoolable
{
    public bool Active { get; private set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float Size { get; set; }
    public float LifeTime { get; set; }
    private float currentLifeTime;
    public string Color { get; set; }

    public Particle()
    {
        Reset();
    }

    public void Reset()
    {
        Active = false;
        Position = new Vector2(0, 0);
        Velocity = new Vector2(0, 0);
        Size = 1.0f;
        LifeTime = 1.0f;
        currentLifeTime = 0;
        Color = "White";
    }

    public void Activate(Vector2 position, Vector2 velocity, float size, float lifeTime, string color)
    {
        Active = true;
        Position = position;
        Velocity = velocity;
        Size = size;
        LifeTime = lifeTime;
        currentLifeTime = 0;
        Color = color;
    }

    public bool Update(float deltaTime)
    {
        if (!Active) return false;

        // 시간 업데이트
        currentLifeTime += deltaTime;
        if (currentLifeTime >= LifeTime)
        {
            Active = false;
            return true; // 수명 종료, 풀로 반환
        }

        // 이동 처리
        Position = new Vector2(
            Position.X + Velocity.X * deltaTime,
            Position.Y + Velocity.Y * deltaTime
        );

        // 크기 감소 (페이드 아웃 효과)
        Size = Size * (1.0f - (currentLifeTime / LifeTime));

        return false;
    }
}

// 파티클 시스템
public class ParticleSystem
{
    private ObjectPool<Particle> particlePool;
    private List<Particle> activeParticles = new List<Particle>();
    private Random random = new Random();

    public ParticleSystem(int defaultPoolSize = 100, int maxPoolSize = 1000)
    {
        particlePool = new ObjectPool<Particle>(defaultPoolSize, maxPoolSize);
    }

    public void CreateExplosion(Vector2 position, int particleCount, float power)
    {
        for (int i = 0; i < particleCount; i++)
        {
            Particle particle = particlePool.Get();

            // 무작위 방향
            float angle = (float)(random.NextDouble() * Math.PI * 2);
            float speed = (float)(random.NextDouble() * power);

            Vector2 velocity = new Vector2(
                (float)Math.Cos(angle) * speed,
                (float)Math.Sin(angle) * speed
            );

            // 색상 선택
            string[] colors = { "Red", "Orange", "Yellow" };
            string color = colors[random.Next(colors.Length)];

            // 파티클 설정
            particle.Activate(
                position,
                velocity,
                (float)(random.NextDouble() * 2 + 1), // 크기 1-3
                (float)(random.NextDouble() * 1.5 + 0.5), // 수명 0.5-2초
                color
            );

            activeParticles.Add(particle);
        }

        Console.WriteLine($"폭발 효과 생성: 위치 ({position.X}, {position.Y}), 파티클 {particleCount}개");
    }

    public void Update(float deltaTime)
    {
        for (int i = activeParticles.Count - 1; i >= 0; i--)
        {
            Particle particle = activeParticles[i];

            if (particle.Update(deltaTime))
            {
                // 수명이 다한 파티클은 풀로 반환
                activeParticles.RemoveAt(i);
                particlePool.Return(particle);
            }
        }
    }

    public int ActiveParticleCount => activeParticles.Count;
}

// 총알 관리 시스템
public class BulletManager
{
    private ObjectPool<Bullet> bulletPool;
    private List<Bullet> activeBullets = new List<Bullet>();

    public BulletManager(int defaultPoolSize = 50, int maxPoolSize = 500)
    {
        bulletPool = new ObjectPool<Bullet>(defaultPoolSize, maxPoolSize);
    }

    public void FireBullet(Vector2 position, Vector2 direction, float speed, float damage)
    {
        Bullet bullet = bulletPool.Get();

        // 방향 정규화
        float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
        if (length > 0)
        {
            direction.X /= length;
            direction.Y /= length;
        }

        // 총알 설정
        bullet.Activate(position, direction);
        bullet.Speed = speed;
        bullet.Damage = damage;

        activeBullets.Add(bullet);

        Console.WriteLine($"총알 발사: 위치 ({position.X}, {position.Y}), 방향 ({direction.X}, {direction.Y})");
    }

    public void Update(float deltaTime)
    {
        for (int i = activeBullets.Count - 1; i >= 0; i--)
        {
            Bullet bullet = activeBullets[i];

            if (bullet.Update(deltaTime))
            {
                // 수명이 다한 총알은 풀로 반환
                activeBullets.RemoveAt(i);
                bulletPool.Return(bullet);
            }
        }
    }

    // 총알과 적의 충돌 검사 (간단한 구현)
    public void CheckCollisions(List<Enemy> enemies, ParticleSystem particleSystem)
    {
        for (int i = activeBullets.Count - 1; i >= 0; i--)
        {
            Bullet bullet = activeBullets[i];

            foreach (var enemy in enemies)
            {
                float distance = enemy.DistanceTo(bullet.Position);

                if (distance < 1.0f) // 간단한 충돌 체크
                {
                    // 적에게 데미지
                    enemy.TakeDamage(bullet.Damage);

                    // 총알 효과 및 반환
                    particleSystem.CreateExplosion(bullet.Position, 10, 5.0f);
                    activeBullets.RemoveAt(i);
                    bulletPool.Return(bullet);
                    break;
                }
            }
        }
    }

    public int ActiveBulletCount => activeBullets.Count;
}

// 게임에서의 사용 예시
public class ObjectPoolingGameExample
{
    private BulletManager bulletManager;
    private ParticleSystem particleSystem;
    private List<Enemy> enemies = new List<Enemy>();
    private Vector2 playerPosition;
    private Random random = new Random();

    public void Start()
    {
        Console.WriteLine("오브젝트 풀링 데모 시작");

        // 시스템 초기화
        bulletManager = new BulletManager(100, 1000);
        particleSystem = new ParticleSystem(200, 2000);
        playerPosition = new Vector2(50, 50);

        // 적 생성
        for (int i = 0; i < 10; i++)
        {
            Vector2 position = new Vector2(
                (float)(random.NextDouble() * 100),
                (float)(random.NextDouble() * 100)
            );

            Enemy enemy = new Enemy($"적_{i}", position);
            enemies.Add(enemy);
        }

        Console.WriteLine("게임 시스템 초기화 완료");

        // 게임 루프 시뮬레이션
        float deltaTime = 0.05f;
        float gameTime = 0;
        int fireRate = 5; // 초당 발사 횟수
        float fireTimer = 0;

        for (int frame = 0; frame < 200; frame++)
        {
            gameTime += deltaTime;
            fireTimer += deltaTime;

            if (frame % 20 == 0)
                Console.WriteLine($"\n=== 게임 시간: {gameTime:F1}초 ===");

            // 플레이어 이동 (원 운동)
            playerPosition = new Vector2(
                50 + (float)Math.Cos(gameTime) * 20,
                50 + (float)Math.Sin(gameTime) * 20
            );

            // 총알 발사
            if (fireTimer >= 1.0f / fireRate)
            {
                fireTimer = 0;

                // 가장 가까운 적 방향으로 발사
                Enemy closestEnemy = FindClosestEnemy();

                if (closestEnemy != null)
                {
                    Vector2 direction = new Vector2(
                        closestEnemy.Position.X - playerPosition.X,
                        closestEnemy.Position.Y - playerPosition.Y
                    );

                    bulletManager.FireBullet(playerPosition, direction, 15.0f, 20.0f);
                }
            }

            // 단발성 파티클 효과 (간헐적)
            if (frame % 30 == 0)
            {
                Vector2 effectPos = new Vector2(
                    (float)(random.NextDouble() * 100),
                    (float)(random.NextDouble() * 100)
                );

                particleSystem.CreateExplosion(effectPos, 30, 8.0f);
            }

            // 시스템 업데이트
            bulletManager.Update(deltaTime);
            particleSystem.Update(deltaTime);

            // 충돌 검사
            bulletManager.CheckCollisions(enemies, particleSystem);

            // 모든 적이 처치되면 게임 재시작
            if (enemies.Count == 0)
            {
                Console.WriteLine("모든 적을 처치했습니다! 게임 재시작...");

                // 적 다시 생성
                for (int i = 0; i < 10; i++)
                {
                    Vector2 position = new Vector2(
                        (float)(random.NextDouble() * 100),
                        (float)(random.NextDouble() * 100)
                    );

                    Enemy enemy = new Enemy($"적_{i}", position);
                    enemies.Add(enemy);
                }
            }

            // 시스템 상태 보고 (주기적)
            if (frame % 50 == 0)
            {
                Console.WriteLine($"시스템 상태:");
                Console.WriteLine($"- 활성 총알: {bulletManager.ActiveBulletCount}개");
                Console.WriteLine($"- 활성 파티클: {particleSystem.ActiveParticleCount}개");
                Console.WriteLine($"- 남은 적: {enemies.Count}개");
            }
        }

        Console.WriteLine("시뮬레이션 종료");
    }

    private Enemy FindClosestEnemy()
    {
        if (enemies.Count == 0)
            return null;

        Enemy closest = enemies[0];
        float closestDist = closest.DistanceTo(playerPosition);

        for (int i = 1; i < enemies.Count; i++)
        {
            float dist = enemies[i].DistanceTo(playerPosition);
            if (dist < closestDist)
            {
                closest = enemies[i];
                closestDist = dist;
            }
        }

        return closest;
    }
}
