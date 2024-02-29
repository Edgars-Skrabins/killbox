using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisabler : MonoBehaviour
{
    [SerializeField] private float m_waitDuration = 3f;
    [SerializeField] private float m_fadeDuration = 2f;
    [SerializeField] private CanvasGroup m_canvasGroup;

    private void OnEnable()
    {
        m_canvasGroup.alpha = 1f;
        StartCoroutine(StartFadeAnimation());
    }

    private IEnumerator StartFadeAnimation()
    {
        float elapsedTime = 0f;
        yield return new WaitForSeconds(m_waitDuration);

        while (elapsedTime < m_fadeDuration)
        {
            m_canvasGroup.alpha = Mathf.Lerp(m_canvasGroup.alpha, 0, elapsedTime / m_fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        m_canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}
