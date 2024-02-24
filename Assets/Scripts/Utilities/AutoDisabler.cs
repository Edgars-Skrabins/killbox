using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisabler : MonoBehaviour
{
    private float m_fadeDuration = 2f;
    private float m_waitDuration = 2f;
    [SerializeField] private CanvasGroup m_alphaGroup;

    private void OnEnable()
    {
        m_alphaGroup.alpha = 1f;
        StartCoroutine(StartFading());
    }

    private IEnumerator StartFading()
    {
        yield return new WaitForSeconds(m_waitDuration);

        float elapsedTime = 0f;

        while (elapsedTime < m_fadeDuration)
        {
            m_alphaGroup.alpha = Mathf.Lerp(m_alphaGroup.alpha, 0f, elapsedTime / m_fadeDuration);
            elapsedTime += Time.fixedDeltaTime;
        }

        m_alphaGroup.alpha = 0f;
        gameObject.SetActive(false);

    }
}
