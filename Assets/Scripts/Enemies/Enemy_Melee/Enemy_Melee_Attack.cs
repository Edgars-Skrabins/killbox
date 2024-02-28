using Killbox.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Melee_Attack : MonoBehaviour
{
    [SerializeField] private EnemyStats m_enemyStatsCS;
    [SerializeField] private Transform m_attackOriginTF;

    [SerializeField] private int m_damage;
    [SerializeField] private int m_damageToEnemies;
    [SerializeField] private EDamageTypes m_damageType;
    [SerializeField] private float m_attackDistance;
    [SerializeField] private float m_playerCollisionDistance;
    [SerializeField] private GameObject m_deathVFX;
    [SerializeField] private LayerMask m_damageableLayersPlayer;
    [SerializeField] private LayerMask m_damageableLayersEnemy;

    private void Update()
    {
        HandleAttack();
        CheckForPlayerProximity();
    }

    [SerializeField] private GameObject m_enemyDebris;

    private void HandleAttack()
    {
        LayerMask damageLayerMask = m_enemyStatsCS.EnemyEffectsCS.IsFriend() ? m_damageableLayersEnemy : m_damageableLayersPlayer;
        if (Physics.Raycast(
                m_attackOriginTF.position,
                m_attackOriginTF.forward,
                out RaycastHit hit, m_attackDistance,
                damageLayerMask))
        {
            Attack(hit);
        }
    }

    private void CheckForPlayerProximity()
    {
        float distanceToPlayer = Vector3.Distance(m_enemyStatsCS.EnemyTf.position, PlayerStats.I.PlayerTF.position);
        if (distanceToPlayer <= m_playerCollisionDistance)
        {
            AttackPlayer();
        }
    }

    private void Attack(RaycastHit _hit)
    {
        int damage = _hit.collider.CompareTag("Enemy") ? m_damageToEnemies : m_damage;

        Health health = _hit.transform.GetComponent<Health>();
        health.TakeDamage(damage, m_damageType, false);
        Vector3 enemyPosition = m_enemyStatsCS.EnemyTf.position;
        Instantiate(m_deathVFX, enemyPosition, Quaternion.identity);
        AudioManager.I.Play("Explode_Enemy");
        Instantiate(m_enemyDebris, enemyPosition, Random.rotation);
        gameObject.SetActive(false);
    }

    private void AttackPlayer()
    {
        Health health = PlayerStats.I.PlayerHealthCS;
        health.TakeDamage(m_damage, m_damageType, false);
        Vector3 enemyPosition = m_enemyStatsCS.EnemyTf.position;
        Instantiate(m_deathVFX, enemyPosition, Quaternion.identity);
        AudioManager.I.Play("Explode_Enemy");
        Instantiate(m_enemyDebris, enemyPosition, Random.rotation);
        gameObject.SetActive(false);
    }
}
