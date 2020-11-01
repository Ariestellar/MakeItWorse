using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    //событие сменить позицию, вызывается когда указывают на книгу
    public Action<int> ChangeOfPosition;

    [SerializeField] private int _number;
    private bool _isHand;

    public int GetNumber()
    {
        return _number;
    }

    public void SetNumber(int number)
    {
        _number = number;
    }

    public void ChangePosition()
    {
        ChangeOfPosition?.Invoke(_number);
    }

    /*
     * Скрыть с полки
     */
    public void HideFromShelf()
    {
        _isHand = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    /*
     * Показать на полке
     */
    public void ShowOnShelf()
    {
        _isHand = false;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }

    public bool IsHand()
    {
        return _isHand;
    }
}
