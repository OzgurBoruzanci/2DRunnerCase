using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyState : IState
{
    public float GroundSpeed { get => 5f; set { } }
    public float Delay { get => 1f; set { } }
    public void EnterState(DifficultyManager difficulty)
    {
        Debug.Log("Enter Easy State");
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
        Debug.Log("Exit Easy State");
    }
}
