using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelection : MonoBehaviour
{
    public string nextSceneNames;
    void Start()
    {
        nextSceneNames = GameObject.Find("Button").GetComponent<startGameScript>().nextSceneName;
    }

    void Update()
    {
        
    }

    public void onLevelSelected(int level)
    {
        if(level == 0)
        {
            nextSceneNames = "GameScene";
        } else if(level == 1)
        {
            nextSceneNames  = "GameScene 1";
        }
        
    }
}
