using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO
{
    public class CenterLine : MonoBehaviour
    {
        public bool[] isBlack;

        private SpriteRenderer spriteRenderer;
        private Color color;

        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            ChangeDividerColor(0);
        }

        public void ChangeDividerColor(int currentBackground)
        {
            // Choose the color, Must manually set colors for specific BGS, TODO
            if (isBlack[currentBackground] == true)
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

        public void SetDividerColor(Color color)
        {
            // Set new color
            spriteRenderer.color = color;
        }
    }
}