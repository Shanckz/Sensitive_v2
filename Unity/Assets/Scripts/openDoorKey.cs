using UnityEngine;

public class openDoorKey : MonoBehaviour
{
    [SerializeField]
    protected GameObject porte;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Key"))
        {
            porte.SetActive(false);
        }
    }
}
