using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class difficutlyToggle : MonoBehaviour
{
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    public TextMeshProUGUI info;
    public string easyInfo;
    public string mediumInfo;
    public string hardInfo;

    public Image easyImage;
    public Image mediumImage;
    public Image hardImage;

    void Start()
    {
        easyInfo = "On <b>Easy</b> difficulty you have <color=red>10 Hearts</color>, you get <color=red>more gold</color> and you have to beat <color=red>Wave 40</color> to complete the level";
        mediumInfo = "On <b>Medium</b> difficulty you have <color=red>5 Hearts</color>, you get <color=red>normal gold</color> and you have to beat <color=red>Wave 50</color> to complete the level";
        hardInfo = "On <b>Hard</b> difficulty you have <color=red>3 Hearts</color>, you get <color=red>less gold</color> and you have to beat <color=red>Wave 60</color> to complete the level";

        easyButton.image.color = Color.white;
        mediumButton.image.color = Color.gray;
        hardButton.image.color = Color.gray;

        int difficulty = 0;

        if(PlayerPrefs.GetInt("difficulty") == 1 && PlayerPrefs.GetInt("difficulty") == 2)
        {
            difficulty = PlayerPrefs.GetInt("difficulty");
        }

        if (difficulty == 0)
        {
            easyButton.image.color = Color.white;
            mediumButton.image.color = Color.gray;
            PlayerPrefs.SetInt("wave", 40);
            hardButton.image.color = Color.gray;
            info.text = easyInfo;
            easyImage.gameObject.SetActive(true);
            mediumImage.gameObject.SetActive(false);
            hardImage.gameObject.SetActive(false);
        }
        else if (difficulty == 1)
        {
            easyButton.image.color = Color.gray;
            mediumButton.image.color = Color.white;
            PlayerPrefs.SetInt("wave", 50);
            hardButton.image.color = Color.gray;
            info.text = mediumInfo;
            easyImage.gameObject.SetActive(false);
            mediumImage.gameObject.SetActive(true);
            hardImage.gameObject.SetActive(false);
        }
        else if (difficulty == 2)
        {
            easyButton.image.color = Color.gray;
            mediumButton.image.color = Color.gray;
            PlayerPrefs.SetInt("wave", 60);
            hardButton.image.color = Color.white;
            info.text = hardInfo;
            easyImage.gameObject.SetActive(false);
            mediumImage.gameObject.SetActive(false);
            hardImage.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("difficulty") == 0)
        {
            PlayerPrefs.SetFloat("GoldMultiplier", 1.15f);
        } else if(PlayerPrefs.GetInt("difficulty") == 1)
        {
            PlayerPrefs.SetFloat("GoldMultiplier", 1f);
        } else if(PlayerPrefs.GetInt("difficulty") == 2)
        {
            PlayerPrefs.SetFloat("GoldMultiplier", 0.9f);
        }

        int difficulty = PlayerPrefs.GetInt("difficulty");

        if (difficulty == 0)
        {
            easyButton.image.color = Color.white;
            mediumButton.image.color = Color.gray;
            hardButton.image.color = Color.gray;
            PlayerPrefs.SetInt("wave", 40);
            info.text = easyInfo;
            easyImage.gameObject.SetActive(true);
            mediumImage.gameObject.SetActive(false);
            hardImage.gameObject.SetActive(false);
        }
        else if (difficulty == 1)
        {
            easyButton.image.color = Color.gray;
            mediumButton.image.color = Color.white;
            hardButton.image.color = Color.gray;
            PlayerPrefs.SetInt("wave", 50);
            info.text = mediumInfo;
            easyImage.gameObject.SetActive(false);
            mediumImage.gameObject.SetActive(true);
            hardImage.gameObject.SetActive(false);
        }
        else if (difficulty == 2)
        {
            easyButton.image.color = Color.gray;
            mediumButton.image.color = Color.gray;
            hardButton.image.color = Color.white;
            PlayerPrefs.SetInt("wave", 60);
            info.text = hardInfo;
            easyImage.gameObject.SetActive(false);
            mediumImage.gameObject.SetActive(false);
            hardImage.gameObject.SetActive(true);
        }
    }

    public void SetDifficulty(int difficulty)
    {
        GameObject.Find("Sound").GetComponent<SoundManager>().PlaySoundEffect(0);
        PlayerPrefs.SetInt("difficulty", difficulty);

        if (difficulty == 0)
        {
            easyButton.image.color = Color.white;
            mediumButton.image.color = Color.gray;
            hardButton.image.color = Color.gray;
            PlayerPrefs.SetInt("wave", 40);
            info.text = easyInfo;
            easyImage.gameObject.SetActive(true);
            mediumImage.gameObject.SetActive(false);
            hardImage.gameObject.SetActive(false);
        }
        else if (difficulty == 1)
        {
            easyButton.image.color = Color.gray;
            mediumButton.image.color = Color.white;
            hardButton.image.color = Color.gray;
            PlayerPrefs.SetInt("wave", 50);
            info.text = mediumInfo;
            easyImage.gameObject.SetActive(false);
            mediumImage.gameObject.SetActive(true);
            hardImage.gameObject.SetActive(false);
        }
        else if (difficulty == 2)
        {
            easyButton.image.color = Color.gray;
            mediumButton.image.color = Color.gray;
            hardButton.image.color = Color.white;
            PlayerPrefs.SetInt("wave", 60);
            info.text = hardInfo;
            easyImage.gameObject.SetActive(false);
            mediumImage.gameObject.SetActive(false);
            hardImage.gameObject.SetActive(true);
        }
    }
}