using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [SerializeField]
    protected float distDetection;
    [SerializeField]
    protected GameObject errant;

    private void OnCollisionEnter(Collision collision)
    {
        if(Vector3.Distance(transform.position, errant.transform.position) <= distDetection)
        {
            Errant1.ciblePrincipaleErrant1 = this.gameObject;
        }
    }
}
