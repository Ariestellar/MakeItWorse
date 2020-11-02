using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private bool _isMovement;
    private Vector3 _direction;
    private float _minSize;
    private float _speedDecrease;

    private void FixedUpdate()
    {
        if (_isMovement)
        {
            transform.position = Vector3.Lerp(transform.position, _direction * 0.15f, 0.05f);
            if (transform.localScale.y > _minSize)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - _speedDecrease, transform.localScale.z);
            }
           
        }
    }

    public void SetMovement()
    {
        _isMovement = true;
        //получаем направление движения из центра куска наружу
        _direction = (GetComponent<Collider>().bounds.center - Vector3.zero).normalized;
        _minSize = UnityEngine.Random.Range(0.75f, 0.9f);
        _speedDecrease = UnityEngine.Random.Range(0.001f, 0.005f);
    }
}
