using UnityEngine;

public class Weapon_IonRifle : ProjectileWeapon
{
    [Space(10)]
    [Header("Ion Rifle settings")]
    [Space(5)]
    [SerializeField] private Transform m_spraySphere;

    protected override void Shoot()
    {
        if (!CheckIfFirePointVisible() || GameManager.I.IsGamePaused || !GameManager.I.IsPlayerAlive) return;

        m_fireRateTimer = 0f;

        Vector3 shootDirection = Random.insideUnitSphere + m_spraySphere.position - m_firePoints[0].position;
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
