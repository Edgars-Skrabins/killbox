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
    [Space(10)]
    [Header("MiniGun overheat effect")]
    [SerializeField] private Material m_emissionMaterial;
    [SerializeField] private float m_maxEmissionStrength;
    private float m_emissionStrength;
    private float m_maxSpraySphereTravelDistance;
    private float m_currentSpraySphereDistance;
    private float m_traveledSpraySphereDistanceInPercent;

    private void OnEnable()
    {
        ResetSpray();
    }

    private void Start()
    {
        m_maxSpraySphereTravelDistance = Vector3.Distance(m_spraySphereTF.position, m_widestSpraySphereTF.position);
    }

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

        UpdateHeatColor();
        TrackSpraySphereDistance();
    }

    private void ResetSpray()
    {
        m_spraySphereTF.position = m_narrowestSpraySphereTF.position;
    }

    private void UpdateHeatColor()
    {
        const float emissionOffset = 1;
        m_emissionStrength = m_maxEmissionStrength * m_traveledSpraySphereDistanceInPercent + emissionOffset;
        m_emissionMaterial.SetColor("_EmissionColor", Color.red * m_emissionStrength);
    }

    private void TrackSpraySphereDistance()
    {
        m_currentSpraySphereDistance = Vector3.Distance(m_spraySphereTF.position, m_narrowestSpraySphereTF.position);
        m_traveledSpraySphereDistanceInPercent = m_currentSpraySphereDistance / m_maxSpraySphereTravelDistance;
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
