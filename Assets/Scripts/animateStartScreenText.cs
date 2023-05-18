using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class animateStartScreenText : MonoBehaviour
{
    public float floatHeight = 10f; 
    public float floatSpeed = 1f;
    public float delayTime = 1f;

    private RectTransform rectTransform;
    private Vector3 startPos;
    

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.position;
        StartCoroutine(FloatAnimation()); 
    }

    IEnumerator FloatAnimation()
    {
        yield return new WaitForSeconds(delayTime);

        while (true)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            rectTransform.position = new Vector3(startPos.x, newY, startPos.z);
            yield return null;
        }
    }
}
