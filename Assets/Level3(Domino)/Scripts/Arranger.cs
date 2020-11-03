using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//для быстрой расстановки домино
public class Arranger : MonoBehaviour
{
    [SerializeField] private Transform[] _prefabDominos;
    [SerializeField] private int _countDomino;
    
    private void Start()
    {
        _prefabDominos = GetComponentsInChildren<Transform>();
        float positionZ = 0;
        for (int i = 1; i < _countDomino; i++)
        {
            GameObject domino = Instantiate(_prefabDominos[Random.Range(0, _prefabDominos.Length)].gameObject);
            domino.transform.position = new Vector3(domino.transform.position.x, domino.transform.position.y, positionZ);
            positionZ += 0.7f;
        }           
    }
}
