using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance = null;

    [SerializeField]
    private GameObject playerObject;

    private GameObject _player;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            _player = Instantiate(playerObject);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
    }


    public GameObject getPlayer()
    {
        return _player;
    }
}
