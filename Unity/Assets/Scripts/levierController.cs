namespace VRTK.Controllables.PhysicsBased
{
    using UnityEngine;
    using VRTK.GrabAttachMechanics;
    using VRTK.SecondaryControllerGrabActions;

    /// <summary>
    /// A physics based rotatable object.
    /// </summary>
    /// <remarks>
    /// **Required Components:**
    ///  * `Collider` - A Unity Collider to determine when an interaction has occured. Can be a compound collider set in child GameObjects. Will be automatically added at runtime.
    ///  * `Rigidbody` - A Unity Rigidbody to allow the GameObject to be affected by the Unity Physics System. Will be automatically added at runtime.
    ///
    /// **Optional Components:**
    ///  * `VRTK_ControllerRigidbodyActivator` - A Controller Rigidbody Activator to automatically enable the controller rigidbody when near the rotator.
    /// 
    /// **Script Usage:**
    ///  * Create a rotator container GameObject and set the GameObject that is to become the rotator as a child of the newly created container GameObject.
    ///  * Place the `VRTK_PhysicsRotator` script onto the GameObject that is to become the rotatable object and ensure the Transform rotation is `0, 0, 0`.
    ///  * Create a nested GameObject under the rotator GameObject and position it where the hinge should operate.
    ///  * Apply the nested hinge GameObject to the `Hinge Point` parameter on the Physics Rotator script.
    ///
    ///   > The rotator GameObject must not be at the root level and needs to have the Transform rotation set to `0,0,0`. This is the reason for the container GameObject requirement. Any positioning of the rotator must be set on the parent container GameObject.
    /// </remarks>
    [AddComponentMenu("VRTK/Scripts/Interactables/Controllables/Physics/VRTK_PhysicsRotator")]
    public class levierController : VRTK_PhysicsRotator
    {
        public static bool isClosed;
        protected float valueAngle;

        protected override void Awake()
        {
            base.Awake();
            isClosed = false;
        }

        protected override void Update()
        {
            if(doorControl.doorIsClosed == true)
            {
                isLocked = true;
            }
            if(doorControl.doorIsClosed == false)
            {
                base.Update();
                valueAngle = GetValue();
                isLocked = false;
                Debug.Log("angle levier " + valueAngle);
                if (valueAngle <= -80.0f && isClosed == false)
                {
                    isClosed = true;
                    isLocked = true;
                    Errant1.unactiveErrant1 = false;
                    Errant1.ciblePrincipaleErrant1 = null;
                }
            }
        }
    }
}
