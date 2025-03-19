namespace tree
{
    public class SkillTreeNode
    {
        public int SkillId { get; private set; }
        public string SkillName { get; private set; }
        public int Cost { get; private set; }
        public bool IsUnlocked { get; private set; }
        public List<SkillTreeNode> Children { get; private set; }
        public SkillTreeNode(int skillId, string skillName, int cost)
        {
            SkillId = skillId;
            SkillName = skillName;
            Cost = cost;
            IsUnlocked = false;
            Children = new List<SkillTreeNode>();
        }
        public void AddChild(SkillTreeNode childSkill)
        {
            Children.Add(childSkill);
        }

        public bool Unlock()
        {
            if (IsUnlocked)
                return false;

            IsUnlocked = true;
            Console.WriteLine($"{SkillName} 스킬이 해금되었습니다!");
            return true;
        }
    }
}
