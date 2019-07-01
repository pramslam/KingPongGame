using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO   {
    public class Ball : MonoBehaviour
    {
        public float speed = 90;
        private float startingSpeed;
        private float minSpeed = 30.0f;
        private float maxSpeed = 150.0f;

        private bool isPaused = false;
        private Vector2 pausedVector = new Vector2(0,0);
        private Rigidbody2D rigidBody;

        private GameObject leftWall;
        private GameObject rightWall;
        private GameObject leftPaddle;
        private GameObject rightPaddle;

        private GameManager gameManager;

        private void Awake()
        {
            // Set GameObjects
            rigidBody = GetComponent<Rigidbody2D>();
            gameManager = FindObjectOfType<GameManager>();
            leftWall = GameObject.Find("Wall Left");
            rightWall = GameObject.Find("Wall Right");
            leftPaddle = GameObject.Find("Paddle Left");
            rightPaddle = GameObject.Find("Paddle Right");

            // Save initial speed
            startingSpeed = speed;
        }

        // Start is called before the first frame update
        void Start()
        {
            ServeBall(randomNumber());       // Serve ball to random player
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            // Hit the left paddle
            if (col.gameObject == leftPaddle)
            {
                // Calculate hit Factor
                float y = HitFactor(transform.position,
                                    col.transform.position,
                                    col.collider.bounds.size.y);

                // Calculate direction, make length=1 via .normalized
                Vector2 dir = new Vector2(1, y).normalized;

                // Set Velocity with dir * speed
                rigidBody.velocity = dir * speed;

                // Play bounce sound
                gameManager.PlayBounce();
            }

            // Hit the right paddle
            if (col.gameObject == rightPaddle)
            {
                // Calculate hit Factor
                float y = HitFactor(transform.position,
                                    col.transform.position,
                                    col.collider.bounds.size.y);

                // Calculate direction, make length=1 via .normalized
                Vector2 dir = new Vector2(-1, y).normalized;

                // Set Velocity with dir * speed
                rigidBody.velocity = dir * speed;

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
            if (col.gameObject == leftWall)
            {
                gameManager.PlayScore();
                gameManager.AddScoreRight();
                gameManager.ScreenShake();
            }

            // Hit the right wall
            if (col.gameObject == rightWall)
            {
                gameManager.PlayScore();
                gameManager.AddScoreLeft();
                gameManager.ScreenShake();
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            // Hit the left wall?
            if (col.gameObject == leftWall)
            {
                //ResetBall();
                ResetScoredBall(1);
                ServeBall(0);
            }

            // Hit the right wall?
            if (col.gameObject == rightWall)
            {
                //ResetBall();
                ResetScoredBall(0);
                ServeBall(1);
            }
        }

        // Public Functions
        #region
        public void ServeBall(int player)
        {
            Vector3 heading;
            isPaused = false;

            if (player == 0)
            {
                heading = leftPaddle.transform.position - gameObject.transform.position;
            }
            else
            {
                heading = rightPaddle.transform.position - gameObject.transform.position;
            }

            float distance = heading.magnitude;
            Vector3 direction = heading / distance;

            rigidBody.velocity = direction * speed;
        }

        public void ResetBall()
        {
            transform.position = new Vector3(0, 0, 0);
        }

        public void ResetScoredBall(int player)
        {
            if (player == 0)
            {
                transform.position = leftPaddle.transform.position + new Vector3(10, 0, 0);
            }
            if (player == 1)
            {
                transform.position = rightPaddle.transform.position + new Vector3(-10, 0, 0);
            }
        }

        public void ResetBallSpeed()
        {
            speed = startingSpeed;
        }

        public void PauseBall()
        {
            // Pause
            if (!isPaused)
            {
                isPaused = true;
                pausedVector = rigidBody.velocity;
                rigidBody.velocity = Vector2.zero;
            }
            // Unpause
            else if (isPaused)
            {
                isPaused = false;
                rigidBody.velocity = pausedVector;
            }
        }

        public void ChangeBallSpeed(float newSpeed)
        {
            speed += newSpeed;

            // Clamp speed
            if (speed <= minSpeed) { speed = minSpeed; }
            if (speed >= maxSpeed) { speed = maxSpeed; }

            Vector2 dir = rigidBody.velocity.normalized;    // Get direction
            rigidBody.velocity = dir * speed;               // Set velocity
        }
        #endregion

        int randomNumber()
        {
            int range = Random.Range(0, 100);
            range = range % 2;
            if (range >= 1)
                return 1;
            else
                return 0;
        }
    }
}