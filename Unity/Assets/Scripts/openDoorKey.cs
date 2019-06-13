﻿using UnityEngine;

public class openDoorKey : MonoBehaviour
{
    [SerializeField]
    protected GameObject porte;
    protected bool isLocked;
    [SerializeField]
    protected float vitesse = 0.02f;
    protected Vector3 positionFermee;
    protected Vector3 positionOuverte;
    [SerializeField]
    protected float hauteurPorteOuverte;
    protected bool keyCheck;

    private void Start()
    {
        keyCheck = false;
        isLocked = false;
        positionFermee = porte.transform.position;
        positionOuverte = new Vector3(porte.transform.position.x, porte.transform.position.y + hauteurPorteOuverte, porte.transform.position.z);
        porte.transform.position = positionFermee;
    }

    private void Update()
    {
        if (isLocked == false && keyCheck == true)
        {
            porte.transform.position = new Vector3(porte.transform.position.x, porte.transform.position.y + vitesse, porte.transform.position.z);
            if (porte.transform.position.y > positionOuverte.y)
            {
                porte.transform.position = new Vector3(porte.transform.position.x, positionOuverte.y, porte.transform.position.z);
                isLocked = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Key") && Errant1.unactiveErrant1 == false)
        {
            keyCheck = true;
        }
    }
}
