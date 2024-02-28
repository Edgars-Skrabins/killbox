using UnityEngine;

public class Enemy_Effects : MonoBehaviour
{
    [SerializeField] private EnemyStats m_enemyStatsCS;

    [SerializeField] private GameObject m_slowedVFXGO;
    [SerializeField] private GameObject m_stunnedVFXGO;
    [SerializeField] private GameObject m_chargedVFXGO;
    [SerializeField] private GameObject m_normalGraphicsGO;
    [SerializeField] private GameObject m_friendGraphicsGO;
    [SerializeField] private bool m_canBeTurnedIntoFriend;

    private float m_stunTimer;
    private bool m_isStunned;
    private bool m_isCharged;
    private bool m_isFriend;

    private void OnDisable()
    {
        TurnOffAllEffects();
    }

    private void Update()
    {
        if (m_isSlowed)
        {
            UnSlow();
        }

        if (m_isStunned)
        {
            UnStun();
        }
    }

    public void BeFriend()
    {
        if (!m_canBeTurnedIntoFriend || m_isFriend)
        {
            return;
        }

        m_enemyStatsCS.NavMeshAgent.velocity = Vector3.zero;

        m_isFriend = true;
        m_enemyStatsCS.PopupSpawnerCS.SpawnBefriendPopup();
        m_friendGraphicsGO.SetActive(true);
        m_normalGraphicsGO.SetActive(false);
    }

    public void UnFriend()
    {
        m_isFriend = false;
        m_friendGraphicsGO.SetActive(false);
        m_normalGraphicsGO.SetActive(true);
    }

    public bool IsFriend()
    {
        return m_isFriend;
    }

    public void Charge()
    {
        if (!m_enemyStatsCS.EnemyHealthCS.HasExplosion() || m_isCharged)
        {
            return;
        }
        m_isCharged = true;
        m_enemyStatsCS.PopupSpawnerCS.SpawnChargedPopup();
        m_chargedVFXGO.SetActive(true);
    }

    private void UnCharge()
    {
        m_isCharged = false;
        m_chargedVFXGO.SetActive(false);
    }

    public bool IsCharged()
    {
        return m_isCharged;
    }

    public void Stun()
    {
        m_enemyStatsCS.NavMeshAgent.speed = 0;
        m_enemyStatsCS.NavMeshAgent.velocity = Vector3.zero;
        m_stunnedVFXGO.SetActive(true);
        if (!m_isStunned)
        {
            m_enemyStatsCS.PopupSpawnerCS.SpawnStunnedPopup();
        }
        m_isStunned = true;
    }

    private void UnStun()
    {
        m_stunTimer += Time.deltaTime;
        if (m_stunTimer >= m_enemyStatsCS.StunDuration)
        {
            m_isStunned = false;
            m_stunnedVFXGO.SetActive(false);
            m_enemyStatsCS.NavMeshAgent.speed = m_enemyStatsCS.EnemySpeed;
            m_stunTimer = 0;
        }
    }

    private float m_slowTimer;
    private bool m_isSlowed;

    public void Slow()
    {
        if (m_isStunned)
        {
            return;
        }

        m_slowedVFXGO.SetActive(true);
        m_enemyStatsCS.NavMeshAgent.speed = m_enemyStatsCS.EnemySpeed - m_enemyStatsCS.EnemySlowAmount;
        if (!m_isSlowed)
        {
            m_enemyStatsCS.PopupSpawnerCS.SpawnSlowedPopup();
        }
        m_isSlowed = true;
    }

    private void UnSlow()
    {
        m_slowTimer += Time.deltaTime;
        if (m_isStunned)
        {
            m_isSlowed = false;
            m_slowedVFXGO.SetActive(false);
            m_slowTimer = 0;
        }
        if (m_slowTimer >= m_enemyStatsCS.SlowDuration)
        {
            m_isSlowed = false;
            m_slowedVFXGO.SetActive(false);
            m_enemyStatsCS.NavMeshAgent.speed = m_enemyStatsCS.EnemySpeed;
            m_slowTimer = 0;
        }
    }

    public void TurnOffAllEffects()
    {
        m_isSlowed = false;
        m_slowedVFXGO.SetActive(false);
        m_enemyStatsCS.NavMeshAgent.speed = m_enemyStatsCS.EnemySpeed;
        m_slowTimer = 0;
        m_isStunned = false;
        m_stunnedVFXGO.SetActive(false);
        m_enemyStatsCS.NavMeshAgent.speed = m_enemyStatsCS.EnemySpeed;
        m_stunTimer = 0;
        UnCharge();
        UnFriend();
    }
}
