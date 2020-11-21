using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Компонент управляет движением объекта когда тот находится "в руке"
public class ObjectInHandMovement : MonoBehaviour
{
    private IChecker _orderCheker;
    [SerializeField] private int _numberObjectsToBePlaced;
    [SerializeField] private float _distanceFromCamera;
    [SerializeField] private GameObject _briffingPanel;
    
    private ObjectInHand _bookInHand;
    private PlaceForObject _bookEmpty;
    private int _numberBook;
    private Camera _camera;  

    private void Awake()
    {
        _orderCheker = GetComponent<IChecker>();
        _camera = Camera.main;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            _briffingPanel.SetActive(false);
            RaycastHit hit;
            Ray rayAttack = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayAttack, out hit) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                if (hit.transform.gameObject.GetComponent<ObjectInHand>())
                {
                    if (_bookInHand == null)//Если первый раз коснулись книги
                    {          
                        //Берем объект в руку
                        _bookInHand = hit.transform.gameObject.GetComponent<ObjectInHand>();                        
                        _bookInHand.gameObject.layer = 2;

                    }                    
                }
                else//Если тянем не отрывая от экрана указывая на что угодно кроме книги:
                {
                    //Если объект в руках:
                    if (_bookInHand != null)
                    {
                        //Меняем ему позицию 
                        _bookInHand.transform.position = new Vector3(hit.point.x, hit.point.y, _distanceFromCamera);

                        if (hit.transform.gameObject.GetComponent<PlaceForObject>())
                        {
                            if (hit.transform.gameObject.GetComponent<PlaceForObject>().IsFilled != true)
                            {
                                if (_bookEmpty != null)
                                {
                                    _bookEmpty.EmptyPlace();
                                    _bookEmpty = null;
                                }
                                _bookEmpty = hit.transform.gameObject.GetComponent<PlaceForObject>();
                                _bookInHand.gameObject.SetActive(false);
                                _bookEmpty.PutBookInPlace(_bookInHand.GetComponent<MeshRenderer>().materials, _bookInHand.ColorType);
                            }                            
                        }
                        else
                        {
                            if (_bookEmpty == true)
                            {
                                _bookEmpty.EmptyPlace();
                                _bookInHand.gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
        else//Если оторвали от экрана палец
        {
            //Если в руках объект:
            if (_bookInHand != null)
            {                                
                _bookInHand.gameObject.layer = 0;
                _bookInHand = null;
                if (_bookEmpty.IsFilled)
                {
                    _numberBook += 1;
                    if (_numberBook == _numberObjectsToBePlaced)
                    {
                        _orderCheker.Check();
                    }
                }
                _bookEmpty = null;
            }            
        }
    }
}
