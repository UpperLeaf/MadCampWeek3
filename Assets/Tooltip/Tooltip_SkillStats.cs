using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Tooltip_SkillStats : MonoBehaviour
{

    private static Tooltip_SkillStats instance;

    [SerializeField]
    private Camera uiCamera;
    [SerializeField]
    private RectTransform canvasRectTransform;

    private Text nameText;
    private Text descriptionText;
    private Text costText;
    private Text hotKeyText;

    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        nameText = transform.Find("nameText").GetComponent<Text>();
        descriptionText = transform.Find("descriptionText").GetComponent<Text>();
        costText = transform.Find("costText").GetComponent<Text>();
        hotKeyText = transform.Find("hotKeyText").GetComponent<Text>();

        HideTooltip();
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;

        Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y - backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height + backgroundRectTransform.rect.height;
        }
        transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }

    private void ShowTooltip(string skillName, string skillDescription, int cost, String hotKey)
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        nameText.text = skillName;
        descriptionText.text = skillDescription;
        costText.text = cost.ToString() + " SP";
        hotKeyText.text = hotKey;

        Update();
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string skillName, string skillDescription, int cost, String hotKey)
    {
        instance.ShowTooltip(skillName, skillDescription, cost, hotKey);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }





    public static void AddTooltip(Transform transform, string skillName, string skillDescription, int cost, String hotKey)
    {
        if (transform.GetComponent<Button_UI>() != null)
        {
            transform.GetComponent<Button_UI>().MouseOverOnceTooltipFunc = () => Tooltip_SkillStats.ShowTooltip_Static(skillName, skillDescription, cost, hotKey);
            transform.GetComponent<Button_UI>().MouseOutOnceTooltipFunc = () => Tooltip_SkillStats.HideTooltip_Static();
        }
    }

}