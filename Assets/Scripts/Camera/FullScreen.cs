using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{

    [SerializeField] private Toggle m_fullscreenToggleButton;

    public void ChangeFullScreen()
    {
        if(m_fullscreenToggleButton.isOn)
        {
            Screen.fullScreen = true;
        }
        else if(!m_fullscreenToggleButton.isOn)
        {
            Screen.fullScreen = false;
        }
    }
}
