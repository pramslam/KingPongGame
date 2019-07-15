using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO
{
    public class BackgroundVideo : MonoBehaviour
    {
        public UnityEngine.Video.VideoClip[] videoClip;

        private UnityEngine.Video.VideoPlayer videoPlayer;
        private UnityEngine.Video.VideoPlayer vp;
        private int currentVideo = 0;

        // Start is called before the first frame update
        void Start()
        {
            // Setup gameobject
            videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();

            // Video settings
            videoPlayer.waitForFirstFrame = true;
            videoPlayer.playOnAwake = false;
            videoPlayer.clip = videoClip[currentVideo];
            videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
            videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
            videoPlayer.targetMaterialProperty = "_MainTex";
            videoPlayer.isLooping = true;

            videoPlayer.Play();                             // Play first video
        }

        // Public functions
        #region
        public void ChangeBackground()
        {
            currentVideo += 1;

            // Cycle back to first video
            if (currentVideo >= videoClip.Length) { currentVideo = 0; }

            videoPlayer.clip = videoClip[currentVideo];     // Change background
            videoPlayer.Play();                             // Play Video
        }

        public int GetCurrentBackground() { return currentVideo; }
        #endregion
    }
}
