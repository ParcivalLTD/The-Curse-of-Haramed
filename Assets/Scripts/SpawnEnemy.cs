using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}

public class SpawnEnemy : MonoBehaviour
{

    public GameObject[] waypoints;
    public GameObject testEnemyPrefab;
    public Wave[] waves;
    public int timeBetweenWaves = 5;
    public int goldperWave = 100;
    public GameObject[] monsterIcons;
    private int monsterCost;

    private GameManagerBehavior gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;


    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

        foreach (GameObject icon in monsterIcons)
        {
            icon.SetActive(false);
        }
    }

    void Update()
    {

        if (gameManager.Wave >= 0)
        {
            monsterIcons[0].SetActive(true);
        }
        if (gameManager.Wave >= 9)
        {
            //GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(7);
            monsterIcons[1].SetActive(true);
        }
        if (gameManager.Wave >= 19)
        {
            //GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(7);
            monsterIcons[2].SetActive(true);
        }
        if (gameManager.Wave >= 29)
        {
            //GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(7);
            monsterIcons[3].SetActive(true);
        }
        if (gameManager.Wave >= 29)
        {
            //GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(7);
            monsterIcons[4].SetActive(true);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            monsterIcons[2].SetActive(true);
            monsterIcons[3].SetActive(true);
            monsterIcons[1].SetActive(true);
            monsterIcons[4].SetActive(true);    
        }

        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) &&
                enemiesSpawned < waves[currentWave].maxEnemies)
            {
                lastSpawnTime = Time.time;
                GameObject newEnemy = (GameObject)
                    Instantiate(waves[currentWave].enemyPrefabs[Random.Range(0, waves[currentWave].enemyPrefabs.Length)]);
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                enemiesSpawned++;
            }
            if (enemiesSpawned == waves[currentWave].maxEnemies &&
                GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gameManager.Wave++;
                goldperWave += 100;

                if(gameManager.goldenHogObtained)
                {
                    gameManager.Gold = Mathf.RoundToInt((gameManager.Gold + goldperWave) * 1.1f);
                } else
                {
                    gameManager.Gold = Mathf.RoundToInt(gameManager.Gold + goldperWave);
                }

                
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
        }
        else
        {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(12);
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }

        foreach (GameObject icon in monsterIcons)
        {
            
            
            if (icon.name == "cat0")
            {
                monsterCost = 200;
            } else if (icon.name == "platapus0")
            {
                monsterCost = 500;
            }
            else if (icon.name == "gorilla0")
            {
                monsterCost = 850;
            }
            else if (icon.name == "frog0")
            {
                monsterCost = 1300;
            }
            else if (icon.name == "magicmirt0")
            {
                monsterCost = 1600;
            }
            else
            {
                monsterCost = 0;
            }

            if (gameManager.Gold < monsterCost)
            {
                SpriteRenderer spriteRenderer = icon.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.gray;
                icon.transform.Find("strikthrough").GetComponent<SpriteRenderer>().gameObject.SetActive(true);

            } else
            {
                SpriteRenderer spriteRenderer = icon.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                icon.transform.Find("strikthrough").GetComponent<SpriteRenderer>().gameObject.SetActive(false);
            }
        }

    }


}
