using UnityEngine;

namespace Script.Weapon
{
    public class spreadCollider : MonoBehaviour
    {
        [HideInInspector] public bool triggered;
        [HideInInspector] public GameObject foe;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Foe"))
            {
                triggered = true;
                foe = other.gameObject;
            }
            else
            {
                triggered = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Foe"))
            {
                triggered = true;
                foe = other.gameObject;
            }
            else
            {
                triggered = false;
            }
        }
    }
}

