internal partial class Program
{
    // 게임에서의 사용 예시
    public class RPGGameExample
    {
        private SkillTreeSystem skillSystem = new SkillTreeSystem();

        public void Start()
        {
            // 게임 시작 시 기술 트리 초기화
            skillSystem.Initialize();

            // 기술 트리 표시
            skillSystem.DisplaySkillTree();

            // 플레이어가 새 기술을 해금하려고 할 때
            Console.WriteLine("\n플레이어가 '검술 숙련' 기술을 해금하려고 합니다...");
            skillSystem.UnlockSkill(201);

            Console.WriteLine("\n플레이어가 '강력한 일격' 기술을 해금하려고 합니다...");
            skillSystem.UnlockSkill(301);

            // 선행 조건이 충족되지 않은 기술 해금 시도
            Console.WriteLine("\n플레이어가 '회오리 공격' 기술을 해금하려고 합니다...");
            skillSystem.UnlockSkill(401);

            // 업데이트된 기술 트리 표시
            skillSystem.DisplaySkillTree();
        }
    }
}
