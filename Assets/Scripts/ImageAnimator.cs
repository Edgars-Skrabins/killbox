using UnityEngine;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour
{
    [SerializeField] Sprite[] m_sprite;
    [SerializeField] private float m_maxFlipRate;
    private float m_flipRate;
    private Image m_image;
    private float m_time;
    private int m_imageNumber;

    
    void Start()
    {
        m_image = GetComponent<Image>();
        m_imageNumber = 0;
    }

    void Update()
    {
        Animate();
        
        HandleFlipRate();
    }

    private void HandleFlipRate()
    {
        m_flipRate = m_maxFlipRate * ((float)PlayerStats.I.PlayerHealth / PlayerStats.I.PlayerMaxHealth);
    }

    private void Animate()
    {
        m_time += Time.deltaTime;

        if (m_time > m_flipRate)
        {
            m_image.sprite = m_sprite[m_imageNumber];
            if (m_imageNumber == m_sprite.Length - 1)
            {
                m_imageNumber = 0;
            }
            else
            {
                m_imageNumber++;
            }
            m_time = 0;
        }
    }
}
