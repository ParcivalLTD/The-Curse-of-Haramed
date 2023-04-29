using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miscUpgrades : MonoBehaviour
{
    public GameObject panel;
    public int cursorDamage;
    public int cursorCost = 5;
    public int cursorIncrease = 10;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void onMiscUpgradeButton()
    {
        if(panel.activeSelf)
        {
            panel.SetActive(false);
        } else
        {
            panel.SetActive(true);
        }
    }

    public void onCursorUpgrade()
    {
        cursorDamage += cursorIncrease;
    }
}
