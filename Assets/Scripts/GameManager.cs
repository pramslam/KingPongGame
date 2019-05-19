using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO   {
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;     //Allows other scripts to call functions from GameManager.             

        public Ball ball;
        
        public ScoreManager scoreManager;
        public AudioManager audioManager;
        public Timer timer;

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
