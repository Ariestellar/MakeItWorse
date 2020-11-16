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

    public void Checking(StatusGame status)
    {
        StartCoroutine(DelayCheking(status));
                
    }

    public IEnumerator DelayCheking(StatusGame status)
    {
        yield return new WaitForSeconds(1.2f);
        _getResultGame?.Invoke(status);
    }
}
