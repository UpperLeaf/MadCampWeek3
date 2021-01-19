
using System;
using System.Collections.Generic;

namespace Skills
{
    public enum SkillType
    {
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
}