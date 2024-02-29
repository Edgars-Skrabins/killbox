using UnityEngine;

public class Bullet_Blazer : Bullet
{
    [SerializeField] private int m_maxBounces;
    [SerializeField] private float m_bounceCheckRadius;
    [SerializeField] private LayerMask m_enemyLayer;
    [SerializeField] private LayerMask m_obstacleLayerMask;

    protected override void Impact(Collider _otherCollider)
    {
        Health health = _otherCollider.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(m_bulletDamage, m_damageType, m_doesCharge);
        }
        if (m_hasImpactVFX) PlayImpactVFX();
        AudioManager.I.Play(m_impactSFX);

        if (m_maxBounces > 0 && _otherCollider.CompareTag("Enemy"))
        {
            m_maxBounces -= 1;
            RelaunchBullet();
            return;
        }

        Destroy(gameObject);
    }

    private void RelaunchBullet()
    {
        const int maxColliders = 10;
        Collider[] colliders = new Collider[maxColliders];
        EnemyStats currentClosestEnemyStatsCS = null;
        float closestEnemyDistance = m_bounceCheckRadius;
        int amountOfColliders = Physics.OverlapSphereNonAlloc(m_bulletTF.position, m_bounceCheckRadius, colliders, m_enemyLayer);
        for (int i = 0; i < amountOfColliders; i++)
        {
            Transform enemyTF = colliders[i].GetComponent<Transform>();
            Vector3 enemyPosition = enemyTF.position;
            Vector3 directionToEnemy = enemyPosition - m_bulletTF.position;
            if (!Physics.Raycast(m_bulletTF.position, directionToEnemy, Mathf.Infinity, m_enemyLayer))
            {
                continue;
            }
            float distanceToEnemy = Vector3.Distance(enemyPosition, m_bulletTF.position);
            if (distanceToEnemy <= closestEnemyDistance)
            {
                closestEnemyDistance = distanceToEnemy;
                currentClosestEnemyStatsCS = colliders[i].GetComponent<EnemyStats>();
            }
        }
        if (!currentClosestEnemyStatsCS)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 m_targetEnemyPosition = currentClosestEnemyStatsCS.GetComponent<Transform>().position;
        m_bulletTF.LookAt(m_targetEnemyPosition);
        LaunchBullet();
    }
}
