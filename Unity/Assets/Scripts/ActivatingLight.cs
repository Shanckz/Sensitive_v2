using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatingLight : MonoBehaviour
{

    [Header("Toutes les lights dans l'ordre de spawn")]
    public GameObject[] LightPrefab;
    [Header("Le mesh des lights")]
    public GameObject[] MeshLight;
    [Header("L'emissive material pour les lights")]
    public Material[] EmissiveLight;
    [Space]
    [Header("Lights Info")]
    public float[] TempsEntreChaqueLight;
    [Space]
    [Header("Le Tag associé à ton player")]
    public string PlayerTag;

    [Header("FX")]
    [SerializeField] GameObject m_lightOnFx;


    private void Awake()
    {
        for (int i = 0, l = LightPrefab.Length; i < l; ++i)
        {
            if(LightPrefab[i] != null)
            {
                if (LightPrefab[i].activeSelf)
                {
                    LightPrefab[i].SetActive(false);
                }
            }
        }
    }


    bool oneTime;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag) && !oneTime)
        {
            SpawnLights();
            oneTime = true;
        }
    }




    void SpawnLights()
    {
        StartCoroutine(SpawnLightWithTime());
    }

    IEnumerator SpawnLightWithTime()
    {
        for (int i = 0, l = LightPrefab.Length; i < l; ++i)
        {
            if (LightPrefab[i] != null)
            {
                yield return new WaitForSeconds(TempsEntreChaqueLight[i]);
                LightPrefab[i].SetActive(true);
                Level.AddFX(m_lightOnFx, LightPrefab[i].transform.position, LightPrefab[i].transform.rotation);
                MeshLight[i].GetComponent<MeshRenderer>().material = EmissiveLight[i];

            }
        }
        StopCoroutine(SpawnLightWithTime());
    }

}
