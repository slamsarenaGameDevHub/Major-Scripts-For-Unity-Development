using System.Collections.Generic;
using UnityEngine;

public class RadioSystem : MonoBehaviour
{
   public  List<Station> Stations;
   [SerializeField] Channel channelPrefab;
   private List<Channel> channels;
   
   public void SetupChannels()
   {
   	channels=new List<Channel>();
	   foreach(Station s in Stations)
	   {
	   Channel channel=Instantiate(channelPrefab, transform);
		   channel.SetupChannel(s.ChannelName,s.ChannelFrequency,s.ChannelClip);
	   }
	   channels.Add(channel);
   }
}
[System.Serializable]
public class Station
{
	public string ChannelName;
	public float ChannelFrequency;
	public AudioClip ChannelClip;
} 
