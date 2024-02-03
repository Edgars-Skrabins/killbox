using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainMenuTips : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI m_tipsTXT;
    [SerializeField] private string[] m_tipsArray;

    private int m_currentTipIndex;
    private void OnEnable()
    {
        if(m_tipsTXT)
        {
            m_tipsTXT.gameObject.SetActive(true);
        }
        ShowRandomTip();
    }

    private void OnDisable()
    {
        if(m_tipsTXT)
        {
            m_tipsTXT.gameObject.SetActive(false);
        }
    }

    public void ShowRandomTip()
    {
        int rand = Random.Range(0, m_tipsArray.Length);
        if(rand == m_currentTipIndex)
        {
            if (rand + 1 >= m_tipsArray.Length)
            {
                rand -= 1;
            }
            else if (rand - 1 < 0)
            {
                rand += 1;
            }
            else
            {
                rand += 1;
            }
        }
        m_currentTipIndex = rand;
        
        ShowTip(rand);
    }
    
    private void ShowTip(int _tipIndex)
    {
        if(m_tipsTXT)
        {
            m_tipsTXT.text = m_tipsArray[_tipIndex];
        }
    }

}
