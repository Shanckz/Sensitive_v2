using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumiTimer : MonoBehaviour
{
    [SerializeField]
    protected bool enableLight = false;

    [SerializeField]
    protected GameObject[] listLight;
    protected int currentLight = 0;

    [SerializeField]
    protected GameObject player;

    protected bool disableScript = false;

    protected float timeLastLight;
    protected float delay = 0.5f;

    private void Update()
    {
        if (enableLight == true && Time.time > timeLastLight + delay && disableScript == false)
        {
            listLight[currentLight].SetActive(true);
            currentLight += 1;
            timeLastLight = Time.time;
            if (currentLight >= listLight.Length)
            {
                disableScript = true;
            }
        }
        if (Vector3.Distance(transform.position, player.transform.position) < 3 && enableLight == false)
        {
            enableLight = true;
            timeLastLight = Time.time;
            listLight[currentLight].SetActive(true);
            currentLight += 1;
        }
    }
}
