using MoreMountains.Feedbacks;
using UnityEngine;

public abstract class ProjectileWeapon : MonoBehaviour
{

    [Header(" ----- Firing Settings -----")]
    [Space(5)]
    [SerializeField] protected Transform[] m_firePoints;
    [SerializeField] protected GameObject m_bulletPrefab;
    [SerializeField] protected float m_fireRateInSeconds;
    [SerializeField] protected LayerMask m_firePointVisibleLayer;

    [Space(20)]

    [Header(" ----- Shot Animation Settings -----")]
    [Space(5)]

    [SerializeField] protected bool m_hasShotAnimation;
    [SerializeField] protected WeaponMovement m_weaponMovementCS;

    [Space(20)]

    [Header(" ----- Shot Feedback Settings -----")]
    [Space(5)]
    [SerializeField] protected bool m_hasShotFeedback;
    [SerializeField] protected MMF_Player m_shotFeedback;
    [SerializeField] protected string m_shootSFX;
    [SerializeField] protected float m_shootSFXRate;
    protected float _shot_timer;

    [Space(20)]

    [Header(" ----- Muzzle VFX Settings -----")]
    [Space(5)]
    [SerializeField] protected bool m_hasMuzzleVFX;
    [SerializeField] protected GameObject[] m_muzzleVFXArray;
    [SerializeField] protected Transform m_muzzleTF;

    [Space(20)]

    [Space(20)]
    [Header(" ----- FirePointVisibleSettings ----- ")]
    [Space(5)]

    [SerializeField] private GameObject m_crossHairImage;
    [SerializeField] private GameObject m_cantShootImage;

    protected float m_fireRateTimer;

    protected void Awake()
    {
        m_fireRateTimer = m_fireRateInSeconds;
    }

    protected virtual void Update()
    {
        if (GameManager.I.IsGamePaused) return;

        FirePointVisibleGFX();
        CountFireTimer();
        Inputs();
    }

    protected virtual void FirePointVisibleGFX()
    {
        if (CheckIfFirePointVisible())
        {
            if (!m_crossHairImage.activeInHierarchy)
            {
                m_crossHairImage.SetActive(true);
            }

            if (m_cantShootImage.activeInHierarchy)
            {
                m_cantShootImage.SetActive(false);
            }

        }
        else
        {
            if (!m_cantShootImage.activeInHierarchy)
            {
                m_cantShootImage.SetActive(true);
            }

            if (m_crossHairImage.activeInHierarchy)
            {
                m_crossHairImage.SetActive(false);
            }
        }
    }

    protected virtual bool CheckIfFirePointVisible()
    {

        Vector3 cameraToMuzzle = m_firePoints[0].position - PlayerStats.I.PlayerCamera.transform.position;


        if (Physics.Raycast(PlayerStats.I.PlayerCamera.transform.position, cameraToMuzzle, out RaycastHit hit, 100f, m_firePointVisibleLayer))
        {
            if (hit.collider.CompareTag("Muzzle"))
            {
                return true;
            }
        }

        return false;
    }

    protected virtual void CountFireTimer()
    {
        m_fireRateTimer += Time.deltaTime;
    }

    protected virtual void Inputs()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CanShoot())
        {
            Shoot();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && CanShoot())
        {
            _shot_timer = m_shootSFXRate;
        }
    }

    protected bool CanShoot()
    {
        if (m_fireRateTimer >= m_fireRateInSeconds)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void Shoot()
    {
        if (!CheckIfFirePointVisible() || GameManager.I.IsGamePaused || !GameManager.I.IsPlayerAlive) return;

        m_fireRateTimer = 0f;

        foreach (var firePoint in m_firePoints)
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

    protected virtual void PlayShotAnimation()
    {
        if (m_weaponMovementCS) m_weaponMovementCS.PlayShotAnimation();
    }

    protected virtual void PlayShotFeedback()
    {
        if (m_shotFeedback) m_shotFeedback.PlayFeedbacks();
    }

    // protected virtual void PlayMuzzleVFX()
    // {
    //     if (m_muzzleVFXArray.Length <= 0) return;
    //
    //     foreach (var vfx in m_muzzleVFXArray)
    //     {
    //         vfx.GetComponent<ParticleSystem>().Play();
    //     }
    //
    // }

    protected virtual void PlayMuzzleVFX()
    {
        if (m_muzzleVFXArray.Length <= 0) return;

        foreach (var vfx in m_muzzleVFXArray)
        {
            var obj = Instantiate(vfx, m_muzzleTF.position, m_muzzleTF.rotation);
            obj.transform.SetParent(m_muzzleTF);
        }

    }

    private void OnEnable()
    {
        m_fireRateTimer = m_fireRateInSeconds;
    }
}
