using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallMovement : MonoBehaviour
{    
    private Ball _ballInHand;        
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
                if (hit.transform.gameObject.GetComponent<Ball>())
                {
                    if (_ballInHand == null)//Если первый раз коснулись мяча
                    {
                        //Берем мяч в руку
                        _ballInHand = hit.transform.gameObject.GetComponent<Ball>();
                        _ballInHand.GetComponent<Rigidbody>().isKinematic = true;
                        _ballInHand.gameObject.layer = 2;

                    }
                }
                else//Если тянем не отрывая от экрана указывая на что угодно кроме мяча:
                {
                    //Если в руках мяч:
                    if (_ballInHand != null && _ballInHand.IsFall != true)
                    {
                        //Меняем ему позицию 
                        _ballInHand.transform.position = new Vector3(hit.point.x, -0.5f, 1.25f);  
                    }
                }
            }
        }
        else//Если оторвали от экрана палец
        {
            //Если в руках шар:
            if (_ballInHand != null)
            {
                _ballInHand.GetComponent<Rigidbody>().isKinematic = false;
                _ballInHand.gameObject.layer = 0;
                _ballInHand = null;                           
            }
        }
    }
}
