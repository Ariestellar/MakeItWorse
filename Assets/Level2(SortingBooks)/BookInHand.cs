using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInHand : MonoBehaviour
{
    [SerializeField] private ColorType _colorType;
    public ColorType ColorType { get => _colorType; }
}
