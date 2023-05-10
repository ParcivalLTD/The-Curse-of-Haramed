using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class startGameScript : MonoBehaviour
{
    public TextMeshProUGUI theText;
    private float currentSliderValue = 0f;
    private float targetSliderValue = 0f;
    private float sliderSpeed = 5f; // Adjust the speed of the slider movement here

    public string nextSceneName;

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneAsync());
        AudioListener audioListener = FindObjectOfType<AudioListener>();
        if (audioListener != null)
        {
            Destroy(audioListener.gameObject);
        }
    }

    void Update()
    {
        currentSliderValue = Mathf.Lerp(currentSliderValue, targetSliderValue, Time.deltaTime * sliderSpeed);

        if (SliderHolder.slider != null)
        {
            SliderHolder.slider.value = currentSliderValue;
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }


    IEnumerator LoadSceneAsync()
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);

        while (!loadingOperation.isDone)
        {
            targetSliderValue = loadingOperation.progress;
            yield return null;
        }


        AsyncOperation sceneOperation = SceneManager.LoadSceneAsync(nextSceneName);

        while (!sceneOperation.isDone)
        {
            if (SliderHolder.slider != null)
            {
                SliderHolder.slider.value = sceneOperation.progress;
            }
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextSceneName));
        SceneManager.UnloadSceneAsync("LoadingScene");
    }
}
