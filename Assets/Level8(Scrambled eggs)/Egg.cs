using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private AimMode _aimMode;

    private void OnMouseDown()
    {
        _aimMode.enabled = true;
    }
}
