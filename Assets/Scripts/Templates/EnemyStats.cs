using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyStats : MonoBehaviour
{

    [SerializeField] protected string m_enemyName;

    [SerializeField] protected int m_enemyHealth;
    public int EnemyHealth {get => m_enemyHealth; set => m_enemyHealth = value;}


    [SerializeField] protected int m_enemyStartingHealth;


    [SerializeField] protected int m_enemyScoreValue;
    public int EnemyScoreValue {get => m_enemyScoreValue;}


    [SerializeField] protected float m_enemySlowAmount;
    public float EnemySlowAmount {get => m_enemySlowAmount; set => m_enemySlowAmount = value;}

    
    [SerializeField] protected float m_slowDuration;
    public float SlowDuration {get => m_slowDuration;}

    [SerializeField] protected float m_stunDuration;
    public float StunDuration {get => m_stunDuration;}


    [SerializeField] protected float m_enemySpeed;
    public float EnemySpeed {get => m_enemySpeed; set => m_enemySpeed = value;}
    

    [SerializeField] protected NavMeshAgent m_navMeshAgent;
    public NavMeshAgent NavMeshAgent {get => m_navMeshAgent;}

    [SerializeField] protected int m_explosiveDamage;
    
    public int ExplosiveDamage {get => m_explosiveDamage; set => m_explosiveDamage = value;}


    protected virtual void OnEnable()
    {
        EnemyManager.I.GetEnemyList(m_enemyName).Add(gameObject);
        EnemyHealth = m_enemyStartingHealth;
        m_navMeshAgent.speed = EnemySpeed;
    }

    protected virtual void OnDisable()
    {
        EnemyManager.I.GetEnemyList(m_enemyName).Remove(gameObject);
    }
}
