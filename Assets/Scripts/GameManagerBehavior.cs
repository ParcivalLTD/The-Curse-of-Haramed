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
            }
            health = value;
            healthLabel.text = "" + health;

            if (health <= 0 && !gameOver)
            {
                gameOver = true;
                GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
                gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
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
            if (goldenHogObtained && value > 0)
            {
                value = (int)(value * 1.1f);
            }
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

    void Start()
    {

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
    }

}
