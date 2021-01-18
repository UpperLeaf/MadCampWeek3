using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerWithSkill : MonoBehaviour {

    private PlayerSkills playerSkills;

    private void Awake() {
        playerSkills = new PlayerSkills();
        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
    }

    private float maxSpeed;
    private float maxHitPoints;

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e) {
        switch (e.skillType) {
            case PlayerSkills.SkillType.Speed_1:
                SetMovementSpeed(65f);
                break;
            case PlayerSkills.SkillType.Speed_2:
                SetMovementSpeed(80f);
                break;
            case PlayerSkills.SkillType.Heart_1:
                SetHealthAmountMax(120f);
                break;
            case PlayerSkills.SkillType.Heart_2:
                SetHealthAmountMax(150f);
                break;
            case PlayerSkills.SkillType.Ultimate:
                SetHealthAmountMax(300f);
                SetMovementSpeed(100f);
                break;
            }
    }

    private void Start() {
        maxSpeed = 40f;
        maxHitPoints = 100f;
    }

    public PlayerSkills GetPlayerSkills() {
        return playerSkills;
    }

    // Can use dash
    public bool CanUseDash() {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Dash_1);
    }

    // Can use fireball
    public bool CanUseFireball() {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Fireball);
    }

    private void SetMovementSpeed(float movementSpeed) {
        maxSpeed = movementSpeed;
    }

    private void SetHealthAmountMax(float healthAmountMax) {
        maxHitPoints = healthAmountMax;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CanUseFireball())
                Debug.Log("파이어볼!!!");
            else
                Debug.Log("아직 파이어볼 사용 불가!!!");

        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            if (CanUseDash())
                Debug.Log("대시!!!");
            else
                Debug.Log("아직 대시 사용 불가!!!");

        }
    }

}
