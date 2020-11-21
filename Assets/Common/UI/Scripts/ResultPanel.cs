using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private Text _textResult;
    [SerializeField] private Text _textNumberPoints;
    [SerializeField] private GameObject _amountOfMoneyEarned;
    [SerializeField] private Animator _background;
    [SerializeField] private Button _next;
    [SerializeField] private Button _reset;
    [SerializeField] private Image _bgImage;
    [SerializeField] private Sprite _defeat;
    [SerializeField] private Sprite _victory;
    [SerializeField] private GameObject _star;
    [SerializeField] private GameObject _shadingPanel;

    private void Awake()
    {
        _next.onClick.AddListener(OnClickNext);
        _reset.onClick.AddListener(OnClickReset);       
    }

    public void ShowResult(StatusGame status)
    {
        _shadingPanel.SetActive(true);
        if (status == StatusGame.DEFEAT)
        {
            _bgImage.sprite = _defeat;
            _next.gameObject.SetActive(false);
            _amountOfMoneyEarned.SetActive(false);
            _textResult.text = Texts.TextDefeat;
            _textResult.color = Color.white;
        }
        else
        {
            _star.SetActive(true);
            _bgImage.sprite = _victory;
            _background.enabled = true;
            _textResult.text = Texts.TextVictory;
            _textResult.color = Color.black;
        }
        
        this.gameObject.SetActive(true);
    }
    public int GetNumberPointsLevel()
    {
        return Convert.ToInt32(_textNumberPoints.text);
    }

    private void OnClickNext()
    {
        int numberScene = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (numberScene > SceneManager.sceneCountInBuildSettings - 1)
        {
            numberScene = 1;
        }
        PlayerPrefs.SetInt("CurrenScene", numberScene);
        PlayerPrefs.SetInt("CurrenNumberMoney", GameStat.totalPoints);
        PlayerPrefs.Save();
        SceneManager.LoadScene(numberScene);
    }

    private void OnClickReset()
    {
        PlayerPrefs.SetInt("CurrenScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("CurrenNumberMoney", GameStat.totalPoints);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }       
}
