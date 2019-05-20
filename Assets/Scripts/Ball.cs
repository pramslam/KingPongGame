﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO   {
    public class Ball : MonoBehaviour
    {
        public float speed = 30;
        public float spawnVariance = 3;

        public GameObject firstPaddle;
        public GameObject leftWall;
        public GameObject rightWall;

        public GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {
            // Initial Velocity
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            // Note: 'col' holds the collision information. If the
            // Ball collided with a paddle, then:
            //   col.gameObject is the paddle
            //   col.transform.position is the paddle's position
            //   col.collider is the paddle's collider

            // Hit the left paddle
            if (col.gameObject.name == "Paddle Left")
            {
                // Calculate hit Factor
                float y = HitFactor(transform.position,
                                    col.transform.position,
                                    col.collider.bounds.size.y);

                // Calculate direction, make length=1 via .normalized
                Vector2 dir = new Vector2(1, y).normalized;

                // Set Velocity with dir * speed
                GetComponent<Rigidbody2D>().velocity = dir * speed;

                // Play bounce sound
                gameManager.PlayBounce();
            }

            // Hit the right paddle
            if (col.gameObject.name == "Paddle Right")
            {
                // Calculate hit Factor
                float y = HitFactor(transform.position,
                                    col.transform.position,
                                    col.collider.bounds.size.y);

                // Calculate direction, make length=1 via .normalized
                Vector2 dir = new Vector2(-1, y).normalized;

                // Set Velocity with dir * speed
                GetComponent<Rigidbody2D>().velocity = dir * speed;

                // Play bounce sound
                gameManager.PlayBounce();
            }

            // Hit the top or bottom wall
            if (col.gameObject.name == "Wall Top" || col.gameObject.name == "Wall Bottom")
            {
                // Play bounce sound
                gameManager.PlayBounce();
            }
        }

        float HitFactor(Vector2 ballPos, Vector2 paddlePos, float paddleHeight)
        {
            // ascii art:
            // ||  1 <- at the top of the racket
            // ||
            // ||  0 <- at the middle of the racket
            // ||
            // || -1 <- at the bottom of the racket
            return (ballPos.y - paddlePos.y) / paddleHeight;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            // Hit the left wall?
            if (col.gameObject.name == "Wall Left")
            {
                gameManager.PlayScore();
                gameManager.AddScoreRight();
            }

            // Hit the right wall?
            if (col.gameObject.name == "Wall Right")
            {
                gameManager.PlayScore();
                gameManager.AddScoreLeft();
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            // Hit the left wall?
            if (col.gameObject.name == "Wall Left")
            {
                Debug.Log("Particles and Re-serve Ball");
            }

            // Hit the right wall?
            if (col.gameObject.name == "Wall Right")
            {
                Debug.Log("Particles and Re-serve Ball");
            }
        }

        void ServeBall(int scoringPlayer)
        {
            if (scoringPlayer == 1)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
                Debug.Log("Serve to the left player");
            }
            if (scoringPlayer == 0)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
                Debug.Log("Serve to the right player");
            }
        }
    }
}