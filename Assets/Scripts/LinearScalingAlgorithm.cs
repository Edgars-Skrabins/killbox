using UnityEngine;

public class LinearScalingAlgorithm : MonoBehaviour
{
    
    private float m_sizeValue;
    [SerializeField] private float m_healthValue;
    [SerializeField] private float m_maxHealthValue;

    [ContextMenu("TakeDownValue")]
    private void TakeDownValue()
    {
        m_healthValue -= 0.5f;
    }
    
    private void Update()
    {
        float healthPercentage = m_healthValue / m_maxHealthValue;
        
        float originalScale = 10;
        m_sizeValue = originalScale * healthPercentage;
        //m_sizeValue = originalScale * (1 - healthPercentage);
        
        Mathf.Clamp(m_sizeValue, 1, 10);
        transform.localScale = new Vector3(m_sizeValue, m_sizeValue, m_sizeValue);
        

    }

}
