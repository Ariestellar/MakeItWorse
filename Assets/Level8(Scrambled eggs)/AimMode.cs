using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class AimMode : MonoBehaviour
{
    [SerializeField] private GameObject _markerPrefab;     
    [SerializeField] private EggManager _eggManager;     
    private GameObject _marker;    
    private bool _isOverFryingPan;    
    private Animator _animator;    
    private bool _isAim = true;    

    public bool IsOverFryingPan { get => _isOverFryingPan;}

    private void Awake()
    {        
        _marker = Instantiate(_markerPrefab);
        _marker.SetActive(false);
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {        
        _marker.SetActive(true);
    }

    private void OnDisable()
    {        
        _marker.SetActive(false);
    }

    public Vector3 GetMarkerPosition()
    {
        return _marker ? _marker.transform.position : Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (_isAim)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.transform.gameObject.tag == "Pan" || hit.transform.gameObject.GetComponent<FriedEggZone>())
                {
                    _marker.SetActive(true);
                    _isOverFryingPan = true;
                    _marker.transform.position = new Vector3(hit.point.x, 1.9f, hit.point.z);
                }
                else
                {
                    _isOverFryingPan = false;
                    _marker.SetActive(false);
                }
            }
        }                 
    }

    public void GetWhereEggFalls()
    {
        _isAim = false;
        _marker.SetActive(false);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {            
            if (hit.transform.gameObject.GetComponent<FriedEggZone>())
            {
                hit.transform.gameObject.GetComponent<FriedEggZone>().FriedEgg.SetActive(true);
                _eggManager.Checking(StatusGame.DEFEAT);
                gameObject.SetActive(false);
                
            }
            else
            {
                _animator.SetTrigger("EggFellUnsuccessfully");
                _eggManager.Checking(StatusGame.VICTORY);
            }

        }
    }
}
