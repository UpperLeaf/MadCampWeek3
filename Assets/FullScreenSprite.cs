using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;


    //게임 시작시 Sprite BackGround을 Full Screen으로 만들어주는 Script
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        Vector2 scale = transform.localScale;
        if (cameraSize.x >= cameraSize.y)
        {
            scale *= cameraSize.x / spriteSize.x;
        }
        else
        {
            scale *= cameraSize.y / spriteSize.y;
        }

        transform.position = Vector2.zero;
        transform.localScale = scale;
    }
}
