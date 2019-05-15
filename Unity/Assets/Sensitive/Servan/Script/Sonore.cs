using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonore : MonoBehaviour
{

    public float niveauSonore;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            niveauSonore = 100;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            niveauSonore = 0;
        }
    }
}
