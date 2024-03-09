using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class LowGraphics : MonoBehaviour
{
    public static string M_GRAPHICS_PREF = "GraphicsSettings";
    private Toggle m_toggleLowGraphicsSettings;

    [SerializeField] private RenderPipelineAsset[] m_qualityLevels;

    private void Awake()
    {
        if (!m_toggleLowGraphicsSettings) m_toggleLowGraphicsSettings = GetComponent<Toggle>();

        if (PlayerPrefs.HasKey(M_GRAPHICS_PREF))
        {
            m_toggleLowGraphicsSettings.isOn = PlayerPrefs.GetInt(M_GRAPHICS_PREF) == 0;
            QualitySettings.SetQualityLevel(0);
        }
        else
        {
            m_toggleLowGraphicsSettings.isOn = true;
            EnableHighGraphics(true);
        }

    }

    private void OnEnable()
    {

        m_toggleLowGraphicsSettings.onValueChanged.AddListener(EnableHighGraphics);
    }

    private void OnDisable()
    {
        m_toggleLowGraphicsSettings.onValueChanged.RemoveListener(EnableHighGraphics);
    }

    private void EnableHighGraphics(bool _isOn)
    {
        if (_isOn)
        {
            PlayerPrefs.SetInt(M_GRAPHICS_PREF, 0);
            QualitySettings.SetQualityLevel(0);
            QualitySettings.renderPipeline = m_qualityLevels[0];
            return;
        }
        PlayerPrefs.SetInt(M_GRAPHICS_PREF, 1);
        QualitySettings.SetQualityLevel(1);
        QualitySettings.renderPipeline = m_qualityLevels[1];
    }
}
