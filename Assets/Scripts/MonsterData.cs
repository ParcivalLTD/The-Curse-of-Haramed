using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MonsterLevel
{
    public int cost;
    public GameObject visualization;
    public GameObject bullet;
    public float fireRate;
    public int damage;
    public int speed;
    public float scale = 1f;
    public int Level;

    void Start()
    {
        BulletBehavior bulletC = bullet.GetComponent<BulletBehavior>();
        bulletC.damage = damage;
        bulletC.speed = speed;
        bulletC.transform.localScale = new Vector3(scale, scale, scale);
    }
}

public class MonsterData : MonoBehaviour
{

    public List<MonsterLevel> levels;
    private MonsterLevel currentLevel;
    public string nameOfMonster;

    void Start()
    {

    }

    void Update()
    {
    }

    public string getNameOfMonster()
    {
        return nameOfMonster;
    }

    public MonsterLevel CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel);

            GameObject levelVisualization = levels[currentLevelIndex].visualization;
            for (int i = 0; i < levels.Count; i++)
            {
                if (levelVisualization != null)
                {
                    if (i == currentLevelIndex)
                    {
                        levels[i].visualization.SetActive(true);
                    }
                    else
                    {
                        levels[i].visualization.SetActive(false);
                    }
                }
            }
        }
    }

    void OnEnable()
    {
        CurrentLevel = levels[0];
    }

    public MonsterLevel GetNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public void IncreaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1)
        {
            CurrentLevel = levels[currentLevelIndex + 1];
        }
    }

    public int GetTotalCost()
    {
        int totalCost = 0;
        foreach (MonsterLevel level in levels)
        {
            totalCost += level.cost;
        }
        return totalCost;
    }


}
