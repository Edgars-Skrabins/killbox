using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] private WeaponRandomizer m_weaponRandomizerCS;
    
    [SerializeField] public GameObject m_playMenu;
    [SerializeField] private Image m_weaponRandomizerTimer_UI;
    [SerializeField] private Image m_playerHealth_UI;
    [SerializeField] private TextMeshProUGUI m_weaponText;
    [SerializeField] private TextMeshProUGUI m_scoreText;
    [SerializeField] private TextMeshProUGUI m_playScoreText;

#region Pause Menu
    [SerializeField] public GameObject m_pauseMenu;

    //[SerializeField] private TextMeshProUGUI m_pauseHighscoreText;

    [SerializeField] public Slider m_sensitivitySlider;
    [SerializeField] public Slider m_audioSlider;
    [SerializeField] public Slider m_musicSlider;
    [SerializeField] public Slider m_sfxSlider;


    #endregion

    #region Death Menu
    [SerializeField] public GameObject m_deathMenu;

    [SerializeField] private TextMeshProUGUI m_deathHighscoreText;
    [SerializeField] private TextMeshProUGUI m_deathFinalScore;



    #endregion
    public GameObject m_settingsMenu;
    public GameObject m_leaderboardMenu;

    private void Awake()
    {
        m_audioSlider.onValueChanged.AddListener(SetMasterVolume);
        m_musicSlider.onValueChanged.AddListener(SetMusicVolume);
        m_sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        m_sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
        m_audioSlider.value = PlayerPrefs.GetFloat(AudioManager.I.Master_Volume);
        m_musicSlider.value = PlayerPrefs.GetFloat(AudioManager.I.Music_Volume);
        m_sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.I.SFX_Volume);

        PlayerStats.I.MouseSensitivity = m_sensitivitySlider.value;

        if (!AudioManager.I.Playing("BGM_Game"))
        {
            AudioManager.I.Play("BGM_Game");
        }
        if (AudioManager.I.Playing("BGM_MainMenu"))
        {
            AudioManager.I.Stop("BGM_MainMenu");
        }
        if (AudioManager.I.Playing("BGM_Death"))
        {
            AudioManager.I.Stop("BGM_Death");
        }
    }
    

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (GameManager.I.IsGamePaused)
            {
                GameManager.I.UnpauseGame();
            }
            else
            {
                GameManager.I.PauseGame();
            }

        }

        //PlayerStats.I.MouseSensitivity = m_sensitivitySlider.value;
        
        UpdateMenus();
        UpdateHUD();

    }

    private void UpdateHUD()
    {
        m_playerHealth_UI.fillAmount = (float)PlayerStats.I.PlayerHealth / PlayerStats.I.PlayerMaxHealth;

        m_weaponRandomizerTimer_UI.fillAmount = m_weaponRandomizerCS.m_randomizeTimer / m_weaponRandomizerCS.m_randomizeFrequencyInSeconds;
        m_scoreText.text = PlayerStats.I.PlayerScore.ToString();
        m_weaponText.text = WeaponManager.I.m_currentWeaponName;

        /*
        m_playerHealth_UItext = PlayerStats.I.PlayerHealth.ToString();

        m_weaponRandomizerTimer_UI.text = m_weaponRandomizerCS.m_randomizeTimer.ToString(CultureInfo.CurrentCulture);
        */
    }

    private void UpdateMenus()
    {
        m_playScoreText.text = PlayerStats.I.PlayerScore.ToString();

        if (!GameManager.I.IsPlayerAlive)
        {
            m_deathHighscoreText.text = PlayerStats.I.PlayerHighScore.ToString();
            m_deathFinalScore.text = PlayerStats.I.PlayerScore.ToString();
            m_deathMenu.SetActive(true);
            return;
        }

        if (GameManager.I.IsGamePaused)
        {
            m_pauseMenu.SetActive(true);
        }
        else
        {
            m_pauseMenu.SetActive(false);
        }
    }

    public void RestartLevel()
    {
        SaveSettingPrefs();

        SceneManager.LoadScene("Level_Medieval");
    }

    public void QuitToMenu()
    {
        SaveSettingPrefs();

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

        if (GameManager.I.IsGamePaused)
        {
            GameManager.I.UnpauseGame();
        }

        SceneManager.LoadScene("MainMenu");
    }

    private void SaveSettingPrefs()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", m_sensitivitySlider.value);
        PlayerPrefs.SetFloat(AudioManager.I.Master_Volume, m_audioSlider.value);
        PlayerPrefs.SetFloat(AudioManager.I.Music_Volume, m_musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.I.SFX_Volume, m_sfxSlider.value);
        PlayerPrefs.Save();
    }

    public void QuitGame()
    {
        SaveSettingPrefs();

        Application.Quit();
    }


    private void SetMasterVolume(float value)
    {
        AudioManager.I.Mixer.SetFloat(AudioManager.I.Master_Volume, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(AudioManager.I.Master_Volume, m_audioSlider.value);
        PlayerPrefs.Save();
    }

    private void SetMusicVolume(float value)
    {
        AudioManager.I.Mixer.SetFloat(AudioManager.I.Music_Volume, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(AudioManager.I.Music_Volume, m_musicSlider.value);
        PlayerPrefs.Save();
    }

    private void SetSFXVolume(float value)
    {
        AudioManager.I.Mixer.SetFloat(AudioManager.I.SFX_Volume, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(AudioManager.I.SFX_Volume, m_sfxSlider.value);
        PlayerPrefs.Save();
    }
}
