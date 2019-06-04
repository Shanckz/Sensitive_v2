using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Errant1 : MonoBehaviour
{
    public static bool unactiveErrant1;
    [SerializeField]
    protected GameObject[] waypoints;
    protected List<GameObject> waypointsUncheck;
    protected GameObject player;
    public static GameObject ciblePrincipaleErrant1;
    [SerializeField]
    protected float distanceMaxZone1 = 20;
    [SerializeField]
    protected float distanceMaxZone2 = 100;
    private NavMeshAgent Agent;
    [SerializeField]
    protected float distanceStopPlayer = 1.5f;
    [SerializeField]
    protected GameObject potentialNextWaypoint;
    protected bool pointreached;
    protected bool attenteOK;
    protected bool haveStartWaitTime;
    protected float startWaitTime;
    [SerializeField]
    protected float waitingTime;
    protected Vector3 lastPosPlayer;
    protected bool frame1skip;
    [SerializeField]
    protected float playerDistBtwTwoFrame = 2f;
    protected float playerDist;
    protected bool cibleIsPlayer;
    [SerializeField]
    protected float killDistance = 1.5f;


    protected enum etat
    {
        patrouille,
        poursuite,
    }
    [SerializeField]
    protected etat myEtat;

	void Start ()
    {
        frame1skip = false;
        unactiveErrant1 = false;
        Agent = GetComponent<NavMeshAgent>();
        waypointsUncheck = new List<GameObject>();
        myEtat = etat.patrouille;
        foreach (var waypoint in waypoints)
        {
            waypointsUncheck.Add(waypoint);
        }
        pointreached = false;
        attenteOK = false;
        haveStartWaitTime = false;
        cibleIsPlayer = false;
        Agent.stoppingDistance = 0;
    }

    void FixedUpdate()
    {
        if (player && lastPosPlayer == Vector3.zero)
        {
            lastPosPlayer = player.transform.position;
            cibleIsPlayer = true;
            myEtat = etat.poursuite;
        }
        playerDist = Vector3.Distance(lastPosPlayer, player.transform.position);
        lastPosPlayer = player.transform.position;
    }

    void Update ()
    {
        if (frame1skip)
        {
            player = GameObject.FindGameObjectWithTag("PlayerFoot");
            if (unactiveErrant1 == false)
            {
                SelectEtat();
                MortPlayer();
                Debug.Log(Vector3.Distance(transform.position, player.transform.position));
            }
        }
        if (!frame1skip)
        {
            frame1skip = true;
        }
	}

    void SelectEtat()
    {
        if(myEtat == etat.patrouille)
        {
            Patrouille();
            Detection();
        }
        if(myEtat == etat.poursuite)
        {
            Detection();
            Poursuite();
        }
    }

    void Patrouille()
    {
        Agent.stoppingDistance = 0;
        if (!Agent.hasPath)
        {
            pointreached = true;
        }
        if (pointreached == true && attenteOK == false && haveStartWaitTime == false)
        {
            startWaitTime = Time.time;
            haveStartWaitTime = true;
        }
        if (pointreached == true && attenteOK == false && haveStartWaitTime == true)
        {
            if(Time.time > startWaitTime + waitingTime)
            {
                attenteOK = true;
            }
        }
        if (pointreached == true && attenteOK == true)
        {
            potentialNextWaypoint = waypointsUncheck[Random.Range(0, waypointsUncheck.Count - 1)];
            Agent.SetDestination(potentialNextWaypoint.transform.position);
            pointreached = false;
            attenteOK = false;
            haveStartWaitTime = false;
            if(waypointsUncheck.Count == 1)
            {
                foreach (var waypoint in waypoints)
                {
                    waypointsUncheck.Add(waypoint);
                }
            }
            waypointsUncheck.Remove(potentialNextWaypoint);
        }
    }

    void Detection()
    {
        if(Vector3.Distance(player.transform.position, this.gameObject.transform.position) <= distanceMaxZone1)
        {
            Agent.SetDestination(player.transform.position);
            cibleIsPlayer = true;
            myEtat = etat.poursuite;
            pointreached = false;
            attenteOK = false;
            haveStartWaitTime = false;
        }
        else
        {
            if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) <= distanceMaxZone2)
            {
                if (playerDist >= playerDistBtwTwoFrame)
                {
                    Agent.SetDestination(player.transform.position);
                    cibleIsPlayer = true;
                    myEtat = etat.poursuite;
                    pointreached = false;
                    attenteOK = false;
                    haveStartWaitTime = false;
                }
            }
            if(ciblePrincipaleErrant1 != null)
            {
                Agent.SetDestination(ciblePrincipaleErrant1.transform.position);
                cibleIsPlayer = false;
                myEtat = etat.poursuite;
                pointreached = false;
                attenteOK = false;
                haveStartWaitTime = false;
            }
        }
    }

    void Poursuite()
    {
        Agent.stoppingDistance = distanceStopPlayer;
        if (!Agent.hasPath)
        {
            if(cibleIsPlayer == false && ciblePrincipaleErrant1 != null)
            {
                ciblePrincipaleErrant1 = null;
            }
            pointreached = true;
            if (pointreached == true && attenteOK == false && haveStartWaitTime == false)
            {
                startWaitTime = Time.time;
                haveStartWaitTime = true;
            }
            if (pointreached == true && attenteOK == false && haveStartWaitTime == true)
            {
                if (Time.time > startWaitTime + waitingTime)
                {
                    attenteOK = true;
                }
            }
            if (pointreached == true && attenteOK == true)
            {
                pointreached = false;
                attenteOK = false;
                haveStartWaitTime = false;
                myEtat = etat.patrouille;
            }
        }
    }

    void MortPlayer()
    {
        if (Agent.hasPath && Vector3.Distance(transform.position, player.transform.position) < killDistance && DeathManager.deathPlayer == false && cibleIsPlayer == true)
        {
            Debug.Log("Le joueur est mort");
            DeathManager.deathPlayer = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, killDistance);
        Gizmos.DrawWireSphere(transform.position, distanceMaxZone1);
        Gizmos.DrawWireSphere(transform.position, distanceMaxZone2);
    }
}