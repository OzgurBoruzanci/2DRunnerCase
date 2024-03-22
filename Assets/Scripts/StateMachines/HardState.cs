using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardState : IState
{
    public float GroundSpeed { get => 15f; set { } }
    public float Delay { get => 0.5f; set { } }
    public void EnterState(DifficultyManager difficulty)
    {
        Debug.Log("Enter Hard State");
    }

    public void UpdateState(DifficultyManager difficulty)
    {
        if (difficulty.currentStateEnum == DifficultyState.Medium)
        {
            difficulty.ChangeState(new MediumState());
        }
    }

    public void ExitState(DifficultyManager difficulty)
    {
        Debug.Log("Exit Hard State");
    }
}
