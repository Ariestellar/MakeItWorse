using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ResultPanel _resultPanel;
    [SerializeField] private UpPanel _upPanel;
    [SerializeField] private Text _tutorText;
    [SerializeField] private Animator _briffingPanel;
    private CanvasScaler _canvasScaler;

    private void Awake()
    {
        _canvasScaler = GetComponent<CanvasScaler>();
    }

    private void Start()
    {
        _briffingPanel.SetTrigger(Convert.ToString(SceneManager.GetActiveScene().buildIndex));
        if (Screen.width <= 480)
        {
            _canvasScaler.scaleFactor = 0.4f;
        }
        else if (Screen.width <= 750)
        {
            _canvasScaler.scaleFactor = 0.65f;
        }
        else if (Screen.width <= 1080)
        {
            _canvasScaler.scaleFactor = 1;
        }
        _tutorText.text = Texts.TutorText[SceneManager.GetActiveScene().buildIndex];
    }
    public void ShowResultPanel(StatusGame statusGame)
    {        
        _resultPanel.ShowResult(statusGame);
        if (statusGame == StatusGame.VICTORY)
        {
            _upPanel.AddMoney(_resultPanel.GetNumberPointsLevel());
        }        
    }

}
