using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingСolors : MonoBehaviour, IGameLogic
{
    [SerializeField] private Jar[] _jars;
    [SerializeField] private int _ballsHitBanks;

    private Action<StatusGame> _getResultsGame;

    public void SetActionResultsGame(Action<StatusGame> action)
    {
        _getResultsGame = action;
    }

    public void ScoreHit()
    {
        _ballsHitBanks += 1;
        if (_ballsHitBanks == 4)
        {
            StartCoroutine(DelayChecker());
            //Check();
        }
    }

    private void Check()
    {
        int countNonMatches = 0;
        foreach (var jar in _jars)
        {
            if (jar.NumberNonMatches != 0)
            {
                countNonMatches += 1;
            }
        }

        if (countNonMatches == 0)
        {
            _getResultsGame?.Invoke(StatusGame.DEFEAT);
        }
        else
        {
            _getResultsGame?.Invoke(StatusGame.VICTORY);
        }
    }

    private IEnumerator DelayChecker()
    {
        yield return new WaitForSeconds(2);
        Check();
    }
}
