using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private GameObject cineMachineObject;

    private GameObject _cineMachine;


    [SerializeField]
    public Vector2 startPosition;

    [SerializeField]
    private Sprite backgroundImage;

    private void Start()
    {
        Camera.main.GetComponentInChildren<Image>().sprite = backgroundImage;
        _cineMachine = Instantiate(cineMachineObject);
        _cineMachine.GetComponent<CinemachineVirtualCamera>().Follow = PlayerManager.Instance.getPlayer().transform;
        _cineMachine.GetComponent<CinemachineConfiner>().m_BoundingShape2D = gameObject.GetComponent<PolygonCollider2D>();
        PlayerManager.Instance.getPlayer().transform.position = startPosition;
    }
}
