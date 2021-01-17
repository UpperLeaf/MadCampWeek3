using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeSpace;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    [SerializeField]
    private GameObject _mapPanel;
    private Transform[] _mapNodes;


    [SerializeField]
    private Sprite normalMonster;

    [SerializeField]
    private Sprite boss;

    [SerializeField]
    private Sprite rest;



    private static int MAX_MAP_WIDTH = 19;
    private static int MAX_MAP_HEIGHT = 8;

    private void Awake()
    {
        if (null == _instance)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }
        

    private void Start()
    {
    }  
}
