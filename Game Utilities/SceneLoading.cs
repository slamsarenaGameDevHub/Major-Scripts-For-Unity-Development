using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
	[SerializeField] GameObject loadScreen;
	[SerializeField] Slider loadSlider;

    public void LoadScene(string SceneName)
    {
      StartCoroutine(SliderLoadScene(SceneName));
    }
	IEnumerator SliderLoadScene(string sceneName)
	{
		AsyncOperation operation=SceneManager.LoadSceneAsync(sceneName); 
		while(!operation.isDone)
		{
			float progress=Mathf.Clamp01(operation.progress/.9f);
		loadSlider.value=progress;
			yield return null;
		}
	}
}
