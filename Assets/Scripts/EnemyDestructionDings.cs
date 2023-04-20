using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyDestructionDings : MonoBehaviour
{
    public delegate void EnemyDelegate(GameObject enemy);
    public EnemyDelegate enemyDelegate;
    private GameManagerBehavior gameManager;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehavior>();
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        HealthBar healthBar =
                    gameObject.transform.Find("HealthBar").gameObject.GetComponent<HealthBar>();
        healthBar.currentHealth -= Mathf.Max(1, 0);
        if (healthBar.currentHealth <= 0)
        {
            Destroy(gameObject);

            gameManager.Gold += (int)healthBar.maxHealth / 2;
        }
    }

    void OnDestroy()
    {
        if (enemyDelegate != null)
        {
            enemyDelegate(gameObject);
        }
    }

}
