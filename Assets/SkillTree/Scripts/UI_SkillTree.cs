using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_SkillTree : MonoBehaviour {

    [SerializeField] private Material skillLockedMaterial;
    [SerializeField] private Material skillUnlockableMaterial;
    [SerializeField] private SkillUnlockPath[] skillUnlockPathArray;
    [SerializeField] private Sprite lineSprite;
    [SerializeField] private Sprite lineGlowSprite;

    private PlayerSkills playerSkills;
    private List<SkillButton> skillButtonList;
    Text skillPointText;


    public void SetPlayerSkills(PlayerSkills playerSkills)
    {
        this.playerSkills = playerSkills;

        skillButtonList = new List<SkillButton>();
 
        skillButtonList.Add(new SkillButton(transform.Find("Dash_1"), playerSkills, PlayerSkills.SkillType.Dash_1));
        skillButtonList.Add(new SkillButton(transform.Find("Dash_2"), playerSkills, PlayerSkills.SkillType.Dash_2));
        skillButtonList.Add(new SkillButton(transform.Find("Heart_1"), playerSkills, PlayerSkills.SkillType.HEART));
        skillButtonList.Add(new SkillButton(transform.Find("SlashAttack"), playerSkills, PlayerSkills.SkillType.Slash));
        skillButtonList.Add(new SkillButton(transform.Find("AttackDamage"), playerSkills, PlayerSkills.SkillType.AttackDamage));
        skillButtonList.Add(new SkillButton(transform.Find("MoveSpeed"), playerSkills, PlayerSkills.SkillType.MoveSpeed));
        skillButtonList.Add(new SkillButton(transform.Find("CastDamage"), playerSkills, PlayerSkills.SkillType.CastDamage));
        skillButtonList.Add(new SkillButton(transform.Find("Fireball"), playerSkills, PlayerSkills.SkillType.Fireball));
        skillButtonList.Add(new SkillButton(transform.Find("Darkness"), playerSkills, PlayerSkills.SkillType.Darkness));

        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;

        skillPointText = transform.Find("SkillPointText").GetComponent<Text>();
        UpdateVisuals();
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        transform.Find("SkillPointText").GetComponent<Text>().text = playerSkills.skillManager.skillPoints.ToString();

        foreach (SkillButton skillButton in skillButtonList)
        {
            skillButton.UpdateVisual();
        }

        // Darken all links
        foreach (SkillUnlockPath skillUnlockPath in skillUnlockPathArray)
        {
            foreach (Image linkImage in skillUnlockPath.linkImageArray)
            {
                linkImage.color = new Color(.5f, .5f, .5f);
                linkImage.sprite = lineSprite;
            }
        }

        foreach (SkillUnlockPath skillUnlockPath in skillUnlockPathArray)
        {
            if (playerSkills.IsSkillUnlocked(skillUnlockPath.skillType) || playerSkills.CanUnlock(skillUnlockPath.skillType))
            {
                // Skill unlocked or can be unlocked
                foreach (Image linkImage in skillUnlockPath.linkImageArray)
                {
                    linkImage.color = Color.white;
                    linkImage.sprite = lineGlowSprite;
                }
            }
        }
    }

    ///*
    // * Represents a single Skill Button
    // * */
    private class SkillButton
    {

        private Transform transform;
        private Image image;
        private Image backgroundImage;
        private TMPro.TextMeshProUGUI cost;

        private PlayerSkills playerSkills;
        private PlayerSkills.SkillType skillType;

        public SkillButton(Transform transform, PlayerSkills playerSkills, PlayerSkills.SkillType skillType)
        {
            Debug.Log("스킬 버튼 만드는 중: " + skillType);
            this.transform = transform;
            this.playerSkills = playerSkills;
            this.skillType = skillType;


            if (transform.Find("image") != null)
            {
                image = transform.Find("image").GetComponent<Image>();
            }
            backgroundImage = transform.Find("background").GetComponent<Image>();

            PlayerSkills.SkillInfo skillInfo = playerSkills.SkillDictionary[skillType];

            Tooltip_SkillStats.AddTooltip(transform, skillInfo.getName(), skillInfo.getDesc(), skillInfo.getCost(), skillInfo.getHotKey());


            transform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                Debug.Log(skillType + " 클릭");

                if (!playerSkills.IsSkillUnlocked(skillType)) // 이미 배우지 않은 스킬만 추가로 배울 수 있음
                {
                    Debug.Log(skillType + "을 배워볼까요??");

                    // Skill not yet unlocked
                    if (!playerSkills.TryUnlockSkill(skillType))
                    {
                        Debug.Log("Cannot unlock " + skillType + "!");
                        Tooltip_Warning.ShowTooltip_Static("아직 " + skillInfo.getName() + "을 배울 수 없습니다!");
                    }
                    else Debug.Log(skillType + "을 배웠습니다!");

                }
                else Debug.Log(skillType + "은 이미 배운 스킬입니다");

            };
        }

        public void UpdateVisual()
        {
            // TODO material 수정
            if (playerSkills.IsSkillUnlocked(skillType))
            {
                if (image != null)
                {
                    Color tmpColor = image.color;
                    tmpColor.a = 1f;
                    image.color = tmpColor;
                    image.material = null;
                }
                backgroundImage.material = null;
            }
            else
            {
                if (playerSkills.CanUnlock(skillType))
                {
                    if (image != null)
                    {
                        Color tmpColor = image.color;
                        tmpColor.a = 0.3f;
                        image.color = tmpColor;
                    }
                    backgroundImage.color = UtilsClass.GetColorFromString("694320");
                }
                else
                {
                    if (image != null)
                    {
                        Color tmpColor = image.color;
                        tmpColor.a = 0.1f;
                        image.color = tmpColor;
                    }
                    backgroundImage.color = new Color(.3f, .3f, .3f);
                }
            }
        }

    }


    [System.Serializable]
    public class SkillUnlockPath
    {
        public PlayerSkills.SkillType skillType;
        public Image[] linkImageArray;
    }

}
