﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour {

    public GameObject impactFX;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
        {
            Level.AddFX(impactFX, transform.position, transform.rotation);
        }
    }
}
