using UnityEngine;

public class Enemy_Melee_Movement : MonoBehaviour
{
    [SerializeField] private EnemyStats m_enemyStatsCS;

    private void Update()
    {
        if (GameManager.I.IsGamePaused) return;

        if (!m_enemyStatsCS.EnemyEffectsCS.IsFriend())
        {
            MoveEnemyTowardsPlayer();
            LookAtPlayer();
            return;
        }

        FindNearestEnemy();
        MoveTowardsEnemy();
        LookAtEnemy();
    }

    private void MoveEnemyTowardsPlayer()
    {
        m_enemyStatsCS.NavMeshAgent.SetDestination(PlayerStats.I.PlayerTF.position);
    }

    private void LookAtPlayer()
    {
        transform.LookAt(PlayerStats.I.PlayerTF.position + new Vector3(0, 0.4f, 0));
    }

    private void MoveTowardsEnemy()
    {
        if (m_closestEnemyStatsCS)
        {
            m_enemyStatsCS.NavMeshAgent.SetDestination(m_closestEnemyStatsCS.EnemyTf.position);
        }
    }

    private void LookAtEnemy()
    {
        if (m_closestEnemyStatsCS)
        {
            transform.LookAt(m_closestEnemyStatsCS.EnemyTf.position + new Vector3(0, 0.4f, 0));
        }
    }

    [SerializeField] private LayerMask m_enemyLayer;
    [SerializeField] private float m_defaultEnemyLookupRange;
    private EnemyStats m_closestEnemyStatsCS;

    private void FindNearestEnemy()
    {
        const int maxColliders = 10;
        Collider[] colliders = new Collider[maxColliders];

        int amountOfColliders = Physics.OverlapSphereNonAlloc(
            m_enemyStatsCS.EnemyTf.position,
            m_defaultEnemyLookupRange,
            colliders,
            m_enemyLayer);

        EnemyStats currentClosestEnemyStatsCS = null;
        float closestEnemyDistance = m_defaultEnemyLookupRange;
        for (int i = 0; i < amountOfColliders; i++)
        {
            Transform enemyTF = colliders[i].GetComponent<Transform>();
            if (enemyTF == transform)
            {
                continue;
            }
            Vector3 enemyPosition = enemyTF.position;
            float distanceToEnemy = Vector3.Distance(enemyPosition, m_enemyStatsCS.EnemyTf.position);
            if (distanceToEnemy <= closestEnemyDistance)
            {
                closestEnemyDistance = distanceToEnemy;
                currentClosestEnemyStatsCS = colliders[i].GetComponent<EnemyStats>();
            }
        }
        m_closestEnemyStatsCS = currentClosestEnemyStatsCS;
    }
}
