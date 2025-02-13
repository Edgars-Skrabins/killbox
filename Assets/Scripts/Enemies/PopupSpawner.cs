using TMPro;
using UnityEngine;

public class PopupSpawner : MonoBehaviour
{
    [Space(5)]
    [Header("General Popup settings")]
    [Space(5)]
    [SerializeField] private Transform m_popupSpawnLocationTF;
    [Space(10)]
    [Header("Damage Popup settings")]
    [Space(5)]
    [SerializeField] private GameObject m_damagePopupGO;
    [SerializeField] private float m_damagePopupMinFontSize;
    [SerializeField] private float m_damagePopupMinFontSizeMaxFontSize;

    public void SpawnDamagePopup(int _damage)
    {
        GameObject obj = Instantiate(m_damagePopupGO, Random.insideUnitSphere + m_popupSpawnLocationTF.position,
            m_popupSpawnLocationTF.transform.rotation);

        TextMeshProUGUI popupText = obj.GetComponentInChildren<TextMeshProUGUI>();
        popupText.text = _damage.ToString();
        popupText.fontSize = Random.Range(m_damagePopupMinFontSize, m_damagePopupMinFontSizeMaxFontSize);
    }

    [Space(10)]
    [Header("Slowed Popup settings")]
    [Space(5)] [SerializeField]
    private GameObject m_slowedPopupGO;
    [SerializeField] private float m_slowedPopupMinFontSize;
    [SerializeField] private float m_slowedPopupMaxFontSize;

    public void SpawnSlowedPopup()
    {
        GameObject obj = Instantiate(m_slowedPopupGO, Random.insideUnitSphere + m_popupSpawnLocationTF.position,
            m_popupSpawnLocationTF.transform.rotation);
        TextMeshProUGUI popupText = obj.GetComponentInChildren<TextMeshProUGUI>();
        popupText.fontSize = Random.Range(m_slowedPopupMinFontSize, m_slowedPopupMaxFontSize);
    }

    [Space(10)]
    [Header("Stunned Popup settings")]
    [Space(5)]
    [SerializeField] private GameObject m_stunnedPopupGO;
    [SerializeField] private float m_stunnedPopupMinFontSize;
    [SerializeField] private float m_stunnedPopupMaxFontSize;

    public void SpawnStunnedPopup()
    {
        GameObject obj = Instantiate(m_stunnedPopupGO, Random.insideUnitSphere + m_popupSpawnLocationTF.position,
            m_popupSpawnLocationTF.transform.rotation);

        TextMeshProUGUI popupText = obj.GetComponentInChildren<TextMeshProUGUI>();
        popupText.fontSize = Random.Range(m_stunnedPopupMinFontSize, m_stunnedPopupMaxFontSize);
    }

    [Space(10)]
    [Header("Charged Popup settings")]
    [Space(5)]
    [SerializeField] private GameObject m_chargedPopupGO;
    [SerializeField] private float m_chargedPopupMinFontSize;
    [SerializeField] private float m_chargedPopupMaxFontSize;

    public void SpawnChargedPopup()
    {
        GameObject obj = Instantiate(m_chargedPopupGO, Random.insideUnitSphere + m_popupSpawnLocationTF.position,
            m_popupSpawnLocationTF.transform.rotation);

        TextMeshProUGUI popupText = obj.GetComponentInChildren<TextMeshProUGUI>();
        popupText.fontSize = Random.Range(m_chargedPopupMinFontSize, m_chargedPopupMaxFontSize);
    }

    [Space(10)]
    [Header("Befriend Popup settings")]
    [Space(5)]
    [SerializeField] private GameObject m_befriendPopupGO;
    [SerializeField] private float m_befriendPopupMinFontSize;
    [SerializeField] private float m_befriendPopupMaxFontSize;

    public void SpawnBefriendPopup()
    {
        GameObject obj = Instantiate(m_befriendPopupGO, Random.insideUnitSphere + m_popupSpawnLocationTF.position,
            m_popupSpawnLocationTF.transform.rotation);

        TextMeshProUGUI popupText = obj.GetComponentInChildren<TextMeshProUGUI>();
        popupText.fontSize = Random.Range(m_befriendPopupMinFontSize, m_befriendPopupMaxFontSize);
    }
}
