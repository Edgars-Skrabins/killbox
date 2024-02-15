using Killbox.Enums;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Orb : Health
{
    [SerializeField] private MMF_Player m_getShotFeedback;

    private bool m_hasBeenActivated;
    [SerializeField] private int m_health;
    [SerializeField] private GameObject m_announceDifficulty;
    [SerializeField] private GameObject m_orbHitVFX;
    [SerializeField] private GameObject m_orbDestroyVFX;

    public override void TakeDamage(int _damage)
    {
        m_health -= _damage;
        if (m_health <= 0) Die();

        m_getShotFeedback.Initialization();
        m_getShotFeedback.PlayFeedbacks();
        Instantiate(m_orbHitVFX, transform.position, transform.rotation);
    }

    public override void TakeDamage(int _damage, int _explosiveDamage)
    {
        m_health -= _damage;
        if (m_health <= 0) Die();

        m_getShotFeedback.Initialization();
        m_getShotFeedback.PlayFeedbacks();
        Instantiate(m_orbHitVFX, transform.position, transform.rotation);
    }

    public override void TakeDamage(int _damage, EDamageTypes _damageType, bool _chargeTarget)
    {
        // TODO: Implement taking damage
    }

    protected override void Die(EDamageTypes _damageType)
    {
        if (!m_hasBeenActivated)
        {
            Instantiate(m_announceDifficulty, transform.position, transform.rotation);
            Instantiate(m_orbDestroyVFX, transform.position, transform.rotation);
            SetHardestDifficulty();
        }
    }

    private void SetHardestDifficulty()
    {
        GameManager.I.TimeSinceGameStart = 10000f;
        m_getShotFeedback.Initialization();
        m_getShotFeedback.PlayFeedbacks();
        m_hasBeenActivated = true;

        Destroy(gameObject);
    }
}
