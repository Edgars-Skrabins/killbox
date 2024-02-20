using UnityEngine;

public class Bullet_Pippy : Bullet
{
    [SerializeField] private int m_bounces;

    protected override void Impact(Collider _otherCollider)
    {
        if (_otherCollider.TryGetComponent(out Health health))
        {
            health.TakeDamage(m_bulletDamage, m_damageType, m_doesCharge);
            if (m_hasImpactVFX) PlayImpactVFX();
            AudioManager.I.Play(m_impactSFX);
            Destroy(gameObject);
            return;
        }

        if (m_bounces <= 0)
        {
            Destroy(gameObject);
            return;
        }

        LaunchBullet();
        m_bounces -= 1;
    }
}
