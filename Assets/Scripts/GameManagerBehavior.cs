using UnityEngine;
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

    private float time;
    public Text timeLabel;
    private int killCount;
    public Text killCountLabel;

    public int Health
    {
        get
        {
            return health;
        }
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
                GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
                gameOverText.GetComponent<Animator>().SetBool("gameOver", true);

                GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(4);

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
            waveLabel.text = (wave + 1).ToString();
        }
    }

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldLabel.GetComponent<Text>().text = "$" + gold;
        }
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
        time = 0f;
        UpdateTimeLabel();
        killCount = 0;
        killCountLabel.GetComponent<Text>().text = "0";

        Time.timeScale = 1f;

        Gold = 1000;
        Wave = 0;
        Health = 5;
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
        time += Time.deltaTime;
        UpdateTimeLabel();
        Screen.fullScreen = GameObject.Find("Pause").GetComponent<pausegame>().isFullscreen;

    }

}
