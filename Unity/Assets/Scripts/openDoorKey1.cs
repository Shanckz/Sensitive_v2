using UnityEngine;

public class openDoorKey1 : MonoBehaviour
{
    public static bool keyCheck;

    private void Start()
    {
        keyCheck = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            keyCheck = true;
        }
    }
}
