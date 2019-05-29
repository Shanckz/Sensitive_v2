using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : MonoBehaviour
{
    [SerializeField]
    protected GameObject porte;
    protected Vector3 positionFermee;
    protected Vector3 positionOuverte;
    [SerializeField]
    protected float vitesse = 0.02f;
    [SerializeField]
    protected float hauteurPorteOuverte;
    [SerializeField]
    protected GameObject LampeAdesactive1;

    protected bool canLockDoor;
    protected bool isLocked;
    protected bool lampUnactive;

	void Start ()
    {
        lampUnactive = false;
        isLocked = false;
        canLockDoor = false;
        positionFermee = porte.transform.position;
        positionOuverte = new Vector3(porte.transform.position.x, porte.transform.position.y + hauteurPorteOuverte, porte.transform.position.z);
        porte.transform.position = positionOuverte;
	}

    private void Update()
    {
        if(canLockDoor == true && isLocked == false)
        {
            if(lampUnactive == false)
            {
                LampeAdesactive1.SetActive(false);
            }
            porte.transform.position = new Vector3(porte.transform.position.x, porte.transform.position.y - vitesse, porte.transform.position.z);
            if(porte.transform.position.y < positionFermee.y)
            {
                porte.transform.position = new Vector3(porte.transform.position.x, positionFermee.y, porte.transform.position.z);
                isLocked = true;
                Rodeur1.unactiveRodeur1 = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFoot"))
        {
            canLockDoor = true;
        }
    }
}
