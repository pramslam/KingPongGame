using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DLO   {
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance = null;     //Allows other scripts to call functions from SoundManager.             

        public Text leftScore;
        public Text rightScore;

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

        public void ScoreLeft()
        {
            Debug.Log("ScoreLeft");
        }

        public void ScoreRight()
        {
            Debug.Log("ScoreRight");
        }
    }
}