using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenAnimation : MonoBehaviour
{
    public static LoadingScreenAnimation I { get; private set; }

    [SerializeField] private TMPro.TMP_Text m_animatedText;
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private float m_fadeDuration = 0.3f;
    [SerializeField] private float m_waitBetweenDuration = 0.2f;

    private void Awake()
    {
        if (I != this && I != null)
        {
            Destroy(this);
        }
        else if (I == null)
        {
            I = this;
        }
    }

    private void FixedUpdate()
    {
        if (m_canvasGroup.gameObject.activeInHierarchy)
        {
            StartCoroutine(Animate());
        }
        else
        {
            StopCoroutine(Animate());
        }    
    }

    public void EnableLoadingScreen()
    {
        m_canvasGroup.alpha = 0;
        m_canvasGroup.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    public void DisableLoadingScreen()
    {
        if (m_canvasGroup.gameObject.activeInHierarchy) StartCoroutine(FadeOut());
    }

    private void AnimateText()
    {
        if (m_animatedText.text == "...")
        {
            m_animatedText.text = "";
        }
        else
        {
            m_animatedText.text += ".";
        }
    }

    private IEnumerator Animate()
    {
        while (m_canvasGroup.gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(m_waitBetweenDuration);

            AnimateText();
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < m_fadeDuration)
        {
            m_canvasGroup.alpha = Mathf.Lerp(m_canvasGroup.alpha, 1, elapsedTime / m_fadeDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        m_canvasGroup.alpha = 1;
    }

    private IEnumerator FadeOut()
    {
        m_canvasGroup.alpha = 1;
        float elapsedTime = 0f;

        while (elapsedTime < m_fadeDuration)
        {
            m_canvasGroup.alpha = Mathf.Lerp(m_canvasGroup.alpha, 0, elapsedTime / m_fadeDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        m_canvasGroup.alpha = 0;
        m_canvasGroup.gameObject.SetActive(false);
    }
}
