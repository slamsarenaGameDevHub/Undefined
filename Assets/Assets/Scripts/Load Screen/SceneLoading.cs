using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoading : MonoBehaviour
{
	[SerializeField] GameObject loadScreen;
	[SerializeField] Slider loadSlider;
    [SerializeField] Gradient sliderGradient;
	[SerializeField] Image sliderFill;


	
	void Start()
	{
		loadScreen.SetActive(false);
	}
    public void LoadScene(string SceneName)
    {
		loadScreen.SetActive(true);
      StartCoroutine(SliderLoadScene(SceneName));
    }
	IEnumerator SliderLoadScene(string sceneName)
	{
		AsyncOperation operation=SceneManager.LoadSceneAsync(sceneName); 
		while(!operation.isDone)
		{
			sliderFill.color=sliderGradient.Evaluate(loadSlider.normalizedValue);
			float progress=Mathf.Clamp01(operation.progress/.9f);
			loadSlider.value=progress;
			yield return null;
		}
	}
	
}
