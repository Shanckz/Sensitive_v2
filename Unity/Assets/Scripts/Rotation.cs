namespace VRTK
{
    using UnityEngine;
    using System;
    using System.Collections;

    /// <summary>
    /// Adds a Pointer Direction Indicator to a pointer renderer and determines a given world rotation that can be used by a Destiantion Marker.
    /// </summary>
    /// <remarks>
    /// **Prefab Usage:**
    ///  * Place the `VRTK/Prefabs/PointerDirectionIndicator/PointerDirectionIndicator` prefab into the scene hierarchy.
    ///  * Attach the `PointerDirectionIndicator` scene GameObejct to the `Direction Indicator` inspector parameter on a `VRTK_BasePointerRenderer` component.
    ///
    ///   > This can be useful for rotating the play area upon teleporting to face the user in a new direction without expecting them to physically turn in the play space.
    /// </remarks>
    public class Rotation : MonoBehaviour
    {

        #region Legacy
        //[Header("Control Settings")]

        //[Tooltip("The touchpad axis needs to be above this deadzone for it to register as a valid touchpad angle.")]
        //public Vector2 touchpadDeadzone = Vector2.zero;
        //[Tooltip("The axis to use for the direction coordinates.")]
        //public VRTK_ControllerEvents.Vector2AxisAlias coordinateAxis = VRTK_ControllerEvents.Vector2AxisAlias.Touchpad;

        //[Header("Appearance Settings")]

        //[Tooltip("If this is checked then the reported rotation will include the offset of the headset rotation in relation to the play area.")]
        //public bool includeHeadsetOffset = true;

        //public event PointerDirectionIndicatorEventHandler PointerDirectionIndicatorPositionSet;

        //protected VRTK_ControllerEvents controllerEvents;
        //protected Transform playArea;
        //protected Transform headset;


        //public virtual void OnPointerDirectionIndicatorPositionSet()
        //{
        //    if (PointerDirectionIndicatorPositionSet != null)
        //    {
        //        PointerDirectionIndicatorPositionSet(this);
        //    }
        //}

        //public virtual void Initialize(VRTK_ControllerEvents events)
        //{
        //    controllerEvents = events;
        //    playArea = VRTK_DeviceFinder.PlayAreaTransform();
        //    headset = VRTK_DeviceFinder.HeadsetTransform();
        //}

        //public virtual void SetPosition(bool active, Vector3 position)
        //{
        //    transform.position = position;
        //    gameObject.SetActive(active);
        //    OnPointerDirectionIndicatorPositionSet();
        //}

        //public virtual Quaternion GetRotation()
        //{
        //    float offset = (includeHeadsetOffset ? playArea.eulerAngles.y - headset.eulerAngles.y : 0f);
        //    return Quaternion.Euler(0f, transform.localEulerAngles.y + offset, 0f);
        //}

        //public virtual VRTK_ControllerEvents GetControllerEvents()
        //{
        //    return controllerEvents;
        //}

        //protected virtual void Update()
        //{
        //    if (controllerEvents != null && controllerEvents.GetAxisState(coordinateAxis, SDK_BaseController.ButtonPressTypes.Touch) && !InsideDeadzone(controllerEvents.GetAxis(coordinateAxis)))
        //    {
        //        float touchpadAngle = controllerEvents.GetAxisAngle(coordinateAxis);
        //        float angle = ((touchpadAngle > 180) ? touchpadAngle -= 360 : touchpadAngle) + headset.eulerAngles.y;
        //        transform.localEulerAngles = new Vector3(0f, angle, 0f);
        //    }
        //}

        //protected virtual bool InsideDeadzone(Vector2 currentAxis)
        //{
        //    return (currentAxis == Vector2.zero || (Mathf.Abs(currentAxis.x) <= touchpadDeadzone.x && Mathf.Abs(currentAxis.y) <= touchpadDeadzone.y));
        //}
        #endregion Legacy
        public VRTK_ControllerEvents _controllerEvents;
        public VRTK_PointerDirectionIndicator directionIndicator;
        public Transform headset;
        public Transform playArea;

        private Vector3 lastDirection;
        private bool haveDirection = false;
        private bool _isInitialized = false;

        private void OnEnable()
        {
            StartCoroutine(WaitForFirstFrame());
        }

        private void Update()
        {
            if (_isInitialized == false)
            {
                return;
            }

            directionIndicator.SetPosition(true, new Vector3(headset.position.x, playArea.position.y, headset.position.z));
            if(haveDirection == false && Mathf.Abs(_controllerEvents.GetTouchpadAxis().x) > 0.5 || Mathf.Abs(_controllerEvents.GetTouchpadAxis().y) > 0.5)
            {
                haveDirection = true;
                lastDirection = _controllerEvents.GetTouchpadAxis();
                lastDirection.z = -lastDirection.y;
                lastDirection.y = 0;
                Debug.Log("Take direction ok");
            }
            if (haveDirection == true && _controllerEvents.GetTouchpadAxis() == Vector2.zero && lastDirection != Vector3.zero)
            {
                float angle = playArea.transform.eulerAngles.y - headset.transform.eulerAngles.y;
                playArea.rotation = directionIndicator.GetRotation()/* * Quaternion.Euler(0, angle, 0)*/;
                //Quaternion.Inverse(playArea.rotation) * targetRotation;

                //ovrCamera.rotation = Quaternion.Euler(lastDirection);
                //ovrCamera.rotation = Quaternion.Euler(0, lastDirection.y, 0);
                haveDirection = false;
            }
            //_controllerEvents.GetTouchpadAxis
            //directionIndicator.GetRotation();
        }

        private IEnumerator WaitForFirstFrame()
        {
            yield return null;
            _isInitialized = true;
            directionIndicator.Initialize(_controllerEvents);
            headset = VRTK_DeviceFinder.HeadsetTransform();
            playArea = VRTK_DeviceFinder.PlayAreaTransform();
        }
    }
}