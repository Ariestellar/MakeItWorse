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
    [SerializeField] private Button _next;
    [SerializeField] private Button _reset;

    private void Awake()
    {
        _next.onClick.AddListener(OnClickNext);
        _reset.onClick.AddListener(OnClickReset);
    }

    public void ShowResult(StatusGame status)
    {
        if (status == StatusGame.DEFEAT)
        {
            _next.gameObject.SetActive(false);            
        }
        _textResult.text = Convert.ToString(status);
        this.gameObject.SetActive(true);
    }

    private void OnClickNext()
    {
        int numberScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (numberScene > SceneManager.sceneCountInBuildSettings - 1)
        {
            numberScene = 1;
        }        
        SceneManager.LoadScene(numberScene);
    }

    private void OnClickReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
