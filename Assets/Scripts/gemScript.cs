using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemScript : MonoBehaviour
{
    private GameManagerBehavior gameManager;

    void Start()
    {
        float randomRotation = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                Destroy(gameObject);
                gameManager.Gems += 1;
                //GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySoundEffect(0);
                GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(5);
            }
        }
    }
}
