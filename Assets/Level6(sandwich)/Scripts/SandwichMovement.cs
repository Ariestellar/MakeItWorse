using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichMovement : MonoBehaviour
{
    [SerializeField] private RotatePanel _rotatePanel;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void EndFlip()
    {
        _rotatePanel.gameObject.SetActive(true);
        _animator.enabled = false;
    }
}
