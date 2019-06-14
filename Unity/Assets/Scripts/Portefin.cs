using UnityEngine;

public class Portefin : MonoBehaviour
{
    public static bool canOpen;
    [SerializeField]
    protected GameObject porte;
    protected bool isLocked;
    [SerializeField]
    protected float vitesse = 0.02f;
    protected Vector3 positionFermee;
    protected Vector3 positionOuverte;
    [SerializeField]
    protected float hauteurPorteOuverte;

    void Start ()
    {
        canOpen = false;
        isLocked = false;
        positionFermee = porte.transform.position;
        positionOuverte = new Vector3(porte.transform.position.x , porte.transform.position.y + hauteurPorteOuverte, porte.transform.position.z);
        porte.transform.position = positionFermee;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isLocked == false && canOpen == true)
        {
            porte.transform.position = new Vector3(porte.transform.position.x , porte.transform.position.y + vitesse, porte.transform.position.z);
            if (porte.transform.position.y > positionOuverte.y)
            {
                porte.transform.position = new Vector3(porte.transform.position.x, positionOuverte.y, porte.transform.position.z);
                isLocked = true;
            }
        }
    }
}
