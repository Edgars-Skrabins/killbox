using UnityEngine;

public class Weapon_AutoShotgun : ProjectileWeapon
{
    [Space(20)]
    [Header("Shot spread settings")]
    [Space(5)]
    [SerializeField] private float m_randomBulletSpreadX;
    [SerializeField] private float m_randomBulletSpreadY;

    private Transform m_weaponTF;

    private void Start()
    {
        m_weaponTF = transform;
    }

    protected override void Shoot()
    {
        if (!CheckIfFirePointVisible() || GameManager.I.IsGamePaused || !GameManager.I.IsPlayerAlive) return;

        m_fireRateTimer = 0f;

        foreach (Transform firePoint in m_firePoints)
        {
            Vector3 bulletRotation = firePoint.localEulerAngles;
            Vector3 localEulerAngle = m_weaponTF.localEulerAngles;
            bulletRotation.x = localEulerAngle.x + Random.Range(-m_randomBulletSpreadX, m_randomBulletSpreadX);
            bulletRotation.y = localEulerAngle.y + Random.Range(-m_randomBulletSpreadY, m_randomBulletSpreadY);
            firePoint.localEulerAngles = bulletRotation;

            Instantiate(m_bulletPrefab, firePoint.position, firePoint.rotation);
        }

        if (m_hasShotAnimation) PlayShotAnimation();
        if (m_hasShotFeedback) PlayShotFeedback();
        if (m_hasMuzzleVFX) PlayMuzzleVFX();

        AudioManager.I.Play(m_shootSFX);
    }
}
