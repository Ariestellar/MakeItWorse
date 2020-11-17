using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private GameObject _pointLight;
    [SerializeField] private GameObject _mainLight;

    public void LightGarland()
    {
        _mainLight.SetActive(false);
        _pointLight.SetActive(true);
    }
}
