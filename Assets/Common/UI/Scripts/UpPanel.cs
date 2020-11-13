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
    private int _money;

    private void Awake()
    {
        _numberLevel.text = "Level " + Convert.ToString(SceneManager.GetActiveScene().buildIndex);
        _revert.onClick.AddListener(OnClickReset);
        _textMoney.text = Convert.ToString(GameStat.totalPoints);
    }

    private void OnClickReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddMoney(int numberMoney)
    {
        GameStat.IncreaseTotalNumberPoints(numberMoney);
        _money = Convert.ToInt32(_textMoney.text);
        StartCoroutine(ConsistentIncreaseMoney(numberMoney + _money));

    }

    private IEnumerator ConsistentIncreaseMoney(int numberMoney)
    {        
        while (_money != numberMoney)
        {           
            _money += 1;
            _textMoney.text = Convert.ToString(_money);
            yield return new WaitForSeconds(0.01f);
        }        
    }
}
