using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class move : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    private Vector3 mov;
    private int x1;
    private int x2;
    private int y1;
    private int y2;
    private int v = 4;
    public int niveauSonore;

	void Start ()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            x1 = -1;
        }
        else x1 = 0;
        if (Input.GetKey(KeyCode.D))
        {
            x2 = 1;
        }
        else x2 = 0;
        if (Input.GetKey(KeyCode.Z))
        {
            y1 = 1;
        } else y1 = 0;
        if (Input.GetKey(KeyCode.S))
        {
            y2 = -1;
        } else y2 = 0;
        playerAgent.velocity = new Vector3(x1 + x2, 0, y1 + y2)*v;
        if(playerAgent.velocity != Vector3.zero)
        {
            niveauSonore = 50;
        }
        else
        {
            niveauSonore = 10;
        }
	}
}
