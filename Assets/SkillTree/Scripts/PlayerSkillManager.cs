using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSkillManager : MonoBehaviour
{
    private PlayerSkills playerSkills;

    [SerializeField]
    public int skillPoints;

    public bool canUseDash;
    public bool isDashInvincible;

    [SerializeField]
    public List<PlayerSkills.SkillType> unlockedSkillTypeList; // 플레이어가 사용 가능한 스킬의 목록

    public Player _player; // 공격력, 마력, 체력, 이동속도
    public AttackManager attackManager; // 스킬 사용 가능 여부

    private void Awake()
    {
        playerSkills = new PlayerSkills(this);
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        canUseDash = false;
    }


    public PlayerSkills GetPlayerSkills()
    {
        return playerSkills;
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        switch (e.skillType)
        {
            case PlayerSkills.SkillType.AttackDamage:
                _player.plusAttackDamage();
                break;

            case PlayerSkills.SkillType.CastDamage:
                _player.plusMagicDamage();
                break;
            case PlayerSkills.SkillType.HEART:
                _player.plusMaxHp();
                break;
            case PlayerSkills.SkillType.MoveSpeed:
                _player.plusMoveSpeed();
                break;
            case PlayerSkills.SkillType.Fireball:
                attackManager.AttachFireballAttack();
                break;
            case PlayerSkills.SkillType.Darkness:
                attackManager.AttachDarknessAttack();
                break;
            case PlayerSkills.SkillType.Slash:
                attackManager.AttachSlashAttack();
                break;
            case PlayerSkills.SkillType.Sword:
                attackManager.AttachSwordAttack();
                break;
            case PlayerSkills.SkillType.Dash_1:
                canUseDash = true;
                break;
            case PlayerSkills.SkillType.Dash_2:
                isDashInvincible = true;
                break;

        }
    }
    
}
