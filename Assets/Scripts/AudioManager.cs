using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] files;
    public AudioClip[] bounceSounds;

    public AudioClip[] scoreSounds;

    public AudioSource player;

    // Start is called before the first frame update
    void Start()
    {
        //Ball.ballHit += PlayBounce;
        //Ball.score += PlayScore;
    }

    public void PlayBounce(int i)
    {
        if (i < bounceSounds.Length && bounceSounds != null)
        {
            player.PlayOneShot(bounceSounds[i]);
        }
    }

    public void PlayScore(int i)
    {
        if (i < scoreSounds.Length && scoreSounds != null)
        {
            player.PlayOneShot(scoreSounds[i]);
        }
    }
}
