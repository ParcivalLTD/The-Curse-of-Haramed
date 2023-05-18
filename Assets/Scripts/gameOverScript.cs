using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameOverScript : MonoBehaviour
{
    public GameObject round;
    private GameManagerBehavior gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void Update()
    {
        round.GetComponent<TextMeshProUGUI>().text = "Round: " + (gameManager.Wave + 1);
    }

    public void returnToMenu()
    {
        Time.timeScale = 1.0f;
        GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
        gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
    }
}
