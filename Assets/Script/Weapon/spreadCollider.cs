using UnityEngine;

namespace Script.Weapon
{
    public class spreadCollider : MonoBehaviour
    {
        [HideInInspector] public bool triggered;
        [HideInInspector] public Collider foe;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Foe"))
            {
                triggered = true;
                foe = other;
            }

            triggered = false;
        }
    }
}

