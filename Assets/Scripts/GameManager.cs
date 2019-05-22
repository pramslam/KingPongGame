using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO   {
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;  // Allows other scripts to call functions from GameManager.             

        public ScoreManager scoreManager;
        public AudioManager audioManager;
        public Timer timer;
        public Ball ball;

        public int pointsPerScore = 100;            // Points given when scoring

        private bool isEndGame;

        // Ensures a singleton
        void Awake()
        {
            //Check if there is already an instance of GameManager
            if (instance == null)
                instance = this;
            //If instance already exists:
            else if (instance != this)
                Destroy(gameObject);

            //Set GameManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            isEndGame = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                if (!isEndGame == true)
                {
                    PauseGame();
                }
            }

            if (Input.GetKeyDown("r"))
            {
                RestartGame();
            }

            if (timer.GetTimeLeft() <= 0)
            {
                if (isEndGame == false)
                {
                    isEndGame = true;
                    PauseGame();
                }
            }
        }

        // Public Functions
        #region
        public void PlayBounce()    { audioManager.PlayBounce(); }

        public void PlayScore()     { audioManager.PlayScore(); }

        public void AddScoreLeft()  { scoreManager.AddScoreLeft(pointsPerScore); }

        public void AddScoreRight() { scoreManager.AddScoreRight(pointsPerScore); }
        
        void PauseGame()
        {
            ball.PauseBall();
            timer.PauseTimer();
        }

        void RestartGame()
        {
            isEndGame = false;
            timer.ResetTimer();
            ball.ResetBall();
            ball.ServeBall(Random.Range(0,1));
        }
        #endregion
    }
}
