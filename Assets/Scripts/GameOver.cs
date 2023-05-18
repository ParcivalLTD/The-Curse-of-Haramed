using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject round;
    public TextMeshProUGUI roundText;
    private GameManagerBehavior gameManager;

    void RestartLevel()
    {
        SceneManager.LoadScene("Startscreen");
    }

}
