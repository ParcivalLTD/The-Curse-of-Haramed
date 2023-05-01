using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestructionDings : MonoBehaviour
{
    public delegate void EnemyDelegate(GameObject enemy);
    public EnemyDelegate enemyDelegate;
    private GameManagerBehavior gameManager;
    public int cursorDamage;
    public GameObject GemPrefab;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehavior>();
    }

    void Update()
    {
        cursorDamage = GameObject.Find("Upgrades").gameObject.GetComponent<miscUpgrades>().cursorDamage;
    }

    private void OnMouseDown()
    {
        HealthBar healthBar =
                    gameObject.transform.Find("HealthBar").gameObject.GetComponent<HealthBar>();
        healthBar.currentHealth -= Mathf.Max(cursorDamage, 0);
        if (healthBar.currentHealth <= 0)
        {
            Destroy(gameObject);

            gameManager.Gold += (int)healthBar.maxHealth / 2;

            if (Random.value < 0.1)
            {
                GameObject gem = Instantiate(GemPrefab, transform.position, Quaternion.identity);
                Destroy(gem, 3);
            }
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
