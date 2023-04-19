using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public GameObject canvas;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void OnButtonClick()
    {

        GameObject[] openspots = GameObject.FindGameObjectsWithTag("Openspot");
        foreach (GameObject openspot in openspots)
        {
            if (openspot != this.gameObject)
            {
                openspot.GetComponent<PlaceMonster>().HideCanvas();
            }
        }
    }

}
