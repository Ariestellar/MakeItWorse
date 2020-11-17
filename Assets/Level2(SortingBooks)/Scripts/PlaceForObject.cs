using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Компонент вешается на пустое место заготовленное для объекта
public class PlaceForObject : MonoBehaviour
{
    [SerializeField] private ColorType _colorType;
    [SerializeField] private MeshRenderer _meshRenderer;
    private bool _isCorrectColorType;    
    private bool _isFilled;        
    private Animator _animator;

    public bool IsFilled { get => _isFilled;}
    public bool IsCorrectColorType { get => _isCorrectColorType; }

    private void Awake()
    {        
        _animator = GetComponent<Animator>();
    }

    public void PutBookInPlace(Material[] materialsBook, ColorType colorType)
    {
        if (_colorType == colorType)
        {
            _isCorrectColorType = true;
        }
        else
        {
            _isCorrectColorType = false;
        }
        _meshRenderer.enabled = true;
        _animator.SetTrigger("Insert");

        _meshRenderer.materials = materialsBook;
        _isFilled = true;
    }

    public void EmptyPlace()
    {
        _meshRenderer.enabled = false;
        _animator.SetTrigger("TakeAway");
        
        _isFilled = false;
    }
}
