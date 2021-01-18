using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public NodeSpace.Node.NodeType nodeType;

    [SerializeField]
    private GameObject[] mapsObject;

    public void Start()
    {
        nodeType = MapManager.Instance.now.GetNodeType();
        int randomMap = Random.Range(0, mapsObject.Length);
        GameObject map = Instantiate(mapsObject[randomMap]);

        GameObject _player = PlayerManager.Instance.getPlayer();
        _player.SetActive(true);
        _player.transform.position = map.GetComponent<MapController>().startPosition;

    }
}
