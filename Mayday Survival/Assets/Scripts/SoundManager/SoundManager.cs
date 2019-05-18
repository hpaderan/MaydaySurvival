using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager instance = null;

    public bool isNight;
    public float fadeTime;
    private float fadeT;
    public Sound[] sounds;
    public DualTheme music;
    #region singleton
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        #endregion

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.playOnAwake = s.startOnPlay;
            s.source.loop = s.loop;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        SetUpMusic();

        fadeT = 0;
    }
    

    private void SetUpMusic()
    {
        music.source = gameObject.AddComponent<AudioSource>();
        music.sourceB = gameObject.AddComponent<AudioSource>();

        music.source.clip = music.clip;
        music.sourceB.clip = music.clipB;

        music.source.playOnAwake = music.startOnPlay;
        music.sourceB.playOnAwake = music.startOnPlay;

        music.source.loop = music.loop;
        music.sourceB.loop = music.loop;

        music.maxVolume = music.volume;

        music.source.volume = music.volume;
        music.sourceB.volume = music.volume;

        music.source.pitch = music.pitch;
        music.sourceB.pitch = music.pitch;
    }
    private void Update()
    {
        HandleMusic();
    }
    private void HandleMusic()
    {

        HandleFade();

        //Volumes are always opposite
        float halfPoint = music.maxVolume / 2f;
        music.VolDiff = music.source.volume - halfPoint;
        music.sourceB.volume = halfPoint - music.VolDiff;
    }
    /// <summary>
    /// Handle fade between night and day theme
    /// </summary>
    private void HandleFade()
    {
        if (isNight)
        {
            if (music.source.volume > 0.05)
            {
                fadeT += Time.deltaTime / fadeTime;
                music.source.volume = Mathf.Lerp(music.source.volume, 0, fadeT);
            }
            else
            {
                music.source.volume = 0;
                fadeT = 0;
            }
        }
        else
        {
            if (music.maxVolume - music.source.volume > 0.05)
            {
                fadeT += Time.deltaTime / fadeTime;
                music.source.volume = Mathf.Lerp(music.source.volume, music.maxVolume, fadeT);
            }
            else
            {
                music.source.volume = music.maxVolume;
                fadeT = 0;
            }

        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.delay > 0)
        {
            s.source.PlayDelayed(s.delay);
        }
        else
        {
            s.source.Play();
        }
    }

    public void Play(string name, float delay)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayDelayed(s.delay + delay);
    }
}
