using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameWonScript : MonoBehaviour
{
    private GameManagerBehavior gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
    }

    void Update()
    {
    }

    public void freeplay()
    {
        transform.Find("thing").gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void returnToMenu()
    {
        Time.timeScale = 1.0f;
        GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
        gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
    }
}
