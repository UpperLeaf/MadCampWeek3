using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private GameObject _player;
    
    [SerializeField] 
    private UI_SkillTree uiSkillTree;
    
    private void Start()
    {
        _player = PlayerManager.Instance.getPlayer();
        uiSkillTree.SetPlayerSkills(_player.GetComponent<PlayerSkillManager>().GetPlayerSkills());
    }
}
