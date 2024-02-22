using Killbox.Enums;
using UnityEngine;

public class Enemy_Melee_Attack : MonoBehaviour
{
    [SerializeField] private EnemyStats m_enemyStatsCS;
    [SerializeField] private Transform m_attackOriginTF;

    [SerializeField] private int m_damage;
    [SerializeField] private int m_damageToEnemies;
    [SerializeField] private EDamageTypes m_damageType;
    [SerializeField] private float m_attackDistance;
    [SerializeField] private GameObject m_deathVFX;
    [SerializeField] private LayerMask m_damageableLayersPlayer;
    [SerializeField] private LayerMask m_damageableLayersEnemy;

    private void Update()
    {
        Attack();

#if UNITY_EDITOR
        DebugGizmos();
#endif
    }

    [SerializeField] private GameObject m_enemyDebris;

    private void Attack()
    {
        int damage = m_enemyStatsCS.EnemyEffectsCS.IsFriend() ? m_damageToEnemies : m_damage;
        LayerMask damageLayerMask = m_enemyStatsCS.EnemyEffectsCS.IsFriend() ? m_damageableLayersEnemy : m_damageableLayersPlayer;
        if (Physics.Raycast(
                m_attackOriginTF.position,
                m_attackOriginTF.forward,
                out RaycastHit hit, m_attackDistance,
                damageLayerMask))
        {
            Health health = hit.transform.GetComponent<Health>();
            health.TakeDamage(damage, m_damageType, false);
            Instantiate(m_deathVFX, transform.position, Quaternion.identity);
            AudioManager.I.Play("Explode_Enemy");
            Instantiate(m_enemyDebris, transform.position, Random.rotation);
            gameObject.SetActive(false);
        }
    }

    private void DebugGizmos()
    {
        Debug.DrawRay(m_attackOriginTF.position, m_attackOriginTF.forward * m_attackDistance, Color.red);
    }
}
