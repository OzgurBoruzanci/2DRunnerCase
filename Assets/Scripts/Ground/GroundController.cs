using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] private Transform ground1;
    [SerializeField] private Transform ground2;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float switchPositionX = -20f;
    private Vector3 ground1Pos;
    private Vector3 ground2Pos;
    private bool canMove = false;

    private void OnEnable()
    {
        EventManager.GameStart += GameStart;
        EventManager.GameOver += GameOver;
        EventManager.ChangesStateVariables += ChangesStateVariables;
    }
    private void Start()
    {
        ground1Pos = ground1.position;
        ground2Pos = ground2.position;
    }
    private void OnDisable()
    {
        EventManager.GameStart -= GameStart;
        EventManager.GameOver -= GameOver;
        EventManager.ChangesStateVariables -= ChangesStateVariables;
    }
    void Update()
    {
        if (canMove)
        {
            MoveTheGround();
            CheckGroundPosition();
        }
    }
    private void GameStart()
    {
        ground1.transform.position = ground1Pos;
        ground2.transform.position = ground2Pos;
        canMove = true;
    }
    private void GameOver()
    {
        canMove = false;
    }
    private void ChangesStateVariables(DifficultyManager difficulty)
    {
        moveSpeed = difficulty.CurrentGroundSpeed;
    }
    private void MoveTheGround()
    {
        ground1.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        ground2.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
    private void CheckGroundPosition()
    {
        if (ground1.position.x <= switchPositionX)
        {
            ground1.position = new Vector3(ground1.position.x + (2*ground2.localScale.x), ground2.position.y, ground2.position.z);
            ground1.GetComponent<ObstaclesController>().RandomizeObstaclePositions();
        }
        if (ground2.position.x <= switchPositionX)
        {
            ground2.position = new Vector3(ground2.position.x + (2*ground1.localScale.x), ground1.position.y, ground1.position.z);
            ground2.GetComponent<ObstaclesController>().RandomizeObstaclePositions();
        }
    }
}
