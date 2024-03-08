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
    [SerializeField] private Slider m_musicSlider;
    [SerializeField] private Slider m_sfxSlider;
    [SerializeField] private AudioMixerGroup GroupMasterVolume;
    [SerializeField] private GameObject Canvas;

    private void Awake()
    {
        m_audioSlider.onValueChanged.AddListener(SetMasterVolume);
        m_musicSlider.onValueChanged.AddListener(SetMusicVolume);
        m_sfxSlider.onValueChanged.AddListener(SetSFXVolume);

    }

    private void FixedUpdate()
    {
        LoadingScreenAnimation.I.DisableLoadingScreen();
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
        m_audioSlider.value = PlayerPrefs.GetFloat(AudioManager.I.Master_Volume, 0.5f);
        m_musicSlider.value = PlayerPrefs.GetFloat(AudioManager.I.Music_Volume, 0.5f);
        m_sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.I.SFX_Volume, 0.5f);

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

    private void SetMasterVolume(float value)
    {
        AudioManager.I.Mixer.SetFloat(AudioManager.I.Master_Volume, Mathf.Log10(value) * 20);
    }
    private void SetMusicVolume(float value)
    {
        AudioManager.I.Mixer.SetFloat(AudioManager.I.Music_Volume, Mathf.Log10(value) * 20);
    }
    private void SetSFXVolume(float value)
    {
        AudioManager.I.Mixer.SetFloat(AudioManager.I.SFX_Volume, Mathf.Log10(value) * 20);
    }

    public void LoadGame()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", m_sensitivitySlider.value);
        PlayerPrefs.SetFloat(AudioManager.I.Master_Volume, m_audioSlider.value);
        PlayerPrefs.SetFloat(AudioManager.I.Music_Volume, m_musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.I.SFX_Volume, m_sfxSlider.value);

        PlayerPrefs.Save();

        LoadingScreenAnimation.I.EnableLoadingScreen();

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
