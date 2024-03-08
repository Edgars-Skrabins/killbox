using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIndicatorUI : MonoBehaviour
{
    public static string M_WINDICATOR_PREF = "WeaponIndicator";
    private Toggle m_toggleWeaponIndicator;

    private void Awake()
    {
        if(!m_toggleWeaponIndicator) m_toggleWeaponIndicator = GetComponent<Toggle>();

        if (PlayerPrefs.HasKey(M_WINDICATOR_PREF))
        {
            m_toggleWeaponIndicator.isOn = PlayerPrefs.GetInt(M_WINDICATOR_PREF) != 0;
        }
        else
        {
            m_toggleWeaponIndicator.isOn = true;
            EnableWeaponIndicator(true);
        }

    }

    private void OnEnable()
    {

        m_toggleWeaponIndicator.onValueChanged.AddListener(EnableWeaponIndicator);
    }

    private void OnDisable()
    {
        m_toggleWeaponIndicator.onValueChanged.RemoveListener(EnableWeaponIndicator);
    }

    private void EnableWeaponIndicator(bool _isOn)
    {
        if (_isOn)
        {
            PlayerPrefs.SetInt(M_WINDICATOR_PREF, 1);
            return;
        }

        PlayerPrefs.SetInt(M_WINDICATOR_PREF, 0);

    }
}
