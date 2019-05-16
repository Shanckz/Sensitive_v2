using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FX : MonoBehaviour {

	SenseManager m_senseManager;
	AudioSource m_audioSource;

	void Start(){
		m_senseManager = SenseManager.Instance;
		m_audioSource = GetComponent<AudioSource>();

		m_senseManager.AddFxInList(this);
		m_audioSource.outputAudioMixerGroup = m_senseManager.GetAudioMixer();

		//Invoke("Die", 5);
	}

	public void On_SenseChanged(){
		m_audioSource.outputAudioMixerGroup = m_senseManager.GetAudioMixer();
	}
	
	void Die(){
		m_senseManager.RemoveFxInList(this);
		Destroy(gameObject);
	}
}
