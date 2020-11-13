using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggManager : MonoBehaviour, IGameLogic
{
    private Action<StatusGame> _getResultGame;

    public void SetActionResultsGame(Action<StatusGame> action)
    {
        _getResultGame = action;
    }
}
