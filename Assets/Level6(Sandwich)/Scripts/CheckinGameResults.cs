using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckinGameResults : MonoBehaviour, IGameLogic
{
    [SerializeField] private Animator _animatorCameraMovement;
    private Action<StatusGame> _getResultGame;

    public void SetActionResultsGame(Action<StatusGame> action)
    {
        _getResultGame = action;
    }


    public void Checking(String tag)
    {
        _animatorCameraMovement.enabled = true;
        StartCoroutine(DelayChecking(tag));       
    }

    private IEnumerator DelayChecking(String tag)
    {
        yield return new WaitForSeconds(2);
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
