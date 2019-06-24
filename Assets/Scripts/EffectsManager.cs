using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO
{
    public class EffectsManager : MonoBehaviour
    {
        private GameObject leftPaddlePS;
        private GameObject rightPaddlePS;

        private void Awake()
        {
            // Set GameObjects
            leftPaddlePS = GameObject.Find("Paddle Left").transform.GetChild(0).gameObject;
            rightPaddlePS = GameObject.Find("Paddle Right").transform.GetChild(0).gameObject;
        }

        // Public functions
        #region
        public void LeftOnFire(bool onFire)
        {
            leftPaddlePS.SetActive(onFire);
        }

        public void RightOnFire(bool onFire)
        {
            rightPaddlePS.SetActive(onFire);
        }
        #endregion
    }
}