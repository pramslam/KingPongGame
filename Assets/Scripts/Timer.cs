using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

namespace DLO   {
    public class Timer : MonoBehaviour
    {
        public int startTime = 180;
        public Text timerText;      // UI text object

        bool isRunning = false;
        int timeLeft;               // Seconds left overall
        
        // Start is called before the first frame update
        void Start()
        {
            isRunning = true;
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
            while (isRunning)
            {
                yield return new WaitForSeconds(1);
                timeLeft--;
            }
        }

        string ConvertSteps2Time(float steps)
        {
            string seconds = ((int)steps % 60).ToString("F0");
            string minutes = (((int)steps / 60) % 60).ToString("F0");

            return minutes + " : " + seconds;
        }

        // Public Functions
        #region
        public void PauseTimer() { isRunning = !isRunning; }

        public void ResetTimer() { timeLeft = startTime; }

        public int GetTimeLeft() { return timeLeft; }
        #endregion
    }
}