using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speedScript : MonoBehaviour
{
    private Image buttonImage;
    public Sprite normalImage;
    public Sprite twoXImage;

    public bool isTwoXSpeed = false;

    public void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleSpeed();
        }
    }

    public void ToggleSpeed()
    {
        if (isTwoXSpeed)
        {
            Time.timeScale = 1f;
            buttonImage.sprite = normalImage;
            isTwoXSpeed = false;
        }
        else
        {
            Time.timeScale = 2f;
            buttonImage.sprite = twoXImage;
            isTwoXSpeed = true;
        }
    }
}
