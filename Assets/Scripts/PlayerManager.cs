using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance = null;

    [SerializeField]
    private PlayerState playerState;

    [SerializeField]
    private GameObject player;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("Player Manager Destroyed");
            Destroy(this.gameObject);
        }
    }

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
    }

}
