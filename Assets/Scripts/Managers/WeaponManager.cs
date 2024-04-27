using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponManager : Singleton<WeaponManager>
{
    #region Weapon Class

    [Serializable]
    public class Weapon
    {
        public string m_weaponName;
        public GameObject m_weaponGO;
        public GameObject m_weaponIcon;
    }

    #endregion

    public Weapon[] m_Weapons;

    [HideInInspector] public string m_currentWeaponName;
    [SerializeField] private WeaponRandomizer m_WeaponRandomizerCS;

    protected override void Awake()
    {
        base.Awake();

        m_currentWeaponName = m_Weapons[0].m_weaponName;
        EquipWeapon(m_currentWeaponName);
    }

    public void EquipWeapon(string _weaponName)
    {
        m_currentWeaponName = _weaponName;

        foreach (var weapon in m_Weapons)
        {
            if (weapon.m_weaponName == _weaponName)
            {
                weapon.m_weaponGO.SetActive(true);
            }
            else
            {
                weapon.m_weaponGO.SetActive(false);
            }
        }
    }

    [ContextMenu("EquipRandomWeapon")]
    public void EquipRandomWeapon()
    {
        int rand = Random.Range(0, m_Weapons.Length);

        if (m_currentWeaponName == m_Weapons[rand].m_weaponName)
        {
            if (m_Weapons.Length == 1)
            {
                return;
            }

            if (rand + 1 >= m_Weapons.Length)
            {
                rand -= 1;
            }
            else if (rand - 1 < 0)
            {
                rand += 1;
            }
            else
            {
                rand += 1;
            }
        }

        EquipWeapon(m_Weapons[rand].m_weaponName);
        m_WeaponRandomizerCS.ResetRandomizerTimer();
    }

    public void ShowNextWeaponIcon(string _weaponName)
    {
        foreach (var weapon in m_Weapons)
        {
            if (weapon.m_weaponName == _weaponName)
            {
                weapon.m_weaponIcon.SetActive(true);
            }
            else
            {
                weapon.m_weaponIcon.SetActive(false);
            }
        }
    }

    public string GetRandomWeaponName()
    {
        int rand = Random.Range(0, m_Weapons.Length);

        if (m_currentWeaponName == m_Weapons[rand].m_weaponName)
        {
            if (m_Weapons.Length == 1)
            {
                return null;
            }

            if (rand + 1 >= m_Weapons.Length)
            {
                rand -= 1;
            }
            else if (rand - 1 < 0)
            {
                rand += 1;
            }
            else
            {
                rand += 1;
            }
        }

        return m_Weapons[rand].m_weaponName;
    }
}