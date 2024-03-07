using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvasGroup;

    [SerializeField] private float m_duration = 5f;
    [SerializeField] private AnimationCurve m_fadeCurve;

    [SerializeField] private AudioClip m_audioClip;
    [SerializeField] [Range(0f, 1f)] private float m_volume;
    private AudioSource m_audioSource;

    private void Awake()
    {
        if (m_audioClip)
        {
            m_audioSource = gameObject.AddComponent<AudioSource>();
            m_audioSource.playOnAwake = false;
            m_audioSource.loop = false;
            m_audioSource.volume = m_volume;
            m_audioSource.clip = m_audioClip;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(PlayAnimation());
    }

    private void Update()
    {
        if (Input.GetButtonUp("Jump"))
        {
            SkipAnimation();
        }
    }

    private void SkipAnimation()
    {
        StopCoroutine(PlayAnimation());
        LoadNextScene();
    }

    private IEnumerator PlayAnimation()
    {
        if (m_audioClip)
        {
            m_audioSource.Play();
        }

        float elapsedTime = 0f;

        while (elapsedTime < m_duration)
        {
            float normalizedTime = elapsedTime / m_duration;
            float alpha = m_fadeCurve.Evaluate(normalizedTime);

            m_canvasGroup.alpha = alpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        m_canvasGroup.alpha = 0;
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
