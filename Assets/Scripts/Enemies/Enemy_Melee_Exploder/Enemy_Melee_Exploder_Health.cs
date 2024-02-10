using UnityEngine;

public class Enemy_Melee_Exploder_Health : Health_Enemy
{
    [Space(10)]
    [Header("Enemy melee exploder settings")]
    [Space(5)]
    [SerializeField] private float m_explosionRadius;
    [SerializeField] private LayerMask m_explosionLayers;
    [SerializeField] private int m_explosionDamage;

    public override void TakeDamage(int _damage)
    {
        m_explosionDamage = m_enemyStatsCS.ExplosiveDamage;
        base.TakeDamage(_damage);
    }

    public override void TakeDamage(int _damage, int _explosiveDamage)
    {
        m_explosionDamage = _explosiveDamage;
        base.TakeDamage(_damage, _explosiveDamage);
    }

    protected override void Die()
    {
        base.Die();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_explosionRadius, m_explosionLayers);
        foreach (Collider hit in hitColliders)
        {
            if (hit.TryGetComponent(out Health health))
            {
                if (health) health.TakeDamage(m_explosionDamage);
            }
        }
    }
}
