using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void EnterState(DifficultyManager difficulty);
    void UpdateState(DifficultyManager difficulty);
    void ExitState(DifficultyManager difficulty);
    float GroundSpeed { get; set; }
    float Delay { get; set; }
}
