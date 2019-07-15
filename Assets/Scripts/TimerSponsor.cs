using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

namespace DLO   {
    public class TimerSponsor : MonoBehaviour
    {
        public int adDelay = 3;             // Delay before playing, in seconds
        public int adTime = 5;              // Play length, in seconds

        private bool isPaused;
        private int timeLeft;               // Seconds left overall
        
        // Start is called before the first frame update
        void Start()
        {
            isPaused = false;
            timeLeft = adDelay;
            Time.timeScale = 1;             // Ensure the timescale is right
        }

        // Update is called once per frame
        void Update()
        {
            // Prevents memory leak
            if (timeLeft <= 0)
            {
                isPaused = true;
                StopCoroutine("MinusTime");
            }
        }

        // Coroutine
        IEnumerator MinusTime()
        {
            while (!isPaused)
            {
                yield return new WaitForSeconds(1);
                timeLeft--;
            }
        }

        // Public Functions
        #region
        public int GetTimeLeft() { return timeLeft; }

        public void SetDelayTimer()
        {
            isPaused = false;
            timeLeft = adDelay;
            StopCoroutine("MinusTime");
            StartCoroutine("MinusTime");
        }

        public void SetAdTimer()
        {
            isPaused = false;
            timeLeft = adTime;
            StopCoroutine("MinusTime");
            StartCoroutine("MinusTime");
        }
        #endregion
    }
}