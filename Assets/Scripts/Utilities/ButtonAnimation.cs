using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private Vector3 m_originalScale;
    private float m_duration = ButtonAnimVars.M_Duration;
    private float m_upScaleMultiplier = ButtonAnimVars.M_UpScaleMultiplier;
    private float m_downScaleMultiplier = ButtonAnimVars.M_DownScaleMultiplier;

    private bool m_isHovering;

    void Start()
    {
        m_originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_isHovering = true;
        StartCoroutine(ScaleAnimation());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_isHovering = false;
        if (gameObject.activeInHierarchy)
        {
            if (!m_isHovering)
            {
                StopAllCoroutines();
            }
            StartCoroutine(ScaleAnimation());
        }
    }

    private IEnumerator ScaleAnimation()
    {
        Vector3 targetScale;

        if (m_isHovering)
        {
            targetScale = m_originalScale * m_upScaleMultiplier;
        }
        else
        {
            targetScale = m_originalScale;
        }

        float elapsedTime = 0f;

        while (elapsedTime < m_duration)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, elapsedTime / m_duration);
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
