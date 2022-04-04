using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audio;
    [SerializeField] private AudioSource audioSource;
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void audioPlay(string soundname)
    {
        switch (soundname)
        {
            case "jump":
                audioSource.PlayOneShot(audio[0]);
                audioSource.Play();
                break;
            case "powerup":
                audioSource.PlayOneShot(audio[1]);
                break;
            case "score":
                audioSource.PlayOneShot(audio[2]);
                break;
            case "dead":
                audioSource.PlayOneShot(audio[3]);
                break;
            case "hit":
                audioSource.PlayOneShot(audio[4]);
                break;
            case "giant":
                audioSource.PlayOneShot(audio[5]);
                break;
            case "btn_click":
                audioSource.PlayOneShot(audio[6]);
                break;
            case "destroy":
                audioSource.PlayOneShot(audio[7]);
                break;

        }
        
    }

}
