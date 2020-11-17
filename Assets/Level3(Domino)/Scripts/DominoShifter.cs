using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LevelManager))]
public class DominoShifter : MonoBehaviour
{
    [SerializeField] private List<GameObject> _dominos;
    private LevelManager _levelManager;
    private Camera _camera;
    [SerializeField] private CameraMovement _cameraMovement;
    private GameObject _dominoInHand;

    private void Awake()
    {
        _camera = Camera.main;
        _cameraMovement = Camera.main.GetComponent<CameraMovement>();
        _levelManager = GetComponent<LevelManager>();
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
                    if (_dominoInHand == null)//Если первый раз коснулись домино
                    {
                        _cameraMovement.SetTarget(hit.transform.gameObject.transform.position);
                        _dominoInHand = hit.transform.gameObject;
                        _dominoInHand.layer = 2;//Взятая в руки домино игнорирует райкаст
                        TurnOffRestDominoes();//все остальные домино игнорируют райкаст
                        _dominoInHand.GetComponent<Rigidbody>().isKinematic = false;
                    }
                }
                else//Если тянем не отрывая от экрана указывая на что угодно:
                {
                    //Меняем ей позицию 
                    _dominoInHand.transform.position = new Vector3(hit.point.x, 0, hit.point.z);

                    if (hit.transform.gameObject.tag == "LayingArea")
                    {
                        if (_dominoInHand.transform.rotation.x > -0.01f)
                        {
                            _dominoInHand.transform.Rotate(Vector3.left, 2.5f);                            
                        }
                        else
                        {
                            _dominoInHand = null;
                            StopCarryingDominoes();
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
                StopCarryingDominoes();
            }
        }
    }

    private void TurnOffRestDominoes()
    {
        //_dominos.ForEach(domino => domino.layer = 2);
        _dominos.ForEach(domino => domino.GetComponent<Rigidbody>().isKinematic = true);
    }

    private void StopCarryingDominoes()
    {
        _cameraMovement.ShiftCamera();
        _dominos.ForEach(domino => domino.GetComponent<Rigidbody>().isKinematic = false);
        _levelManager.SetDominos(_dominos);
        StartCoroutine(_levelManager.StartFallTimer());        
        this.enabled = false;
    }
}
