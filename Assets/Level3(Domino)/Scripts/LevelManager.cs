using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour, IGameLogic
{
    [SerializeField] private GameObject _pushingElement;
    [SerializeField] private List<GameObject> _dominos;

    //Сила толкания домино
    private float _pushingForce = 200;   
    private Action<StatusGame> _getResultsGame;

    public IEnumerator StartFallTimer()
    {
        yield return new WaitForSeconds(1);
        RunDominoPrinciple();
    }

    //Висит на кнопке запуска
    private void RunDominoPrinciple()
    {
        _pushingElement.GetComponent<Rigidbody>().isKinematic = false;
        _pushingElement.GetComponent<Rigidbody>().AddForce(Vector3.forward * _pushingForce);
        StartCoroutine(StartResultTimer());
    }

    private IEnumerator StartResultTimer()
    {
        yield return new WaitForSeconds(3);
        int numberStayDomino = 0;
        foreach (var domino in _dominos)
        {
            Debug.Log(domino.transform.rotation.eulerAngles.x);
            if (domino.transform.rotation.eulerAngles.x <= 0.006f)
            {
                numberStayDomino += 1;                
                break;
            }
        }

        if (numberStayDomino == 0)
        {
            _getResultsGame?.Invoke(StatusGame.DEFEAT);
        }
        else
        {
            _getResultsGame?.Invoke(StatusGame.VICTORY);
        }
        
    }

    public void SetActionResultsGame(Action<StatusGame> action)
    {
        _getResultsGame += action;
    }

    public void SetDominos(List<GameObject> dominos)
    {
        _dominos = dominos;
    }
}
