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
            {SkillType.Fireball, new SkillInfo("Fireball", "강력한 파이어볼을 발사합니다.", "C", 3) },
            {SkillType.Dash_1, new SkillInfo("Dash1", "빠르게 이동하며 1초간 데미지를 받지 않습니다", "V", 3) },
            {SkillType.Dash_2, new SkillInfo("Dash2", "빠르게 이동하며 3초간 데미지를 받지 않습니다.", "V", 3) },

            {SkillType.Speed_1, new SkillInfo("Fireball", "강력한 파이어볼을 발사합니다.", "C", 3) },
            {SkillType.Speed_2, new SkillInfo("Fireball", "강력한 파이어볼을 발사합니다.", "C", 3) },
            {SkillType.Heart_1, new SkillInfo("Fireball", "강력한 파이어볼을 발사합니다.", "C", 3) },
            {SkillType.Heart_2, new SkillInfo("Fireball", "강력한 파이어볼을 발사합니다.", "C", 3) },
            {SkillType.Explosion, new SkillInfo("Fireball", "강력한 파이어볼을 발사합니다.", "C", 3) },
            {SkillType.Ultimate, new SkillInfo("Fireball", "강력한 파이어볼을 발사합니다.", "C", 3) },
        };

    public enum SkillType {
        None,
        Dash_1,
        Dash_2,
        Fireball,
        Speed_1,
        Speed_2,
        Heart_1,
        Heart_2,
        Explosion,
        Ultimate,
    }

    private List<SkillType> unlockedSkillTypeList;
    private int skillPoints;

    public PlayerSkills() {
        unlockedSkillTypeList = new List<SkillType>();
        skillPoints = 100;
    }

    public void AddSkillPoint() {
        skillPoints++;
        OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetSkillPoints() {
        return skillPoints;
    }

    private void UnlockSkill(SkillType skillType)
    {
        if (!IsSkillUnlocked(skillType))
        {
            unlockedSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
        }
    }

    public bool IsSkillUnlocked(SkillType skillType) {
        return unlockedSkillTypeList.Contains(skillType);
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
            case SkillType.Heart_2:     return SkillType.Heart_1;
            case SkillType.Speed_2:     return SkillType.Speed_1;
            case SkillType.Dash_2:    return SkillType.Dash_1;
        }
        return SkillType.None;
    }

    public bool TryUnlockSkill(SkillType skillType) {
        SkillInfo skillInfo = SkillDictionary[skillType];
        int skillCost = (skillInfo != null) ? skillInfo.getCost() : 1;

        if (CanUnlock(skillType)) {
            if (skillPoints > skillCost) {
                skillPoints -= skillCost;
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
