using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderChekerGarland : MonoBehaviour, IGameLogic, IChecker
{
    [SerializeField] private PlaceForObject[] _placesForLamp;
    [SerializeField] private LightManager _lightManager;
    private Action<StatusGame> _getResultsGame;

    public void Check()
    {        
        StartCoroutine(DelayCheck());
    }

    public void SetActionResultsGame(Action<StatusGame> action)
    {
        _getResultsGame = action;
    }

    private IEnumerator DelayShowResult(StatusGame status)
    {
        yield return new WaitForSeconds(2.2f);
        _getResultsGame.Invoke(status);
    }
    private IEnumerator DelayCheck()
    {
        yield return new WaitForSeconds(1);
        int numberCompare = 0;
        foreach (var placeForBook in _placesForLamp)
        {
            if (placeForBook.IsCorrectColorType)
            {
                numberCompare += 1;
            }
        }
        if (numberCompare != _placesForLamp.Length)
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

}
