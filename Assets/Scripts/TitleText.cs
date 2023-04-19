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

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = monster.GetComponent<MonsterData>().getNameOfMonster() + "(<color=red>" + monster.GetComponent<MonsterData>().CurrentLevel.Level + "</color>)";
    }
}
