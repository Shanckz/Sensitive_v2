using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openPorteBureau : MonoBehaviour
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

    void Start ()
    {
        isLocked = false;
        positionFermee = porte.transform.position;
        positionOuverte = new Vector3(porte.transform.position.x + hauteurPorteOuverte, porte.transform.position.y, porte.transform.position.z);
        porte.transform.position = positionFermee;
    }
	
	void Update () {
        if (openDoorKey1.keyCheck)
        {
            if (isLocked == false)
            {
                porte.transform.position = new Vector3(porte.transform.position.x + vitesse, porte.transform.position.y, porte.transform.position.z);
                if (porte.transform.position.x > positionOuverte.x)
                {
                    porte.transform.position = new Vector3(porte.transform.position.x, positionOuverte.y, porte.transform.position.z);
                    isLocked = true;
                }
            }
        }
    }
}
