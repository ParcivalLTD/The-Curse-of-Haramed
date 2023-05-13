using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveGenerator
{
    private List<GameObject> enemyPrefabs;
    private GameObject[] waypoints;
    private int maxEnemiesPerWave;
    private int waveNumber;
    private float spawnInterval;
    private float spawnTimer;
    private float waveDelay;
    private bool isWaitingForWave;
    public GameManagerBehavior gameManager;

    public WaveGenerator(List<GameObject> enemyPrefabs, GameObject[] waypoints)
    {
        this.enemyPrefabs = enemyPrefabs;
        this.waypoints = waypoints;
        maxEnemiesPerWave = 10;
        waveNumber = 0;
        spawnInterval = 0.5f;
        spawnTimer = 1f;
        waveDelay = 5f;
        isWaitingForWave = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    public List<GameObject> GenerateWave()
    {
        waveNumber++;
        int enemyCount = Mathf.Min(waveNumber, maxEnemiesPerWave);

        List<GameObject> wave = new List<GameObject>();

        List<int> spawnZiffern = GetSpawnZiffern(waveNumber);

        for (int i = 0; i < spawnZiffern.Count; i++)
        {
            int spawnZiffer = spawnZiffern[i];
            if (spawnZiffer > 0)
            {
                int enemyIndex = Mathf.Clamp(spawnZiffer - 1, 0, enemyPrefabs.Count - 1);
                GameObject enemyPrefab = enemyPrefabs[enemyIndex];
                wave.Add(enemyPrefab);
            }
        }

        maxEnemiesPerWave += 1;
        spawnInterval *= 0.95f;
        waveDelay += 1f;

        isWaitingForWave = true;
        CoroutineHelper.Instance.StartCoroutine(StartNextWaveDelayed());
        string output = "";
        for (int i = 0; i < spawnZiffern.Count(); i++)
        {
            output += spawnZiffern[i] + "-";
        }
        Debug.Log("Wave " + waveNumber + ": " + output);
        return wave;
    }

    private List<int> GetSpawnZiffern(int waveNumber)
    {
        List<int> spawnZiffern = new List<int>();

        int maxZiffernSum = Mathf.Min(waveNumber, maxEnemiesPerWave);
        int ziffernSum = 0;

        while (ziffernSum < maxZiffernSum)
        {
            int remainingZiffern = maxZiffernSum - ziffernSum;
            int maxSegmentSize = Mathf.Min(remainingZiffern, 15);
            int segmentSize = Random.Range(1, maxSegmentSize + 1);
            spawnZiffern.Add(segmentSize);
            ziffernSum += segmentSize;
        }

        return spawnZiffern;
    }

    private IEnumerator StartNextWaveDelayed()
    {
        yield return new WaitForSeconds(5f);

        isWaitingForWave = false;
    }

    public bool IsWaitingForWave()
    {
        return isWaitingForWave;
    }
}