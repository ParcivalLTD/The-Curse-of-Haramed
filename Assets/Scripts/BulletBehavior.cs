using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed;
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    private float distance;
    private float startTime;
    public GameObject GemPrefab;
    private GameManagerBehavior gameManager;
    public bool critical = false;

    public void setDamage(int damage1)
    {
        this.damage = damage1;
    }

    public void setSpeed(int speed1)
    {
        this.speed = speed1;
    }

    void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehavior>();
    }

    void Update()
    {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar =
                    healthBarTransform.gameObject.GetComponent<HealthBar>();
                healthBar.currentHealth -= Mathf.Max(damage, 0);


                if (healthBar.currentHealth <= 0)
                {
                    Destroy(target);

                    if (UnityEngine.Random.value < 0.1)
                    {
                        GameObject gem = Instantiate(GemPrefab, transform.position, Quaternion.identity);
                        gem.transform.parent = GameObject.Find("GemsContainer").transform;
                        Destroy(gem, 3);
                    }

                }

            }
            Destroy(gameObject);
        }

    }
}
