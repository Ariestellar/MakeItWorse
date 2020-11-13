using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

[RequireComponent(typeof(Camera))]
public class AimMode : MonoBehaviour
{
    [SerializeField] private GameObject _markerPrefab; 
    
    private GameObject _marker; 
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _marker = Instantiate(_markerPrefab);
        _marker.SetActive(false);
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
        RaycastHit hit;
        Ray rayAttack = _camera.ScreenPointToRay(Input.mousePosition);//Проверить или изменить мышь и тач

        if (Physics.Raycast(rayAttack, out hit))
        {
            if (hit.transform.gameObject.tag == "Pan")
            {
                _marker.SetActive(true);
                _marker.transform.position = new Vector3(hit.point.x, 1.9f, hit.point.z);
            }
            else
            {
                _marker.SetActive(false);
            }

        }               
    }    
}
