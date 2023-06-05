using DanielLochner.Assets.SimpleScrollSnap;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class medalScript : MonoBehaviour
{
    public string gameSceneName;
    public Sprite[] medals;
    public Image[] medalOnPanel;
    public Sprite medalNo;
    private int[] unlockedInts;

    void Start()
    {
        unlockedInts = new int[3];
        unlockedInts[0] = 0;
        unlockedInts[1] = 1;
        unlockedInts[2] = 2;
    }

    public void getAllMedals()
    {
        PlayerPrefs.SetInt(gameSceneName + "_" + 0, 1);
        PlayerPrefs.SetInt(gameSceneName + "_" + 1, 1);
        PlayerPrefs.SetInt(gameSceneName + "_" + 2, 1);
    }

    void Update()
    {
        for (int i = 0; i < unlockedInts.Length; i++)
        {
            unlockedInts[i] = PlayerPrefs.GetInt(gameSceneName + "_" + i);
        }

        for (int i = 0; i < medalOnPanel.Length; i++)
        {
            if (unlockedInts[i] == 1)
            {
                medalOnPanel[i].sprite = medals[i];
            } else
            {
                medalOnPanel[i].sprite = medalNo;
            }
        }
    }
}
