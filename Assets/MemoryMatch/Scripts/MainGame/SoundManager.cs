using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("==========SOURCE==========")]
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioClip _bgMusic;
    [Header("===========SFX===========")]
    [SerializeField] private AudioSource _FX;
    [SerializeField] private AudioClip[] _clips;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _backgroundMusic.clip = _bgMusic;
        _backgroundMusic.Play();
    }
    public void PlayAudioClip(int index)
    {
        if (_FX.isPlaying && index == 6)
            return;
        _FX.clip = _clips[index];
        _FX.Play();
    }
}