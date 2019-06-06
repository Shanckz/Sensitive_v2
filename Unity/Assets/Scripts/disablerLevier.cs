namespace VRTK.Controllables.PhysicsBased
{
    using UnityEngine;

    public class disablerLevier : MonoBehaviour
    {
        [SerializeField]
        protected GameObject[] ArrayGameObjectsAactiver;

        void Start()
        {
            foreach (var gb in ArrayGameObjectsAactiver)
            {
                gb.SetActive(false);
            }
        }

        private void Update()
        {
            if (levierController.isClosed == true)
            {
                foreach (var gb in ArrayGameObjectsAactiver)
                {
                    gb.SetActive(true);
                }
            }
        }
    }
}
