using UnityEngine;

public class OpenDoorBoutonBureau : MonoBehaviour
{
    private Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (openDoorKey1.keyCheck)
        {
            myAnimator.SetBool("canOpen", true);
        }
    }
}
