using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EzySlice;
using System;
using UnityEditorInternal;

public class SliceManager : MonoBehaviour
{
    [SerializeField] private LineRendererManager _lineRendererManager;    
    [SerializeField] private GameObject objectToShatter;
    [SerializeField] private Material crossSectionMaterial;
    [SerializeField] private List<GameObject> _sliceLines;
    [SerializeField] private List<GameObject> _pieces;

    [SerializeField] private GameObject _knife;
    private LineRenderer _sliceLineRenderer;
    private GameObject _currentSliceLine;
    //[SerializeField]private GameObject _prefabSlice;//для теста
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
        if (Input.GetMouseButton(0))//Если мы касаемся экрана:
        {           
            RaycastHit hit;
            Ray rayAttack = _camera.ScreenPointToRay(Input.mousePosition);//пускаем луч в точку касания            

            if (Physics.Raycast(rayAttack, out hit) && EventSystem.current.IsPointerOverGameObject() == false)//и проверяем не нажат ли UI
            {
                if (hit.transform.gameObject.tag == "Cake")//Если луч встречает торт
                {    
                    if (_isMouseButtonPressed == false)//Если это было первое касание по торту то:
                    {
                        _sliceLineRenderer = _lineRendererManager.Create();//Создаем линию для отображения если возвращает null значит линий уже много(больше 4) хватит их создавать
                        if (_sliceLineRenderer)//если все таки не null
                        {
                            _startPointSwipe = hit.point;//ставим точку начала резки

                            _knife.SetActive(true);//показхываем нож если он был до этого скрыт(перед первым запуском он не скрыт а лежит на столе)
                            _knife.GetComponent<Animator>().enabled = true;//включаем у ножа анимацию резки

                            _sliceLineRenderer.SetPosition(0, _startPointSwipe);//Задаем начальную позицию на рисованной линии
                            _currentSliceLine = new GameObject("ScliceLine");//Создаем настоящюю (не рисованную) линию разреза
                            //_currentSliceLine = Instantiate(_prefabSlice);//для сравнения можно было использовать префаб линии
                            _currentSliceLine.transform.parent = this.transform;//для порядка на сцене прячем эту линию в родителя SliceManager
                            _isMouseButtonPressed = true;//Устанавливаем флаг первого касания что бы сюда не возвращаться
                        }                                               
                    }
                    else//Если это не первое касание по торту(тоесть мы ведем лучем по торту не отрывая пальца)
                    {                        
                        if (_sliceLineRenderer)//если у нас есть рисуемая линия 
                        {                           
                            _currentPointSwipe = hit.point;//получаем текушюю точку местоположение пальца
                            _knife.transform.position = _currentPointSwipe;//устанавливаем в эту точку нож
                            _sliceLineRenderer.SetPosition(1, _currentPointSwipe);//устанавливаем эту точку для линии отрисовки

                            //Это логика создания и деформации линии разреза(не линии рисовки) в доль свайпа:
                            _currentSliceLine.transform.position = GetCenterCutPosition(_startPointSwipe, _currentPointSwipe);
                            Vector3 directionLook = _currentPointSwipe - _currentSliceLine.transform.position;
                            _currentSliceLine.transform.LookAt(_currentPointSwipe, directionLook.normalized);
                            _currentSliceLine.transform.localScale = new Vector3(_currentSliceLine.transform.localScale.x, _currentSliceLine.transform.localScale.y, Vector3.Distance(_startPointSwipe, _currentPointSwipe));
                            _currentSliceLine.transform.position = new Vector3(_currentSliceLine.transform.position.x, 0, _currentSliceLine.transform.position.z);
                        }
                                            
                    }                    
                }
            }           
        }
        else//Если мы убираем палец от экрана:
        {
            //и при этом у нас есть текущая линия нарезки
            if (_currentSliceLine != null)
            {
                //то добавляем линию в список линии(в дальнейшем для разрезки)
                _sliceLines.Add(_currentSliceLine);
                //Убираем ссылку на текущую линию
                _currentSliceLine = null;
                //после окончания вырисовывания линии прячем нож
                _knife.SetActive(false);
                //Когда мы закончили рисовать последнюю линию (4 по счету)
                if (_lineRendererManager.GetNumberLines() == 4)
                {
                    Slice();//запускаем нарезку вместо кнопки
                }
            }            
            _isMouseButtonPressed = false;            
        }
    }

    /*
     * Нарезка
     * -запускается после проведения 4 линии нарезки
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
