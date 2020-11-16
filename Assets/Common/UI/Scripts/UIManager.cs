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
    private void Start()
    {
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
