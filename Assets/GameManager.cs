using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager _ui;
    [SerializeField] private GameObject _gameLogicObject;

    private void Awake()
    {
        Action<StatusGame> showStatusGame = _ui.ShowResultPanel;        
        _gameLogicObject.GetComponent<GameLogic>().SetActionResultsGame(showStatusGame);
    }
}
