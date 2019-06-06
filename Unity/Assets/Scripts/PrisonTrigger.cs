using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrisonTrigger : MonoBehaviour

{

    public GameObject _light;
    [SerializeField]
    protected GameObject meshLight;
    [SerializeField]
    protected Material lightMaterialwithEmissive;
    [SerializeField]
    protected Material lightMaterialwithoutEmissive;
    [SerializeField] private bool actived = false;
    [SerializeField] private float offTime = 0;
    [SerializeField] private float timer = 5.0f;
    //[SerializeField] string PlayerTag;

    [Header("Objet a activer / désactiver")]
    public GameObject objectToDisable;
    public GameObject objectToAble;
    public float TimerObjet;

    [Header("FX")]
    [SerializeField] GameObject m_screamerFx;

    bool screamerIsPlayed = false;

    private void Update()
    {
        if (!_light.activeSelf && actived)
        {
            offTime += Time.deltaTime;

            if (offTime > timer)
            {
                _light.SetActive(true);
                meshLight.GetComponent<MeshRenderer>().material = lightMaterialwithEmissive;

                if (!screamerIsPlayed)
                {
                    screamerIsPlayed = true;
                    Level.AddFX(m_screamerFx, transform.position, transform.rotation);
                }
            }
            if (offTime > TimerObjet)
            {
                objectToDisable.SetActive(false);
                objectToAble.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerFoot") && !actived)
        {
            _light.SetActive(false);
            meshLight.GetComponent<MeshRenderer>().material = lightMaterialwithoutEmissive;
            actived = true;
        }
    }
}
