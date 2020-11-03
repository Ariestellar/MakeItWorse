using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpPanel : MonoBehaviour
{
    [SerializeField] private Text _numberLevel;
    [SerializeField] private Button _mainMenu;
    [SerializeField] private Button _settings;

    private void Awake()
    {
        _numberLevel.text = "Level " + Convert.ToString(SceneManager.GetActiveScene().buildIndex);
        _mainMenu.onClick.AddListener(OnClickMainMenu);
        _settings.onClick.AddListener(OnClickSettings);
    }

    private void OnClickMainMenu()
    { 
    
    }

    private void OnClickSettings()
    {

    }
}
