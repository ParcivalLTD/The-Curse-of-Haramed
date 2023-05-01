using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScene : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        SliderHolder.slider = slider;
    }
}

