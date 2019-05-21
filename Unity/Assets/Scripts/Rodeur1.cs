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
    protected GameObject player;
    [SerializeField]
    protected float killDistance = 1.0f;
    [SerializeField]
    protected GameObject death;
    protected bool wasInactive;
    public bool unactive;
    #endregion

    protected enum etat
    {
        attente,
        firstActivation,
        poursuitePlayer,
    }
    protected etat myEtat;

    private void Start()
    {
        unactive = false;
        active = false;
        Agent = GetComponent<NavMeshAgent>();
        wasInactive = false;
    }

    void Update ()
    {
        if(wasInactive == false)
        {
            if(activation.activeInHierarchy == false)
            {
                wasInactive = true;
            }
        }
        if(unactive == false && Agent.isStopped == false && wasInactive)
        {
            Activation();
            if (active == true)
            {
                SelectEtat();
            }
        }
        if(unactive)
        {
            gameObject.SetActive(false);
        }
	}

    void Activation()
    {
        if (active == false)
        {
            if (activation.activeInHierarchy == true)
            {
                active = true;
                myEtat = etat.firstActivation;
            }
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
        if(unactive == true)
        {
            gameObject.SetActive(false);
        }
    }

    void FirstActivation()
    {
        Agent.Warp(firstPos.transform.position);
        Agent.updateRotation = false;
        Agent.transform.LookAt(player.transform.position);
        myEtat = etat.attente;
        beginTimer = Time.time;
    }

    void Attente()
    {
        if (Time.time > beginTimer + durationTimer)
        {
            myEtat = etat.poursuitePlayer;
        }
    }

    void PoursuitePlayer()
    {
        Agent.updateRotation = true;
        Agent.destination = player.transform.position;
        Agent.stoppingDistance = killDistance;
        if (Agent.hasPath && Agent.remainingDistance < killDistance)
        {
            Debug.Log(Agent.remainingDistance);
            Debug.Log("Le joueur est mort");
        }
    }
}