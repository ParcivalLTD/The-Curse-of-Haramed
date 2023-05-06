using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    public GameObject monster;
    private GameManagerBehavior gameManager;
    public GameObject sellforobj;


    void Start()
    {
        monster = transform.parent.parent.parent.gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void Update()
    {
        int refundAmount = (int)(monster.GetComponent<MonsterData>().CurrentLevel.cost * 0.7f);
        sellforobj.GetComponent<TextMeshProUGUI>().text = "$" + refundAmount.ToString();
    }

    public void OnButtonClick()
    {
        int refundAmount = (int)(monster.GetComponent<MonsterData>().CurrentLevel.cost * 0.7f);
        gameManager.Gold += refundAmount;
        Destroy(monster);
        transform.parent.parent.gameObject.SetActive(false);

        GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(8);
    }

}
