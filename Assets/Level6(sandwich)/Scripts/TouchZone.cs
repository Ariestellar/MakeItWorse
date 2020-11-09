using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchZone : MonoBehaviour
{
    [SerializeField] private Animator _plateAnimator;
    [SerializeField] private Animator _sandwichAnimator;

    public void RunLevel()
    {
        _plateAnimator.enabled = true;
        _sandwichAnimator.enabled = true;
    }
}
