using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AimMode))]
public class Egg : MonoBehaviour
{    
    private Camera _camera;
    private AimMode _aimMode;
    private Animator _animator;
    [SerializeField] private GameObject _briffingPanel;

    private void Awake()
    {
        _camera = Camera.main;
        _aimMode = GetComponent<AimMode>();
        _animator = GetComponent<Animator>();
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
                if (hit.transform.gameObject.GetComponent<Egg>())
                {
                    _animator.SetTrigger("CrackEgg");
                    _aimMode.enabled = true;
                    _aimMode = hit.transform.GetComponent<AimMode>();                    
                }

                if (_aimMode != null)
                {                    
                    _aimMode.transform.position = new Vector3(hit.point.x, 3.2f, hit.point.z);
                }
            }
        }
        else
        {
            if (_aimMode != null && _aimMode.IsOverFryingPan)
            {
                _animator.SetTrigger("PourEgg");
                _aimMode = null;
            }
        }
    }
}
