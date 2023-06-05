using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class unlockPanelScrollSnap : MonoBehaviour
{
    public GameObject theText;
    public GameObject image;

    void Start()
    {
        image.SetActive(false);
        theText.SetActive(false);
    }

    void Update()
    {
        
    }

    public void lockPanel()
    {
        theText.SetActive(false);
        image.SetActive(true);
    }

    public void unlockPanel()
    {
        theText.SetActive(true);
        image.SetActive(false);
    }
}
