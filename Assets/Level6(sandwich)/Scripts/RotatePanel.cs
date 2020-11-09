using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatePanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject _tutorPanel;
    [SerializeField] private Rigidbody _sandwich;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _tutorPanel.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.delta.y > 0)
        {
            _sandwich.transform.Rotate(Vector3.right, 5);
        }
        else
        {
            _sandwich.transform.Rotate(Vector3.right, -5);
        }        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _sandwich.isKinematic = false;
    }
}
