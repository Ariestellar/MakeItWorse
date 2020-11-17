using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderChekerGarland : MonoBehaviour, IGameLogic, IChecker
{
    [SerializeField] private PlaceForObject[] _placesForBook;
    [SerializeField] private LightManager _lightManager;
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
            _lightManager.LightGarland();
            StartCoroutine(DelayShowResult(StatusGame.VICTORY));             
        }
        else
        {
            _lightManager.LightGarland();
            StartCoroutine(DelayShowResult(StatusGame.DEFEAT));            
        }
    }

    public void SetActionResultsGame(Action<StatusGame> action)
    {
        _getResultsGame = action;
    }

    private IEnumerator DelayShowResult(StatusGame status)
    {
        yield return new WaitForSeconds(2);
        _getResultsGame.Invoke(status);
    }
}
