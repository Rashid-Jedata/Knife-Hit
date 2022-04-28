using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

enum AudioClips
{
    Crash, Hit, Shoot, TargetDestroyed
}

public class AudioManager : MonoBehaviour
{
    internal static AudioManager instance;

    AudioSource audioSource;

    [SerializeField]
    List<AudioClip> Clips;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        ChangeMusicVolume();
        ChangeBackGroundMusicVolume();
    }

    private void ChangeBackGroundMusicVolume()
    {
        audioSource.volume = ConfigurationData.BackGroundMusicVolume;
        print(ConfigurationData.MusicVolume);
    }

    internal void PlayerClip(AudioClips Name)
    {
        audioSource.PlayOneShot(Clips[(int)Name]);
    }

    internal void ChangeMusicVolume()
    {
        audioSource.volume = ConfigurationData.MusicVolume;
        print(ConfigurationData.MusicVolume);
    }

}
