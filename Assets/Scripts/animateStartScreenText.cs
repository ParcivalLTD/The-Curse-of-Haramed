using UnityEngine;
using TMPro;
using System.Collections;

public class animateStartScreenText : MonoBehaviour
{
    public float floatHeight = 10f; // The maximum height the text will float
    public float floatSpeed = 1f; // The speed at which the text will float
    public float delayTime = 1f; // The delay before the floating starts

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
