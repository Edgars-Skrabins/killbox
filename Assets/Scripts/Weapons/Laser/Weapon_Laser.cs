using System;
using Killbox.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon_Laser : MonoBehaviour
{
    [SerializeField] private int m_weaponDamage;
    [SerializeField] private EDamageTypes m_damageType;
    [SerializeField] private bool m_doesChargeTarget;
    [SerializeField] private int m_specialEnemyExplosionDamage;
    [SerializeField] private float m_fireRateInSeconds;

    [Space(10)]
    [Header("Laser graphics")]
    [Space(5)]
    [SerializeField] private LayerMask m_laserLayers;
    [SerializeField] private Transform m_firePointTF;
    [SerializeField] private LineRenderer m_laserPrefab;

    [Space(5)]
    [Header("Laser settings")]
    [SerializeField] private int m_extraDamageScaling;
    [SerializeField] private float m_laserRange;

    #region Laser class

    [Serializable]
    public class Laser
    {
        public LineRenderer m_Line;
    }

    #endregion

    [SerializeField] private Laser[] m_laserArray;

    [Space(20)]
    [Header(" ----- FirePointVisibleSettings ----- ")]
    [Space(5)]
    [SerializeField] private GameObject m_crossHairImage;
    [SerializeField] private GameObject m_cantShootImage;

    [Space(20)]
    [Header(" ----- SoundSettings ----- ")]
    [Space(5)]
    [SerializeField] private string m_shootSFX;
    [SerializeField] private string[] m_firstShotSFX;
    [SerializeField] private string m_lastShotSFX;

    private float[] m_fireRateTimers;

    private void Start()
    {
        InitializeLasers();
    }

    private void InitializeLasers()
    {
        foreach (var laserArray in m_laserArray)
        {
            laserArray.m_Line = Instantiate(m_laserPrefab, transform.position, Quaternion.identity);
            laserArray.m_Line.enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (!CheckIfFirePointVisible() || GameManager.I.IsGamePaused || !GameManager.I.IsPlayerAlive) return;

        CountTimer();
        Inputs();
    }


    private void Update()
    {
        FirePointVisibleGFX();
    }

    private void FirePointVisibleGFX()
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

    private void CountTimer()
    {
        for (int i = 0; i < m_fireRateTimers.Length; i++)
        {
            m_fireRateTimers[i] += Time.deltaTime;
        }
    }

    private void Inputs()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CheckIfFirePointVisible())
        {
            ShootLaser();
            if (!AudioManager.I.Playing(m_shootSFX))
            {
                AudioManager.I.Play(m_shootSFX);
            }
        }
        else
        {
            TurnAllLaserOff();
            if (AudioManager.I.Playing(m_shootSFX))
            {
                AudioManager.I.Stop(m_shootSFX);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AudioManager.I.Play(m_firstShotSFX[Random.Range(0, m_firstShotSFX.Length)]);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            AudioManager.I.Play(m_lastShotSFX);
        }
    }


    private Vector3 laserDirection;

    private void ShootLaser()
    {
        RaycastHit[] raycastHits = new RaycastHit[m_laserArray.Length];
        bool hasShotFirstLaser = false;

        for (int i = 0; i < m_laserArray.Length; i++)
        {
            if (!hasShotFirstLaser)
            {
                if (Physics.Raycast(m_firePointTF.position, m_firePointTF.forward, out raycastHits[i], m_laserRange, m_laserLayers))
                {
                    TurnLaserOn(m_firePointTF.position, raycastHits[i].point, i);

                    laserDirection = Vector3.Reflect(m_firePointTF.forward, raycastHits[i].normal);

                    if (raycastHits[i].collider.TryGetComponent(out Health health))
                    {
                        if (!CanShoot(m_fireRateTimers[i])) return;

                        health.TakeDamage(m_weaponDamage, m_damageType, m_doesChargeTarget);
                        m_fireRateTimers[i] = 0;

                        for (int j = 1; j < m_laserArray.Length; j++)
                        {
                            Laser obj = m_laserArray[j];

                            obj.m_Line.enabled = false;
                        }

                        return;
                    }
                    hasShotFirstLaser = true;
                }
                else
                {
                    Vector3 inFrontOfFirePoint = m_firePointTF.position + m_firePointTF.forward * m_laserRange;
                    TurnLaserOn(m_firePointTF.position, inFrontOfFirePoint, i);

                    for (int j = 1; j < m_laserArray.Length; j++)
                    {
                        Laser obj = m_laserArray[j];

                        obj.m_Line.enabled = false;
                    }

                    return;
                }
            }
            else
            {
                if (Physics.Raycast(raycastHits[i - 1].point, laserDirection, out raycastHits[i], m_laserRange, m_laserLayers))
                {
                    TurnLaserOn(raycastHits[i - 1].point, raycastHits[i].point, i);

                    if (raycastHits[i].collider.TryGetComponent(out Health health))
                    {
                        if (!CanShoot(m_fireRateTimers[i])) return;

                        health.TakeDamage(m_weaponDamage + m_extraDamageScaling * i, m_damageType, m_doesChargeTarget);
                        m_fireRateTimers[i] = 0;

                        for (int j = i + 1; j < m_laserArray.Length; j++)
                        {
                            Laser objB = m_laserArray[j];

                            objB.m_Line.enabled = false;
                        }

                        return;
                    }
                    laserDirection = Vector3.Reflect(laserDirection, raycastHits[i].normal);
                }
                else
                {
                    Vector3 inFrontOfFirePoint = raycastHits[i - 1].point + laserDirection * m_laserRange;
                    TurnLaserOn(raycastHits[i - 1].point, inFrontOfFirePoint, i);
                    for (int j = i + 1; j < m_laserArray.Length; j++)
                    {
                        Laser objB = m_laserArray[j];

                        objB.m_Line.enabled = false;
                    }
                    return;
                }
            }
        }
    }

    private void TurnLaserOn(Vector3 _startingPosition, Vector3 _endPosition, int _laserIndex)
    {
        m_laserArray[_laserIndex].m_Line.enabled = true;
        m_laserArray[_laserIndex].m_Line.SetPosition(0, _startingPosition);
        m_laserArray[_laserIndex].m_Line.SetPosition(1, _endPosition);
    }

    private void TurnAllLaserOff()
    {
        foreach (var laser in m_laserArray)
        {
            laser.m_Line.enabled = false;
        }
        if (AudioManager.I.Playing(m_shootSFX))
        {
            AudioManager.I.Stop(m_shootSFX);
        }
    }

    private bool CanShoot(float timer)
    {
        return timer >= m_fireRateInSeconds;
    }

    [SerializeField] private LayerMask m_firePointVisibleLayer;

    private bool CheckIfFirePointVisible()
    {
        Vector3 cameraToMuzzle = m_firePointTF.position - PlayerStats.I.PlayerCamera.transform.position;

        if (Physics.Raycast(PlayerStats.I.PlayerCamera.transform.position, cameraToMuzzle, out RaycastHit hit, 100f,
                m_firePointVisibleLayer))
        {
            if (hit.collider.CompareTag("Muzzle"))
            {
                return true;
            }
        }

        return false;
    }

    private void OnEnable()
    {
        if (m_fireRateTimers == null)
        {
            m_fireRateTimers = new float[m_laserArray.Length];
        }

        for (int i = 0; i < m_fireRateTimers.Length; i++)
        {
            m_fireRateTimers[i] = m_fireRateInSeconds;
        }
    }

    private void OnDisable()
    {
        TurnAllLaserOff();
    }
}
