using Killbox.Enums;
using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerHealth : Health
{
    private void Start()
    {
        m_healFeedback.Initialization();
        m_damageFeedback.Initialization();
    }

    [SerializeField] private MMF_Player m_damageFeedback;

    public override void TakeDamage(int _damage, EDamageTypes _damageType, bool _chargeTarget)
    {
        AudioManager.I.Play("Ugh");
        PlayerStats.I.PlayerHealth -= _damage;
        m_damageFeedback.PlayFeedbacks();
        if (PlayerStats.I.PlayerHealth <= 0)
        {
            Die(_damageType);
        }
    }

    [SerializeField] private MMF_Player m_healFeedback;

    public void Heal(int _healingAmount)
    {
        AudioManager.I.Play("Heal");
        PlayerStats.I.PlayerHealth += _healingAmount;
        m_healFeedback.PlayFeedbacks();
    }

    protected override void Die(EDamageTypes _damageType)
    {
        GameManager.I.PlayerDead();
    }
}
