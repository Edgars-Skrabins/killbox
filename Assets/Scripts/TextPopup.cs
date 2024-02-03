using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{

    [SerializeField] private MMF_Player m_textPopupFeedback;
    [SerializeField] private float m_textPopupLifetime;

    [SerializeField] private AnimationCurve m_textColorAnimationCurve;
    [SerializeField] private TextMeshProUGUI m_textPopupText;

    private void OnEnable()
    {
        PlayTextPopupFeedback();        
    }

    private float m_textPopupLifetimeTimer;
    
    private void Update()
    {
        m_textPopupLifetimeTimer += Time.deltaTime;

        m_textPopupText.color = new Color(m_textPopupText.color.r, m_textPopupText.color.g, m_textPopupText.color.b, 
            m_textColorAnimationCurve.Evaluate(m_textPopupLifetimeTimer));
        
        if(m_textPopupLifetimeTimer > m_textPopupLifetime) Destroy(gameObject);
    }

    private void PlayTextPopupFeedback()
    {
        m_textPopupFeedback.Initialization();
        m_textPopupFeedback.PlayFeedbacks();
    }
}
