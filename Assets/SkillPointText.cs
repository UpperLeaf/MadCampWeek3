using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointText : MonoBehaviour
{
    Text skillPointText;
    // Start is called before the first frame update
    void Start()
    {
        skillPointText = transform.Find("SkillPointText").GetComponent<Text>();
        skillPointText.text = "hello wrold";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
