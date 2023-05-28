using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{
    public Text goldLabel;
    private int gold;
    public Text waveLabel;
    public GameObject[] nextWaveLabels;
    public bool gameOver = false;
    private int wave;
    public Text healthLabel;
    public GameObject[] healthIndicator;
    private int health;
    public bool canvasIsShown = false;
    private static Vector3 savedPosition;
    private int gems;
    public Text gemsLabel;
    public bool goldenHogObtained = false;
    public GameObject tutorial;

    public List<RectTransform> draggablePanels;

    private float time;
    public Text timeLabel;
    private int killCount;
    public Text killCountLabel;

    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public bool gameWon;

    private Vector3 position;

    public GameObject plusAnimationPrefab;

public int Health
{
    get { return health; }
    set
    {
        if (value < health)
        {
            Camera.main.GetComponent<CameraShake>().Shake();
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(2);
        }
        health = value;
        healthLabel.text = "" + health;
        if (health <= 0 && !gameOver)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(0);
        }
        for (int i = 0; i < healthIndicator.Length; i++)
        {
            if (i < Health)
            {
                healthIndicator[i].SetActive(true);
            }
            else
            {
                healthIndicator[i].SetActive(false);
            }
        }
    }
}

public void SavePosition(Vector3 position)
    {
        savedPosition = position;
    }

    public float GetSavedPosition(int x)
    {
        if (x == 0)
        {
            return savedPosition.x;
        }
        else if (x == 1) { return savedPosition.y; }
        else if (x == 2) { return savedPosition.z; }
        else
        {
            return 0;
        }
    }

    public int Wave
    {
        get
        {
            return wave;
        }
        set
        {
            wave = value;
            if (!gameOver)
            {
                for (int i = 0; i < nextWaveLabels.Length; i++)
                {
                    nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                }
            }
            if(wave == PlayerPrefs.GetInt("wave"))
            {
                victoryPanel.transform.Find("thing").gameObject.SetActive(true);
                Time.timeScale = 0;
                gameWon = true;
            }
            waveLabel.text = (wave + 1).ToString();
        }
    }

    public int Gold
    {
        get { return gold; }
        set
        {
            int goldDifference = value - gold;
            float goldMultiplier = PlayerPrefs.GetFloat("GoldMultiplier", 1f);
            if (goldDifference > 0)
            {
                goldDifference = Mathf.RoundToInt(goldDifference * goldMultiplier);
            }
            gold = value;
            goldLabel.GetComponent<Text>().text = "$" + gold;
            if (goldDifference > 100 && goldDifference != 500)
            {
                plusAnimationPrefab.GetComponent<Text>().text = "+ $" + goldDifference.ToString();
                StartCoroutine(DisplayGoldDifference(true));
            }
            if (goldDifference < 0)
            {
                plusAnimationPrefab.GetComponent<Text>().text = "- $" + Mathf.Abs(goldDifference).ToString();
                StartCoroutine(DisplayGoldDifference(false));
            }
        }
    }

    private IEnumerator DisplayGoldDifference(bool isPos)
    {
        if (!isPos)
        {
            plusAnimationPrefab.GetComponent<Text>().color =  new Color32(188, 79, 82, 255);
        }
        else
        {
            plusAnimationPrefab.GetComponent<Text>().color = new Color32(173, 158, 89, 255);
        }
        plusAnimationPrefab.transform.position = position;
        float startY = plusAnimationPrefab.transform.position.y;
        float endY = startY + 10f;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            float newY = Mathf.Lerp(startY, endY, elapsedTime / 1f);
            plusAnimationPrefab.transform.position = new Vector3(plusAnimationPrefab.transform.position.x, newY, plusAnimationPrefab.transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        plusAnimationPrefab.GetComponent<Text>().text = "";
        plusAnimationPrefab.transform.position = position;
    }


    public int Gems
    {
        get
        {
            return gems;
        }
        set
        {
            gems = value;
            gemsLabel.GetComponent<Text>().text = gems.ToString();
        }
    }

    private void UpdateTimeLabel()
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeLabel.GetComponent<Text>().text = timeText;
    }

    public void UpdateKillCount()
    {
        killCount++;
        killCountLabel.GetComponent<Text>().text = killCount.ToString();
    }


    void Start()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            tutorial.SetActive(true);
        }

        time = 0f;
        UpdateTimeLabel();
        killCount = 0;
        killCountLabel.GetComponent<Text>().text = "0";

        position = plusAnimationPrefab.transform.position;

        Time.timeScale = 1f;

        Gold = 750;
        Wave = 0;
        int difficulty = PlayerPrefs.GetInt("difficulty");
        if (difficulty == 0)
        {
            Health = 10;
        } else if (difficulty == 1)
        {
            Health = 5;
        } else if (difficulty == 2)
        {
            Health = 3;
        }
        Gems = 0;
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject monster in monsters)
        {
            if (monster.GetComponent<PlaceMonster>().getCanvas() == true)
            {
                foreach (GameObject monster2 in monsters)
                {
                    monster2.GetComponent<PlaceMonster>().HideCanvas();
                }
                monster.GetComponent<PlaceMonster>().ShowCanvas();
                break;
            }
        }

        GameObject[] canvases = GameObject.FindGameObjectsWithTag("CanvasPre");
    }

    public void ShowCanvas()
    {
        canvasIsShown = true;
    }

    public void HideCanvas()
    {
        canvasIsShown = false;
    }

    public bool getCanvas()
    {
        return canvasIsShown;
    }

    void Update()
    {

        if (GameObject.Find("Speed").GetComponent<speedScript>().isTwoXSpeed)
        {
            time += Time.deltaTime / 2f;
        }
        else
        {
            time += Time.deltaTime;
        }

        UpdateTimeLabel();
        Screen.fullScreen = GameObject.Find("Pause").GetComponent<pausegame>().isFullscreen;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameObject.Find("Pause").GetComponent<pausegame>().isPaused)
            {
                GameObject.Find("Pause").GetComponent<pausegame>().TogglePause();
            }
            else
            {
                GameObject.Find("Pause").GetComponent<pausegame>().TogglePause();
            }
        }

        if (Input.GetKeyDown(KeyCode.F11)){
            GameObject.Find("Pause").GetComponent<pausegame>().ToggleFullscreen();
        }
    }

}
