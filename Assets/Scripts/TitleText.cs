using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleText : MonoBehaviour
{
    public GameObject monster;
    private GameManagerBehavior gameManager;
    void Start()
    {
        monster = transform.parent.parent.parent.gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void Update()
    {
        if (monster.GetComponent<MonsterData>().CurrentLevel.Level != 10)
        {
            this.GetComponent<TextMeshProUGUI>().text = monster.GetComponent<MonsterData>().getNameOfMonster() + "(<color=red>" + monster.GetComponent<MonsterData>().CurrentLevel.Level + "</color>)";
        }
        else
        {
            this.GetComponent<TextMeshProUGUI>().text = monster.GetComponent<MonsterData>().getNameOfMonster() + "(<color=red>MAX</color>)";
        }
    }
}
