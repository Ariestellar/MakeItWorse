using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] private BookSorter _bookSorter;
    //Расстояние на котором отображается книга в руке
    private float _distanceViewBookInHand = -5;
    private GameObject _viewBookInHand;    
    private Book _bookInHand;    
    private Camera _camera;    

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
                    if (_bookInHand == null)//Если первый раз коснулись книги
                    {                        
                        //Создаем отображение книги в руке
                        _viewBookInHand = Instantiate(hit.transform.gameObject);
                        _viewBookInHand.layer = 2;//Взятая в руки книга игнорирует райкаст

                        //Прячем книгу с полки
                        _bookInHand = hit.transform.gameObject.GetComponent<Book>();
                        _bookInHand.HideFromShelf();

                        //Передаем ссылку на книгу взятую с полки в сортировщик книг
                        _bookSorter.SetBookInHand(_bookInHand);
                    }
                    //Если тянем не отрывая от экрана указывая на книгу:
                    else
                    {
                        //Вызываем событие "Сменить позицию" у книги на которую указываем
                        hit.transform.gameObject.GetComponent<Book>().ChangePosition();
                        
                    }
                }
                else//Если тянем не отрывая от экрана указывая на что угодно кроме книги:
                {
                    //Если в руках книга:
                    if (_viewBookInHand != null)
                    {
                        //Меняем ей позицию 
                        _viewBookInHand.transform.position = new Vector3(hit.point.x, hit.point.y, _distanceViewBookInHand);
                    }
                }
            }
        }
        else//Если оторвали от экрана палец
        {
            //Если в руках книга:
            if (_viewBookInHand != null)
            {
                Destroy(_viewBookInHand);//Уничтожаем ее отображение в руке
                _bookInHand.ShowOnShelf();//Показываем книгу на полке

                _bookInHand = null;
            }            
        }
    }
}
