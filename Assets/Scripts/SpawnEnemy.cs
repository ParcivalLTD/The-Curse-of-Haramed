﻿using UnityEngine;
using System.Collections;

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

    private GameManagerBehavior gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;


    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

    }

    void Update()
    {
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
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold + goldperWave);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
                if (gameManager.Wave == 9)
                {
                    monsterIcons[1].SetActive(true);
                }
            }
        }
        else
        {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }


}
