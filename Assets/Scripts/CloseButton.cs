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
        transform.parent.parent.gameObject.SetActive(false);
    }
}
