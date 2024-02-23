using UnityEngine;

public class Bullet_RocketLauncher : Bullet
{
    [Space(10)]
    [Header(" ----- Explosion Settings ----- ")]
    [Space(5)]
    [SerializeField] private float m_explosionRadius;
    [SerializeField] private int m_explosionDamage;
    [SerializeField] private LayerMask m_explosionLayers;
    [SerializeField] private GameObject m_explosionSFX;

    [Space(10)]
    [Header("Debris explosion settings")]
    [Space(5)]
    [SerializeField] private float m_explosionForce;
    [SerializeField] private float m_upwardsModifier;

    protected override void Impact(Collider _otherCollider)
    {
        Transform bulletTransform = transform;

        Health impactHealth = _otherCollider.GetComponent<Health>();

        if (impactHealth != null)
        {
            impactHealth.TakeDamage(m_bulletDamage, m_damageType, m_doesCharge);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_explosionRadius, m_explosionLayers);
        Collider[] debrisColliders = Physics.OverlapSphere(bulletTransform.position, m_explosionRadius, LayerMask.GetMask("Debris"));

        foreach (Collider debrisCollider in debrisColliders)
        {
            if (debrisCollider.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(m_explosionForce, bulletTransform.position, m_explosionRadius, m_upwardsModifier);
            }
        }

        foreach (Collider hit in colliders)
        {
            Health explosionHealth = hit.GetComponent<Health>();

            if (explosionHealth)
            {
                explosionHealth.TakeDamage(m_explosionDamage, m_damageType, m_doesCharge);
            }
        }

        PlayImpactVFX();
        Instantiate(m_explosionSFX, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_explosionRadius);
    }
}
