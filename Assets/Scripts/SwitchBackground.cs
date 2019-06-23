using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO
{
    public class SwitchBackground : MonoBehaviour
    {
        public Sprite[] backgrounds;

        private SpriteRenderer spriteRenderer;
        private int currentBackground = 0;

        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Public functions
        public void ChangeBackground()
        {
            int previousBackground = currentBackground;
            currentBackground += 1;

            // Cycle back to first background
            if (currentBackground >= backgrounds.Length) { currentBackground = 0; }

            // Change background
            spriteRenderer.sprite = backgrounds[currentBackground];
        }

        public int GetCurrentBackground()
        {
            return currentBackground;
        }
    }
}
