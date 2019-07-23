using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DLO   {
    public class SponsorsManager : MonoBehaviour
    {
        public GameObject sponsorsUI;
        public GameObject mainUI;
        public GameObject subUI;
        public GameObject microUI;
        public Text presentedByText;
        public Image mainSponsorImage;
        public Image subSponsorLeftImage;
        public Image subSponsorRightImage;
        public Image microSponsorImageLL;
        public Image microSponsorImageLR;
        public Image microSponsorImageRL;
        public Image microSponsorImageRR;
        public Sprite[] mainSponsors;
        public Sprite[] subSponsors;
        public Sprite[] microSponsors;

        private TimerSponsor timer;
        private bool delayingAd = false;
        private bool playingAd = false;
        private bool playedMain;
        private bool playedSub;
        private bool playedMicro;
        private int currentMain = 0;
        private int currentSub = 0;
        private int currentMicro = 0;

        private void Awake()
        {
            timer = FindObjectOfType<TimerSponsor>();
        }

        // Start is called before the first frame update
        void Start()
        {
            playedMain = false;
            playedSub = false;
            playedMicro = false;
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
            mainUI.SetActive(true);
            subUI.SetActive(false);
            microUI.SetActive(false);
        }

        void ShowSubSponsor()
        {
            mainUI.SetActive(false);
            subUI.SetActive(true);
            microUI.SetActive(false);
        }

        void ShowMicroSponsor()
        {
            mainUI.SetActive(false);
            subUI.SetActive(false);
            microUI.SetActive(true);
        }
        void ShowCredits()
        {
            /*
            credits.gameObject.SetActive(false);
            creditsSpecial.gameObject.SetActive(false);
            */
        }

        void ChooseSponsor()
        {
            // swap between sponsors
            if (mainSponsors.Length >= 1 && playedMain == false)
            {
                // main sponsor ads
                mainSponsorImage.sprite = mainSponsors[currentMain];
                ShowMainSponsor();

                currentMain++;
                if (currentMain >= mainSponsors.Length)
                {
                    currentMain = 0;
                    playedMain = true;
                }
            }
            else if (subSponsors.Length >= 1 && playedSub == false)
            {
                // sub sponsor ads
                subSponsorLeftImage.sprite = subSponsors[currentSub];
                subSponsorRightImage.sprite = subSponsors[currentSub + 1];
                ShowSubSponsor();

                currentSub = currentSub + 2;
                if (currentSub >= subSponsors.Length)
                {
                    currentSub = 0;
                    playedSub = true;
                }
            }
            else if (microSponsors.Length >= 1 && playedMicro == false)
            {
                // micro sponsor ads
                microSponsorImageLL.sprite = microSponsors[currentMicro];
                microSponsorImageLR.sprite = microSponsors[currentMicro + 1];
                microSponsorImageRL.sprite = microSponsors[currentMicro + 2];
                microSponsorImageRR.sprite = microSponsors[currentMicro + 3];
                ShowMicroSponsor();

                currentMicro = currentMicro + 4;
                if (currentMicro >= microSponsors.Length)
                {
                    currentMicro = 0;
                    playedMicro = true;
                }
            }
            else
            {
                playedMain = false;
                playedSub = false;
                playedMicro = false;
                
                // Credits
                Debug.Log("Credits");
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