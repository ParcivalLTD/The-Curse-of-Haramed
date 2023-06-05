using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class miscUpgrades : MonoBehaviour
{
    public GameObject panel;

    public int cursorDamage;
    public int cursorIncrease;
    public int currentGems;
    private GameManagerBehavior gameManager;
    public GameObject cursorUpgradeButton;
    public GameObject critChanceUpgradeButton;
    public int cursorCost;
    public int cursorLevel;

    public GameObject maldonadoUpgradeButton;
    public int maldonadoCost;
    private bool maldonadoBought = false;

    public GameObject goldenHogUpgradeButton;
    public int goldenHogCost;
    private bool goldenHogBought = false;

    public GameObject handOfBloodUpgadeButton;
    public int handOfBloodCost;
    public bool handOfBloodBought = false;

    public GameObject buySpot;
    public int buySpotCostGold;
    public int buySpotCostGems;

    public GameObject magomedsGlassesUpgradeButton;
    public int magomedsGlassesCost;
    private bool magomedsGlassesBought = false;

    public GameObject spanishHomeworkUpgradeButton;
    public int spanishHomeworkCost;
    private bool spanishHomeworkBought = false;

    public Sprite[] buffSprites;
    private Sprite[] activatedBuffSprites;
    private int numberOfPermanentBuffs = 0;

    public Sprite upgrades;
    public Sprite upgradesPressed;

    public int critChanceIncrease;
    public int critChanceLevel;
    public int critChanceCost;
    public int critChance;
    void Start()
    {
        panel.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

        cursorDamage = 1;
        cursorIncrease = 5; 
        cursorLevel = 1;
        cursorCost = 2;

        maldonadoCost = 10;
        goldenHogCost = 10;
        handOfBloodCost = 15;
        magomedsGlassesCost = 10;
        spanishHomeworkCost = 10;

        critChanceIncrease = 10;
        critChanceLevel = 1;
        critChanceCost = 15;

        buySpotCostGems = 20;
        buySpotCostGold = 100000;

        maldonadoUpgradeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = maldonadoCost.ToString();
        goldenHogUpgradeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = goldenHogCost.ToString();
        handOfBloodUpgadeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = handOfBloodCost.ToString();
        magomedsGlassesUpgradeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = magomedsGlassesCost.ToString();
        spanishHomeworkUpgradeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = spanishHomeworkCost.ToString();

        buySpot.transform.Find("upgradeFor (1)").GetComponent<TextMeshProUGUI>().text = buySpotCostGems.ToString();
        buySpot.transform.Find("upgradeFor").GetComponent<TextMeshProUGUI>().text = buySpotCostGold.ToString();


        critChanceUpgradeButton.transform.Find("upgradeFor").GetComponent<TextMeshProUGUI>().text = critChanceCost.ToString();
        critChanceUpgradeButton.transform.Find("info").GetComponent<TextMeshProUGUI>().text = "<b>Crit \nChance (" + critChanceLevel + "):</b>\n\n" + critChance.ToString() + "%";

        cursorUpgradeButton.transform.Find("upgradeFor").GetComponent<TextMeshProUGUI>().text = cursorCost.ToString();
        cursorUpgradeButton.transform.Find("info").GetComponent<TextMeshProUGUI>().text = "<b>Cursor (" + cursorLevel + "):</b>\n\n" + cursorDamage.ToString() + " to " + (cursorDamage + cursorIncrease).ToString();
    }

    public void onCritChanceUpgrade()
    {
        if (gameManager.Gems >= critChanceCost)
        {
            gameManager.Gems -= critChanceCost;
            critChance += critChanceIncrease;
            critChanceCost += 5;
            critChanceLevel++;
        }
    }

    public void onOpenSpotBuy()
    {
        if (gameManager.Gems >= buySpotCostGems && gameManager.Gold >= buySpotCostGold)
        {
            gameManager.Gems -= buySpotCostGems;
            gameManager.Gold -= buySpotCostGold;
            buySpotCostGems += 2;
            buySpotCostGold *= 2;
        }
    }

    void Update()
    {
        if(gameManager.Gems < cursorCost)
        {
            cursorUpgradeButton.GetComponent<Button>().interactable = false;
            cursorUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "<size=30>Not enough gems!</size>";
        } else
        {
            cursorUpgradeButton.GetComponent<Button>().interactable = true;
            cursorUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "Upgrade";
        }

        if (gameManager.Gems < critChanceCost)
        {
            critChanceUpgradeButton.GetComponent<Button>().interactable = false;
            critChanceUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "<size=30>Not enough gems!</size>";
        }
        else
        {
            critChanceUpgradeButton.GetComponent<Button>().interactable = true;
            critChanceUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "Upgrade";
        }

        buySpot.transform.Find("upgradeFor (1)").GetComponent<TextMeshProUGUI>().text = buySpotCostGems.ToString();
        buySpot.transform.Find("upgradeFor").GetComponent<TextMeshProUGUI>().text = buySpotCostGold.ToString();

        cursorUpgradeButton.transform.Find("upgradeFor").GetComponent<TextMeshProUGUI>().text = cursorCost.ToString();
        cursorUpgradeButton.transform.Find("info").GetComponent<TextMeshProUGUI>().text = "<b>Cursor (" + cursorLevel + "):</b>\n\n" + cursorDamage.ToString() + " to " + (cursorDamage + cursorIncrease).ToString();

        critChanceUpgradeButton.transform.Find("upgradeFor").GetComponent<TextMeshProUGUI>().text = critChanceCost.ToString();
        critChanceUpgradeButton.transform.Find("info").GetComponent<TextMeshProUGUI>().text = "<b>Crit \nChance (" + critChanceLevel + "):</b>\n\n" + critChance.ToString() + "%";

        if (gameManager.Gems < maldonadoCost)
        {
            maldonadoUpgradeButton.GetComponent<Button>().interactable = false;
            maldonadoUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "<size=30>Not enough gems!</size>";
        } else
        {
            maldonadoUpgradeButton.GetComponent<Button>().interactable = true;
            maldonadoUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "Buy";
        }

        if (gameManager.Gems < buySpotCostGems || gameManager.Gold < buySpotCostGold)
        {
            buySpot.GetComponent<Button>().interactable = false;
            buySpot.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "<size=30>Not enough gems!</size>";
        }
        else
        {
            buySpot.GetComponent<Button>().interactable = true;
            buySpot.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "Buy";
        }

        if (gameManager.Gems < goldenHogCost)
        {
            goldenHogUpgradeButton.GetComponent<Button>().interactable = false;
            goldenHogUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "<size=30>Not enough gems!</size>";
        }
        else
        {
            goldenHogUpgradeButton.GetComponent<Button>().interactable = true;
            goldenHogUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "Buy";
        }

        if (gameManager.Gems < handOfBloodCost)
        {
            handOfBloodUpgadeButton.GetComponent<Button>().interactable = false;
            handOfBloodUpgadeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "<size=30>Not enough gems!</size>";
        }
        else
        {
            handOfBloodUpgadeButton.GetComponent<Button>().interactable = true;
            handOfBloodUpgadeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "Buy";
        }

        if (gameManager.Gems < magomedsGlassesCost)
        {
            magomedsGlassesUpgradeButton.GetComponent<Button>().interactable = false;
            magomedsGlassesUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "<size=30>Not enough gems!</size>";
        }
        else
        {
            magomedsGlassesUpgradeButton.GetComponent<Button>().interactable = true;
            magomedsGlassesUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "Buy";
        }

        if (gameManager.Gems < spanishHomeworkCost)
        {
            spanishHomeworkUpgradeButton.GetComponent<Button>().interactable = false;
            spanishHomeworkUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "<size=30>Not enough gems!</size>";
        }
        else
        {
            spanishHomeworkUpgradeButton.GetComponent<Button>().interactable = true;
            spanishHomeworkUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "Buy";
        }

        if(handOfBloodBought)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyDestructionDings>().HandOfBloodBought = true;
            }
        }
    }

    public void onMiscUpgradeButton()
    {
        if(panel.activeSelf)
        {
            panel.SetActive(false);
            transform.Find("Button").GetComponent<Image>().sprite = upgrades;

        } else
        {
            panel.SetActive(true);
            transform.Find("Button").GetComponent<Image>().sprite = upgradesPressed;
            GameObject[] openspots = GameObject.FindGameObjectsWithTag("Openspot");
            foreach (GameObject openspot in openspots)
            {
                openspot.GetComponent<PlaceMonster>().HideCanvas();
            }
        }
    }

    public void setButtonImage()
    {
        transform.Find("Button").GetComponent<Image>().sprite = upgrades;
    }

    public void onMaldonadoBuy()
    {
        if (gameManager.Gems >= maldonadoCost && maldonadoBought == false)
        {
            transform.Find("buffs").transform.Find("buff" + (numberOfPermanentBuffs + 1)).gameObject.GetComponent<Image>().sprite = buffSprites[0];
            transform.Find("buffs").transform.Find("buff" + (numberOfPermanentBuffs + 1)).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            gameManager.Gems -= maldonadoCost;
            numberOfPermanentBuffs++;
            maldonadoUpgradeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = "-";
            maldonadoUpgradeButton.transform.Find("check").gameObject.SetActive(true);
            maldonadoUpgradeButton.transform.Find("text").gameObject.SetActive(false);
            maldonadoBought = true;

            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            foreach (GameObject monster in monsters)
            {
                monster.GetComponent<MonsterData>().CurrentLevel.fireRate = monster.GetComponent<MonsterData>().CurrentLevel.fireRate * 0.9f;
            }
        }
    }

    public void onGoldenHogBuy()
    {
        if (gameManager.Gems >= goldenHogCost && goldenHogBought == false)
        {
            transform.Find("buffs").transform.Find("buff" + (numberOfPermanentBuffs + 1)).gameObject.GetComponent<Image>().sprite = buffSprites[1];
            transform.Find("buffs").transform.Find("buff" + (numberOfPermanentBuffs + 1)).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            gameManager.Gems -= goldenHogCost;
            numberOfPermanentBuffs++;
            goldenHogUpgradeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = "-";
            goldenHogUpgradeButton.transform.Find("check").gameObject.SetActive(true);
            goldenHogUpgradeButton.transform.Find("text").gameObject.SetActive(false);
            goldenHogBought = true;

            gameManager.goldenHogObtained = true;
        }
    }

    public void onHandOfBloodBuy()
    {
        if (gameManager.Gems >= handOfBloodCost && handOfBloodBought == false)
        {
            transform.Find("buffs").transform.Find("buff" + (numberOfPermanentBuffs + 1)).gameObject.GetComponent<Image>().sprite = buffSprites[2];
            transform.Find("buffs").transform.Find("buff" + (numberOfPermanentBuffs + 1)).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            gameManager.Gems -= handOfBloodCost;
            numberOfPermanentBuffs++;
            handOfBloodUpgadeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = "-";
            handOfBloodUpgadeButton.transform.Find("check").gameObject.SetActive(true);
            handOfBloodUpgadeButton.transform.Find("text").gameObject.SetActive(false);
            handOfBloodBought = true;
        }
    }

    public void onMagomedsGlassesBuy()
    {
        if (gameManager.Gems >= magomedsGlassesCost && magomedsGlassesBought == false)
        {
            transform.Find("buffs").transform.Find("buff" + (numberOfPermanentBuffs + 1)).gameObject.GetComponent<Image>().sprite = buffSprites[3];
            transform.Find("buffs").transform.Find("buff" + (numberOfPermanentBuffs + 1)).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            gameManager.Gems -= magomedsGlassesCost;
            numberOfPermanentBuffs++;
            magomedsGlassesUpgradeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = "-";
            magomedsGlassesUpgradeButton.transform.Find("check").gameObject.SetActive(true);
            magomedsGlassesUpgradeButton.transform.Find("text").gameObject.SetActive(false);
            magomedsGlassesBought = true;

            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            foreach (GameObject monster in monsters)
            {
                monster.GetComponent<CircleCollider2D>().radius *= 1.4f;
            }
        }
    }

    public void onSpanishHomeworkBuy()
    {
        if (gameManager.Gems >= spanishHomeworkCost && spanishHomeworkBought == false)
        {
            transform.Find("buffs").transform.Find("buff" + (numberOfPermanentBuffs + 1)).gameObject.GetComponent<Image>().sprite = buffSprites[4];
            transform.Find("buffs").transform.Find("buff" + (numberOfPermanentBuffs + 1)).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            gameManager.Gems -= spanishHomeworkCost;
            numberOfPermanentBuffs++;
            spanishHomeworkUpgradeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = "-";
            spanishHomeworkUpgradeButton.transform.Find("check").gameObject.SetActive(true);
            spanishHomeworkUpgradeButton.transform.Find("text").gameObject.SetActive(false);
            spanishHomeworkBought = true;

            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            foreach (GameObject monster in monsters)
            {
                monster.GetComponent<MonsterData>().CurrentLevel.damage = Mathf.RoundToInt(monster.GetComponent<MonsterData>().CurrentLevel.damage * 1.1f);
            }
        }
    }

    public void onCursorUpgrade()
    {
        if(gameManager.Gems >= cursorCost)
        {
            gameManager.Gems -= cursorCost;
            cursorDamage += cursorIncrease;
            cursorCost ++;
            cursorLevel++;
        }
    }
}
