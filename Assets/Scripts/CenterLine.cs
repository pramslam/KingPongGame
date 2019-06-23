using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterLine : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeDividerColor(int currentBackground)
    {
        // Choose the color
        if (currentBackground == 0 || currentBackground == 2)
        {
            color = new Color(0, 0, 0, 1);  // Black
        }
        else
        {
            color = new Color(1, 1, 1, 1);  // White
        }

        // Set new color
        spriteRenderer.color = color;
    }
}
