using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AmbienceController : MonoBehaviour
{
	public static event Action TriggerAmbience;
	
	
	[Header("Ambient Sounds")]
	[SerializeField] List<AudioClip> ambientClips= new List<AudioClip>();
	int clipToPlay;
	AudioSource ambienceSource;
	
	public static void OnEventOccured()
	{
		TriggerAmbience?.Invoke();
	}
	void OnEnable()
	{
		TriggerAmbience+=PlayAmbience;
		ambienceSource=GetComponent<AudioSource>();
	}
	void OnDisable()
	{
		TriggerAmbience-=PlayAmbience;
	}

	void PlayAmbience()
	{
		ambienceSource.Stop(); 
		clipToPlay=UnityEngine.Random.Range(0,ambientClips.Count);
		ambienceSource.clip=ambientClips[clipToPlay];
		ambienceSource.Play();
	}
   	
    
}
