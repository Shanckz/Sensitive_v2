using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    //public GameObject[] Light;
    public bool _isFlickering;
    Light _light;
    private float timer;
    [Header("Light Mesh")]
    public GameObject _lightMaterial;
    [Space]
    [Header("Light On")]
    public Material _emissive_On;
    [Space]
    [Header("Light Off")]
    public Material _emissive_Off;

    void Start()
    {
        _light = GetComponent<Light>();
        if (_isFlickering)
        {
            StartCoroutine(LightFlickering());
        }
    }

    IEnumerator LightFlickering()
    {
        _light.intensity = 1;
        _lightMaterial.GetComponent<MeshRenderer>().material = _emissive_On;
        timer = Random.Range(1f, 3);
        yield return new WaitForSeconds(timer);
        _light.intensity = 0;
        _lightMaterial.GetComponent<MeshRenderer>().material = _emissive_Off;
        timer = Random.Range(1f, 4);
        yield return new WaitForSeconds(timer);
        StartCoroutine(LightFlickering());
    }
}
