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
        public int maxOnFire = 3;                   // Scores in a row to catch on fire

        private bool isEndGame;
        private int leftOnFire = 0;
        private int rightOnFire = 0;

        private ScoreManager scoreManager;
        private AudioManager audioManager;
        private EffectsManager effectsManager;
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
            effectsManager = FindObjectOfType<EffectsManager>();
            timer = FindObjectOfType<Timer>();
            ball = FindObjectOfType<Ball>();
            canvas = FindObjectOfType<Canvas>();
            screenShake = FindObjectOfType<ScreenShake>();
            switchBackground = FindObjectOfType<SwitchBackground>();
            centerLine = FindObjectOfType<CenterLine>();

            // Set game variables
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
            effectsManager.LeftOnFire(false);
            effectsManager.RightOnFire(false);
            audioManager.PlayRestartGame();
            leftOnFire = 0;
            rightOnFire = 0;
            ball.ResetBall();
            ball.ResetBallSpeed();
            ball.ServeBall(randomNumber());
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

        void CheckPaddleOnFire(string player)
        {
            if (player == "Left")
            {
                // Do not play effects if already enabled
                if (rightOnFire >= maxOnFire)
                {
                    // Reset opponent fire
                    effectsManager.RightOnFire(false);
                    audioManager.PlayOffFire();
                }

                if (rightOnFire != 0) { rightOnFire = 0; }

                // Do not play effects if already enabled
                if (leftOnFire < maxOnFire)
                {
                    // Check effects
                    leftOnFire++;
                    if (leftOnFire >= maxOnFire)
                    {
                        // Play effects
                        effectsManager.LeftOnFire(true);
                        audioManager.PlayOnFire();
                    }
                }
            }

            if (player == "Right")
            {
                // Do not play effects if already enabled
                if (leftOnFire >= maxOnFire)
                {
                    // Reset opponent fire
                    effectsManager.LeftOnFire(false);
                    audioManager.PlayOffFire();
                }

                if (leftOnFire != 0) { leftOnFire = 0; }

                // Do not play effects if already enabled
                if (rightOnFire < maxOnFire)
                {
                    // Check effects
                    rightOnFire++;
                    if (rightOnFire >= maxOnFire)
                    {
                        // Play effects
                        effectsManager.RightOnFire(true);
                        audioManager.PlayOnFire();
                    }
                }
            }
        }

        // Public Functions
        #region
        public float GetPaddleSpeed()   { return paddleSpeed; }
        public void PlayBounce()        { audioManager.PlayBounce(); }
        public void PlayScore()         { audioManager.PlayScore(); }
        public void ScreenShake()       { screenShake.TriggerShake(); }

        public void AddScoreLeft()
        {
            scoreManager.AddScoreLeft(pointsPerScore);
            CheckPaddleOnFire("Left");
        }
        public void AddScoreRight()
        {
            scoreManager.AddScoreRight(pointsPerScore);
            CheckPaddleOnFire("Right");
        }

        public void SwitchBackground()
        {
            switchBackground.ChangeBackground();
            centerLine.ChangeDividerColor(switchBackground.GetCurrentBackground());
        }
        #endregion
    }
}
