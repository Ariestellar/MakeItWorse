using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jar : MonoBehaviour
{
    [SerializeField] private ColorType _colorType;
    public UnityEvent BallInJar;

    private int _numberNonMatches = 0;

    public int NumberNonMatches { get => _numberNonMatches;}

    public void CheckColorType(ColorType colorTypeBalls)
    {
        if (_colorType != colorTypeBalls)
        {
            _numberNonMatches += 1;
        }
        BallInJar?.Invoke();
    }
}
