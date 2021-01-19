using NodeSpace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mapGeneratorObject;

    [SerializeField]
    private GameObject playerSpriteObject;

    private GameObject _playerObject;

    public MapGenerator mapGenerator;

    private static MapManager instance = null;

    private Node[][] maps;

    public Node now;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            mapGenerator = mapGeneratorObject.GetComponent<MapGenerator>();
            GameStart();
        }
        else
            Destroy(gameObject);
    }

    private void GameStart()
    {
        maps = mapGenerator.createMap();
        now = mapGenerator.getStartNode();
        if (_playerObject != null)
            Destroy(_playerObject);
        playerSpriteObject.transform.position = new Vector3(now._position_x, now._position_y, -2);
        _playerObject = Instantiate(playerSpriteObject, mapGenerator.gameObject.transform, true);
    }

    public static MapManager Instance
    {
        get
        {
            return instance;
        }
    }


    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if(hit.collider != null && hit.collider.tag == "Node")
            {
                Vector3 clickPos3D = hit.collider.transform.position;
                Vector2 clickPos2D = new Vector2(clickPos3D.x, clickPos3D.y);
                Node node = mapGenerator.getNodeByVector2(clickPos2D);

                if (now.GetNextNodes().Contains(node))
                {
                    ChangeScene(node);
                }
            }
        }
    }


    private void ChangeScene(Node nextNode)
    {
        now = nextNode;
        _playerObject.transform.position = new Vector3(nextNode._position_x, nextNode._position_y, -2);
        mapGenerator.gameObject.SetActive(false);

        if (now.GetNodeType().Equals(Node.NodeType.STORE))
            SceneManager.LoadScene("StoreScene");
        else
            SceneManager.LoadScene("GameScene");
    }
}

