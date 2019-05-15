using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField]
    protected LayerMask myLayerMask;

    protected Vector3 pos;
    
    private enum Type
    {
        rodeur,
        errant,
        pisteur,
    }
    [SerializeField]
    private Type type;

    [SerializeField]
    private GameObject[] pointInteret;
    private List<GameObject> myPointsInt;
    private int pointInt;

    [SerializeField]
    private GameObject[] objSonore;
    private GameObject plusSonore;

    private bool tempo;
    private float temp;
    [SerializeField]
    private float t;

    #region Metrics IA
    [SerializeField]
    private float Vrodeur;
    [SerializeField]
    private float VrodeurPoursuite;
    [SerializeField]
    private float VrodeurCherche;
    [SerializeField]
    private float VisionRodeur;
    [SerializeField]
    private float Verrant;
    [SerializeField]
    private float VerrantPoursuite;
    [SerializeField]
    private float VerrantCherche;
    [SerializeField]
    private float Vpisteur;
    [SerializeField]
    private float VpisteurPoursuite;
    [SerializeField]
    private float VpisteurCherche;
    #endregion

    [SerializeField]
    private GameObject Player;
    private Transform lastPosP;

    protected List<GameObject> dejaConnu;
    protected List<float> niveauSonoreConnu;

    protected Ray playerIA;

    private enum Etat
    {
        Attente,
        Patrouille,
        Poursuite,
        MiseEnAlerte,
        Cherche,
    }
    private Etat etat;

    [SerializeField]
    private bool Attente;

    [SerializeField]
    private List<GameObject> Objectif;
    private int O;
    [SerializeField]
    private List<GameObject> Activation;

    private Vector3 potentialNextPatDest;
    private NavMeshAgent Agent;

	void Start ()
    {
        Agent = GetComponent<NavMeshAgent>();

        StartEtat();

        SelectMonster();
        myPointsInt = new List<GameObject>();
        plusSonore = Player;
        if(t == 0)
        {
            t = 2;
        }

        O = 0;
    }
	
	void Update ()
    {
        GestionEtat();
        Debug.DrawLine(transform.position, Agent.destination,Color.black);
        pos =  Player.transform.position - transform.position;
    }

    void DetectionRodeur()
    {
        if (-60 < Vector3.Angle(transform.forward, pos) && Vector3.Angle(transform.forward, pos) < 60)
        {
            Debug.DrawRay(transform.position, pos, Color.red);
            RaycastHit hit;
            if(Physics.Raycast(transform.position, pos, out hit))
            {
                if (Physics.Raycast(transform.position, pos, out hit, myLayerMask))
                {
                    if (hit.collider.tag == "Player")
                    {
                        if(hit.distance <= VisionRodeur)
                        {
                            lastPosP = Player.transform;
                            etat = Etat.Poursuite;
                            tempo = true;
                            myPointsInt.Clear();
                        }
                    }
                }
            }
        }
    }
    void DetectionErrant()
    {
        Debug.Log(Player.GetComponent<move>().niveauSonore - Vector3.Distance(transform.position, Player.transform.position));
        foreach (var s in objSonore)
        {
            if(plusSonore != Player && s.GetComponent<Sonore>().niveauSonore - Vector3.Distance(transform.position, s.transform.position) > Mathf.Max(plusSonore.GetComponent<Sonore>().niveauSonore, 30))
            {
                plusSonore = s;
                Agent.destination = plusSonore.transform.position;
                etat = Etat.Poursuite;
            }
            if (plusSonore == Player && s.GetComponent<Sonore>().niveauSonore - Vector3.Distance(transform.position, s.transform.position) > Mathf.Max(plusSonore.GetComponent<move>().niveauSonore, 30))
            {
                plusSonore = s;
                Agent.destination = plusSonore.transform.position;
                etat = Etat.Poursuite;
            }
        }
        if(plusSonore != Player && Player.GetComponent<move>().niveauSonore - Vector3.Distance(transform.position, Player.transform.position) > Mathf.Max(plusSonore.GetComponent<Sonore>().niveauSonore, 30))
        {
            plusSonore = Player;
            Agent.destination = plusSonore.transform.position;
            etat = Etat.Poursuite;
        }
        if(plusSonore == Player && Player.GetComponent<move>().niveauSonore - Vector3.Distance(transform.position, Player.transform.position) > 30)
        {
            Agent.destination = plusSonore.transform.position;
            etat = Etat.Poursuite;
        }
        if(Agent.remainingDistance < 2)
        {
            if (dejaConnu.Contains(plusSonore))
            {
                dejaConnu.Add(plusSonore);
                if(plusSonore != Player)
                {
                    niveauSonoreConnu.Add(plusSonore.GetComponent<move>().niveauSonore);
                }
                else
                {
                    niveauSonoreConnu.Add(plusSonore.GetComponent<Sonore>().niveauSonore);
                }
            }
        }
    }
    void DetectionPisteur()
    {

    }

    void EnPatrouille()
    {
        if (Objectif.Count == 0)
        {
            Objectif = new List<GameObject>();
            Agent.destination = Objectif[O].transform.position;
        }
        if (Vector3.Distance(Agent.pathEndPosition, transform.position) < 1.1f)
        {
            O += 1;
            if (O >= Objectif.Count)
            {
                O = 0;
            }
            potentialNextPatDest = Objectif[O].transform.position;
            Agent.destination = potentialNextPatDest;
        }
        if(type == Type.rodeur)
        {
            DetectionRodeur();
        }
        if (type == Type.errant)
        {
            DetectionErrant();
        }
        if (type == Type.pisteur)
        {
            DetectionPisteur();
        }
    }

    void EnPoursuiteRodeur()
    {
        Agent.stoppingDistance = 2;
        Debug.DrawRay(Agent.transform.position, pos, Color.green);
        Agent.speed = VrodeurPoursuite;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, pos, out hit, myLayerMask))
        {
            if (hit.collider.tag == "Player")
            {
                if (hit.distance <= VisionRodeur)
                {
                    Agent.destination = Player.transform.position;
                }
            }
            else
            {
                lastPosP = Player.transform;
                Agent.destination = lastPosP.position;
                etat = Etat.Cherche;
                Agent.stoppingDistance = 0;
            }
        }
    }
    void EnPoursuiteErrant()
    {
        if (plusSonore == Player)
        {
            Agent.stoppingDistance = 2;
        }
        else
        {
            Agent.stoppingDistance = 0;
        }
        Debug.DrawRay(Agent.transform.position,plusSonore.transform.position - Agent.transform.position, Color.green);  
        Agent.speed = VerrantPoursuite;
        DetectionErrant();
    }
    void EnPoursuitePisteur()
    {

    }

    void EnRecherche()
    {
        if (Vector3.Distance(Agent.pathEndPosition, transform.position) < 1.1f)
            {
            if(tempo == true)
            {
                temp = Time.time;
                tempo = false;
            }
            if(Time.time > temp + 2)
            {
                if (myPointsInt.Count == 0)
                {
                    pointInt = 0;
                    foreach (var i in pointInteret)
                    {
                        if(Vector3.Distance(transform.position, i.transform.position) < 10)
                        {
                            myPointsInt.Add(i);
                        }
                    }
                    if(myPointsInt.Count != 0)
                    {
                        Agent.destination = myPointsInt[pointInt].transform.position;
                    }
                    else
                    {
                        etat = Etat.Patrouille;
                        tempo = true;
                    }
                }
                else
                {
                    if (Vector3.Distance(Agent.pathEndPosition, transform.position) < 1.1f)
                    {
                        pointInt += 1;
                        if (pointInt >= myPointsInt.Count)
                        {
                            etat = Etat.Patrouille;
                            tempo = true;
                            myPointsInt.Clear();
                            O -= 1;
                            return;
                        }
                        Agent.destination = myPointsInt[pointInt].transform.position;
                    }
                }
            }
        }
        if (type == Type.rodeur)
        {
            DetectionRodeur();
        }
    }

    void GestionEtat()
    {
        if (etat == Etat.Patrouille)
        {
            EnPatrouille();
        }
        if(etat == Etat.Poursuite)
        {
            if(type == Type.rodeur)
            {
                EnPoursuiteRodeur();
            }
            if (type == Type.errant)
            {
                EnPoursuiteErrant();
            }
            if (type == Type.pisteur)
            {
                EnPoursuitePisteur();
            }
        }
        if(etat == Etat.Cherche)
        {
            EnRecherche();
        }
    }

    void StartEtat()
    {
        if (Attente == true)
        {
            etat = Etat.Attente;
        }
        else
        {
            etat = Etat.Patrouille;
        }
    }

    void SelectMonster()
    {
        if( type == Type.rodeur && Vrodeur != 0)
        {
            Agent.speed = Vrodeur;
        }
        if (type == Type.errant && Verrant != 0)
        {
            Agent.speed = Verrant;
        }
        if (type == Type.pisteur && Vpisteur != 0)
        {
            Agent.speed = Vpisteur;
        }
    }
}
