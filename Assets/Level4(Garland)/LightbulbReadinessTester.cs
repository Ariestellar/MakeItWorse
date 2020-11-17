using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbReadinessTester : MonoBehaviour
{
    [SerializeField] private int _numberRedyLamp;    

    public void AddRedyLamp()
    {
        _numberRedyLamp += 1;
        if (_numberRedyLamp == 3)
        { 
            
        }
    }
    
}
