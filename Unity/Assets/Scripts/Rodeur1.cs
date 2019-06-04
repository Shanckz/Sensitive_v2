using UnityEngine;
using UnityEngine.AI;

public class Rodeur1 : MonoBehaviour
{
    #region Variables
    [SerializeField]
    protected GameObject activation;
    protected bool active;
    [SerializeField]
    protected GameObject firstPos;
    private NavMeshAgent Agent;
    private float beginTimer;
    [SerializeField]
    private float durationTimer = 2f;
    [SerializeField]
    protected float killDistance = 1.0f;
    protected bool wasInactive;
    protected bool wasActive;
    public static bool unactiveRodeur1;
    protected Transform posPlayer;
    protected GameObject footPlayer;
    #endregion

    protected enum etat
    {
        attente,
        firstActivation,
        poursuitePlayer,
    }
    protected etat myEtat;

    Animator m_animator;

    private void Start()
    {
        unactiveRodeur1 = false;
        active = false;
        Agent = GetComponent<NavMeshAgent>();
        wasInactive = false;
        wasActive = false;
        m_animator = GetComponentInChildren<Animator>();
    }

    void Update ()
    {
        if (wasActive == false)
        {
            if (activation.activeInHierarchy == true)
            {
                Debug.Log("WasActive");
                wasActive = true;
            }
        }
        if(wasInactive == false && wasActive == true)
        {
            if(activation.activeInHierarchy == false)
            {
                Debug.Log("WasInactive");
                wasInactive = true;
            }
        }
        if(unactiveRodeur1 == false && wasInactive)
        {
            Activation();
            if (active == true)
            {
                SelectEtat();
            }
        }
        if(unactiveRodeur1)
        {
            Destroy(this.gameObject);
        }
	}

    void Activation()
    {
        if (active == false)
        {
            active = true;
            myEtat = etat.firstActivation;
            footPlayer = GameObject.FindGameObjectWithTag("PlayerFoot");
        }
    }

    void SelectEtat()
    {
        if (myEtat == etat.firstActivation)
        {
            FirstActivation();
        }
        if(myEtat == etat.attente)
        {
            Attente();
        }
        if(myEtat == etat.poursuitePlayer)
        {
            PoursuitePlayer();
        }
        if(unactiveRodeur1 == true)
        {
            gameObject.SetActive(false);
        }
    }

    void FirstActivation()
    {
        Agent.Warp(firstPos.transform.position);
        Agent.updateRotation = false;
        Agent.transform.LookAt(footPlayer.transform);
        myEtat = etat.attente;
        beginTimer = Time.time;
    }

    void Attente()
    {
        m_animator.SetTrigger("Awake");
        if (Time.time > beginTimer + durationTimer)
        {
            myEtat = etat.poursuitePlayer;
        }
    }

    void PoursuitePlayer()
    {
        m_animator.SetTrigger("Run");
        Agent.updateRotation = true;
        posPlayer = footPlayer.transform;
        Agent.SetDestination(posPlayer.position);
        Agent.stoppingDistance = killDistance;
        if (Agent.hasPath && Agent.remainingDistance < killDistance && DeathManager.deathPlayer == false)
        {
            Debug.Log("Le joueur est mort");
            DeathManager.deathPlayer = true;
        }
    }
}