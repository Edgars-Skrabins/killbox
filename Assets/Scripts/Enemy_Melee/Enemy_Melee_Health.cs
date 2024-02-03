using System;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Melee_Health : Health
{
    [SerializeField] private EnemyStats m_enemyStatsCS;
    [SerializeField] private MMF_Player m_takeDamageFeedback;

    [SerializeField] private GameObject m_hitEnemySFX;
    [SerializeField] private GameObject m_dieEnemySFX;
    [SerializeField] private GameObject m_slowedVFX;
    [SerializeField] private GameObject m_stunnedVFX;

    public override void TakeDamage(int _damage)
    {
        SpawnDamagePopup(_damage);

        m_enemyStatsCS.EnemyHealth -= _damage;
        if (m_takeDamageFeedback) m_takeDamageFeedback.PlayFeedbacks();
        if (m_enemyStatsCS.EnemyHealth <= 0)
        {
            Die();
        }

        //hit sound
        Instantiate(m_hitEnemySFX, transform.position, transform.rotation);
    }

    public override void TakeDamage(int _damage, int _explosiveDamage)
    {
        SpawnDamagePopup(_damage);

        m_enemyStatsCS.EnemyHealth -= _damage;
        if (m_takeDamageFeedback) m_takeDamageFeedback.PlayFeedbacks();
        if (m_enemyStatsCS.EnemyHealth <= 0)
        {
            Die();
        }

        //hit sound
        Instantiate(m_hitEnemySFX, transform.position, transform.rotation);
    }

    private float m_stunTimer;
    private bool m_isStunned;

    public override void Stun()
    {
        m_enemyStatsCS.NavMeshAgent.speed = 0;
        m_enemyStatsCS.NavMeshAgent.velocity = Vector3.zero;
        m_stunnedVFX.SetActive(true);
        if (!m_isStunned) SpawnStunnedPopup();
        m_isStunned = true;
    }

    private void UnStun()
    {
        m_stunTimer += Time.deltaTime;
        if (m_stunTimer >= m_enemyStatsCS.StunDuration)
        {
            m_isStunned = false;
            m_stunnedVFX.SetActive(false);
            m_enemyStatsCS.NavMeshAgent.speed = m_enemyStatsCS.EnemySpeed;
            m_stunTimer = 0;
        }
    }

    private float m_slowTimer;
    private bool m_isSlowed;

    public override void Slow()
    {

        if (m_isStunned) return;

        m_slowedVFX.SetActive(true);
        m_enemyStatsCS.NavMeshAgent.speed = m_enemyStatsCS.EnemySpeed - m_enemyStatsCS.EnemySlowAmount;
        if (!m_isSlowed) SpawnSlowedPopup();
        m_isSlowed = true;
    }

    private void UnSlow()
    {
        m_slowTimer += Time.deltaTime;

        if (m_isStunned)
        {
            m_isSlowed = false;
            m_slowedVFX.SetActive(false);
            m_slowTimer = 0;
        }

        if (m_slowTimer >= m_enemyStatsCS.SlowDuration)
        {
            m_isSlowed = false;
            m_slowedVFX.SetActive(false);
            m_enemyStatsCS.NavMeshAgent.speed = m_enemyStatsCS.EnemySpeed;
            m_slowTimer = 0;
        }
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

    [SerializeField] private GameObject m_enemyDeathVFX;
    [SerializeField] private GameObject m_enemyDebri;

    protected override void Die()
    {
        AddScore();
        Instantiate(m_enemyDebri, transform.position, Random.rotation);
        Instantiate(m_enemyDeathVFX, transform.position, Quaternion.identity);
        Instantiate(m_dieEnemySFX, transform.position, transform.rotation);

        gameObject.SetActive(false);
    }

    private void AddScore()
    {
        if (GameManager.I.IsPlayerAlive)
        {
            PlayerStats.I.PlayerScore += m_enemyStatsCS.EnemyScoreValue;
        }
    }

    [Space(5)]
    [Header("General Popup settings")]
    [Space(5)]
    [SerializeField] private Transform m_popupTF;

    [Space(10)]
    [Header("Damage Popup settings")]
    [Space(5)]
    [SerializeField] private GameObject m_damagePopup;

    [SerializeField] private float m_damagePopupMinFontSize;
    [SerializeField] private float m_damagePopupMinFontSizeMaxFontSize;

    private void SpawnDamagePopup(int _damage)
    {
        //var obj = Instantiate(m_damagePopup, m_damagePopupTF.transform.position, m_damagePopupTF.transform.rotation);
        var obj = Instantiate(m_damagePopup, Random.insideUnitSphere + m_popupTF.position, m_popupTF.transform.rotation);

        TextMeshProUGUI tmproText = obj.GetComponentInChildren<TextMeshProUGUI>();
        tmproText.text = _damage.ToString();
        tmproText.fontSize = Random.Range(m_damagePopupMinFontSize, m_damagePopupMinFontSizeMaxFontSize);
    }

    [Space(10)]
    [Header("Slowed Popup settings")]
    [Space(5)]
    [SerializeField] private GameObject m_slowedPopup;

    [SerializeField] private float m_slowedPopupMinFontSize;
    [SerializeField] private float m_slowedPopupMaxFontSize;

    private void SpawnSlowedPopup()
    {
        var obj = Instantiate(m_slowedPopup, Random.insideUnitSphere + m_popupTF.position, m_popupTF.transform.rotation);

        TextMeshProUGUI tmproText = obj.GetComponentInChildren<TextMeshProUGUI>();
        tmproText.fontSize = Random.Range(m_slowedPopupMinFontSize, m_slowedPopupMaxFontSize);
    }

    [Space(10)]
    [Header("Stunned Popup settings")]
    [Space(5)]
    [SerializeField] private GameObject m_stunnedPopup;

    [SerializeField] private float m_stunnedPopupMinFontSize;
    [SerializeField] private float m_stunnedPopupMaxFontSize;

    private void SpawnStunnedPopup()
    {
        var obj = Instantiate(m_stunnedPopup, Random.insideUnitSphere + m_popupTF.position, m_popupTF.transform.rotation);

        TextMeshProUGUI tmproText = obj.GetComponentInChildren<TextMeshProUGUI>();
        tmproText.fontSize = Random.Range(m_stunnedPopupMinFontSize, m_stunnedPopupMaxFontSize);
    }

    private void OnEnable()
    {
        TurnOffAllEffects();
    }

    private void TurnOffAllEffects()
    {
        m_isSlowed = false;
        m_slowedVFX.SetActive(false);
        m_enemyStatsCS.NavMeshAgent.speed = m_enemyStatsCS.EnemySpeed;
        m_slowTimer = 0;
        m_isStunned = false;
        m_stunnedVFX.SetActive(false);
        m_enemyStatsCS.NavMeshAgent.speed = m_enemyStatsCS.EnemySpeed;
        m_stunTimer = 0;
    }
}