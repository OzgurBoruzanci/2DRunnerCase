using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Action Reposition;
    public static Action GameOver;
    public static Action GameStart;
    public static Action<DifficultyManager> ChangesStateVariables;
}