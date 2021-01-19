using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerSkills {

    public event EventHandler OnSkillPointsChanged;
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs {
        public SkillType skillType;
    }

    public PlayerSkillManager skillManager;

    public class SkillInfo
    {
        String name;
        String desc;
        int cost;
        String hotKey;


        public SkillInfo(String name, String desc, String hotKey, int cost = 1)
        {
            this.name = name;
            this.desc = desc;
            this.cost = cost;
            this.hotKey = hotKey;
        }

        public int getCost()
        {
            return cost;
        }

        public String getDesc()
        {
            return desc;
        }

        public String getName()
        {
            return name;
        }

        public String getHotKey()
        {
            return hotKey;
        }
    }

    public Dictionary<SkillType, SkillInfo> SkillDictionary = new Dictionary<SkillType, SkillInfo>()
        {
            {SkillType.Slash, new SkillInfo("Slash Attack", "기본 공격", "X", 0)},
            {SkillType.Dash_1, new SkillInfo("Dash", "대시", "Z", 2)},
            {SkillType.Dash_2, new SkillInfo("Dash None Damage", "무적 대시", "Z", 4)},
            {SkillType.Fireball, new SkillInfo("Fireball", "파이어볼", "A", 3)},
            {SkillType.Darkness, new SkillInfo("Darkness", "어둠구체", "S", 5)},
            {SkillType.Sword, new SkillInfo("Sword", "슬래시", "D", 4)},
            {SkillType.MoveSpeed, new SkillInfo("Move Speed", "이동속도", null , 2)},
            {SkillType.CastDamage, new SkillInfo("Cast Damage", "마법 공격력", null , 3)},
            {SkillType.AttackDamage, new SkillInfo("Attack Damage", "물리 공격력", null , 3)},
            {SkillType.HEART, new SkillInfo("Max Hp", "최대 체력", null , 3)},
        };

    public enum SkillType {
        None,
        Slash,
        Dash_1,
        Dash_2,
        Fireball,
        Darkness,
        Sword,
        MoveSpeed,
        CastDamage,
        AttackDamage,
        HEART
    }

    public PlayerSkills(PlayerSkillManager skillManager) {
        this.skillManager = skillManager;
    }

    private void UnlockSkill(SkillType skillType)
    {
        if (!IsSkillUnlocked(skillType))
        {
            skillManager.unlockedSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
        }
    }

    public bool IsSkillUnlocked(SkillType skillType) {
        return skillManager.unlockedSkillTypeList.Contains(skillType);
    }

    public bool CanUnlock(SkillType skillType) { // 선수 스킬을 모두 배웠는지 
        SkillType skillRequirement = GetSkillRequirement(skillType);

        if (skillRequirement != SkillType.None) {
            if (IsSkillUnlocked(skillRequirement)) {
                return true;
            } else {
                return false;
            }
        } else {
            return true;
        }
    }

    public SkillType GetSkillRequirement(SkillType skillType) {
        switch (skillType) {
            case SkillType.Dash_2:    return SkillType.Dash_1;
        }
        return SkillType.None;
    }

    public bool TryUnlockSkill(SkillType skillType) {
        SkillInfo skillInfo = SkillDictionary[skillType];
        int skillCost = (skillInfo != null) ? skillInfo.getCost() : 1;

        if (CanUnlock(skillType)) {
            if (skillManager.skillPoints >= skillCost) {
                skillManager.skillPoints -= skillCost;
                OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
                UnlockSkill(skillType);
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

}
