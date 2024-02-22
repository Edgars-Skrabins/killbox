using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AudioPauseHandler : MonoBehaviour
{
    private AudioSource m_audioSource;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventManager.I.OnGamePaused += PauseAudio;
        EventManager.I.OnGameUnPaused += ResumeAudio;
    }

    private void ResumeAudio()
    {
        m_audioSource.Play();
    }

    private void PauseAudio()
    {
        m_audioSource.Pause();
    }

    private void OnDisable()
    {
        EventManager.I.OnGamePaused -= PauseAudio;
        EventManager.I.OnGameUnPaused -= ResumeAudio;
    }
}
