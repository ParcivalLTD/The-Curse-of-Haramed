using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Data.Common;

public class UpgradeButton : MonoBehaviour
{
    public GameObject monster;
    private GameManagerBehavior gameManager;

    public GameObject damageButton;
    public GameObject fireRateButton;
    public GameObject radiusButton;

    public GameObject damageUpgradeData;
    public GameObject fireRateUpgradeData;
    public GameObject radiusUpgradeData;

    public TextMeshProUGUI damageUpgradeFor;
    public TextMeshProUGUI fireRateUpgradeFor;
    public TextMeshProUGUI radiusUpgradeFor;

    public int damageCost;
    public int fireRateCost;
    public int radiusCost;

    public int damageIncrease = 7;

    public int damageLevel = 1;
    public int fireRateLevel = 1;
    public int radiusLevel = 1;

    public float radius;

    public float fireRateMultiplier = 0.83f;
    public float radiusMultiplier = 0.1f;
    public int damageMultiplier = 7;


    void Start()
    {
        monster = transform.parent.parent.parent.gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

        damageCost = monster.GetComponent<MonsterData>().levels[0].cost;
        damageUpgradeData.GetComponent<TextMeshProUGUI>().text = "<b>Damage (" + damageLevel + "): </b>\n\n" + monster.GetComponent<MonsterData>().CurrentLevel.damage + " to " + (monster.GetComponent<MonsterData>().CurrentLevel.damage + damageIncrease);
        damageUpgradeFor.GetComponent<TextMeshProUGUI>().text = "- $" + damageCost;
        monster.GetComponent<MonsterData>().CurrentLevel.bullet.GetComponent<BulletBehavior>().damage = monster.GetComponent<MonsterData>().levels[0].damage;

        fireRateCost = monster.GetComponent<MonsterData>().levels[0].cost;
        fireRateUpgradeData.GetComponent<TextMeshProUGUI>().text = "<b>Fire Rate (" + fireRateLevel + "): </b>\n\n" + Math.Round((1 / (monster.GetComponent<MonsterData>().CurrentLevel.fireRate)), 1) * 10 + " to " + Math.Round((1 / (monster.GetComponent<MonsterData>().CurrentLevel.fireRate * 0.83f))*10, 1);
        fireRateUpgradeFor.GetComponent<TextMeshProUGUI>().text = "- $" + fireRateCost;
        monster.GetComponent<MonsterData>().CurrentLevel.fireRate = monster.GetComponent<MonsterData>().levels[0].fireRate;

        radius = monster.GetComponent<CircleCollider2D>().radius;
        radiusCost = monster.GetComponent<MonsterData>().levels[0].cost;
        radiusUpgradeData.GetComponent<TextMeshProUGUI>().text = "<b>Radius (" + radiusLevel + "): </b>\n\n" + radius * 10 + " to " + (radius + 0.1f)*10;
        radiusUpgradeFor.GetComponent<TextMeshProUGUI>().text = "- $" + radiusCost;

    }

    public void damageUpgrade()
    {
        if (gameManager.Gold >= damageCost)
        {
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(0);

            damageLevel++;

            gameManager.Gold -= damageCost;
            monster.GetComponent<MonsterData>().CurrentLevel.damage += damageIncrease;
            monster.GetComponent<MonsterData>().CurrentLevel.bullet.GetComponent<BulletBehavior>().damage += damageIncrease;
            damageCost = Mathf.RoundToInt(damageCost * 2.1f);
            monster.GetComponent<MonsterData>().setTotalCost(damageCost);
            damageUpgradeData.GetComponent<TextMeshProUGUI>().text = "<b>Damage (" + damageLevel + "): </b>\n\n" + monster.GetComponent<MonsterData>().CurrentLevel.damage + " to " + (monster.GetComponent<MonsterData>().CurrentLevel.damage + damageIncrease);
            damageUpgradeFor.GetComponent<TextMeshProUGUI>().text = "- $" + damageCost;

            monster.GetComponent<MonsterData>().GetNextLevel().damage = monster.GetComponent<MonsterData>().CurrentLevel.damage;
            monster.GetComponent<MonsterData>().GetNextLevel().bullet.GetComponent<BulletBehavior>().damage = monster.GetComponent<MonsterData>().CurrentLevel.bullet.GetComponent<BulletBehavior>().damage;
            monster.GetComponent<MonsterData>().GetNextLevel().cost = damageCost;
            monster.GetComponent<MonsterData>().GetNextLevel().fireRate = monster.GetComponent<MonsterData>().CurrentLevel.fireRate;
            monster.GetComponent<MonsterData>().IncreaseLevel();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(10);
        }
    }

    public void fireRateUpgrade()
    {
        if (gameManager.Gold >= fireRateCost)
        {
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(0);

            fireRateLevel++;
            gameManager.Gold -= fireRateCost;
            monster.GetComponent<MonsterData>().CurrentLevel.fireRate *= 0.83f;
            fireRateCost = Mathf.RoundToInt(fireRateCost * 2.3f);
            fireRateUpgradeData.GetComponent<TextMeshProUGUI>().text = "<b>Fire Rate (" + fireRateLevel + "): </b>\n\n" + Math.Round(1 / (monster.GetComponent<MonsterData>().CurrentLevel.fireRate), 1) * 10 + " to " + Math.Round(1 / (monster.GetComponent<MonsterData>().CurrentLevel.fireRate * 0.83f), 1) * 10;
            fireRateUpgradeFor.GetComponent<TextMeshProUGUI>().text = "- $" + fireRateCost;
            monster.GetComponent<MonsterData>().setTotalCost(damageCost);

            monster.GetComponent<MonsterData>().GetNextLevel().damage = monster.GetComponent<MonsterData>().CurrentLevel.damage;
            monster.GetComponent<MonsterData>().GetNextLevel().cost = fireRateCost;
            monster.GetComponent<MonsterData>().GetNextLevel().fireRate = monster.GetComponent<MonsterData>().CurrentLevel.fireRate;
            monster.GetComponent<MonsterData>().IncreaseLevel();
        } else
        {
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(10);
        }
    }

    public void radiusUpgrade()
    {
        if (gameManager.Gold >= radiusCost)
        {
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(0);

            radiusLevel++;
            gameManager.Gold -= radiusCost;
            monster.GetComponent<CircleCollider2D>().radius += 0.32f;
            radiusCost = Mathf.RoundToInt(radiusCost * 1.9f);
            radiusUpgradeData.GetComponent<TextMeshProUGUI>().text = "<b>Radius (" + radiusLevel + "): </b>\n\n" + Math.Round(monster.GetComponent<CircleCollider2D>().radius, 1) * 10 + " to " + Math.Round(monster.GetComponent<CircleCollider2D>().radius + 0.32f, 1) * 10;
            radiusUpgradeFor.GetComponent<TextMeshProUGUI>().text = "- $" + radiusCost;
            monster.GetComponent<MonsterData>().GetNextLevel().damage = monster.GetComponent<MonsterData>().CurrentLevel.damage;
            monster.GetComponent<MonsterData>().GetNextLevel().cost = radiusCost;
            monster.GetComponent<MonsterData>().GetNextLevel().fireRate = monster.GetComponent<MonsterData>().CurrentLevel.fireRate;
            monster.GetComponent<MonsterData>().IncreaseLevel();
            monster.GetComponent<MonsterData>().setTotalCost(damageCost);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(10);
        }

    }

    private void Update()
    {
        if(monster.GetComponent<MonsterData>().nameOfMonster == "Magicmirt")
        {
            radiusCost = (int) (1439 * monster.GetComponent<MonsterData>().CurrentLevel.Level * 1.2f);
            radiusUpgradeFor.GetComponent<TextMeshProUGUI>().text = "- $" + radiusCost.ToString();
            damageUpgradeFor.GetComponent<TextMeshProUGUI>().text = "- $" + damageCost.ToString();
            fireRateUpgradeFor.GetComponent<TextMeshProUGUI>().text = "- $" + fireRateCost.ToString();
            damageCost = 0;
            fireRateCost = 0;
        }

        if (monster.GetComponent<MonsterData>().nameOfMonster == "Platapus")
        {
            radiusButton.GetComponent<Button>().interactable = false;
            radiusButton.GetComponent<Button>().onClick.RemoveAllListeners();
        }

        if (gameManager.Gold < damageCost)
        {
            damageButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            damageButton.GetComponent<Button>().interactable = true;
        }

        if (gameManager.Gold < fireRateCost)
        {
            fireRateButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            fireRateButton.GetComponent<Button>().interactable = true;
        }

        if (gameManager.Gold < radiusCost)
        {
            radiusButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            radiusButton.GetComponent<Button>().interactable = true;
        }

        if (damageLevel > 1 && radiusLevel > 1)
        {
            fireRateButton.GetComponentInChildren<TextMeshProUGUI>().text = "<u><b>Locked</u></b>";
            fireRateButton.GetComponent<Button>().interactable = false;
            fireRateButton.GetComponent<Image>().sprite = null;
            fireRateButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            fireRateUpgradeData.GetComponent<TextMeshProUGUI>().text = "<b>Fire Rate (" + fireRateLevel + "): </b>\n\n" + Math.Round(1 / (monster.GetComponent<MonsterData>().CurrentLevel.fireRate), 1) * 10;
            fireRateUpgradeFor.GetComponent<TextMeshProUGUI>().text = "";
        }

        if (damageLevel > 1 && fireRateLevel > 1)
        {
            radiusButton.GetComponentInChildren<TextMeshProUGUI>().text = "<u><b>Locked</u></b>";
            radiusButton.GetComponent<Button>().interactable = false;
            radiusButton.GetComponent<Image>().sprite = null;
            radiusButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            radiusUpgradeData.GetComponent<TextMeshProUGUI>().text = "<b>Radius (" + radiusLevel + "): </b>\n\n" + Math.Round(monster.GetComponent<CircleCollider2D>().radius, 1) * 10;
            radiusUpgradeFor.GetComponent<TextMeshProUGUI>().text = "";
        }

        if (fireRateLevel > 1 && radiusLevel > 1)
        {
            damageButton.GetComponentInChildren<TextMeshProUGUI>().text = "<u><b>Locked</u></b>";
            damageButton.GetComponent<Button>().interactable = false;
            damageButton.GetComponent<Image>().sprite = null;
            damageButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            damageUpgradeData.GetComponent<TextMeshProUGUI>().text = "<b>Damage (" + damageLevel + "): </b>\n\n" + monster.GetComponent<MonsterData>().CurrentLevel.damage;
            damageUpgradeFor.GetComponent<TextMeshProUGUI>().text = "";
        }

        int totalUpgrades = damageLevel + fireRateLevel + radiusLevel;

        if (totalUpgrades >= 12)
        {
            damageButton.GetComponentInChildren<TextMeshProUGUI>().text = "-";
            damageButton.GetComponent<Button>().interactable = false;
            fireRateButton.GetComponentInChildren<TextMeshProUGUI>().text = "-";
            fireRateButton.GetComponent<Button>().interactable = false;
            radiusButton.GetComponentInChildren<TextMeshProUGUI>().text = "-";
            radiusButton.GetComponent<Button>().interactable = false;

            radiusUpgradeData.GetComponent<TextMeshProUGUI>().text = "<b>Radius (" + radiusLevel + "): </b>\n\n" + Math.Round(monster.GetComponent<CircleCollider2D>().radius, 1) * 10;
            radiusUpgradeFor.GetComponent<TextMeshProUGUI>().text = "";
            fireRateUpgradeData.GetComponent<TextMeshProUGUI>().text = "<b>Fire Rate (" + fireRateLevel + "): </b>\n\n" + Math.Round(1 / (monster.GetComponent<MonsterData>().CurrentLevel.fireRate), 1) * 10;
            fireRateUpgradeFor.GetComponent<TextMeshProUGUI>().text = "";
            damageUpgradeData.GetComponent<TextMeshProUGUI>().text = "<b>Damage (" + damageLevel + "): </b>\n\n" + monster.GetComponent<MonsterData>().CurrentLevel.damage;
            damageUpgradeFor.GetComponent<TextMeshProUGUI>().text = "";
        } 
    }
}
