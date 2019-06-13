using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCarte : MonoBehaviour
{
    [SerializeField]
    protected GameObject hs;
    [SerializeField]
    protected GameObject es;
    [SerializeField]
    protected GameObject open;

    void Start ()
    {
        hs.SetActive(true);
        es.SetActive(false);
        open.SetActive(false);
	}
	
	void Update ()
    {
		if(hs.activeSelf == true && openDoorLevier1.levierActived == true)
        {
            hs.SetActive(false);
            es.SetActive(true);
        }
        if(es.activeSelf == true && openDoorKey1.bureauButtonActived == true)
        {
            es.SetActive(false);
            open.SetActive(true);
        }
	}
}
