using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public GameObject monster;
    private GameManagerBehavior gameManager;
    public GameObject UpgradeforObj;
    public GameObject UpgradeData;
    public GameObject UpgradeData2;

    void Start()
    {
        monster = transform.parent.parent.parent.gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void Update()
    {
        if (!CanUpgradeMonster(monster))
        {
            this.GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }

        MonsterData monsterData = monster.GetComponent<MonsterData>();

        MonsterLevel currentLevel = monsterData.CurrentLevel;
        MonsterLevel nextLevel = monsterData.GetNextLevel();
        string upgradeInfo = "Damage: \n" + currentLevel.damage + " \nSpeed: ->\n" + currentLevel.speed + "   ->\nFirerate:\n" + currentLevel.fireRate + "  ->\n";
        string upgradeInfo2 = "\n" + nextLevel.damage + "\n\n" + nextLevel.speed + "\n\n" + nextLevel.fireRate + "";
        UpgradeData.GetComponent<TextMeshProUGUI>().text = upgradeInfo;
        UpgradeData2.GetComponent<TextMeshProUGUI>().text = upgradeInfo2;

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
