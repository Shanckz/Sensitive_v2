﻿using UnityEngine;
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
    protected bool wasActive;
    public bool unactive;
    protected Vector3 posPlayer;
    protected GameObject footPlayer;
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
        wasActive = false;
    }

    void Update ()
    {
        if (wasActive == false)
        {
            if (activation.activeInHierarchy == true)
            {
                wasActive = true;
            }
        }
        if(wasInactive == false && wasActive == true)
        {
            if(activation.activeInHierarchy == false)
            {
                wasInactive = true;
            }
        }
        if(unactive == false && wasInactive)
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
        posPlayer = footPlayer.transform.position;
        Agent.destination = posPlayer;
        Agent.stoppingDistance = killDistance;
        if (Agent.hasPath && Agent.remainingDistance < killDistance)
        {
            Debug.Log("Le joueur est mort");
            DeathManager.deathPlayer = true;
        }
    }
}