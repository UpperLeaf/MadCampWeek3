
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InvisibleWallController : MonoBehaviour
{
    private BoxCollider2D boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
