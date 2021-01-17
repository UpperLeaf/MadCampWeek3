using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using static NodeSpace.Node;

public class ClickAction : MonoBehaviour, IPointerClickHandler
{
    public NodeType nodeType;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(nodeType);
    }
}
