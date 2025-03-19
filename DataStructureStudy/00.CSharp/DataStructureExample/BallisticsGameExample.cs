using DataStructure.Lib;

// 간단한 탄도학 물리 시스템 예시
public class BallisticsSystem
{
    private const float GRAVITY = 9.8f;

    // 포물선 궤적 계산 (발사체가 특정 지점에 도달하는데 필요한 발사 각도와 속도)
    public bool CalculateTrajectory(
        Vector2 start,
        Vector2 target,
        float maxVelocity,
        out float angle,
        out float velocity)
    {
        angle = 0;
        velocity = 0;

        float dx = target.X - start.X;
        float dy = target.Y - start.Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

        // 수평 거리가 너무 짧으면 계산 불가능
        if (Math.Abs(dx) < 0.001f)
        {
            Console.WriteLine("수평 거리가 너무 짧아 계산할 수 없습니다.");
            return false;
        }

        // 발사 각도 계산 (두 가지 가능한 각도 중 하나 선택)
        float g = GRAVITY;
        float v2 = maxVelocity * maxVelocity;
        float term = v2 * v2 - g * (g * dx * dx + 2 * dy * v2);

        if (term < 0)
        {
            Console.WriteLine("현재 최대 속도로는 목표에 도달할 수 없습니다.");
            return false;
        }

        // 두 가지 가능한 각도 계산
        float angle1 = (float)Math.Atan((v2 + Math.Sqrt(term)) / (g * dx));
        float angle2 = (float)Math.Atan((v2 - Math.Sqrt(term)) / (g * dx));

        // 일반적으로 낮은 각도 선택 (더 효율적인 탄도)
        angle = Math.Min(angle1, angle2);

        // 필요한 초기 속도 계산
        velocity = (float)(dx * g / (2 * Math.Cos(angle) * Math.Sin(angle)));

        // 속도가 최대 속도를 초과하는 경우 최대 속도 사용
        if (velocity > maxVelocity)
        {
            velocity = maxVelocity;
        }

        Console.WriteLine($"발사 해법: 각도 {angle * 180 / Math.PI:F2}도, 속도 {velocity:F2} m/s");
        return true;
    }

    // 시간에 따른 발사체 위치 계산
    public Vector2 CalculatePosition(Vector2 start, float angle, float velocity, float time)
    {
        float vx = velocity * (float)Math.Cos(angle);
        float vy = velocity * (float)Math.Sin(angle);

        float x = start.X + vx * time;
        float y = start.Y + vy * time - 0.5f * GRAVITY * time * time;

        return new Vector2(x, y);
    }

    // 발사체의 전체 궤적 생성 (시각화 또는 예측용)
    public List<Vector2> GenerateTrajectoryPoints(
        Vector2 start,
        float angle,
        float velocity,
        float timeStep = 0.1f,
        int maxPoints = 100)
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(start);

        for (int i = 1; i < maxPoints; i++)
        {
            float time = i * timeStep;
            Vector2 pos = CalculatePosition(start, angle, velocity, time);
            points.Add(pos);

            // 지면에 닿았으면 종료
            if (pos.Y <= 0)
                break;
        }

        return points;
    }

    // 포물선 이동 중 충돌 감지
    public bool CheckCollisionDuringTrajectory(
        Vector2 start,
        float angle,
        float velocity,
        List<Obstacle> obstacles,
        out Vector2 hitPoint,
        out float hitTime)
    {
        hitPoint = new Vector2(0, 0);
        hitTime = 0;

        float timeStep = 0.02f;
        int maxSteps = 1000;
        Vector2 prevPos = start;

        for (int i = 1; i <= maxSteps; i++)
        {
            float time = i * timeStep;
            Vector2 currentPos = CalculatePosition(start, angle, velocity, time);

            // 지면에 닿았는지 확인
            if (currentPos.Y <= 0)
            {
                hitPoint = new Vector2(currentPos.X, 0);
                hitTime = time;
                return true;
            }

            // 장애물과 충돌했는지 확인
            foreach (var obstacle in obstacles)
            {
                if (LineIntersectsObstacle(prevPos, currentPos, obstacle))
                {
                    // 보다 정확한 교차점 계산이 필요하지만, 간단한 예시에서는 현재 위치 사용
                    hitPoint = currentPos;
                    hitTime = time;
                    return true;
                }
            }

            prevPos = currentPos;
        }

        return false;
    }

    private bool LineIntersectsObstacle(Vector2 lineStart, Vector2 lineEnd, Obstacle obstacle)
    {
        // 간단한 구현: 장애물을 원으로 가정
        float closest = ClosestPointOnLineSegment(lineStart, lineEnd, obstacle.Position);
        return closest <= obstacle.Radius;
    }

    private float ClosestPointOnLineSegment(Vector2 lineStart, Vector2 lineEnd, Vector2 point)
    {
        Vector2 line = new Vector2(lineEnd.X - lineStart.X, lineEnd.Y - lineStart.Y);
        float len = (float)Math.Sqrt(line.X * line.X + line.Y * line.Y);

        if (len < 0.0001f)
            return (float)Math.Sqrt(
                Math.Pow(point.X - lineStart.X, 2) +
                Math.Pow(point.Y - lineStart.Y, 2));

        // 정규화된 선분 방향
        Vector2 dir = new Vector2(line.X / len, line.Y / len);

        // 선분 시작점에서 점까지의 벡터
        Vector2 v = new Vector2(point.X - lineStart.X, point.Y - lineStart.Y);

        // 내적을 통해 선분 위의 투영점 위치 계산
        float t = dir.X * v.X + dir.Y * v.Y;

        // 투영점이 선분 범위 밖이면 끝점 중 하나와의 거리 반환
        if (t < 0)
            return (float)Math.Sqrt(
                Math.Pow(point.X - lineStart.X, 2) +
                Math.Pow(point.Y - lineStart.Y, 2));

        if (t > len)
            return (float)Math.Sqrt(
                Math.Pow(point.X - lineEnd.X, 2) +
                Math.Pow(point.Y - lineEnd.Y, 2));

        // 투영점 좌표
        Vector2 projection = new Vector2(
            lineStart.X + dir.X * t,
            lineStart.Y + dir.Y * t);

        // 점과 투영점 사이의 거리
        return (float)Math.Sqrt(
            Math.Pow(point.X - projection.X, 2) +
            Math.Pow(point.Y - projection.Y, 2));
    }
}

// 장애물 클래스
public class Obstacle
{
    public Vector2 Position { get; private set; }
    public float Radius { get; private set; }

    public Obstacle(Vector2 position, float radius)
    {
        Position = position;
        Radius = radius;
    }
}

// 게임에서의 사용 예시
public class BallisticsGameExample
{
    private BallisticsSystem ballisticsSystem = new BallisticsSystem();
    private List<Obstacle> obstacles = new List<Obstacle>();
    private Random random = new Random();

    public void Start()
    {
        Console.WriteLine("탄도학 시스템 데모 시작");

        // 장애물 생성
        for (int i = 0; i < 5; i++)
        {
            float x = 20 + random.Next(60);
            float y = random.Next(20);
            float radius = 2 + random.Next(4);

            obstacles.Add(new Obstacle(new Vector2(x, y), radius));
            Console.WriteLine($"장애물 #{i + 1} 생성: 위치 ({x}, {y}), 반경 {radius}");
        }

        // 발사 시뮬레이션
        Vector2 cannon = new Vector2(0, 0); // 대포 위치
        Vector2 target = new Vector2(80, 10); // 목표 위치
        float maxVelocity = 30.0f;

        Console.WriteLine($"대포 위치: ({cannon.X}, {cannon.Y})");
        Console.WriteLine($"목표 위치: ({target.X}, {target.Y})");
        Console.WriteLine($"최대 발사 속도: {maxVelocity} m/s");

        // 발사 각도와 속도 계산
        if (ballisticsSystem.CalculateTrajectory(cannon, target, maxVelocity, out float angle, out float velocity))
        {
            Console.WriteLine($"발사 각도: {angle * 180 / Math.PI:F2}도");
            Console.WriteLine($"발사 속도: {velocity:F2} m/s");

            // 궤적 생성
            List<Vector2> trajectory = ballisticsSystem.GenerateTrajectoryPoints(cannon, angle, velocity);

            Console.WriteLine("\n예측 궤적:");
            for (int i = 0; i < trajectory.Count; i += 5) // 가독성을 위해 모든 포인트를 표시하지 않음
            {
                Console.WriteLine($"시간 {i * 0.1:F1}초: 위치 ({trajectory[i].X:F1}, {trajectory[i].Y:F1})");
            }

            // 충돌 확인
            if (ballisticsSystem.CheckCollisionDuringTrajectory(
                cannon, angle, velocity, obstacles, out Vector2 hitPoint, out float hitTime))
            {
                Console.WriteLine($"\n충돌 감지: 시간 {hitTime:F2}초, 위치 ({hitPoint.X:F2}, {hitPoint.Y:F2})");

                if (hitPoint.Y <= 0.001f)
                {
                    if (Math.Abs(hitPoint.X - target.X) < 1.0f)
                    {
                        Console.WriteLine("목표 명중!");
                    }
                    else
                    {
                        Console.WriteLine($"지면 충돌: 목표로부터 {Math.Abs(hitPoint.X - target.X):F2}m 떨어진 위치");
                    }
                }
                else
                {
                    Console.WriteLine("장애물과 충돌!");
                }
            }

            // 다른 각도로 시도
            float alternativeAngle = (float)(60 * Math.PI / 180); // 60도
            Console.WriteLine($"\n대체 발사 각도로 시도: {alternativeAngle * 180 / Math.PI}도");

            // 해당 각도에 필요한 속도 계산
            float dx = target.X - cannon.X;
            float dy = target.Y - cannon.Y;
            float altVelocity = (float)Math.Sqrt(
                (9.8f * dx * dx) /
                (2 * Math.Cos(alternativeAngle) * Math.Cos(alternativeAngle) *
                 (dx * Math.Tan(alternativeAngle) - dy))
            );

            if (altVelocity <= maxVelocity)
            {
                Console.WriteLine($"발사 속도: {altVelocity:F2} m/s");

                // 충돌 확인
                if (ballisticsSystem.CheckCollisionDuringTrajectory(
                    cannon, alternativeAngle, altVelocity, obstacles, out Vector2 altHitPoint, out float altHitTime))
                {
                    Console.WriteLine($"충돌 감지: 시간 {altHitTime:F2}초, 위치 ({altHitPoint.X:F2}, {altHitPoint.Y:F2})");

                    if (altHitPoint.Y <= 0.001f)
                    {
                        if (Math.Abs(altHitPoint.X - target.X) < 1.0f)
                        {
                            Console.WriteLine("목표 명중!");
                        }
                        else
                        {
                            Console.WriteLine($"지면 충돌: 목표로부터 {Math.Abs(altHitPoint.X - target.X):F2}m 떨어진 위치");
                        }
                    }
                    else
                    {
                        Console.WriteLine("장애물과 충돌!");
                    }
                }
            }
            else
            {
                Console.WriteLine($"필요한 속도 {altVelocity:F2}m/s가 최대 속도를 초과합니다.");
            }
        }
    }
}