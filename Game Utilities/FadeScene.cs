using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FadeScene : MonoBehaviour
{
	string sceneToLoad;
	[SerializeField]Animator sceneFadeAnimator;
	public void LoadScene(string sceneName)
	{
		sceneToLoad=sceneName;
		sceneFadeAnimator.SetTrigger("Load Scene");
	}
	public void FadeToScene()
	{
		SceneManager.LoadScene(sceneToLoad);
	}
   
}
