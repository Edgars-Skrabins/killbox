using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private Vector3 m_originalScale;
    private float m_duration = ButtonAnimVars.M_Duration;
    private float m_upScaleMultiplier = ButtonAnimVars.M_UpScaleMultiplier;

    private Texture2D m_customCursor;
    private Texture2D m_customCursorHover;
    private string m_cursorTexturePath = ButtonAnimVars.M_CursorPath;
    private string m_cursorHoverTexturePath = ButtonAnimVars.M_CursorHoverPath;

    private bool m_isHovering;

    void Start()
    {
        SetCustomCursor(m_cursorTexturePath);
        SetCustomHoverCursor(m_cursorHoverTexturePath);
        m_originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(m_customCursorHover, new Vector2(m_customCursor.width / 2, m_customCursor.height / 2), CursorMode.Auto);
        m_isHovering = true;
        StartCoroutine(ScaleAnimation());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(m_customCursor, new Vector2(m_customCursor.width/2, m_customCursor.height / 2), CursorMode.Auto);
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

    private void SetCustomCursor(string imagePath)
    {
        byte[] fileData = System.IO.File.ReadAllBytes(imagePath);
        m_customCursor = new Texture2D(2, 2);
        m_customCursor.LoadImage(fileData);
    }

    private void SetCustomHoverCursor(string imagePath)
    {
        byte[] fileData = System.IO.File.ReadAllBytes(imagePath);
        m_customCursorHover = new Texture2D(2, 2);
        m_customCursorHover.LoadImage(fileData);
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
