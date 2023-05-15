using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
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
        Debug.Log("sdfsdf");
        if (level == 0)
        {
            nextSceneNames = "GameScene";
        }
        else if (level == 1)
        {
            nextSceneNames = "GameScene1";
        }
    }
}
