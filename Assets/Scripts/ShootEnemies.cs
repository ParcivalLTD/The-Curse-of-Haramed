using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShootEnemies : MonoBehaviour
{
    public List<GameObject> enemiesInRange;
    private float lastShotTime;
    private MonsterData monsterData;
    public GameObject bulletPrefab;
    public GameObject damageDealt;

    public float critChance;
    private GameObject critLabel;

    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        monsterData = gameObject.GetComponentInChildren<MonsterData>();

        critLabel = GameObject.Find("CritLabel");
        critChance = GameObject.Find("Upgrades").GetComponent<miscUpgrades>().critChance;
    }

    void Update()
    {
        critChance = GameObject.Find("Upgrades").GetComponent<miscUpgrades>().critChance;
        //Debug.Log("" + critChance);

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
        }
        else if (monsterData.nameOfMonster == "Magicmirt")
        {
            
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
        if (monsterData.nameOfMonster == "Gorilla")
        {
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(13);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(9);
        }

        GameManagerBehavior gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

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

        bool isCritical = Random.Range(0f, 100f) < critChance;
        bulletComp.damage = (int) (isCritical ? bulletComp.damage * 1.2f : bulletComp.damage);

        if (gameManager.goldenHogObtained)
        {
            gameManager.Gold += (int) (bulletPrefab.GetComponent<BulletBehavior>().damage / 20 * 1.1f);
        } else
        {
            gameManager.Gold += (int) bulletPrefab.GetComponent<BulletBehavior>().damage / 20;
        }

        if (isCritical)
        {
            critLabel.transform.position = target.transform.position;
            StartCoroutine(DisplayGoldDifference());
        }
    }

    private IEnumerator DisplayGoldDifference()
    {
        critLabel.GetComponent<SpriteRenderer>().color = new Color(critLabel.GetComponent<SpriteRenderer>().color.r, critLabel.GetComponent<SpriteRenderer>().color.g, critLabel.GetComponent<SpriteRenderer>().color.b, 255);
        Vector3 position = critLabel.transform.position;
        float startY = position.y;
        float endY = startY + 1f;
        float elapsedTime = 0f;

        while (elapsedTime < 0.4f)
        {
            float newY = Mathf.Lerp(startY, endY, elapsedTime / 1f);
            position = new Vector3(position.x, newY, position.z);
            critLabel.transform.position = position;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        critLabel.GetComponent<SpriteRenderer>().color = new Color(critLabel.GetComponent<SpriteRenderer>().color.r, critLabel.GetComponent<SpriteRenderer>().color.g, critLabel.GetComponent<SpriteRenderer>().color.b, 0);
        critLabel.transform.position = position;
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
        if(monsterData.nameOfMonster == "Magicmirt")
        {
            other.gameObject.GetComponent<MoveEnemy>().changeSpeed(other.gameObject.GetComponent<MoveEnemy>().speed * 0.5f);
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
        if (monsterData.nameOfMonster == "Magicmirt")
        {
            other.gameObject.GetComponent<MoveEnemy>().changeSpeed(other.gameObject.GetComponent<MoveEnemy>().speed / 0.5f);
        }

    }

}