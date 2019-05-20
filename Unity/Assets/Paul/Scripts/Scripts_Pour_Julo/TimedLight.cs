using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedLight : MonoBehaviour

{

    public GameObject _light;
    [SerializeField] private bool actived = false;
    [SerializeField] private float offTime = 0;
    [SerializeField] private float timer = 5.0f;
    //[SerializeField] string PlayerTag;
    [SerializeField] UnityEvent m_event;

    private void Update()
    {
        if (!_light.activeSelf)
        {
            offTime += Time.deltaTime;

            if (offTime > timer)
            {
                _light.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tag) && !actived)
        {
            _light.SetActive(false);
            actived = true;
        }
    }
}
