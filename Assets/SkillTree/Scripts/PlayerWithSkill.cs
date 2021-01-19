using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerWithSkill : MonoBehaviour
{
    private PlayerSkills playerSkills;
    private Player _player;
    private AttackManager attackManager;

    private void Awake()
    {
        playerSkills = new PlayerSkills();
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
    }

    private float maxSpeed;
    private float maxHitPoints;

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
        }
    }

        //private void Start() {
        //    maxSpeed = 40f;
        //    maxHitPoints = 100f;
        //}

        

        //// Can use dash
        //public bool CanUseDash() {
        //    return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Dash_1);
        //}

        //// Can use fireball
        //public bool CanUseFireball() {
        //    return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Fireball);
        //}

        //private void SetMovementSpeed(float movementSpeed) {
        //    maxSpeed = movementSpeed;
        //}

        //private void SetHealthAmountMax(float healthAmountMax) {
        //    maxHitPoints = healthAmountMax;
        //}


        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.C))
        //    {
        //        if (CanUseFireball())
        //            Debug.Log("파이어볼!!!");
        //        else
        //            Debug.Log("아직 파이어볼 사용 불가!!!");

        //    }
        //    else if (Input.GetKeyDown(KeyCode.V))
        //    {
        //        if (CanUseDash())
        //            Debug.Log("대시!!!");
        //        else
        //            Debug.Log("아직 대시 사용 불가!!!");

        //    }
        //}
    
}
