using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotSoundGenerator : MonoBehaviour {

    [Header("Sounds array")]
    [SerializeField] OneShotSound[] m_oneShotSounds = new OneShotSound[3];

    //OneShotSound m_oneShotSound = new OneShotSound();
    [System.Serializable]
    public class OneShotSound
    {
        [Range (0, 100)] public float m_percentage = 50;
        public GameObject[] m_sounds = new GameObject[1];
    }

    [Header("Timers")]
    [SerializeField] float m_minTimer = 5;
    [SerializeField] float m_maxTimer = 10;

    private void Start()
    {
        StartCoroutine(WaitForNewSound());
    }
    IEnumerator WaitForNewSound()
    {
        float i = NextSound();
        Debug.Log("New sound in: " + i);
        yield return new WaitForSeconds(i);
        Debug.Log("NEW SOUND");
        StartSound();
    }
    private void StartSound()
    {
        GameObject objToInstantiate = ChoseSound(ChoseSoundType());
        Level.AddFX(objToInstantiate, Vector3.zero, Quaternion.identity);

        StartCoroutine(WaitForNewSound());
    }

#region Return function
    private float NextSound()
    {
        return Random.Range(m_minTimer, m_maxTimer);
    }
    private int ChoseSoundType()
    {
        float f = Random.Range(0, 100);
        Debug.Log("Random.Range(0, 100) = " + f);
        if(f < m_oneShotSounds[0].m_percentage)
        {
            Debug.Log("return 0");
            return 0;
        }
        else if (f > m_oneShotSounds[0].m_percentage && f < m_oneShotSounds[0].m_percentage + m_oneShotSounds[1].m_percentage)
        {
            Debug.Log("return 1");
            return 1;
        }
        else if (f > m_oneShotSounds[0].m_percentage + m_oneShotSounds[1].m_percentage)
        {
            Debug.Log("return 2");
            return 2;
        }

        Debug.Log("return MERDE");
        return 0;
    }
    private GameObject ChoseSound(int soundArrayNb)
    {
        int alea = Random.Range(0, m_oneShotSounds[soundArrayNb].m_sounds.Length);
        Debug.Log("ChoseSound alea = " + alea);
        return m_oneShotSounds[soundArrayNb].m_sounds[alea];
    }
#endregion Return function

}
