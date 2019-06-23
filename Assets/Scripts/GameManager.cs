using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO   {
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;  // Allows other scripts to call functions from GameManager.             

        public int pointsPerScore = 100;            // Points given when scoring
        public int gameLength = 180;                // Game length in seconds
        public float paddleSpeed = 20.0f;           // Paddle speed with keyboard
        public float ballSpeedStep = 60.0f;         // Speed increase and decrease amount

        private bool isEndGame;

        private ScoreManager scoreManager;
        private AudioManager audioManager;
        private Timer timer;
        private Ball ball;
        private Canvas canvas;
        private ScreenShake screenShake;
        private SwitchBackground switchBackground;
        private CenterLine centerLine;

        // Ensures a singleton
        void Awake()
        {
            // Check if there is already an instance of GameManager
            if (instance == null)
                instance = this;
            // If instance already exists:
            else if (instance != this)
                Destroy(gameObject);

            // Set GameManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
            DontDestroyOnLoad(gameObject);

            // Set GameObjects
            scoreManager = FindObjectOfType<ScoreManager>();
            audioManager = FindObjectOfType<AudioManager>();
            timer = FindObjectOfType<Timer>();
            ball = FindObjectOfType<Ball>();
            canvas = FindObjectOfType<Canvas>();
            screenShake = FindObjectOfType<ScreenShake>();
            switchBackground = FindObjectOfType<SwitchBackground>();
            centerLine = FindObjectOfType<CenterLine>();

            timer.SetGameLength(gameLength);
        }

        // Start is called before the first frame update
        void Start()
        {
            isEndGame = false;
            PauseGame();
        }

        // Update is called once per frame
        void Update()
        {
            // Controls
            #region

            // Exit game
            if (Input.GetButtonDown("Quit Game"))
            {
                StopAllCoroutines();
                Application.Quit();
            }

            // Pause game
            if (Input.GetButtonDown("Pause Game"))
            {
                if (!isEndGame == true)
                {
                    PauseGame();
                }
                else
                    RestartGame();

            }

            // Switch the background
            if (Input.GetButtonDown("Switch Background"))
            {
                SwitchBackground();
            }

            // Switch the background
            if (Input.GetButtonDown("Increase Ball Speed"))
            {
                ball.ChangeBallSpeed(ballSpeedStep);
            }

            // Switch the background
            if (Input.GetButtonDown("Decrease Ball Speed"))
            {
                ball.ChangeBallSpeed(-ballSpeedStep);
            }

            // Restart game
            if (Input.GetButtonDown("Restart Game"))
            {
                RestartGame();
            }
            #endregion

            // End game when timer runs out
            if (timer.GetTimeLeft() <= 0)
            {
                if (isEndGame == false)
                {
                    isEndGame = true;
                    PauseGame();
                }
            }
        }

        //Public Functions
        #region
        public void PlayBounce()    { audioManager.PlayBounce(); }

        public void PlayScore()     { audioManager.PlayScore(); }

        public void AddScoreLeft()  { scoreManager.AddScoreLeft(pointsPerScore); }

        public void AddScoreRight() { scoreManager.AddScoreRight(pointsPerScore); }

        public float GetPaddleSpeed()   { return paddleSpeed; }

        public void ScreenShake()   { screenShake.TriggerShake(); }

        public void SwitchBackground()
        {
            switchBackground.ChangeBackground();
            centerLine.ChangeDividerColor(switchBackground.GetCurrentBackground());
        }
        #endregion

        void PauseGame()
        {
            ball.PauseBall();
            timer.PauseTimer();
        }

        void RestartGame()
        {
            isEndGame = false;
            timer.ResetTimer();
            scoreManager.ResetScore();
            ball.ResetBall();
            ball.ResetBallSpeed();
            ball.ServeBall(randomNumber());
            //PauseGame();
        }

        int randomNumber()
        {
            int range = Random.Range(1, 100);
            range = range % 2;
            if (range >= 1)
                return 1;
            else
                return 0;
        }
    }
}
