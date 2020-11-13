using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorReachCheck : MonoBehaviour
{    
    [SerializeField] private CheckinGameResults _checkinGameResults;
    [SerializeField] private Animator _jamAnimator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            RayCast();
            //StartCoroutine(DelayRayCast());
        }
    }

    public void RayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit))
        {
            _jamAnimator.enabled = true;
            _checkinGameResults.Checking(hit.transform.gameObject.tag);             
        }
        else
        {
            _checkinGameResults.Checking("Null");
        }
    }

    private IEnumerator DelayRayCast()
    {
        yield return new WaitForSeconds(1);
        RayCast();
    }
}
