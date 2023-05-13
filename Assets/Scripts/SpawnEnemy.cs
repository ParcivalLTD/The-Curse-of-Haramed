using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    private WaveGenerator waveGenerator;
    private List<GameObject> currentWave;
    private int currentIndex;
    public GameObject[] waypoints;
    private float enemySpawnInterval;
    private GameManagerBehavior gameManager;
    public int goldperWave = 100;
    public GameObject[] monsterIcons;
    private int monsterCost;

    private void Start()
    {
        waveGenerator = new WaveGenerator(new List<GameObject>(enemyPrefabs), waypoints);
        StartNextWave();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

        foreach (GameObject icon in monsterIcons)
        {
            icon.SetActive(false);
        }

        enemySpawnInterval = 0.5f;
    }

    private float spawnTimer = 0f;
    private float minSpawnInterval = 0.3f;
    private float maxSpawnInterval = 1.3f;
    private float nextSpawnInterval = 0f;

    private void Update()
    {
        if (gameManager.Wave >= 0)
        {
            monsterIcons[0].SetActive(true);
        }
        if (gameManager.Wave >= 9)
        {
            monsterIcons[1].SetActive(true);
        }
        if (gameManager.Wave >= 19)
        {
            monsterIcons[2].SetActive(true);
        }
        if (gameManager.Wave >= 29)
        {
            monsterIcons[3].SetActive(true);
        }
        if (gameManager.Wave >= 29)
        {
            monsterIcons[4].SetActive(true);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            monsterIcons[2].SetActive(true);
            monsterIcons[3].SetActive(true);
            monsterIcons[1].SetActive(true);
            monsterIcons[4].SetActive(true);
        }

        if ((currentWave == null || currentIndex >= currentWave.Count))
        {
            if (waveGenerator.IsWaitingForWave())
            {
                return;
            }
            else
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies.Length > 0)
                {
                    return;
                }
                gameManager.Wave++;

                if (gameManager.goldenHogObtained)
                {
                    gameManager.Gold = Mathf.RoundToInt((gameManager.Gold + goldperWave) * 1.1f);
                }
                else
                {
                    gameManager.Gold = Mathf.RoundToInt(gameManager.Gold + goldperWave);
                }

                goldperWave += (gameManager.Wave * 2);
                StartNextWave();
            }
        }

        if (spawnTimer <= 0f)
        {
            if (currentIndex < currentWave.Count)
            {
                SpawnEnemyWithInterval(currentWave[currentIndex]);
                currentIndex++;
                nextSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
                spawnTimer = nextSpawnInterval;
            }
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }

        foreach (GameObject icon in monsterIcons)
        {
            if (icon.name == "cat0")
            {
                monsterCost = 200;
            }
            else if (icon.name == "platapus0")
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
            }
            else
            {
                SpriteRenderer spriteRenderer = icon.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
                icon.transform.Find("strikthrough").GetComponent<SpriteRenderer>().gameObject.SetActive(false);
            }
        }
    }


    private void StartNextWave()
    {
        currentWave = waveGenerator.GenerateWave();
        currentIndex = 0;
    }

    private void SpawnEnemyWithInterval(GameObject enemyPrefab)
    {
        enemyPrefab.gameObject.GetComponent<MoveEnemy>().waypoints = waypoints;
        Instantiate(enemyPrefab, waypoints[0].transform.position, Quaternion.identity);

        enemySpawnInterval = Random.Range(5, 6);
    }

}