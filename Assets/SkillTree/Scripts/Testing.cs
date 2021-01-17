using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

    [SerializeField] private PlayerWithSkill playerWithSkill;
    [SerializeField] private UI_SkillTree uiSkillTree;

    private void Start() {
        Debug.Log("PlayerSkills 초기화");
        uiSkillTree.SetPlayerSkills(playerWithSkill.GetPlayerSkills());
    }


}
