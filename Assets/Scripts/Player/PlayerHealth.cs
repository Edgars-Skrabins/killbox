using System;
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

    public override void TakeDamage(int _damage)
    {
        AudioManager.I.Play("Ugh");
        PlayerStats.I.PlayerHealth -= _damage;
        m_damageFeedback.PlayFeedbacks();
        if (PlayerStats.I.PlayerHealth <= 0) Die();
    }

    public override void TakeDamage(int _damage, int _explosiveDamage)
    {
        throw new NotImplementedException();
    }

    public override void Slow()
    {
    }

    protected override void UnSlow()
    {
    }

    public override void Stun()
    {
    }

    protected override void UnStun()
    {
    }

    [SerializeField] private MMF_Player m_healFeedback;

    public void Heal(int _healingAmount)
    {
        AudioManager.I.Play("Heal");
        PlayerStats.I.PlayerHealth += _healingAmount;
        m_healFeedback.PlayFeedbacks();
    }

    protected override void Die()
    {
        GameManager.I.PlayerDead();
    }
}
