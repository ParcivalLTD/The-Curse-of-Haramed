using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class nameScript : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public Button saveButton;
    public GameObject panel;
    public TextMeshProUGUI nameText;

    private const string PlayerNameKey = "PlayerName";

    private void Start()
    {
        
        saveButton.onClick.AddListener(SavePlayerNameToPrefs);
        playerNameInput.onEndEdit.AddListener(delegate { SavePlayerNameToPrefs(); });

        nameText.text = PlayerPrefs.GetString(PlayerNameKey);
    }

    private void Update()
    {
        if (!PlayerPrefs.HasKey(PlayerNameKey) || PlayerPrefs.GetString(PlayerNameKey) == "")
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }

    }

    private void SavePlayerNameToPrefs()
    {
        string playerName = playerNameInput.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString(PlayerNameKey, playerName);
            PlayerPrefs.Save();
            Debug.Log("Player name saved: " + playerName);

            panel.SetActive(false);
        }

        nameText.text = PlayerPrefs.GetString(PlayerNameKey);
    }
}
