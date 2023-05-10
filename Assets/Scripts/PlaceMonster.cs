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

    private bool ctrlqPressed = false;

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
                    if (gameManager.Wave >= 9 || ctrlqPressed)
                    {
                        PlaceMonsterAtIndex(1);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    if (gameManager.Wave >= 19 || ctrlqPressed)
                    {
                        PlaceMonsterAtIndex(2);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    if (gameManager.Wave >= 29 || ctrlqPressed)
                    {
                        PlaceMonsterAtIndex(3);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    if (gameManager.Wave >= 39 || ctrlqPressed)
                    {
                        PlaceMonsterAtIndex(4);
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            ctrlqPressed = true;
        }

        if (Input.GetMouseButtonDown(1) && monster != null)
        {
            RaycastHit2D hit2 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit2.collider != null)
            {
                GameObject hoveredObject = hit2.collider.gameObject;
                if (hoveredObject.tag == "Openspot" && hoveredObject == this.gameObject)
                {
                    int refundAmount = (int)(monster.GetComponent<MonsterData>().CurrentLevel.cost * 0.7f);
                    gameManager.Gold += refundAmount;
                    Destroy(monster);
                    GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(8);
                }
            }
        }

            if (canvasIsShown && canvas != null)
        {
            canvas.SetActive(true);
            if (monster.gameObject.GetComponent<MonsterData>().nameOfMonster != "Platapus")
            {
                canvas.transform.Find("circle").gameObject.SetActive(true);
                canvas.transform.Find("circle").position = monster.transform.position;
                float radius = monster.GetComponent<CircleCollider2D>().radius;
                canvas.transform.Find("circle").localScale = new Vector3(radius, radius, radius);
            }

        }
        else if (canvasIsShown == false && canvas != null)
        {
            canvas.SetActive(false);
            if (monster.gameObject.GetComponent<MonsterData>().nameOfMonster != "Platapus")
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
            GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(0);

            if (canvasIsShown)
            {
                canvasIsShown = false;
                gameManager.SavePosition(canvas.transform.Find("Panel").gameObject.transform.position);
            }
            else
            {
                canvasIsShown = true;
                float x = gameManager.GetSavedPosition(0);
                float y = gameManager.GetSavedPosition(1);
                float z = gameManager.GetSavedPosition(2);
                //canvas.transform.Find("Panel").gameObject.transform.position = new Vector3(x, y, z);
            }
        }

        hideOtherCanvases();
        
    }

    public void hideOtherCanvases()
    {
        GameObject[] openspots = GameObject.FindGameObjectsWithTag("Openspot");
        GameObject.Find("Upgrades").GetComponent<miscUpgrades>().panel.SetActive(false);
        GameObject.Find("Upgrades").GetComponent<miscUpgrades>().setButtonImage();
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

        GameObject.FindGameObjectWithTag("Sound").gameObject.GetComponent<SoundManager>().PlaySoundEffect(10);

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
            if(monster.gameObject.GetComponent<MonsterData>().nameOfMonster != "Platapus")
            {
                canvas.transform.Find("circle").gameObject.SetActive(true);
                canvas.transform.Find("circle").position = monster.transform.position;
                float radius = monster.GetComponent<CircleCollider2D>().radius;
                canvas.transform.Find("circle").localScale = new Vector3(radius, radius, radius);
            }

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