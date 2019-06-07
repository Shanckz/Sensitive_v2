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
        InitializeScript();

    }

	public void On_SenseChanged(){
		m_audioSource.outputAudioMixerGroup = m_senseManager.GetAudioMixer();
	}
	
	void Die(){
		m_senseManager.RemoveFxInList(this);
		Destroy(gameObject);
	}

    void InitializeScript()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();     // On récupère le composant ParticleSystem et on le "met" dans une variable (ps) de type ParticleSystem 
        AudioSource audio = GetComponent<AudioSource>();        // De même pour l'AudioSource
        if ((audio != null) && (ps != null))
        {                   // Si le FX possède du son et un effet de particule alors : 
            if (audio.clip.length >= ps.main.duration)
            {           // Si la duré du son est plus longue ou égale à la duré de l'effet de particule alors :
                Invoke("Die", audio.clip.length);   // On détruit le FX lorsque l'audio est fini
            }
            else if (ps.main.duration >= audio.clip.length)
            {   // Sinon si la duré de l'effet de particule est plus longue ou égale à la duré du son alors :
                if (!ps.main.loop)
                {
                    Invoke("Die", ps.main.duration);   // On détruit le FX lorsque l'effet de particule se termine
                }
            }
        }
        else
        {                                                   // S'il manque ou en effet de particule ou du son alors :
            if (audio != null)
            {                                   // Si le FX possède un "AudioSource" alors :
                Invoke("Die", audio.clip.length);   // On détruit le FX à la fin du son
            }
            if (ps != null)
            {
                if (!ps.main.loop)
                {                                   // Si le FX possède un "ParticleSystem" alors :
                    Invoke("Die", ps.main.duration);   // On détruit le gameObject à la fin de l'effet de particule
                }
            }
        }
    }
}
