using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingPong
{
    public class GameManager : MonoBehaviour
    {
        public Ball ball;
        public Paddle paddleLeft;
        public Paddle paddleRight;

        public ScoreManager scoreManager;
        public AudioManager audioManager;
        public Timer timer;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void playBounce()
        {
            audioManager.PlayBounce();
        }

        void playScore()
        {
            audioManager.PlayScore();
        }
    }
}
