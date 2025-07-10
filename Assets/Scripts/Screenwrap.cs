using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenwrap : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    private bool HasBeenVisible = false;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (HasBeenVisible == false && SpriteRenderer.isVisible)
        {
            HasBeenVisible = true;
        }
        if (HasBeenVisible == false) 
        {
            return;
        }

        Vector2 screenPos =
             Camera.main.WorldToScreenPoint(transform.position);

        Vector2 newScreenPos = screenPos;

        if (screenPos.x < 0)
        {
            newScreenPos.x = Screen.width;
        }
        else if (screenPos.x > Screen.width)
        {
            newScreenPos.x = 0;
        }

        if (screenPos.y < 0)
        {
            newScreenPos.y = Screen.height;
        }
        else if (screenPos.y > Screen.height)
        {
            newScreenPos.y = 0;
        }

        if (newScreenPos != screenPos)
        {
            Vector2 newWorldPos = Camera.main.ScreenToWorldPoint(newScreenPos);
            transform.position = newWorldPos;
        }
    }
}
