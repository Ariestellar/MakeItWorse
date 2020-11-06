using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private ColorType _colorType;

    private Rigidbody _rigidbody;
    private bool _isFall;

    public bool IsFall { get => _isFall;}

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Jar>())
        {
            _rigidbody.isKinematic = false;
            _isFall = true;
            other.gameObject.GetComponent<Jar>().CheckColorType(_colorType);
        }
    }
}
