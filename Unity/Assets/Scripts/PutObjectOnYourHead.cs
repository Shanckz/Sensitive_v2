using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PutObjectOnYourHead : MonoBehaviour {

    public Event[] m_events;

    Event m_event = new Event();
    [System.Serializable] public class Event
    {
        public float m_timer;
        public UnityEvent m_event;
    }

    SenseManager m_senseManager;
    VRTK.VRTK_InteractableObject m_objectToDisable;

    bool m_isEnter = false;
    float m_timer = 0;

    private void Start()
    {
        m_senseManager = SenseManager.Instance;
        m_objectToDisable = GetComponent<VRTK.VRTK_InteractableObject>();
    }

    private void Update()
    {
        if (m_isEnter)
        {
            m_timer += Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("PlayerNeck") && !m_isEnter)
        {
            //print("caca");
            m_isEnter = true;
            StartCoroutine(PlayerInTrigger());
            /*m_objectToDisable.isGrabbable = false;
            m_objectToDisable.GetComponent<Rigidbody>().isKinematic = true;
            m_objectToDisable.gameObject.SetActive(false);
            m_senseManager.ChangeSense();
            Destroy(m_objectToDisable.gameObject);*/
        }
    }

    IEnumerator PlayerInTrigger()
    {
        for(int i = 0, l = m_events.Length; i < l; ++i)
        {
            if(m_events[i].m_timer > m_timer)
            {
                m_events[i].m_event.Invoke();
            }
            yield return null;
        }
    }

    public void ObjectIsGrabbable(bool isGrabbable)
    {
        m_objectToDisable.isGrabbable = isGrabbable;
    }

    public void ChangeSense()
    {
        m_senseManager.SetCanChangeSense(true);
        m_senseManager.ChangeSense();
    }

    public void SetMovement(bool canMove)
    {
        m_senseManager.SetMovement(canMove);
    }

}
