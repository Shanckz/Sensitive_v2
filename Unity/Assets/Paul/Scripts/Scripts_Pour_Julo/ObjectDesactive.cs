using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDesactive : MonoBehaviour
{

    public GameObject objectToDisable;
    public GameObject objectToAble;
    private bool actived = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag) && !actived)
        {
            objectToDisable.SetActive(false);
            objectToAble.SetActive(true);
            actived = true;
           
        }
    }
}

