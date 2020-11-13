using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpPanel : MonoBehaviour
{
    [SerializeField] private Text _numberLevel;
    [SerializeField] private Button _revert;
    [SerializeField] private Text _textMoney;

    private void Awake()
    {
        _numberLevel.text = "Level " + Convert.ToString(SceneManager.GetActiveScene().buildIndex);
        _revert.onClick.AddListener(OnClickReset);        
    }

    private void OnClickReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
