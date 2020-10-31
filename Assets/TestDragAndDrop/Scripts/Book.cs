using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    //событие сменить позицию, вызывается когда указывают на книгу
    public Action<int> ChangeOfPosition;

    [SerializeField] private int _number;

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

    public void Hidden()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}
