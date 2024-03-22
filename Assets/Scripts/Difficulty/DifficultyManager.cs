using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultyState
{
    Easy,
    Medium,
    Hard
}
public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance => s_Instance;
    static DifficultyManager s_Instance;

    public float CurrentGroundSpeed { get; set; }
    public float CurrentDelay { get; set; }
    public DifficultyState currentStateEnum = DifficultyState.Easy;
    private IState currentState;
    private void OnEnable()
    {
        EventManager.GameStart += GameStart;
    }
    private void OnDisable()
    {
        EventManager.GameStart -= GameStart;
    }
    void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
    }
    private void Start()
    {
        GameStart();
    }
    void Update()
    {
        currentState.UpdateState(this);
    }
    private void GameStart()
    {
        currentStateEnum = DifficultyState.Easy;
        currentState = new EasyState();
        currentState.EnterState(this);
        UpdateStateProperties();
    }
    public void ChangeState(IState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
        UpdateStateProperties();
    }
    private void UpdateStateProperties()
    {
        CurrentGroundSpeed = currentState.GroundSpeed;
        CurrentDelay = currentState.Delay;
        EventManager.ChangesStateVariables(this);
    }
    public void ToSpeedUp()
    {
        switch (currentStateEnum)
        {
            case DifficultyState.Easy:
                currentStateEnum = DifficultyState.Medium;
                break;
            case DifficultyState.Medium:
                currentStateEnum = DifficultyState.Hard;
                break;
            case DifficultyState.Hard:
                break;
        }
    }
    public void Slowdown()
    {         switch (currentStateEnum)
        {
            case DifficultyState.Easy:
                break;
            case DifficultyState.Medium:
                currentStateEnum = DifficultyState.Easy;
                break;
            case DifficultyState.Hard:
                currentStateEnum = DifficultyState.Medium;
                break;
        }
    }
}
