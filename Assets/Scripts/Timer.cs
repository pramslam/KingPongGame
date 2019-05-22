using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

namespace DLO   {
    public class Timer : MonoBehaviour
    {
        public int startTime = 180;         // In seconds
        public Text timerText;              // UI text object

        private bool isPaused;              // For pausing
        private int timeLeft;               // Seconds left overall
        
        // Start is called before the first frame update
        void Start()
        {
            isPaused = false;
            timeLeft = startTime;
            StartCoroutine("LoseTime");
            Time.timeScale = 1;             // Ensure the timescale is right
        }

        // Update is called once per frame
        void Update()
        {
            // Show time left
            timerText.text = ConvertSteps2Time(timeLeft);
        }

        // Coroutine
        IEnumerator LoseTime()
        {
            while (!isPaused)
            {
                yield return new WaitForSeconds(1);
                timeLeft--;
            }
        }

        string ConvertSteps2Time(float steps)
        {
            string minutes, seconds;

            int secs = ((int)steps % 60);

            // Format text for numbers smaller than 10
            if (secs < 10) { seconds = "0" + secs.ToString("F0"); }
            else           { seconds = secs.ToString("F0"); }
            
            minutes = (((int)steps / 60) % 60).ToString("F0");

            return minutes + " : " + seconds;
        }

        // Public Functions
        #region
        public void PauseTimer()
        {
            if (isPaused)
            {
                isPaused = false;
                StartCoroutine("LoseTime");
            }
            else if (!isPaused)
            {
                isPaused = true;
                StopCoroutine("LoseTime");
            }
        }

        public void ResetTimer()
        {
            timeLeft = startTime;
            isPaused = false;
            StopCoroutine("LoseTime");
            StartCoroutine("LoseTime");
        }

        public int GetTimeLeft() { return timeLeft; }
        #endregion
    }
}