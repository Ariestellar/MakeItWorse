using System;
using UnityEngine;

[RequireComponent(typeof(IGameLogic))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager _ui;   

    private void Awake()
    {
        Action<StatusGame> showStatusGame = _ui.ShowResultPanel;        
        GetComponent<IGameLogic>().SetActionResultsGame(showStatusGame);
    }
}
