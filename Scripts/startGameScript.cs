using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DanielLochner.Assets.SimpleScrollSnap;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

public class startGameScript : MonoBehaviour
{
    public TextMeshProUGUI theText;
    private float currentSliderValue = 0f;
    private float targetSliderValue = 0f;
    private float sliderSpeed = 2f;
    public UnityEngine.UI.Button start;
    public GameObject namePanel;

    public GameObject[] panels;

    private int frameCount = 0;

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
    }

    public void resetPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("difficulty", 0);
        namePanel.SetActive(true);
    }

    public void unlockAllLevels()
    {
        PlayerPrefs.SetInt("GameScene", 1);
        PlayerPrefs.SetInt("GameScene1", 1);
        PlayerPrefs.SetInt("GameScene2", 1);
    }

    void Update()
    {
        if(PlayerPrefs.GetString("playerName") == "")
        {

        }

        currentSliderValue = Mathf.Lerp(currentSliderValue, targetSliderValue, Time.deltaTime * sliderSpeed);

        if (SliderHolder.slider != null)
        {
            SliderHolder.slider.value = currentSliderValue;
        }

        if (frameCount < 30)
        {
            Debug.Log(PlayerPrefs.GetInt("currentPanel"));
            simpleScrollSnap.GoToPanel(PlayerPrefs.GetInt("currentPanel"));

            frameCount++;
        } else
        {
            if (simpleScrollSnap.SelectedPanel == 0)
            {
                GetComponent<Button>().interactable = true;
                nextSceneName = "GameScene";
                GetComponentInChildren<TextMeshProUGUI>().text = "Start Game";
                GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                GetComponent<Image>().color = Color.white;
                panels[0].GetComponent<unlockPanelScrollSnap>().unlockPanel();
                PlayerPrefs.SetInt("currentPanel", simpleScrollSnap.SelectedPanel);
            }
            if (simpleScrollSnap.SelectedPanel == 1)
            {
                if (PlayerPrefs.GetInt("GameScene") == 1)
                {
                    GetComponent<Button>().interactable = true;
                    nextSceneName = "GameScene1";
                    GetComponentInChildren<TextMeshProUGUI>().text = "Start Game";
                    GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                    GetComponent<Image>().color = Color.white;
                    panels[1].GetComponent<unlockPanelScrollSnap>().unlockPanel();
                    PlayerPrefs.SetInt("currentPanel", simpleScrollSnap.SelectedPanel);
                }
                else
                {
                    GetComponent<Button>().interactable = false;
                    GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                    GetComponent<Image>().color = new Color(171, 22, 22);
                    GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                    panels[1].GetComponent<unlockPanelScrollSnap>().lockPanel();
                    PlayerPrefs.SetInt("currentPanel", simpleScrollSnap.SelectedPanel);
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
                    GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                    panels[2].GetComponent<unlockPanelScrollSnap>().unlockPanel();
                    PlayerPrefs.SetInt("currentPanel", simpleScrollSnap.SelectedPanel);
                }
                else
                {
                    GetComponent<Button>().interactable = false;
                    GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
                    GetComponent<Image>().color = new Color(171, 22, 22);
                    GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                    panels[2].GetComponent<unlockPanelScrollSnap>().lockPanel();
                    PlayerPrefs.SetInt("currentPanel", simpleScrollSnap.SelectedPanel);
                }
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
