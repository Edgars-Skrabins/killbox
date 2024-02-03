using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI m_highscoreText;
    [SerializeField] private Slider m_sensitivitySlider;
    [SerializeField] private Slider m_audioSlider;
    [SerializeField] private AudioMixerGroup GroupMasterVolume;
    [SerializeField] private GameObject Canvas;

    private void Awake()
    {
        m_audioSlider.onValueChanged.AddListener(SetMusicVolume);

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        // main menu
        Canvas.transform.GetChild(0).gameObject.SetActive(true);
        // settings
        Canvas.transform.GetChild(1).gameObject.SetActive(false);

        m_highscoreText.text = PlayerPrefs.GetInt("PlayerHighscore").ToString();

        //if (PlayerPrefs.HasKey("PlayerHighscore"))
        //
        //  m_highscoreText.text = PlayerPrefs.GetInt("PlayerHighscore").ToString();
        //}

        m_sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 50);
        m_audioSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);

        //Music BGM
        if (!AudioManager.I.Playing("BGM_MainMenu"))
        {
            AudioManager.I.Play("BGM_MainMenu");
        }
        if (AudioManager.I.Playing("BGM_Game"))
        {
            AudioManager.I.Stop("BGM_Game");
        }
        if (AudioManager.I.Playing("BGM_Death"))
        {
            AudioManager.I.Stop("BGM_Death");
        }
    }

    private void SetMusicVolume(float value)
    {
        AudioManager.I.Mixer.SetFloat(AudioManager.I.Master_Volume, Mathf.Log10(value) * 20);
    }

    public void LoadGame()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", m_sensitivitySlider.value);
        PlayerPrefs.SetFloat("MasterVolume", m_audioSlider.value);

        PlayerPrefs.Save();

        if (!AudioManager.I.Playing("BGM_Game"))
        {
            AudioManager.I.Play("BGM_Game");
        }
        if (AudioManager.I.Playing("BGM_MainMenu"))
        {
            AudioManager.I.Stop("BGM_MainMenu");
        }

        SceneManager.LoadScene("Level_Medieval");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
