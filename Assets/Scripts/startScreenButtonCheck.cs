using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startScreenButtonCheck : MonoBehaviour
{
    public UnityEngine.UI.Button previousButton;
    public UnityEngine.UI.Button nextButton;
    private SimpleScrollSnap simpleScrollSnap;

    void Start()
    {
        simpleScrollSnap = GameObject.Find("Scroll-Snap").GetComponent<SimpleScrollSnap>();
    }

    private void Update()
    {
        if (simpleScrollSnap.SelectedPanel == (simpleScrollSnap.Panels.Length - 1))
        {
            nextButton.interactable = false;
        }
        else if (simpleScrollSnap.SelectedPanel == (simpleScrollSnap.StartingPanel)) {
            previousButton.interactable = false;
        }

        if (simpleScrollSnap.SelectedPanel != (simpleScrollSnap.Panels.Length - 1))
        {
            nextButton.interactable = true;
        }
        else if (simpleScrollSnap.SelectedPanel != (simpleScrollSnap.StartingPanel))
        {
            previousButton.interactable = true;
        }
    }
}
