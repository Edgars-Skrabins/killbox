using UnityEngine;

public class Enemy_Melee_Movement : MonoBehaviour
{
    [SerializeField] private EnemyStats m_enemyStatsCS;
    
    private void Update()
    {
        if(GameManager.I.IsGamePaused) return;

        MoveEnemyTowardsPlayer();
        LookAtPlayer();
    }

    private void MoveEnemyTowardsPlayer()
    {
        m_enemyStatsCS.NavMeshAgent.SetDestination(PlayerStats.I.PlayerTF.position);
    }

    private void LookAtPlayer()
    {
        transform.LookAt(PlayerStats.I.PlayerTF.position + new Vector3(0,0.4f, 0));
    }
}
