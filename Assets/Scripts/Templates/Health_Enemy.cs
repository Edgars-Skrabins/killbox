using MoreMountains.Feedbacks;
using UnityEngine;

public abstract class Health_Enemy : Health
{
    [SerializeField] private EnemyStats m_enemyStatsCS;
    [SerializeField] private MMF_Player m_takeDamageFeedback;
    [SerializeField] private GameObject m_hitEnemySFX;

    // TODO: Try to do this OnDisable instead and see if it works
    private void OnEnable()
    {
        m_enemyStatsCS.EnemyEffectsCS.TurnOffAllEffects();
    }

    private void AddScore()
    {
        if (GameManager.I.IsPlayerAlive)
        {
            PlayerStats.I.PlayerScore += m_enemyStatsCS.EnemyScoreValue;
        }
    }

    public override void TakeDamage(int _damage)
    {
        m_enemyStatsCS.PopupSpawnerCS.SpawnDamagePopup(_damage);
        m_enemyStatsCS.EnemyHealth -= _damage;
        if (m_takeDamageFeedback) m_takeDamageFeedback.PlayFeedbacks();
        if (m_enemyStatsCS.EnemyHealth <= 0)
        {
            Die();
        }

        PlayHitSound();
    }

    public override void TakeDamage(int _damage, int _explosiveDamage)
    {
        m_enemyStatsCS.PopupSpawnerCS.SpawnDamagePopup(_damage);
        m_enemyStatsCS.EnemyHealth -= _damage;
        if (m_takeDamageFeedback) m_takeDamageFeedback.PlayFeedbacks();
        if (m_enemyStatsCS.EnemyHealth <= 0)
        {
            Die();
        }

        PlayHitSound();
    }

    [SerializeField] private GameObject m_enemyDeathVFX;
    [SerializeField] private GameObject m_dieEnemySFX;
    [SerializeField] private GameObject m_enemyDebris;

    protected override void Die()
    {
        gameObject.SetActive(false);
        AddScore();
        Vector3 thisPosition = transform.position;
        Instantiate(m_enemyDebris, thisPosition, Random.rotation);
        Instantiate(m_enemyDeathVFX, thisPosition, Quaternion.identity);
        Instantiate(m_dieEnemySFX, thisPosition, transform.rotation);
    }

    public override void Slow()
    {
        m_enemyStatsCS.EnemyEffectsCS.Slow();
    }

    protected override void UnSlow()
    {
        m_enemyStatsCS.EnemyEffectsCS.UnSlow();
    }

    public override void Stun()
    {
        m_enemyStatsCS.EnemyEffectsCS.Stun();
    }

    protected override void UnStun()
    {
        m_enemyStatsCS.EnemyEffectsCS.UnStun();
    }

    private void PlayHitSound()
    {
        Instantiate(m_hitEnemySFX, transform.position, transform.rotation);
    }
}
