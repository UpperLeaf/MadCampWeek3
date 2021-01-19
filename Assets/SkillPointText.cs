using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointText : MonoBehaviour
{
    Text skillPointText;
    private PlayerSkills playerSkills;
    private PlayerSkillManager skillManager;

    // Start is called before the first frame update
    void Start()
    {
        skillPointText = transform.Find("SkillPointText").GetComponent<Text>();
    }

    public void SetPlayerSkills(PlayerSkills playerSkills)
    {
        this.playerSkills = playerSkills;
        skillManager = playerSkills.skillManager;
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        skillPointText.text = skillManager.skillPoints.ToString();
    }

}
