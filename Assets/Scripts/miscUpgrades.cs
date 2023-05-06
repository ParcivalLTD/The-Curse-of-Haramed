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
    public int cursorCost;
    public int cursorLevel;

    public GameObject maldonadoUpgradeButton;
    public int maldonadoCost;
    private bool maldonadoBought = false;

    public GameObject goldenHogUpgradeButton;
    public int goldenHogCost;
    private bool goldenHogBought = false;

    public Sprite[] buffSprites;
    private Sprite[] activatedBuffSprites;
    private int numberOfPermanentBuffs = 0;

    public Sprite upgrades;
    public Sprite upgradesPressed;

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

        maldonadoUpgradeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = maldonadoCost.ToString();
        goldenHogUpgradeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = goldenHogCost.ToString();
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

        cursorUpgradeButton.transform.Find("upgradeFor").GetComponent<TextMeshProUGUI>().text = cursorCost.ToString();
        cursorUpgradeButton.transform.Find("info").GetComponent<TextMeshProUGUI>().text = "<b>Cursor (" + cursorLevel + "):</b>\n\n" + cursorDamage.ToString() + " to " + (cursorDamage + cursorIncrease).ToString();

        if(gameManager.Gems < maldonadoCost)
        {
            maldonadoUpgradeButton.GetComponent<Button>().interactable = false;
            maldonadoUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "<size=30>Not enough gems!</size>";
        } else
        {
            maldonadoUpgradeButton.GetComponent<Button>().interactable = true;
            maldonadoUpgradeButton.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "Buy";
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

            //increase firerate of all monsters
        }
    }

    public void onGoldenHogBuy()
    {
        if (gameManager.Gems >= goldenHogCost && goldenHogBought == false)
        {
            transform.Find("buffs").transform.Find("buff" + (numberOfPermanentBuffs + 1)).gameObject.GetComponent<Image>().sprite = buffSprites[1];
            transform.Find("buffs").transform.Find("buff" + (numberOfPermanentBuffs + 1)).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            gameManager.Gems -= maldonadoCost;
            numberOfPermanentBuffs++;
            goldenHogUpgradeButton.transform.Find("buyFor").GetComponent<TextMeshProUGUI>().text = "-";
            goldenHogUpgradeButton.transform.Find("check").gameObject.SetActive(true);
            goldenHogUpgradeButton.transform.Find("text").gameObject.SetActive(false);
            goldenHogBought = true;

            //gameManager.goldenHogObtained = true;
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
