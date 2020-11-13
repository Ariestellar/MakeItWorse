using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckinGameResults : MonoBehaviour, IGameLogic
{
    private Action<StatusGame> _getResultGame;

    public void SetActionResultsGame(Action<StatusGame> action)
    {
        _getResultGame = action;
    }


    public void Checking(String tag)
    {
        if (tag == "Floor")
        {
            _getResultGame?.Invoke(StatusGame.VICTORY);
        }
        else
        {
            _getResultGame?.Invoke(StatusGame.DEFEAT);
        }
    }
}
