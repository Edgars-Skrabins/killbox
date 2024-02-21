using UnityEngine;

public class Bullet_Pippy : Bullet
{
    [SerializeField] private int m_bounces;
    [SerializeField] private float m_sizeIncrease;

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

        IncreaseSize();
        LaunchBullet();
        m_bounces -= 1;
    }

    private void IncreaseSize()
    {
        m_bulletTF.localScale += new Vector3(m_sizeIncrease, m_sizeIncrease, m_sizeIncrease);
    }
}
