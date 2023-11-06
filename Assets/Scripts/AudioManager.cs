using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource powerUpAudio;

    private void Awake()
    {
        // if the instance exist destroy it
        if (instance != null)
            Destroy(this.gameObject);
        else
        {
            // asing the instance static to this object
            instance = this;
            //mantain it to next scene
            DontDestroyOnLoad(this);
        }
        
    }

    public void PlayPowerUpSound()
    {
        powerUpAudio.Play();
    }
}
