using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class infoScript : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (transform.parent.parent.parent.name.Contains("Cat"))
        {
            textMeshPro.text = "This cat has surpassed its forebears and is on the cusp of human-like intelligence. - it also enhances itself with firearms.";
        } else if(transform.parent.parent.parent.name.Contains("Platapus"))
        {
            textMeshPro.text = "A Platapus. With a flick of its paw, it hurls knives and other deadly objects with incredible force, raining destruction upon its foes";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
