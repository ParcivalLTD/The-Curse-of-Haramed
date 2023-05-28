using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class IMonsterPanel : MonoBehaviour
{
    public Image panelImage;
    public TextMeshProUGUI monsterNameText;
    public TextMeshProUGUI infoText;

    protected virtual void Start()
    {
        HidePanel();
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            bool is2xSpeed = GameObject.Find("Speed").GetComponent<speedScript>().isTwoXSpeed;
        }

        Time.timeScale = 0f;
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);

        bool is2xSpeed = GameObject.Find("Speed").GetComponent<speedScript>().isTwoXSpeed;

        if (is2xSpeed)
        {
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void SetImage(GameObject gameObject)
    {
        if (panelImage != null)
        {
            panelImage.sprite = gameObject.transform.Find("Monster0").GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void SetMonsterName(GameObject gameObject)
    {
        if (monsterNameText != null)
        {
            monsterNameText.text = gameObject.GetComponent<MonsterData>().nameOfMonster;
        }
    }

    public void SetInfoText(GameObject gameObject)
    {
        if (infoText != null)
        {
            infoText.text = gameObject.transform.Find("Canvas").transform.Find("Panel").transform.Find("info").GetComponent<TextMeshProUGUI>().text;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            HidePanel();
        }
    }
}