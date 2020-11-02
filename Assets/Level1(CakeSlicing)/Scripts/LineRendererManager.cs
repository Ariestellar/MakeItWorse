using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefabLineRender;
    private int _numberLines;    

    public LineRenderer Create()
    {        
        _numberLines += 1;
        if (_numberLines > 4)
        {
            return null;
        }
        else
        {
            GameObject lineRender = Instantiate(_prefabLineRender, this.transform);
            return lineRender.GetComponent<LineRenderer>();
        }         
    }

    public int GetNumberLines()
    {
        return _numberLines;
    }
}
