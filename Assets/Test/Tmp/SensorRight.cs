using Test.Tmp;
using UnityEngine;

public class SensorRight : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger out floor");

        if (other.CompareTag("Floor"))
        {
            var ennemyBehavior = this.GetComponentInParent<MobMovement>();
            var ennemyRigidbody = this.GetComponentInParent<Rigidbody2D>();

            ennemyRigidbody.velocity = Vector2.zero; // new Vector2(0, 0);
            ennemyBehavior.direction = -1f;
        }
    }
}
