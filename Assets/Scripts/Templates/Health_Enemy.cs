using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Health_Enemy : Health
{
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
        GameObject obj = Instantiate(m_damagePopup, Random.insideUnitSphere + m_popupTF.position, m_popupTF.transform.rotation);

        TextMeshProUGUI popupText = obj.GetComponentInChildren<TextMeshProUGUI>();
        popupText.text = _damage.ToString();
        popupText.fontSize = Random.Range(m_damagePopupMinFontSize, m_damagePopupMinFontSizeMaxFontSize);
    }

    [Space(10)]
    [Header("Slowed Popup settings")]
    [Space(5)]
    [SerializeField] private GameObject m_slowedPopup;

    [SerializeField] private float m_slowedPopupMinFontSize;
    [SerializeField] private float m_slowedPopupMaxFontSize;

    private void SpawnSlowedPopup()
    {
        GameObject obj = Instantiate(m_slowedPopup, Random.insideUnitSphere + m_popupTF.position, m_popupTF.transform.rotation);
        TextMeshProUGUI popupText = obj.GetComponentInChildren<TextMeshProUGUI>();
        popupText.fontSize = Random.Range(m_slowedPopupMinFontSize, m_slowedPopupMaxFontSize);
    }

    [Space(10)]
    [Header("Stunned Popup settings")]
    [Space(5)]
    [SerializeField] private GameObject m_stunnedPopup;

    [SerializeField] private float m_stunnedPopupMinFontSize;
    [SerializeField] private float m_stunnedPopupMaxFontSize;

    private void SpawnStunnedPopup()
    {
        GameObject obj = Instantiate(m_stunnedPopup, Random.insideUnitSphere + m_popupTF.position, m_popupTF.transform.rotation);

        TextMeshProUGUI popupText = obj.GetComponentInChildren<TextMeshProUGUI>();
        popupText.fontSize = Random.Range(m_stunnedPopupMinFontSize, m_stunnedPopupMaxFontSize);
    }
}
