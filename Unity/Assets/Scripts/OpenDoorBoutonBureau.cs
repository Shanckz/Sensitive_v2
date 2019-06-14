using UnityEngine;

public class OpenDoorBoutonBureau : MonoBehaviour
{
    public static bool canOpenDoor;
    private Animator myAnimator;

    private void Start()
    {
        canOpenDoor = false;
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canOpenDoor)
        {
            myAnimator.SetBool("canOpen", true);
        }
    }
}
