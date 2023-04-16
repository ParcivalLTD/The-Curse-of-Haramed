using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public GameObject monster;
    private GameManagerBehavior gameManager;
    public GameObject UpgradeforObj;

    void Start()
    {
        monster = transform.parent.parent.parent.gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void Update()
    {
        if (!CanUpgradeMonster(monster))
        {
            this.enabled = false;
        }

        UpgradeforObj.GetComponent<TextMeshProUGUI>().text = "- " + monster.GetComponent<MonsterData>().GetNextLevel().cost.ToString() + " Gold";
    }

    public void OnButtonClick()
    {
        if (CanUpgradeMonster(monster)) { 
            monster.GetComponent<MonsterData>().IncreaseLevel();
            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        }
    }

    private bool CanUpgradeMonster(GameObject monsterToUpgrade)
    {
        MonsterData monsterData = monsterToUpgrade.GetComponent<MonsterData>();
        MonsterLevel nextLevel = monsterData.GetNextLevel();
        if (nextLevel != null)
        {
            return gameManager.Gold >= nextLevel.cost;
        }
        return false;
    }

}
