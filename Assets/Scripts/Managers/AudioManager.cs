using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I {get; private set;}

    public string Master_Volume = "MasterVolume";
    public string Music_Volume = "MusicVolume";
    public string SFX_Volume = "SFXVolume";
    public AudioMixer Mixer;

    public Sound[] sounds;

    private void Awake()
    {
        #region Singleton

        if (I != this && I != null)
        {
            Destroy(this);
        }
        else if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.MixerGroup;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }

        #endregion
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }

    public bool Playing(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return false;
        }
        return s.source.isPlaying;
    }

    public void PauseAllSounds()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name != "BGM_Game")
            {
                sound.source.Pause();
            }
        }
    }

    public void UnPauseAllSounds()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.UnPause();
        }
    }
}
