using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject[] tutorialObjects;
    private int currentObjectIndex = 0;
    private bool isFirstLoad;
    private bool firstDings = true;
    void Awake()
    {
        isFirstLoad = PlayerPrefs.GetInt("isFirstLoad", 1) == 1;
        if (isFirstLoad && SceneManager.GetActiveScene().name == "GameScene")
        {
            ShowTutorial();
        }
    }

    void Update()
    {
        if(firstDings && SceneManager.GetActiveScene().name == "GameScene")
        {
            Time.timeScale = 0;
        }
    }

    public void ButtonPress()
    {
        if(currentObjectIndex < tutorialObjects.Length)
        {
            tutorialObjects[currentObjectIndex].SetActive(false);
            currentObjectIndex++;
            if (currentObjectIndex < tutorialObjects.Length)
            {
                tutorialObjects[currentObjectIndex].SetActive(true);
                Time.timeScale = 0;
                firstDings = false;
                GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(0);
            }
            else
            {
                Time.timeScale = 1;
                GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(0);
            }
        }
    }

    void ShowTutorial()
    {
        Time.timeScale = 0;
        foreach (GameObject obj in tutorialObjects)
        {
            obj.SetActive(false);
        }
        tutorialObjects[0].SetActive(true);
        currentObjectIndex = 0;
        PlayerPrefs.SetInt("isFirstLoad", 0);
    }
}