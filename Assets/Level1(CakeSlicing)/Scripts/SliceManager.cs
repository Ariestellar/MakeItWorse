using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EzySlice;
using System;

public class SliceManager : MonoBehaviour
{
    //[SerializeField] private CuttingPosition _cuttingPosition;
    [SerializeField] private GameObject _sliceLine;
    [SerializeField] private GameObject objectToShatter;
    [SerializeField] private Material crossSectionMaterial;
    [SerializeField] private List<GameObject> _sliceLines;
    [SerializeField] private List<GameObject> _pieces;
    
    private GameObject _currentSliceLine;
    private Camera _camera;    
    private bool _isMouseButtonPressed;
    private Vector3 _startPointSwipe;
    private Vector3 _currentPointSwipe;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {           
            RaycastHit hit;
            Ray rayAttack = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayAttack, out hit) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                if (hit.transform.gameObject.tag == "Table")
                {
                    if (_isMouseButtonPressed == false)
                    {
                        _startPointSwipe = hit.point;                        
                        //_lineRenderer.SetPosition(0, _startPointSwipe);
                        _currentSliceLine = Instantiate(_sliceLine);
                        _isMouseButtonPressed = true;                        
                    }
                    else
                    {
                        _currentPointSwipe = hit.point;
                        //_lineRenderer.SetPosition(1, _currentPointSwipe);

                        _currentSliceLine.transform.position = GetCenterCutPosition(_startPointSwipe, _currentPointSwipe); 
                        Vector3 directionLook = _currentPointSwipe - _currentSliceLine.transform.position;
                        _currentSliceLine.transform.LookAt(_currentPointSwipe, directionLook.normalized);
                        _currentSliceLine.transform.localScale = new Vector3(_currentSliceLine.transform.localScale.x, _currentSliceLine.transform.localScale.y, Vector3.Distance(_startPointSwipe, _currentPointSwipe));
                        _currentSliceLine.transform.position = new Vector3(_currentSliceLine.transform.position.x, 0, _currentSliceLine.transform.position.z);                        
                    }                    
                }
            }           
        }
        else
        {
            if (_currentSliceLine != null)
            {
                _sliceLines.Add(_currentSliceLine);
                _currentSliceLine = null;
            }            
            _isMouseButtonPressed = false;            
        }
    }

    /*
     * Нарезка
     * -висит на кнопке "Нарезать"
     */
    public void Slice()
    {
        //Первая линия режет целый начальный объект:
        _pieces = GetSlicedPieces(objectToShatter, GetСutLine(_sliceLines[0].transform), crossSectionMaterial);
        objectToShatter.SetActive(false);
        //Destroy(objectToShatter);
       
        //Режем куски от полученных кусков линиями по порядку:   
        for (int i = 1; i < _sliceLines.Count; i++)
        {
            //так как первая линия режет цельный объект то следующий линии начинаем с i=1
            _pieces = SingleLineSlicing(_sliceLines[i], _pieces);            
        }
        _sliceLines.ForEach(line => line.SetActive(false));
        //_lineRenderer.enabled = false;
        PushMovementPieces(_pieces);
    }

    /*
     * Нарезка в доль одной линии 
     * принимает параметры:
     * - линию нарезки
     * - объекты которые нужно нарезать по этой линии
     * возвращает: список объектов нарезанных кусков
     */
    private List<GameObject> SingleLineSlicing(GameObject sliceLine, List<GameObject> objectsForSlicing)
    {
        List<GameObject> tempPieces = new List<GameObject>();

        foreach (var item in objectsForSlicing)
        {
            List<GameObject> currentPiece = GetSlicedPieces(item, GetСutLine(sliceLine.transform), crossSectionMaterial);

            if (currentPiece != null)
            {
                tempPieces.AddRange(currentPiece);
                Destroy(item);
            }
            else//если объект никак не нарезается этой линией, то просто добавляем его неразрезанным в массив
            {                
                tempPieces.Add(item);
            }
        }
        return tempPieces;        
    }

    /*
     * Получить позицию линии разреза, посередине, между началом и концом свайпа (Vector3)*
     * принимает параметры:
     * -начало линии разреза (Vector3)
     * -конец линии разреза (Vector3)
     */
    private Vector3 GetCenterCutPosition(Vector3 startLine, Vector3 endLine)
    {
        return new Vector3((startLine.x + endLine.x) / 2, (startLine.y + endLine.y) / 2, (startLine.z + endLine.z) / 2);
    }

    /*
     * Получить нарезанные кусочки (List<GameObject>)
     * принимает параметры:
     * -объект который нужно нарезать (GameObject)
     * -линию разреза (EzySlice.Plane)
     * -материал который будет виден после разреза (Material)
     */
    private List<GameObject> GetSlicedPieces(GameObject obj, EzySlice.Plane cutLine, Material crossSectionMaterial = null)
    {
        GameObject[] currentShatterObject = obj.SliceInstantiate(cutLine, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f), crossSectionMaterial);
        List<GameObject> currentShatterObjectList = new List<GameObject>();
        if (currentShatterObject != null)
        {            
            foreach (var item in currentShatterObject)
            {
                currentShatterObjectList.Add(item); 
            }            
        }
        else
        {
            currentShatterObjectList = null;
        }        
        return currentShatterObjectList;
    }

    /*
     * Получить линию разреза (EzySlice.Plane)
     * принимает параметры:
     * -положение разреза (Transform)
     */
    private EzySlice.Plane GetСutLine(Transform incisionPosition)
    {
        return new EzySlice.Plane(incisionPosition.position, incisionPosition.right);
    }

    /*
     * Запустить движение кусков после нарезки
     */
    private void PushMovementPieces(List<GameObject> pieces)
    { 
        foreach (var piece in pieces)
        {            
            piece.AddComponent<MeshCollider>().convex = true;                      
        }

        foreach (var piece in pieces)
        {
            Vector3 direction = piece.GetComponent<Collider>().bounds.center - Vector3.zero;            
            piece.transform.position = direction.normalized * 0.15f;
        }
    }
}
