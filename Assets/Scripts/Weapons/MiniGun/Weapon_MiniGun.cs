using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon_MiniGun : ProjectileWeapon
{
    [Space(10)]
    [Header("MiniGun settings")]
    [Space(5)]
    [SerializeField] private Transform m_spraySphereTF;
    [SerializeField] private Transform m_narrowestSpraySphereTF;
    [SerializeField] private Transform m_widestSpraySphereTF;
    [SerializeField] private float m_sprayRecoverySpeed;
    [SerializeField] private float m_sprayWorseningSpeed;

    protected override void Update()
    {
        base.Update();
        if (m_isShooting)
        {
            WidenSpray();
        }
        else
        {
            NarrowSpray();
        }
    }

    private void WidenSpray()
    {
        Vector3 m_directionToWidestSpraySphere = (m_widestSpraySphereTF.localPosition - m_spraySphereTF.localPosition).normalized;
        m_spraySphereTF.Translate(m_directionToWidestSpraySphere * (Time.deltaTime * m_sprayWorseningSpeed));
    }

    private void NarrowSpray()
    {
        Vector3 directionToNarrowestSpraySphere = (m_narrowestSpraySphereTF.localPosition - m_spraySphereTF.localPosition).normalized;
        m_spraySphereTF.Translate(directionToNarrowestSpraySphere * (Time.deltaTime * m_sprayRecoverySpeed));
    }

    protected override void Shoot()
    {
        if (!CheckIfFirePointVisible() || GameManager.I.IsGamePaused || !GameManager.I.IsPlayerAlive) return;

        m_fireRateTimer = 0f;
        m_isShooting = true;
        Vector3 shootDirection = Random.insideUnitSphere + m_spraySphereTF.position - m_firePoints[0].position;
        m_firePoints[0].rotation = Quaternion.LookRotation(shootDirection);

        foreach (Transform firePoint in m_firePoints)
        {
            Instantiate(m_bulletPrefab, firePoint.position, firePoint.rotation);
        }

        if (m_hasShotAnimation) PlayShotAnimation();
        if (m_hasShotFeedback) PlayShotFeedback();
        if (m_hasMuzzleVFX) PlayMuzzleVFX();

        _shot_timer += Time.deltaTime;
        if (_shot_timer > m_shootSFXRate)
        {
            AudioManager.I.Play(m_shootSFX);
            _shot_timer = 0;
        }
    }
}
