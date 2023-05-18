using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DanielLochner.Assets.SimpleScrollSnap;

public class startGameScript : MonoBehaviour
{
    public TextMeshProUGUI theText;
    private float currentSliderValue = 0f;
    private float targetSliderValue = 0f;
    private float sliderSpeed = 2f;
    public UnityEngine.UI.Button start;
    public GameObject lockB;

    public string nextSceneName;
    SimpleScrollSnap simpleScrollSnap;
    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneAsync());
        AudioListener audioListener = FindObjectOfType<AudioListener>();
        if (audioListener != null)
        {
            Destroy(audioListener.gameObject);
        }
    }

    private void Start()
    {
        simpleScrollSnap = GameObject.Find("Scroll-Snap").GetComponent<SimpleScrollSnap>();
        PlayerPrefs.SetInt("GameScene", 1);
    }

    public void resetPrefs()
    {
        PlayerPrefs.SetInt("GameScene", 0);
        PlayerPrefs.SetInt("GameScene1", 0);
        PlayerPrefs.SetInt("GameScene2", 0);
        Debug.Log("dsfsdf");
    }

    void Update()
    {
        currentSliderValue = Mathf.Lerp(currentSliderValue, targetSliderValue, Time.deltaTime * sliderSpeed);

        if (SliderHolder.slider != null)
        {
            SliderHolder.slider.value = currentSliderValue;
        }

        if (simpleScrollSnap.SelectedPanel == 0)
        {

            GetComponent<Button>().interactable = true;
            nextSceneName = "GameScene";
            GetComponentInChildren<TextMeshProUGUI>().text = "Start Game";
            GetComponent<Image>().color = Color.white;
            
        }
        if (simpleScrollSnap.SelectedPanel == 1)
        {
            if (PlayerPrefs.GetInt("GameScene") == 1)
            {
                GetComponent<Button>().interactable = true;
                nextSceneName = "GameScene1";
                GetComponentInChildren<TextMeshProUGUI>().text = "Start Game";
                GetComponent<Image>().color = Color.white;
            }
            else
            {
                GetComponent<Button>().interactable = false;
                GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                GetComponent<Image>().color = new Color(171, 22, 22);
            }
        }
        if (simpleScrollSnap.SelectedPanel == 2)
        {
            if (PlayerPrefs.GetInt("GameScene1") == 1)
            {
                GetComponent<Button>().interactable = true;
                nextSceneName = "GameScene2";
                GetComponentInChildren<TextMeshProUGUI>().text = "Start Game";
                GetComponent<Image>().color = Color.white;
            }
            else
            {
                GetComponent<Button>().interactable = false;
                GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                GetComponent<Image>().color = new Color(171, 22, 22);
            }
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
