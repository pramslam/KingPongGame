using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO   {
    public class Paddle : MonoBehaviour
    {
        public KeyCode moveUp = KeyCode.W;
        public KeyCode moveDown = KeyCode.S;
        public bool firstPlayer = true;

        [SerializeField]
        private bool usingKeyboard = false;
        [SerializeField]
        private float axisMovement = 0.0f;
        private float boundY = 31.0f;                           // Game field boundary
        private float speed = 50.0f;                            // Used with keyboard controls
        private string playerString;
        private string leftAxisString = "LeftVerticalAxis";
        private string rightAxisString = "RightVerticalAxis";
        private Rigidbody2D rigidBody;
        private GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            gameManager = FindObjectOfType<GameManager>();
            usingKeyboard = gameManager.GetPaddleControl();
            speed = gameManager.GetPaddleSpeed();
            SetPlayer();
        }

        // Update is called once per frame
        void Update()
        {
            // Keyboard movement
            if (usingKeyboard)
            {
                var vel = rigidBody.velocity;
                if (Input.GetKey(moveUp))
                {
                    vel.y = speed;
                }
                else if (Input.GetKey(moveDown))
                {
                    vel.y = -speed;
                }
                else
                {
                    vel.y = 0;
                }
                rigidBody.velocity = vel;
            }

            var pos = transform.position;

            // Controller movement
            if (!usingKeyboard)
            {
                axisMovement = Input.GetAxis(playerString);
                pos.y = RangeConvertor(-1.0f, 1.0f, -boundY, boundY, axisMovement);
            }

            // Paddle Boundaries
            if (pos.y > boundY)
            {
                pos.y = boundY;
            }
            else if (pos.y < -boundY)
            {
                pos.y = -boundY;
            }
            transform.position = pos;
        }

        void SetPlayer()
        {
            if (firstPlayer == true)
            {
                playerString = leftAxisString;
            }
            else if (firstPlayer == false)
            {
                playerString = rightAxisString;
                
                // Set keyboard for right player
                moveUp = KeyCode.UpArrow;
                moveDown = KeyCode.DownArrow;
            }
        }

        float RangeConvertor(float originalStart, float originalEnd, float newStart, float newEnd, float value)
        {
            // Converts a set range to another set range, can be used for negatives
            return newStart + (value - originalStart) * (newEnd - newStart) / (originalEnd - originalStart);
		}
    }
}
