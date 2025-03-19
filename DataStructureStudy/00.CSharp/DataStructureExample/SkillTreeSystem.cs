using DataStructure.Lib;

internal partial class Program
{
    // RPG 게임의 기술 트리 시스템 예시
    public class SkillTreeSystem
    {
        private Dictionary<int, SkillTreeNode> skills = new Dictionary<int, SkillTreeNode>();
        private int playerSkillPoints = 10;

        public void Initialize()
        {
            // 기술 트리 구성
            BuildSkillTree();
        }

        private void BuildSkillTree()
        {
            // 전사 클래스 기본 기술
            SkillTreeNode basicCombat = new SkillTreeNode(101, "기본 전투술", 0);
            basicCombat.Unlock(); // 시작부터 해금

            // 1단계 기술들
            SkillTreeNode swordMastery = new SkillTreeNode(201, "검술 숙련", 1);
            SkillTreeNode improvedStrength = new SkillTreeNode(202, "힘 증강", 1);
            SkillTreeNode defensiveTactics = new SkillTreeNode(203, "방어 전술", 1);

            // 2단계 기술들
            SkillTreeNode powerfulStrike = new SkillTreeNode(301, "강력한 일격", 3);
            SkillTreeNode dualWielding = new SkillTreeNode(302, "이중 무기", 3);
            SkillTreeNode shieldMastery = new SkillTreeNode(303, "방패 숙련", 2);
            SkillTreeNode armorMastery = new SkillTreeNode(304, "갑옷 숙련", 2);

            // 3단계 기술들
            SkillTreeNode whirlwind = new SkillTreeNode(401, "회오리 공격", 5);
            SkillTreeNode lastStand = new SkillTreeNode(402, "최후의 저항", 5);

            // 기술 트리 계층 구성
            basicCombat.AddChild(swordMastery);
            basicCombat.AddChild(improvedStrength);
            basicCombat.AddChild(defensiveTactics);

            swordMastery.AddChild(powerfulStrike);
            swordMastery.AddChild(dualWielding);

            defensiveTactics.AddChild(shieldMastery);
            defensiveTactics.AddChild(armorMastery);

            powerfulStrike.AddChild(whirlwind);
            shieldMastery.AddChild(lastStand);

            // 딕셔너리에 등록
            skills.Add(basicCombat.SkillId, basicCombat);
            skills.Add(swordMastery.SkillId, swordMastery);
            skills.Add(improvedStrength.SkillId, improvedStrength);
            skills.Add(defensiveTactics.SkillId, defensiveTactics);
            skills.Add(powerfulStrike.SkillId, powerfulStrike);
            skills.Add(dualWielding.SkillId, dualWielding);
            skills.Add(shieldMastery.SkillId, shieldMastery);
            skills.Add(armorMastery.SkillId, armorMastery);
            skills.Add(whirlwind.SkillId, whirlwind);
            skills.Add(lastStand.SkillId, lastStand);

            Console.WriteLine("기술 트리가 성공적으로 구축되었습니다. 총 기술 수: " + skills.Count);
        }

        // 플레이어가 기술을 해금하려고 할 때 사용
        public bool UnlockSkill(int skillId)
        {
            if (!skills.TryGetValue(skillId, out SkillTreeNode skill))
            {
                Console.WriteLine("오류: 존재하지 않는 기술입니다.");
                return false;
            }

            if (skill.IsUnlocked)
            {
                Console.WriteLine($"'{skill.SkillName}'은(는) 이미 해금되었습니다.");
                return false;
            }

            // 선행 기술 확인
            bool prerequisitesMet = true;
            SkillTreeNode parent = FindParentSkill(skill);

            if (parent != null && !parent.IsUnlocked)
            {
                Console.WriteLine($"오류: 선행 기술 '{parent.SkillName}'이(가) 해금되지 않았습니다.");
                prerequisitesMet = false;
            }

            // 스킬 포인트 확인
            if (skill.Cost > playerSkillPoints)
            {
                Console.WriteLine($"오류: 스킬 포인트가 부족합니다. 필요: {skill.Cost}, 보유: {playerSkillPoints}");
                return false;
            }

            if (prerequisitesMet)
            {
                playerSkillPoints -= skill.Cost;
                skill.Unlock();
                Console.WriteLine($"'{skill.SkillName}'을(를) 해금했습니다! 남은 스킬 포인트: {playerSkillPoints}");
                return true;
            }

            return false;
        }

        private SkillTreeNode FindParentSkill(SkillTreeNode childSkill)
        {
            foreach (SkillTreeNode skill in skills.Values)
            {
                foreach (SkillTreeNode child in skill.Children)
                {
                    if (child.SkillId == childSkill.SkillId)
                    {
                        return skill;
                    }
                }
            }

            return null;
        }

        // 기술 트리 상태 표시
        public void DisplaySkillTree()
        {
            Console.WriteLine("\n===== 전사 기술 트리 =====");
            Console.WriteLine($"사용 가능한 스킬 포인트: {playerSkillPoints}");
            Console.WriteLine("---------------------------");

            // 기본 기술부터 시작해서 트리 순회
            SkillTreeNode root = skills[101]; // 기본 전투술
            DisplaySkillNode(root, 0);
        }

        private void DisplaySkillNode(SkillTreeNode node, int depth)
        {
            string indent = new string(' ', depth * 4);
            string status = node.IsUnlocked ? "[해금됨]" : $"[비용: {node.Cost}]";

            Console.WriteLine($"{indent}├─ {node.SkillName} {status}");

            foreach (SkillTreeNode child in node.Children)
            {
                DisplaySkillNode(child, depth + 1);
            }
        }
    }
}
