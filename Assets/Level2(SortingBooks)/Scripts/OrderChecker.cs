using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderChecker : MonoBehaviour, IGameLogic
{
    [SerializeField] private PlaceForObject[] _placesForBook;
    private Action<StatusGame> _getResultsGame;

    public void Check()
    {
        int numberCompare = 0;
        foreach (var placeForBook in _placesForBook)
        {
            if (placeForBook.IsCorrectColorType)
            {
                numberCompare += 1;
            }
        }
        if (numberCompare != _placesForBook.Length)
        {
            _getResultsGame.Invoke(StatusGame.VICTORY);
        }
        else
        {
            _getResultsGame.Invoke(StatusGame.DEFEAT);
        }
    }

    public void SetActionResultsGame(Action<StatusGame> action)
    {
        _getResultsGame = action;
    }
}
