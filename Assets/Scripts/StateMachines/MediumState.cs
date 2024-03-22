using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumState : IState
{
    public float GroundSpeed { get => 10f; set { } }
    public float Delay { get => 0.75f; set { } }
    public void EnterState(DifficultyManager difficulty)
    {
        Debug.Log("Enter Mediun State");
    }

    public void UpdateState(DifficultyManager difficulty)
    {
        if (difficulty.currentStateEnum == DifficultyState.Hard)
        {
            difficulty.ChangeState(new HardState());
        }
        else if (difficulty.currentStateEnum == DifficultyState.Easy)
        {
            difficulty.ChangeState(new EasyState());
        }
    }

    public void ExitState(DifficultyManager difficulty)
    {
        Debug.Log("Exit Mediun State");
    }
}
