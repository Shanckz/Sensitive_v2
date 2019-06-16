using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    public GameObject soundToPlay;
    public string PlayerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            Level.AddFX(soundToPlay, transform.position, transform.rotation);
        }
    }


}
