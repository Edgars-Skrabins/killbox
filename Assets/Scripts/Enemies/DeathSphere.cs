using Killbox.Enums;
using UnityEngine;

public class DeathSphere : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(0, EDamageTypes.DISINTEGRATION, false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Health health))
        {
            health.TakeDamage(0, EDamageTypes.DISINTEGRATION, false);
        }
    }
}
