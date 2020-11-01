using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _pushingElement;
    //Сила толкания домино
    private float _pushingForce = 200;
    private GameObject _dominoInHand;
    private Camera _camera;
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
                if (hit.transform.gameObject.tag == "Domino")
                {
                    if (_dominoInHand == null)//Если первый раз коснулись книги
                    {
                        _dominoInHand = hit.transform.gameObject;
                        _dominoInHand.layer = 2;//Взятая в руки домино игнорирует райкаст
                    }                   
                }
                else//Если тянем не отрывая от экрана указывая на что угодно:
                {
                    //Если домино в руках:
                    if (_dominoInHand != null)
                    {
                        //Меняем ей позицию 
                        _dominoInHand.transform.position = new Vector3(hit.point.x, 0, hit.point.z);

                        if (hit.transform.gameObject.tag == "LayingArea")
                        {                            
                            if (_dominoInHand.transform.rotation.x < 0.3f)
                            {
                                _dominoInHand.transform.Rotate(Vector3.right, 2.5f);
                            }
                            else
                            {                                                              
                                _dominoInHand = null;
                            }
                            
                        }
                    }
                }
            }
        }
        else//Если оторвали от экрана палец
        {
            //Если домино в руке:
            if (_dominoInHand != null)
            {
                _dominoInHand.layer = 0;
                _dominoInHand = null;
            }
        }
    }

    //Висит на кнопке запуска
    public void RunDominoPrinciple()
    {
        _pushingElement.GetComponent<Rigidbody>().isKinematic = false;
        _pushingElement.GetComponent<Rigidbody>().AddForce(Vector3.forward * _pushingForce);
    }
}
