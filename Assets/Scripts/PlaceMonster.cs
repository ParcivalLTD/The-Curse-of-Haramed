using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceMonster : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    private GameObject monster;
    private static GameObject selectedMonster;
    private GameManagerBehavior gameManager;
    public string canvasName;
    public GameObject canvas;
    public bool canvasIsShown = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    public bool getCanvas() { return canvasIsShown; }
    public void ShowCanvas()
    {
        canvasIsShown = true;
    }

    public void HideCanvas()
    {
        canvasIsShown = false;
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
                    if (gameManager.Wave >= 9)
                    {
                        PlaceMonsterAtIndex(1);
                    }
                }
            }
        }

        

        if (canvasIsShown && canvas != null)
        {
            canvas.SetActive(true);
            canvas.transform.Find("circle").gameObject.SetActive(true);
            canvas.transform.Find("circle").position = monster.transform.position;
            float radius = monster.GetComponent<CircleCollider2D>().radius;
            canvas.transform.Find("circle").localScale = new Vector3(radius, radius, radius);
        }
        else if (canvasIsShown == false && canvas != null)
        {
            canvas.SetActive(false);
            if (monster.transform.name != "Platapus")
            {
            canvas.transform.Find("circle").gameObject.SetActive(false);
            canvas.transform.Find("circle").position = monster.transform.position;
            float radius = monster.GetComponent<CircleCollider2D>().radius;
            canvas.transform.Find("circle").localScale = new Vector3(radius, radius, radius);
            }
        }
    }

    void OnMouseDown()
    {
        if (monster != null)
        {
            canvas = monster.transform.Find(canvasName).gameObject;

            if (canvasIsShown)
            {
                canvasIsShown = false;
                gameManager.SavePosition(canvas.transform.Find("Panel").gameObject.transform.position);
            }
            else
            {
                canvasIsShown = true;
                RectTransform rectTransform = canvas.transform.Find("Panel").GetComponent<RectTransform>();
                float x = gameManager.GetSavedPosition(0);
                float y = gameManager.GetSavedPosition(1);
                float z = gameManager.GetSavedPosition(2);
                rectTransform.anchoredPosition = new Vector2(x, y);
                //canvas.transform.Find("Panel").gameObject.transform.position = new Vector3(x, y, z);
            }
        }

        hideOtherCanvases();
        
    }

    public void hideOtherCanvases()
    {
        GameObject[] openspots = GameObject.FindGameObjectsWithTag("Openspot");
        foreach (GameObject openspot in openspots)
        {
            if (openspot != this.gameObject)
            {
                openspot.GetComponent<PlaceMonster>().HideCanvas();
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
            canvasIsShown = true;
            canvas = monster.transform.Find(canvasName).gameObject;
            canvas.transform.Find("circle").gameObject.SetActive(true);
            canvas.transform.Find("circle").position = monster.transform.position;
            float radius = monster.GetComponent<CircleCollider2D>().radius;
            canvas.transform.Find("circle").localScale = new Vector3(radius, radius, radius);
            hideOtherCanvases();
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