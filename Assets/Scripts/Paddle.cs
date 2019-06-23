using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO   {
    public class Paddle : MonoBehaviour
    {
        // Keyboard Control Variables
        public KeyCode moveUp = KeyCode.W;
        public KeyCode moveDown = KeyCode.S;

        private float speed = 50.0f;
        private float boundY = 31.0f;
        private Rigidbody2D rigidBody;
        private GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            gameManager = FindObjectOfType<GameManager>();

            SetPaddleSpeed();
        }

        // Update is called once per frame
        void Update()
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

            var pos = transform.position;
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

        void SetPaddleSpeed()
        {
            speed = gameManager.GetPaddleSpeed();
        }
    }
}