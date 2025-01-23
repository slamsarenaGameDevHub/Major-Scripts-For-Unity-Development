using System;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixer mixer;

   
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.Source=gameObject.AddComponent<AudioSource>();
            s.Source.clip=s.clip;
            s.Source.loop=s.isLoop;
            s.Source.outputAudioMixerGroup = s.mixer;
            s.Source.pitch = s.Pitch;
            s.Source.volume = s.Volume;
            s.Source.bypassReverbZones = s.byPassEffect;
            
           
        }
        mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX Volume"));
        mixer.SetFloat("Music", PlayerPrefs.GetFloat("Music Volume"));
        mixer.SetFloat("Master", PlayerPrefs.GetFloat("Master Volume"));
    }


    public void PlaySound(string Name)
    {
        Sound s=Array.Find(sounds,Sound=>Sound.ClipName==Name);
        s.Source.Play();
    }
}
