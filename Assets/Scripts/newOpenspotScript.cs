using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class newOpenspotScript : MonoBehaviour
{
    public GameObject spritePrefab;
    public GameObject openspotPrefab;
    public Transform openspotParent;

    private GameObject currentSprite;
    private SpriteRenderer currentSpriteRenderer;
    private bool isSpriteAttached;
    public GameObject[] safezones;

    public Sprite openSpotImage;
    private float rotationAngle;


    private void Update()
    {
        if (isSpriteAttached)
        {
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(currentSprite != null)
            {
                currentSprite.transform.position = cursorPosition;
                //set its order to top
                currentSpriteRenderer.sortingOrder = 1000;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.CompareTag("safeZone") && !hit.collider.CompareTag("Openspot"))
            {
                currentSpriteRenderer.color = Color.white;
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject collidedObject = hit.collider.gameObject;
                    if (IsSafeZone(collidedObject))
                    {
                        GameObject.Find("Sound").GetComponent<SoundManager>().PlaySoundEffect(1);
                        Destroy(currentSprite);
                        GameObject newOpenspot = Instantiate(openspotPrefab, cursorPosition, Quaternion.identity);
                        newOpenspot.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
                        newOpenspot.GetComponent<SpriteRenderer>().sprite = openSpotImage;
                        newOpenspot.transform.parent = openspotParent.transform;
                        newOpenspot.GetComponent<BoxCollider2D>().size *= 1.35f;
                        newOpenspot.transform.localScale *= 0.67f;
                    }
                }
            }
            else
            {
                if(SceneManager.GetActiveScene().name != "GameScene2")
                {
                    currentSpriteRenderer.color = new Color32(228, 142, 142, 255);
                } else
                {
                    currentSpriteRenderer.color = new Color32(255, 0, 132, 255);
                }
            }
        }

        
    }

    private bool IsSafeZone(GameObject obj)
    {
        foreach (GameObject safezone in safezones)
        {
            if (obj == safezone)
            {
                return true;
            }
        }
        return false;
    }

    public void OnButtonClick()
    {
        rotationAngle = Random.Range(0f, 360f);
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentSprite = Instantiate(spritePrefab, cursorPosition, Quaternion.Euler(0f, 0f, rotationAngle));
        currentSpriteRenderer = currentSprite.GetComponent<SpriteRenderer>();
        isSpriteAttached = true;
    }
}
