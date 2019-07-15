using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DLO   {
    public class SponsorsManager : MonoBehaviour
    {
        public GameObject sponsorsUI;
        public Text presentedByText;
        public Image background;
        public Image mainSponsor;
        public Image subSponsorLeft;
        public Image subSponsorRight;

        private TimerSponsor timer;
        private bool delayingAd = false;
        private bool playingAd = false;
        private bool firstSponsor = true;

        private void Awake()
        {
            timer = FindObjectOfType<TimerSponsor>();
        }

        // Start is called before the first frame update
        void Start()
        {
            ClearFlags();
        }

        // Update is called once per frame
        void Update()
        {
            if (timer.GetTimeLeft() <= 0)
            {
                if (delayingAd == true)
                {
                    delayingAd = false;             // Don't delay again
                    playingAd = true;               // Play the Ad
                    timer.SetAdTimer();             // Ad play length
                    ChooseSponsor();                // Choose between main and sub sponsors
                    ShowSponsorWindow();            // Show the sponsor window
                }
                else if (playingAd == true)
                {
                    HideSponsorWindow();
                } 
            }
        }

        void ShowSponsorWindow() { sponsorsUI.SetActive(true); }

        void ClearFlags()
        {
            delayingAd = false;
            playingAd = false;
        }

        void ShowMainSponsor()
        {
            mainSponsor.gameObject.SetActive(true);
            subSponsorLeft.gameObject.SetActive(false);
            subSponsorRight.gameObject.SetActive(false);
        }

        void ShowSubSponsor()
        {
            mainSponsor.gameObject.SetActive(false);
            subSponsorLeft.gameObject.SetActive(true);
            subSponsorRight.gameObject.SetActive(true);
        }

        void ChooseSponsor()
        {
            // flip flop between main sponsor and sub sponsors
            if (firstSponsor == true)
            {
                ShowMainSponsor();
                firstSponsor = false;
            }
            else if (firstSponsor == false)
            {
                ShowSubSponsor();
                firstSponsor = true;
            }
        }

        // Public Functions
        public void HideSponsorWindow()
        {
            sponsorsUI.SetActive(false);
            ClearFlags();
        }

        public void PlayAd()
        {
            timer.SetDelayTimer();              // Start the Ad Delay
            delayingAd = true;                  // Delay playing the Ad
        }
    }
}