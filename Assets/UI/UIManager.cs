using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ResultPanel _resultPanel;
    [SerializeField] private GameObject _upPanel;

    public void ShowResultPanel(StatusGame statusGame)
    {        
        _resultPanel.ShowResult(statusGame);       
    }
}
