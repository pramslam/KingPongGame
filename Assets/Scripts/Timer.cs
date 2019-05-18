using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KingPong
{
    public class Timer : MonoBehaviour
    {
        public float setTime = 180f;

        bool isRunning = false;
        float startTime = 0f;
        float deltaTime = 0f;
        float t = 0f;
        string displayTime = "";

        public Text text;

        // Start is called before the first frame update
        void Start()
        {
            StartTimer();
        }

        // Update is called once per frame
        void Update()
        {
            if (isRunning)
            {
                if (setTime - t <= 0)
                {
                    isRunning = false;
                    //TimesUp();
                }
                else
                {
                    t += Time.deltaTime;
                    displayTime = ConvertSteps2Time(setTime - t);

                    //text.text = displayTime;
                }
            }
        }

        void StartTimer()
        {
            isRunning = true;
            t = 0;
            t = TimerCount();
            displayTime = ConvertSteps2Time(t);
            //text.text = displayTime;
        }

        public float TimerCount()
        {
            deltaTime += Time.deltaTime;
            float dt = setTime - (deltaTime - startTime);

            return dt;
        }

        string ConvertSteps2Time(float steps)
        {
            string seconds = ((int)steps % 60).ToString("F0");
            string minutes = (((int)steps / 60) % 60).ToString("F0");

            return minutes + " : " + seconds;
        }

        public void pauseTimer()
        {
            isRunning = !isRunning;

        }

        public void resetTimer()
        {
            t = 0;
        }
    }
}