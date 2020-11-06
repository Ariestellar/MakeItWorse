using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PlaceForBook : MonoBehaviour
{
    [SerializeField] private ColorType _colorType;

    private bool _isCorrectColorType;    
    private bool _isFilled;    
    private MeshRenderer _meshRenderer;

    public bool IsFilled { get => _isFilled;}
    public bool IsCorrectColorType { get => _isCorrectColorType; }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
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
        _meshRenderer.materials = materialsBook;
        _isFilled = true;
    }

    public void EmptyPlace()
    {
        _meshRenderer.enabled = false;
        _isFilled = false;
    }
}
