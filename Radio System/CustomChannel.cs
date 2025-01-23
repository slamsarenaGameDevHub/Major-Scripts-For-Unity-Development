using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RadioSystem))]
public class CustomChannel : Editor
{
   public override void OnInspectorGUI()
   {
   	base.OnInspectorGUI();
	   RadioSystem radio=(RadioSystem)target;
	   if(GUILayout.Button("Update Channels"))
	   {
	   	radio.SetupChannels();
	   }
   }
}
