using UnityEngine;

public class Bullet_RocketLauncher : Bullet
{
    [Space(10)]
    [Header(" ----- Explosion Settings ----- ")]
    [Space(5)]

    [SerializeField] private float m_explosionRadius;
    [SerializeField] private int m_explosionDamage;
    [SerializeField] private LayerMask m_explosionLayers;
    [SerializeField] private GameObject m_explotionSFX;


    [Space(10)]
    [Header("Debri explosion settings")]
    [Space(5)]

    [SerializeField] private float m_explosionForce;
    [SerializeField] private float m_upwardsModifier;

    
    protected override void Impact(Collider _otherCollider)
    {

        Transform bulletTransform = transform;
        
        Health impactHealth = _otherCollider.GetComponent<Health>();

        if (impactHealth!= null)
        {
            impactHealth.TakeDamage(m_bulletDamage);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_explosionRadius,m_explosionLayers );
        Collider[] debriColliders = Physics.OverlapSphere(bulletTransform.position, m_explosionRadius,LayerMask.GetMask("Debri"));
            
        foreach(var debriCollider in debriColliders)
        {
            if(debriCollider.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(m_explosionForce, bulletTransform.position,m_explosionRadius,m_upwardsModifier);
            }
        }

        foreach(var hit in colliders)
        {
            Health explosionHealth = hit.GetComponent<Health>();
            
            if(explosionHealth)
            {
                explosionHealth.TakeDamage(m_explosionDamage);
            }
        }
        
        PlayImpactVFX();
        Instantiate(m_explotionSFX, transform.position, transform.rotation);

        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_explosionRadius);
    }
    
}
