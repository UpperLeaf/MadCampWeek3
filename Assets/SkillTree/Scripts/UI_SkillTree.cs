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

    private void Awake() {

    }

    public void SetPlayerSkills(PlayerSkills playerSkills)
    {
        this.playerSkills = playerSkills;

        skillButtonList = new List<SkillButton>();
        skillButtonList.Add(new SkillButton(transform.Find("Fireball"), playerSkills, PlayerSkills.SkillType.Fireball));
        skillButtonList.Add(new SkillButton(transform.Find("Ultimate"), playerSkills, PlayerSkills.SkillType.Ultimate));
        skillButtonList.Add(new SkillButton(transform.Find("Explosion"), playerSkills, PlayerSkills.SkillType.Explosion));

        skillButtonList.Add(new SkillButton(transform.Find("Dash_1"), playerSkills, PlayerSkills.SkillType.Dash_1));
        skillButtonList.Add(new SkillButton(transform.Find("Dash_2"), playerSkills, PlayerSkills.SkillType.Dash_2));
        skillButtonList.Add(new SkillButton(transform.Find("Speed_1"), playerSkills, PlayerSkills.SkillType.Speed_1));
        skillButtonList.Add(new SkillButton(transform.Find("Speed_2"), playerSkills, PlayerSkills.SkillType.Speed_2));
        skillButtonList.Add(new SkillButton(transform.Find("Heart_1"), playerSkills, PlayerSkills.SkillType.Heart_1));
        skillButtonList.Add(new SkillButton(transform.Find("Heart_2"), playerSkills, PlayerSkills.SkillType.Heart_2));

        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        UpdateVisuals();

    }

    //    UpdateSkillPoints();
    //}

    //private void PlayerSkills_OnSkillPointsChanged(object sender, System.EventArgs e) {
    //    UpdateSkillPoints();
    //}

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        UpdateVisuals();
    }

    //private void UpdateSkillPoints() {
    //    skillPointsText.SetText(playerSkills.GetSkillPoints().ToString());
    //}

    private void UpdateVisuals()
    {
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

            image = transform.Find("image").GetComponent<Image>();
            backgroundImage = transform.Find("background").GetComponent<Image>();
            cost = transform.Find("skillPointsText").GetComponent<TMPro.TextMeshProUGUI>();

            PlayerSkills.SkillInfo skillInfo = playerSkills.SkillDictionary[skillType];

            cost.text = skillInfo.getCost().ToString();

            //transform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            //{
            //    Tooltip_SkillStats.ShowTooltip_Static(skillInfo.getName(), skillInfo.getDesc(), skillInfo.getCost(), skillInfo.getHotKey());
            //};


            //transform.GetComponent<Button_UI>().MouseOverPerSecFunc = () =>
            //{
            //    Tooltip_SkillStats.HideTooltip_Static();
            //};

            //Tooltip_SkillStats.ShowTooltip_Static()

            //Vector3 tmpPosition = transform.position;
            //tmpPosition.z = 100;
            //transform.position = tmpPosition;

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

        public IEnumerator ShowSkillTooltip()
        {
            while (true)
            {

            }
        }

        public void UpdateVisual()
        {
            // TODO material 수정
            if (playerSkills.IsSkillUnlocked(skillType))
            {
                Color tmpColor = image.color;
                tmpColor.a = 1f;
                image.color = tmpColor;
                image.material = null;
                backgroundImage.material = null;
            }
            else
            {
                if (playerSkills.CanUnlock(skillType))
                {
                    Color tmpColor = image.color;
                    tmpColor.a = 0.3f;
                    image.color = tmpColor;
                    backgroundImage.color = UtilsClass.GetColorFromString("694320");
                    //transform.GetComponent<Button_UI>().enabled = true;
                }
                else
                {
                    Color tmpColor = image.color;
                    tmpColor.a = 0.1f;
                    image.color = tmpColor;
                    backgroundImage.color = new Color(.3f, .3f, .3f);
                    //transform.GetComponent<Button_UI>().enabled = false;
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
