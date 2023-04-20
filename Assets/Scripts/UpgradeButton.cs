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
        string upgradeInfo = "<b>Damage:</b>\n" + currentLevel.damage + "   ->\n<b>Firerate:</b>\n" + System.Math.Round(1 / currentLevel.fireRate, 2) + "  ->\n";
        string upgradeInfo2 = "\n" + nextLevel.damage + "\n\n" + System.Math.Round(1/nextLevel.fireRate, 2) + "";
        UpgradeData.GetComponent<TextMeshProUGUI>().text = upgradeInfo;
        UpgradeData2.GetComponent<TextMeshProUGUI>().text = upgradeInfo2;
        UpgradeforObj.GetComponent<TextMeshProUGUI>().text = "- " + monster.GetComponent<MonsterData>().GetNextLevel().cost.ToString() + " Gold";

        currentLevel.bullet.GetComponent<BulletBehavior>().setDamage(currentLevel.damage);
        currentLevel.bullet.GetComponent<BulletBehavior>().setSpeed(currentLevel.speed);
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
