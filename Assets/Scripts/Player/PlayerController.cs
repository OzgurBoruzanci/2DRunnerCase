using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private Vector3 startPos;
    private bool isSwipe = false;
    private bool canMove = false;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float delay;

    public bool CanMove { get => canMove; set => canMove = value; }
    private void OnEnable()
    {
        EventManager.GameStart += GameStart;
        EventManager.GameOver += GameOver;
        EventManager.ChangesStateVariables += ChangesStateVariables;
    }
    private void OnDisable()
    {
        EventManager.GameStart -= GameStart;
        EventManager.GameOver -= GameOver;
        EventManager.ChangesStateVariables -= ChangesStateVariables;
    }
    private void Start()
    {
        startPos = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            EventManager.GameOver();
        }
        if (other.CompareTag("Faster"))
        {
            DifficultyManager.Instance.ToSpeedUp();
        }
        if (other.CompareTag("Slowly"))
        {
            DifficultyManager.Instance.Slowdown();
        }
    }
    void Update()
    {
#if UNITY_EDITOR
        if(canMove) MouseController();
#else
        if(canMove) TouchedController();
#endif
    }
    private void GameStart()
    {
        transform.position = startPos;
        canMove = true;
    }
    private void GameOver()
    {
        canMove = false;
    }
    private void ChangesStateVariables(DifficultyManager difficulty)
    {
        delay = difficulty.CurrentDelay;
    }
    private void MouseController()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fingerUpPosition = Input.mousePosition;
            fingerDownPosition = Input.mousePosition;
            isSwipe = true;
        }

        if (Input.GetMouseButtonUp(0) && isSwipe)
        {
            isSwipe = false;
            fingerDownPosition = Input.mousePosition;
            SwipeDirectionCheck();
        }
    }
    private void TouchedController()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
                isSwipe = true;
            }

            if (touch.phase == TouchPhase.Ended && isSwipe)
            {
                fingerDownPosition = touch.position;
                SwipeDirectionCheck();
                isSwipe = false;
            }
        }
    }
    private void SwipeDirectionCheck()
    {
        Vector2 distance = fingerDownPosition - fingerUpPosition;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        if (angle < 0)   angle += 360;
        if ((angle >= 15 && angle < 165) || (angle >= 345 && angle < 360))  Jump();
        else if (angle >= 195 && angle < 345)  Tilt();
        else  Debug.Log("Swipe direction is neither up nor down.");
    }
    private void Jump()
    {
        DOTween.Complete("Move");
        transform.DOMoveY(transform.position.y + jumpHeight, delay)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.DOMoveY(transform.position.y - jumpHeight, delay)
                    .SetEase(Ease.InQuad);
            }).SetId("Move");
    }
    private void Tilt()
    {
        var scale = new Vector3(1, 0.5f, 1);
        DOTween.Complete("Move");
        DOTween.Sequence()
            .Append(transform.DOScale(scale, 0.1f))
            .AppendInterval(delay)
            .Append(transform.DOScale(Vector3.one, 0.1f))
            .SetId("Move");
    }
}
