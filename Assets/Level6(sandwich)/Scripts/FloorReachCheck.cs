using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorReachCheck : MonoBehaviour, GameLogic
{
    private Action<StatusGame> _getResultGame;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {            
            Debug.Log("Упал");
            StartCoroutine(DelayCheck());
        }
    }

    public void SetActionResultsGame(Action<StatusGame> action)
    {
        _getResultGame = action;
    }

    public void Check()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit))
        {
            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject.tag == "Floor")
            {
                _getResultGame?.Invoke(StatusGame.VICTORY);
            }
            else
            {
                _getResultGame?.Invoke(StatusGame.DEFEAT);
            }
        }
        else
        {
            _getResultGame?.Invoke(StatusGame.DEFEAT);
        }
    }

    private IEnumerator DelayCheck()
    {
        yield return new WaitForSeconds(1);
        Check();
    }
}
