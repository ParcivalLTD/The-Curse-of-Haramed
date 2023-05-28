using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour
{

    //[HideInInspector]
    public GameObject[] waypoints;
    public int currentWaypoint = 0;
    public float lastWaypointSwitchTime;
    public float speed = 1.0f;
    public bool speedChanged = false;

    public bool isSlowedDown;

    public float totalTimeForPath;

    public float currentTimeOnPath;

    public Vector3 startPosition;
    public float timeScale = 1f;

    void Start()
    {
        lastWaypointSwitchTime = Time.time;

        speed = Random.Range(5.0f, 6.0f);
    }

    void Update()
    {
        startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

        float pathLength = Vector2.Distance(startPosition, endPosition);
        totalTimeForPath = pathLength / (speed);
        currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        if (gameObject.transform.position.Equals(endPosition))
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;

            }
            else
            {
                Destroy(gameObject);
                GameManagerBehavior gameManager =
                GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
                gameManager.Health -= 1;

            }
        }
    }

    public void changeSpeed(float newSpeed)
    {

            speedChanged = true;
            float progress = (Time.time - lastWaypointSwitchTime) / totalTimeForPath;

            speed = newSpeed;
            Vector3 startPosition = waypoints[currentWaypoint].transform.position;
            Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;
            float pathLength = Vector2.Distance(startPosition, endPosition);
            totalTimeForPath = pathLength / speed;

            lastWaypointSwitchTime = Time.time - (progress * totalTimeForPath);
    }

    public GameObject getNextWaypoint()
    {
        return waypoints[currentWaypoint + 1];
    }

    public float DistanceToGoal()
    {
        float distance = 0;
        distance += Vector2.Distance(
            gameObject.transform.position,
            waypoints[currentWaypoint + 1].transform.position);
        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }
        return distance;
    }

}
