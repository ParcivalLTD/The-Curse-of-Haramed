
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{
    public List<GameObject> enemiesInRange;
    private float lastShotTime;
    private MonsterData monsterData;
    public GameObject bulletPrefab;
    public GameObject damageDealt;


    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        monsterData = gameObject.GetComponentInChildren<MonsterData>();
    }

    void Update()
    {
        if (monsterData.nameOfMonster == "Frog")
        {
            if (Time.time - lastShotTime > monsterData.CurrentLevel.fireRate)
            {
                foreach (GameObject enemy in enemiesInRange)
                {
                    Shoot(enemy.GetComponent<Collider2D>());
                }
                lastShotTime = Time.time;
                float damage = bulletPrefab.gameObject.GetComponent<BulletBehavior>().damage;
                int totalDamageInt = int.Parse(damageDealt.GetComponent<TextMeshProUGUI>().text);
                totalDamageInt += (int)(damage * enemiesInRange.Count);
                damageDealt.GetComponent<TextMeshProUGUI>().text = totalDamageInt.ToString();
            }
        } else if (monsterData.nameOfMonster == "Magicmirt")
        {
            foreach (GameObject enemy in enemiesInRange)
            {
                MoveEnemy enemyScript = enemy.GetComponent<MoveEnemy>();
                if (enemyScript != null)
                {
                    enemyScript.speed *= 0.8f;
                    //TODO
                }
            }
        }
        else
        {
            GameObject target = null;
            float minimalEnemyDistance = float.MaxValue;
            foreach (GameObject enemy in enemiesInRange)
            {
                float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();
                if (distanceToGoal < minimalEnemyDistance)
                {
                    target = enemy;
                    minimalEnemyDistance = distanceToGoal;
                }
            }

            if (target != null)
            {
                if (Time.time - lastShotTime > monsterData.CurrentLevel.fireRate)
                {
                    Shoot(target.GetComponent<Collider2D>());
                    lastShotTime = Time.time;
                    float damage = bulletPrefab.gameObject.GetComponent<BulletBehavior>().damage;
                    int totalDamageInt = int.Parse(damageDealt.GetComponent<TextMeshProUGUI>().text);
                    totalDamageInt += (int)damage;
                    damageDealt.GetComponent<TextMeshProUGUI>().text = totalDamageInt.ToString();

                    if (monsterData.nameOfMonster == "Gorilla")
                    {
                        monsterData.CurrentLevel.visualization.GetComponent<Animator>().SetTrigger("Hit");
                        
                    }

                }

                

                if (monsterData.nameOfMonster != "Gorilla")
                {
                    Vector3 direction = gameObject.transform.position - target.transform.position;
                    gameObject.transform.rotation = Quaternion.AngleAxis(
                        Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
                        new Vector3(0, 0, 1));
                }
            }
        }
    }


    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void Shoot(Collider2D target)
    {
        if(monsterData.nameOfMonster == "Gorilla")
        {
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(13);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(9);
        }


        GameObject bulletPrefab = monsterData.CurrentLevel.bullet;

        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
        newBullet.transform.position = startPosition;
        BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            EnemyDestructionDings del =
                other.gameObject.GetComponent<EnemyDestructionDings>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDings del =
                other.gameObject.GetComponent<EnemyDestructionDings>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

}