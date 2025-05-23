using UnityEngine;
using System;
using UnityEngine.Audio;

[Serializable]
public class Sound 
{
    [HideInInspector]
    public AudioSource Source;
    public AudioClip clip;
    public string ClipName;
    public bool isLoop;
    [Range(0, 1)] public float Volume;
    [Range(-3, 3)] public float Pitch;
    public AudioMixerGroup mixer;
    public bool byPassEffect = false;
}
