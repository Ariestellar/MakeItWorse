using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField]private Vector3 _target;
    private Animator _animator;
    public Vector3 Target { get => _target; set => _target = value; }
    [SerializeField] private bool _isGO;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (_isGO)
        {
            transform.position = Vector3.Lerp(transform.position, _target, 0.02f);
        }
    }

    public void  ShiftCamera()
    {        
        _animator.enabled = true;
    }

    private void SetGo()
    {
        _animator.enabled = false;
        _isGO = true;
        _target = new Vector3(transform.position.x, transform.position.y, _target.z);
    }

    public void SetTarget(Vector3 target)
    {
        _target = new Vector3(target.x, target.y, target.z - 3);
    }
}
