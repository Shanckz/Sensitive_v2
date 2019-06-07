using UnityEngine;

public class openDoorKey : MonoBehaviour
{
    [SerializeField]
    protected GameObject porte;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Key") && Errant1.unactiveErrant1 == false)
        {
            porte.SetActive(false);
        }
    }
}
