using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceMonster : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    private GameObject monster;
    private GameManagerBehavior gameManager;
    public string canvasName;
    public GameObject canvas;
    public bool radShow = false;
    public bool canvasIsShown = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void Update()
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            GameObject hoveredObject = hit.collider.gameObject;

            if (hoveredObject.tag == "Openspot" && hoveredObject == this.gameObject)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    PlaceMonsterAtIndex(0);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    if (gameManager.Wave == 9)
                    {
                        PlaceMonsterAtIndex(1);
                    }
                }
            }
        }

        if (canvasIsShown && canvas != null)
        {
            canvas.SetActive(true);
        } else if(canvasIsShown == false && canvas != null)
        {
            canvas.SetActive(false);
        }

        if (radShow && canvas != null)
        {
            canvas.transform.Find("circle").gameObject.SetActive(true);
            canvas.transform.Find("circle").position = monster.transform.position;
            float radius = monster.GetComponent<CircleCollider2D>().radius;
            canvas.transform.Find("circle").localScale = new Vector3(radius, radius, radius);
        } else if (radShow == false && canvas != null) 
        {
            canvas.transform.Find("circle").gameObject.SetActive(false);
            canvas.transform.Find("circle").position = monster.transform.position;
            float radius = monster.GetComponent<CircleCollider2D>().radius;
            canvas.transform.Find("circle").localScale = new Vector3(radius, radius, radius);
        }
    }

    void OnMouseDown()
    {
        if (monster != null)
        {
            canvas = monster.transform.Find(canvasName).gameObject;

            // Find all gameobjects with the tag "CanvasPre"
            GameObject[] canvasPreObjects = GameObject.FindGameObjectsWithTag("CanvasPre");

            foreach (GameObject canvasPreObject in canvasPreObjects)
            {
                // Disable all gameobjects with the tag "CanvasPre" except for the one attached to the monster
                if (canvasPreObject != canvas)
                {
                    canvasPreObject.SetActive(false);
                }
            }

            if (canvasIsShown)
            {
                canvas.SetActive(false);
                canvasIsShown = false;
            }
            else
            {
                canvas.SetActive(true);
                canvasIsShown = true;
            }

            if (radShow)
            {
                canvas.transform.position = monster.transform.position;
                radShow = false;
            }
            else
            {
                radShow = true;
            }
        }
    }



    private void PlaceMonsterAtIndex(int index)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        GameObject selectedObject = hit.collider.gameObject;

        if (selectedObject.tag == "Monster")
        {
            monster = selectedObject;
        }
        else if (CanPlaceMonster(index))
        {
            monster = Instantiate(monsterPrefabs[index], mousePosition, Quaternion.identity);
            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;

            canvasIsShown = false;
        }
    }

    private bool CanUpgradeMonster(GameObject monsterToUpgrade)
    {
        MonsterData monsterData = monsterToUpgrade.GetComponent<MonsterData>();
        MonsterLevel nextLevel = monsterData.GetNextLevel();
        if (nextLevel != null)
        {
            return gameManager.Gold >= nextLevel.cost;
        }
        return false;
    }

    private bool CanPlaceMonster(int index)
    {
        int cost = monsterPrefabs[index].GetComponent<MonsterData>().levels[0].cost;
        return monster == null && gameManager.Gold >= cost && IsMouseOverOpenSpot();
    }

    private bool IsMouseOverOpenSpot()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            return hit.collider.tag == "Openspot";
        }
        return false;
    }


}