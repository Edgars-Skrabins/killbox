using Killbox.Enums;
using MoreMountains.Feedbacks;
using UnityEngine;

public abstract class Health_Enemy : Health
{
    [SerializeField] protected EnemyStats m_enemyStatsCS;
    [SerializeField] protected MMF_Player m_takeDamageFeedback;
    [SerializeField] protected GameObject m_hitEnemySFX;

    [Space(10)]
    [Header("Explosion settings")]
    [Space(5)]
    [SerializeField] private bool m_hasExplosion;
    [SerializeField] private EDamageTypes m_explosionDamageType;
    [SerializeField] private float m_explosionRadius;
    [SerializeField] private LayerMask m_explosionLayers;
    [SerializeField] private int m_explosionDamage;
    [SerializeField] private int m_explosionChargedDamage;


    // TODO: Try to do this OnDisable instead and see if it works
    private void OnEnable()
    {
        m_enemyStatsCS.EnemyEffectsCS.TurnOffAllEffects();
    }

    public bool HasExplosion()
    {
        return m_hasExplosion;
    }

    private void AddScore()
    {
        if (GameManager.I.IsPlayerAlive)
        {
            PlayerStats.I.PlayerScore += m_enemyStatsCS.EnemyScoreValue;
        }
    }

    public override void TakeDamage(int _damage, EDamageTypes _damageType, bool _chargeTarget)
    {
        if (_chargeTarget)
        {
            m_enemyStatsCS.EnemyEffectsCS.Charge();
        }
        switch (_damageType)
        {
            case EDamageTypes.NORMAL:
                break;
            case EDamageTypes.SLOW:
                m_enemyStatsCS.EnemyEffectsCS.Slow();
                break;
            case EDamageTypes.STUN:
                m_enemyStatsCS.EnemyEffectsCS.Stun();
                break;
            case EDamageTypes.DISINTEGRATION:
                break;
            default:
                Debug.LogError("Damage type was no specified");
                break;
        }
        HandleDamage(_damage, _damageType);
    }

    protected virtual void HandleDamage(int _damage, EDamageTypes _damageType)
    {
        if (_damageType == EDamageTypes.DISINTEGRATION)
        {
            Die(_damageType);
            return;
        }

        m_enemyStatsCS.PopupSpawnerCS.SpawnDamagePopup(_damage);
        m_enemyStatsCS.EnemyHealth -= _damage;
        PlayHitSound();
        m_takeDamageFeedback?.PlayFeedbacks();

        if (m_enemyStatsCS.EnemyHealth <= 0)
        {
            Die(_damageType);
        }
    }

    [SerializeField] private GameObject m_enemyDeathVFX;
    [SerializeField] private GameObject m_dieEnemySFX;
    [SerializeField] private GameObject m_enemyDebris;

    protected override void Die(EDamageTypes _damageType)
    {
        gameObject.SetActive(false);
        AddScore();
        Vector3 thisPosition = transform.position;
        Instantiate(m_enemyDebris, thisPosition, Random.rotation);
        Instantiate(m_enemyDeathVFX, thisPosition, Quaternion.identity);
        Instantiate(m_dieEnemySFX, thisPosition, transform.rotation);

        if (!m_hasExplosion || _damageType == EDamageTypes.DISINTEGRATION)
        {
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_explosionRadius, m_explosionLayers);
        foreach (Collider hit in hitColliders)
        {
            if (hit.TryGetComponent(out Health health))
            {
                int explosionDamage = m_enemyStatsCS.EnemyEffectsCS.IsCharged() ? m_explosionChargedDamage : m_explosionDamage;
                health.TakeDamage(explosionDamage, m_explosionDamageType, false);
            }
        }
    }

    private void PlayHitSound()
    {
        Instantiate(m_hitEnemySFX, transform.position, transform.rotation);
    }
}
