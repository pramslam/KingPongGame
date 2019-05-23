using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO   {
    public class Ball : MonoBehaviour
    {
        public float speed = 30;

        private bool isPaused = false;
        private Vector2 pausedVector = new Vector2(0,0);

        private GameObject leftWall;
        private GameObject rightWall;

        private GameManager gameManager;

        private void Awake()
        {
            // Set GameObjects
            gameManager = FindObjectOfType<GameManager>();
            leftWall = GameObject.Find("Wall Left");
            rightWall = GameObject.Find("Wall Right");
        }

        // Start is called before the first frame update
        void Start()
        {
            ServeBall(Random.Range(0,1));       // Serve ball to random player
        }

        void OnCollisionEnter2D(Collision2D col)
        {
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
            // Hit the left wall
            if (col.gameObject.name == "Wall Left")
            {
                gameManager.PlayScore();
                gameManager.AddScoreRight();
            }

            // Hit the right wall
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
                Debug.Log("Insert particle fx here");
                ResetBall();
                ServeBall(0);
            }

            // Hit the right wall?
            if (col.gameObject.name == "Wall Right")
            {
                Debug.Log("Insert particle fx here");
                ResetBall();
                ServeBall(1);
            }
        }

        // Public Functions
        #region
        public void ServeBall(int player)
        {
            isPaused = false;

            if (player == 0)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
            }
            if (player == 1)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            }
        }

        public void ResetBall()
        {
            transform.position = new Vector3(0, 0, 0);
        }

        public void PauseBall()
        {
            // Pause
            if (!isPaused)
            {
                isPaused = true;
                pausedVector = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            // Unpause
            else if (isPaused)
            {
                isPaused = false;
                GetComponent<Rigidbody2D>().velocity = pausedVector;
            }
        }
        #endregion
    }
}