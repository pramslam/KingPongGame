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
        public Text winnerText;
        public Image leftLeaderImage;
        public Image rightLeaderImage;

        private int leftScore = 0;
        private int rightScore = 0;

        private string leftWinnerString = "Left Player Wins!";
        private string rightWinnerString = "Right Player Wins!";
        private string drawWinnerString = "Draw!";

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
            ResetScore();
        }

        void UpdateScore()
        {
            leftScoreText.text = leftScore.ToString();
            rightScoreText.text = rightScore.ToString();

            UpdateLeader();
        }

        // Public Functions
        #region
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
            leftScore = 0;
            rightScore = 0;
            UpdateScore();
        }

        public void HideWinnerText()        { winnerText.enabled = false; }

        public void ShowWinnerText()
        {
            if (leftScore > rightScore)
            {
                winnerText.text = leftWinnerString;         // Update Text
                winnerText.color = new Color(1, 1, 0, 1);   // Yellow
                winnerText.enabled = true;                  // Show Text
            }

            if (rightScore > leftScore)
            {
                winnerText.text = rightWinnerString;        // Update Text
                winnerText.color = new Color(1, 1, 0, 1);   // Yellow
                winnerText.enabled = true;                  // Show Text
            }

            if (leftScore == rightScore)
            {
                winnerText.text = drawWinnerString;         // Update Text
                winnerText.color = new Color(1, 1, 0, 1);   // Yellow
                winnerText.enabled = true;                  // Show Text
            }
        }

        // Updates who is the leader above the player in the lead
        public void UpdateLeader()
        {
            if (leftScore > rightScore)
            {
                leftLeaderImage.enabled = true;
                rightLeaderImage.enabled = false;
            }

            if (rightScore > leftScore)
            {
                leftLeaderImage.enabled = false;
                rightLeaderImage.enabled = true;
            }

            if (leftScore == rightScore)
            {
                leftLeaderImage.enabled = false;
                rightLeaderImage.enabled = false;
            }
        }
        #endregion
    }
}