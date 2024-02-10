using UnityEngine;

public class Bullet_Randomizer : Bullet
{
    protected override void Impact(Collider _otherCollider)
    {
        Health health = _otherCollider.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(m_bulletDamage);
            if (_otherCollider.CompareTag("Enemy") && m_doesSlow)
            {
                health.Slow();
            }
        }

        if (m_hasImpactVFX) PlayImpactVFX();
        AudioManager.I.Play(m_impactSFX);
        Destroy(gameObject);
    }
}
