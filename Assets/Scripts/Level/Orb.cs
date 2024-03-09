using Killbox.Enums;
using MoreMountains.Feedbacks;
using UnityEngine;

public abstract class Orb : Health
{
    [SerializeField] private int m_health;
    [SerializeField] private MMF_Player m_getShotFeedback;
    [SerializeField] private GameObject m_orbHitVFX;
    [SerializeField] private GameObject m_orbDestroyVFX;

    public override void TakeDamage(int _damage, EDamageTypes _damageType, bool _chargeTarget)
    {
        m_health -= _damage;
        if (m_health <= 0)
        {
            Die(_damageType);
        }

        m_getShotFeedback.Initialization();
        m_getShotFeedback.PlayFeedbacks();
        Instantiate(m_orbHitVFX, transform.position, transform.rotation);
    }

    protected override void Die(EDamageTypes _damageType)
    {
        PlayEffect();
        Destroy(gameObject);
    }

    protected virtual void PlayEffect()
    {
        // Implement effect
    }
}