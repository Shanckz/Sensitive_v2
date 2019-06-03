using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SenseManager : MonoBehaviour {

#region Singleton
	public static SenseManager Instance;

	void Awake(){
		if(Instance == null){
			Instance = this;
		}else{
			Debug.LogError("Two instance of SenseManager");
		}
	}
#endregion //Singleton
	
	[Header("Sense")]
	[SerializeField] SenseTypes m_startSense = SenseTypes.Nothing;
	public enum SenseTypes {
		Nothing,
		See,	
		Hear
	}
	[SerializeField] bool m_nextSenseIsSeeIfIStartWithNothingSense = true;
    [SerializeField] float m_timerToChangeSense = 1;

	[Header("Post processing")]
	[SerializeField] GameObject m_seePostProcessing;
	[SerializeField] GameObject m_notSeePostProcessing;

	[Header("Audio mixers")]
	[SerializeField] AudioMixerGroup m_hearAudioMixer;
	[SerializeField] AudioMixerGroup m_notHearAudioMixer;

    [Header("Ambiance sound")]
	[SerializeField] AudioSource[] m_ambianceMusic;

    [Header("Animators")]
    [SerializeField] Animator m_eyesAnimator;

#region Encapsulate
	public AudioMixerGroup HearAudioMixer
    {
        get
        {
            return m_hearAudioMixer;
        }
    }
    public AudioMixerGroup NotHearAudioMixer
    {
        get
        {
            return m_notHearAudioMixer;
        }
    }
#endregion //Encapsulate

	bool m_iHearCorrectly = false;
    bool m_myActualSenseIsSee = false;
    bool m_myActualSenseIsHear = false;

	List<FX> m_fx = new List<FX>();

    VRTK.VRTK_MoveInPlace m_moveInPlace;

	public void AddFxInList(FX fx){
		m_fx.Add(fx);
	}
	public void RemoveFxInList(FX fx){
		m_fx.Remove(fx);
	}

    void Start(){
		switch(m_startSense){ 
			case SenseTypes.Nothing:
				CanISeeCorectly(false);
				CanHearCorectly(false);

				if(m_nextSenseIsSeeIfIStartWithNothingSense){
					ActualSenseIsSee(false);
				}else{
					ActualSenseIsSee(true);
				}

				m_iHearCorrectly = false;
			break;
			case SenseTypes.See:
				CanISeeCorectly(true);
				CanHearCorectly(false);

				ActualSenseIsSee(true);
			break;
			case SenseTypes.Hear:
				CanISeeCorectly(false);
				CanHearCorectly(true);

				ActualSenseIsSee(false);
			break;
		}
	}

	void Update(){
        //if(Input.GetButtonDown("Oculus_CrossPlatform_Button2")){
        if(Input.GetKeyDown(KeyCode.Space)){
            ChangeSense();
		}
	}

	public void ChangeSense(){
        StartCoroutine(ChangeSenseTimer());
        m_eyesAnimator.SetTrigger("Go");
    }

    IEnumerator ChangeSenseTimer()
    {
        yield return new WaitForSeconds(m_timerToChangeSense);
        DoChangeSense();
    }

    void DoChangeSense()
    {
        m_myActualSenseIsSee = !m_myActualSenseIsSee;
        m_myActualSenseIsHear = !m_myActualSenseIsHear;

        if (m_myActualSenseIsSee)
        {
            CanISeeCorectly(true);
            CanHearCorectly(false);

            m_iHearCorrectly = false;
        }
        else if (m_myActualSenseIsHear)
        {
            CanISeeCorectly(false);
            CanHearCorectly(true);

            m_iHearCorrectly = true;
        }

        if (m_fx != null)
        {
            for (int i = 0, l = m_fx.Count; i < l; ++i)
            {
                if (m_fx[i] != null)
                {
                    m_fx[i].On_SenseChanged();
                }
            }
        }
    }

	void CanISeeCorectly(bool b){
		if(b){
			m_seePostProcessing.SetActive(true);
			m_notSeePostProcessing.SetActive(false);
		}else{
			m_seePostProcessing.SetActive(false);
			m_notSeePostProcessing.SetActive(true);
		}
	}

	void CanHearCorectly(bool b){
		if(b){
			for(int i = 0, l = m_ambianceMusic.Length; i < l; ++i){
				m_ambianceMusic[i].outputAudioMixerGroup = m_hearAudioMixer;
			}
		}else{
			for(int i = 0, l = m_ambianceMusic.Length; i < l; ++i){
				m_ambianceMusic[i].outputAudioMixerGroup = m_notHearAudioMixer;
			}
		}
		
	}

	void ActualSenseIsSee(bool b){
		if(b){
			m_myActualSenseIsSee = true;
			m_myActualSenseIsHear = false;

			m_iHearCorrectly = false;
		}else{
			m_myActualSenseIsSee = false;
			m_myActualSenseIsHear = true;

			m_iHearCorrectly = true;
		}
	}

	public AudioMixerGroup GetAudioMixer(){
		if(m_iHearCorrectly){
			return m_hearAudioMixer;
		}else{
			return m_notHearAudioMixer;
		}
	}

    public void SetVRTK_MoveInPlace(VRTK.VRTK_MoveInPlace moveInPlace)
    {
        m_moveInPlace = moveInPlace;
    }

    public void SetMovement(bool canMove)
    {
        m_moveInPlace.leftController = canMove;
        m_moveInPlace.rightController = canMove;
    }

}
