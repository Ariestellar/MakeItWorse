using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookMovement : MonoBehaviour
{    
    private int _numberBooksDelivered;
    private Vector3 _previousBookPosition;
    private BookInHand _bookInHand;
    private PlaceForBook _bookEmpty;
    private int _numberBook;
    private Camera _camera;    
    [SerializeField] private OrderChecker _orderCheker;    

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray rayAttack = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayAttack, out hit) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                if (hit.transform.gameObject.GetComponent<BookInHand>())
                {
                    if (_bookInHand == null)//Если первый раз коснулись книги
                    {          
                        //Берем книгу в руку
                        _bookInHand = hit.transform.gameObject.GetComponent<BookInHand>();                        
                        _bookInHand.gameObject.layer = 2;

                    }                    
                }
                else//Если тянем не отрывая от экрана указывая на что угодно кроме книги:
                {
                    //Если в руках книга:
                    if (_bookInHand != null)
                    {
                        //Меняем ей позицию 
                        _bookInHand.transform.position = new Vector3(hit.point.x, hit.point.y, -1.5f);

                        if (hit.transform.gameObject.GetComponent<PlaceForBook>())
                        {
                            if (hit.transform.gameObject.GetComponent<PlaceForBook>().IsFilled != true)
                            {
                                if (_bookEmpty != null)
                                {
                                    _bookEmpty.EmptyPlace();
                                    _bookEmpty = null;
                                }
                                _bookEmpty = hit.transform.gameObject.GetComponent<PlaceForBook>();
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
            //Если в руках книга:
            if (_bookInHand != null)
            {                                
                _bookInHand.gameObject.layer = 0;
                _bookInHand = null;
                if (_bookEmpty.IsFilled)
                {
                    _numberBook += 1;
                    if (_numberBook == 4)
                    {
                        _orderCheker.Check();
                    }
                }
                _bookEmpty = null;
            }
            
        }
    }
}
