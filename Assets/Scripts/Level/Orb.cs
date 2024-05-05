using Killbox.Enums;
using MoreMountains.Feedbacks;
using UnityEngine;

public abstract class Orb : Health
{
    private bool m_isAlive = true;
    [SerializeField] private int m_health;
    [SerializeField] private MMF_Player m_getShotFeedback;
    [SerializeField] private GameObject m_orbHitVFX;
    [SerializeField] private GameObject m_orbDestroyVFX;
    [SerializeField] private string[] m_deathSounds;

    public override void TakeDamage(int _damage, EDamageTypes _damageType, bool _chargeTarget)
    {
        if (!m_isAlive)
        {
            return;
        }
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
        m_isAlive = false;
        PlayDeathSound();
        Destroy(gameObject);
        PlayEffect();
    }

    private void PlayDeathSound()
    {
        AudioManager.I.Play(m_deathSounds[Random.Range(0, m_deathSounds.Length)]);
    }

    protected virtual void PlayEffect()
    {
        // Implement effect
    }
}