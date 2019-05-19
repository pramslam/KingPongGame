using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DLO   {
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance = null;     //Allows other scripts to call functions from ScoreManager.             

        public Text leftScoreText;
        public Text rightScoreText;

        private int leftScore = 0;
        private int rightScore = 0;

        // Ensures a singleton
        void Awake()
        {
            //Check if there is already an instance of ScoreManager
            if (instance == null)
                instance = this;
            //If instance already exists:
            else if (instance != this)
                Destroy(gameObject);

            //Set ScoreManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
            DontDestroyOnLoad(gameObject);
        }
        
        void Start()
        {
            leftScore = 0;
            rightScore = 0;
            UpdateScore();
        }

        public void AddScoreLeft(int newScoreValue)
        {
            leftScore = leftScore + newScoreValue;
            UpdateScore();
        }

        public void AddScoreRight(int newScoreValue)
        {
            rightScore = rightScore + newScoreValue;
            UpdateScore();
        }

        public void ResetScore()
        {
            Debug.Log("Reset Scores");
            leftScore = 0;
            rightScore = 0;
        }

        void UpdateScore()
        {
            leftScoreText.text = leftScore.ToString();
            rightScoreText.text = rightScore.ToString();
        }
    }
}