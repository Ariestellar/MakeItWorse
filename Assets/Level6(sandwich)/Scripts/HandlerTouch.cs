using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandlerTouch : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] GameObject _briffingPanel;
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {        
        if (Input.GetMouseButton(0))
        {
            _briffingPanel.SetActive(false);
            RaycastHit hit;
            Ray rayAttack = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayAttack, out hit) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                if (hit.transform.gameObject.GetComponent<TouchZone>())
                {
                    hit.transform.gameObject.GetComponent<TouchZone>().RunLevel();
                }
            }
        }
    }
}
