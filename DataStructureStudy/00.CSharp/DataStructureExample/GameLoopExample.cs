// 게임 이벤트 스케줄링 시스템 예시
using DataStructure.Lib;

public class GameEventSystem
{
    private PriorityQueue<GameEvent> eventQueue = new PriorityQueue<GameEvent>();
    private float gameTime = 0f;

    // 게임 루프에서 매 프레임 호출
    public void Update(float deltaTime)
    {
        gameTime += deltaTime;

        // 현재 시간에 실행해야 할 이벤트 처리
        while (!eventQueue.IsEmpty)
        {
            GameEvent nextEvent = eventQueue.Dequeue();

            if (nextEvent.ExecutionTime <= gameTime)
            {
                Console.WriteLine($"[{gameTime:F2}] 이벤트 실행: {nextEvent.Name}");
                nextEvent.Execute();
            }
            else
            {
                // 아직 실행 시간이 되지 않았으면 큐에 다시 넣기
                eventQueue.Enqueue(nextEvent);
                break;
            }
        }
    }

    // 특정 시간 후에 실행할 이벤트 예약
    public void ScheduleEvent(string name, float delay, Action callback)
    {
        float executionTime = gameTime + delay;
        GameEvent gameEvent = new GameEvent(name, executionTime, callback);

        eventQueue.Enqueue(gameEvent);
        Console.WriteLine($"[{gameTime:F2}] 이벤트 예약: {name}, 실행 시간: {executionTime:F2}");
    }

    // 주기적으로 반복되는 이벤트 예약
    public void ScheduleRepeatingEvent(string name, float interval, int repeatCount, Action callback)
    {
        ScheduleEvent(name, interval, () => {
            callback();

            // 남은 반복 횟수가 있으면 다시 예약
            if (repeatCount > 1)
            {
                ScheduleRepeatingEvent(name, interval, repeatCount - 1, callback);
            }
        });
    }
}

// 게임에서의 사용 예시
public class GameLoopExample
{
    private GameEventSystem eventSystem = new GameEventSystem();
    private bool gameRunning = true;
    private float enemySpawnInterval = 5f;
    private int waveSize = 3;

    public void StartGame()
    {
        Console.WriteLine("게임 시작!");

        // 10초 후에 보스 등장 이벤트 예약
        eventSystem.ScheduleEvent("보스 등장", 10f, SpawnBoss);

        // 5초마다 적 생성 이벤트 예약
        eventSystem.ScheduleRepeatingEvent("적 생성 웨이브", enemySpawnInterval, 5, SpawnEnemyWave);
        eventSystem.ScheduleEvent("최종 등장", 30f, SpawnBoss);
        // 플레이어에게 30초 동안 버프 부여
        eventSystem.ScheduleEvent("파워업 아이템 생성", 3f, () => {
            Console.WriteLine("파워업 아이템이 필드에 생성되었습니다!");

            // 플레이어가 아이템을 획득했다고 가정
            eventSystem.ScheduleEvent("파워업 획득", 2f, () => {
                Console.WriteLine("플레이어가 파워업을 획득했습니다! 30초 동안 공격력 2배!");

                // 30초 후 버프 종료
                eventSystem.ScheduleEvent("파워업 종료", 30f, () => {
                    Console.WriteLine("파워업 효과가 종료되었습니다.");
                });
            });
        });

        // 게임 루프 시뮬레이션
        float deltaTime = 0.1f; // 0.1초 간격으로 시뮬레이션
        float gameTime = 0f;

        while (gameRunning && gameTime < 30f) // 30초 동안 시뮬레이션
        {
            eventSystem.Update(deltaTime);
            gameTime += deltaTime;

            // 실제 게임에서는 여기서 다른 게임 로직 처리
            System.Threading.Thread.Sleep(100); // 시뮬레이션을 위한 지연
        }

        Console.WriteLine("게임 종료!");
    }

    private void SpawnBoss()
    {
        Console.WriteLine("보스가 등장했습니다! 조심하세요!");

        // 보스 행동 패턴 예약
        eventSystem.ScheduleRepeatingEvent("보스 특수 공격", 3f, 3, () => {
            Console.WriteLine("보스가 특수 공격을 시전합니다!");
        });
    }

    private void SpawnEnemyWave()
    {
        Console.WriteLine($"새로운 적 웨이브! {waveSize}마리의 적이 생성되었습니다.");
        waveSize++; // 웨이브마다 적의 수 증가
    }
}