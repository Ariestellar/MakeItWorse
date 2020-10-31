using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] private BookSorter _bookSorter;

    private GameObject _bookInHand;    
    private Camera _camera;
    private bool _isMouseButtonPressed;

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
                if (hit.transform.gameObject.GetComponent<Book>())
                {
                    if (_isMouseButtonPressed == false)//Если первый раз коснулись
                    {                        
                        _isMouseButtonPressed = true;
                        _bookInHand = Instantiate(hit.transform.gameObject);
                        _bookInHand.layer = 2;//Взятая в руки книга игнорирует райкаст 
                        hit.transform.gameObject.GetComponent<Book>().Hidden();
                        _bookSorter.SetBookInHand(_bookInHand.GetComponent<Book>());
                    }
                    else//Если тянем не отрывая от экрана указывая на книгу:
                    {
                        //то книга на которую указываем меняет позицию вправо
                        //hit.transform.position = new Vector3(hit.transform.position.x + 1.2f, hit.transform.position.y, hit.transform.position.z);
                        hit.transform.gameObject.GetComponent<Book>().ChangePosition();//Вызываем событие "Сменить позицию" у книги
                    }
                }
                else//Если тянем не отрывая от экрана указывая на что угодно кроме книги:
                {
                    //Если в руках книга:
                    if (_bookInHand != null)
                    {
                        //Меняем ей позицию 
                        _bookInHand.transform.position = new Vector3(hit.point.x, hit.point.y, -10);
                    }
                }
            }
        }
        else//Если оторвали от экрана палец
        {
            //Если в руках книга:
            if (_bookInHand != null)
            {
                Destroy(_bookInHand);//уничтожаем ее
            }
            _isMouseButtonPressed = false;
        }
    }
}
