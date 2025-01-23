using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Channel : MonoBehaviour
{
   public string ChannelName;
   public float channelFrequency;
   public AudioSource channelSource;
   
   public void SetupChannel(string cName,float cFreq,AudioClip cClip )
   {
   	ChannelName=cName;
	   channelFrequency=cFreq;
	   channelSource.clip=cClip;
   }
}
