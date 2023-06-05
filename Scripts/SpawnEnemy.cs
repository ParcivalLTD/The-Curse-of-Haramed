using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    private WaveGenerator waveGenerator;
    private List<GameObject> currentWave;
    private int currentIndex;
    public GameObject[] waypoints;
    private float enemySpawnInterval;
    private GameManagerBehavior gameManager;
    public int goldperWave = 100;
    public GameObject[] monsterIcons;
    private int monsterCost;
    public GameObject catPanel;
    public GameObject platapusPanel;
    public GameObject gorillaPanel;
    public GameObject frogPanel;
    public GameObject magicPanel;

    public SpriteRenderer[] notUnlockedSprites;

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

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (gameManager.Wave >= 0 && !catPanel.GetComponent<catPanel>().hasBeenUnlocked)
        {
            monsterIcons[0].SetActive(true);
            //catPanel.GetComponent<catPanel>().ShowPanel();
            catPanel.GetComponent<catPanel>().hasBeenUnlocked = true;
            notUnlockedSprites[0].sprite = null;
        }
        if (gameManager.Wave >= 9 && !platapusPanel.GetComponent<platapusPanel>().hasBeenUnlocked)
        {
            monsterIcons[1].SetActive(true);
            platapusPanel.GetComponent<platapusPanel>().ShowPanel();
            platapusPanel.GetComponent<platapusPanel>().hasBeenUnlocked = true;
            notUnlockedSprites[1].sprite = null;
        }
        if (gameManager.Wave >= 19 && !gorillaPanel.GetComponent<gorillaPanel>().hasBeenUnlocked)
        {
            monsterIcons[2].SetActive(true);
            gorillaPanel.GetComponent<gorillaPanel>().ShowPanel();
            gorillaPanel.GetComponent<gorillaPanel>().hasBeenUnlocked = true;
            notUnlockedSprites[2].sprite = null;
        }
        if (gameManager.Wave >= 29 && !frogPanel.GetComponent<frogPanel>().hasBeenUnlocked)
        {
            monsterIcons[3].SetActive(true);
            frogPanel.GetComponent<frogPanel>().ShowPanel();
            frogPanel.GetComponent<frogPanel>().hasBeenUnlocked = true;
            notUnlockedSprites[3].sprite = null;
        }
        if (gameManager.Wave >= 39 && !magicPanel.GetComponent<magigMirtPanel>().hasBeenUnlocked)
        {
            monsterIcons[4].SetActive(true);
            magicPanel.GetComponent<magigMirtPanel>().ShowPanel();
            magicPanel.GetComponent<magigMirtPanel>().hasBeenUnlocked = true;
            notUnlockedSprites[4].sprite = null;
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            monsterIcons[2].SetActive(true);
            monsterIcons[3].SetActive(true);
            monsterIcons[1].SetActive(true);
            monsterIcons[4].SetActive(true);
            platapusPanel.GetComponent<platapusPanel>().hasBeenUnlocked = true;
            notUnlockedSprites[1].sprite = null;
            gorillaPanel.GetComponent<gorillaPanel>().hasBeenUnlocked = true;
            notUnlockedSprites[2].sprite = null;
            frogPanel.GetComponent<frogPanel>().hasBeenUnlocked = true;
            notUnlockedSprites[3].sprite = null;
            magicPanel.GetComponent<magigMirtPanel>().hasBeenUnlocked = true;
            notUnlockedSprites[4].sprite = null;
            magicPanel.GetComponent<magigMirtPanel>().ShowPanel();
            frogPanel.GetComponent<frogPanel>().ShowPanel();
            gorillaPanel.GetComponent<gorillaPanel>().ShowPanel();
            platapusPanel.GetComponent<platapusPanel>().ShowPanel();
            
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
                monsterCost = 10000;
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
        if(!gameManager.gameOver)
        {
            Instantiate(enemyPrefab, waypoints[0].transform.position, Quaternion.identity);
        }
        enemySpawnInterval = Random.Range(5, 6);
    }

    private IEnumerator WaitAndDoSomething()
    {
        yield return new WaitForSeconds(5f);
    }
}