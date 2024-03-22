using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstaclesPrefab= new List<GameObject>();
    private List<GameObject> obstacles= new List<GameObject>();
    private List<Vector3> obstaclePositions = new List<Vector3>();
    private List<Vector3> positionUsed = new List<Vector3>();
    [SerializeField] private int numberOfObstacles = 9;
    [SerializeField] private float firstObstaclePositionX;

    void Start()
    {
        firstObstaclePositionX+=transform.position.x;
        PlaceObstacles();
    }

    void PlaceObstacles()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            int index = Random.Range(0, obstaclesPrefab.Count);
            GameObject obstaclePrefab = obstaclesPrefab[index];
            GameObject newObstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            newObstacle.transform.position = new Vector3(firstObstaclePositionX, transform.position.y+2.5f, 0f);
            newObstacle.transform.localScale = Vector3.one;
            newObstacle.transform.SetParent(transform);
            obstacles.Add(newObstacle);
            firstObstaclePositionX += 12f;
            obstaclePositions.Add(newObstacle.transform.localPosition);
        }
        foreach (Vector3 position in obstaclePositions)
        {
            positionUsed.Add(position);
        }
    }

    public void RandomizeObstaclePositions()
    {
        
        obstaclePositions.Clear();
        foreach (GameObject obstacle in obstacles)
        {
            var index = Random.Range(0, positionUsed.Count - 1);
            obstacle.transform.localPosition = positionUsed[index];
            positionUsed.RemoveAt(index);
            obstaclePositions.Add(obstacle.transform.position);
        }
    }
}
