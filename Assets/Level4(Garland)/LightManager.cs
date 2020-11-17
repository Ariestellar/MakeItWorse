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
        //StartCoroutine(Flashing());
    }

    private IEnumerator DelayOn()
    {
        yield return new WaitForSeconds(0.2f);
        _pointLight.SetActive(true);       
    }
    private IEnumerator DelayOff()
    {
        yield return new WaitForSeconds(0.3f);
        _pointLight.SetActive(false);
        StartCoroutine(DelayOn());
    }
    private IEnumerator Flashing()
    {
        yield return new WaitForSeconds(0.2f);
        _pointLight.SetActive(true);
        StartCoroutine(DelayOff());
    }
}
