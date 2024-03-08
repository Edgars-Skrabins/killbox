using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsGamePaused {get; set;}

    public bool IsPlayerAlive {get; set;}

    [SerializeField] private float m_timeSinceGameStart;
    public float TimeSinceGameStart {get => m_timeSinceGameStart; set => m_timeSinceGameStart = value;}

    private GameObject m_player;
    private PlayerUI m_playerUI;

    private void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }

    private void Update()
    {
        CountGameTimer();
    }

    private void CountGameTimer()
    {
        TimeSinceGameStart += Time.deltaTime;
    }

    public void PauseGame()
    {
        if (!IsPlayerAlive) return;

        IsGamePaused = true;
        AudioManager.I.PauseAllSounds();
        EventManager.I.OnGamePaused_Invoke();

        Time.timeScale = 0;

        m_player = GameObject.FindWithTag("Player");
        if (m_player != null)
        {
            m_playerUI = m_player.GetComponent<PlayerUI>();
            Cursor.lockState = CursorLockMode.None;
            m_player.GetComponent<PlayerControls>().enabled = false;
            m_playerUI.m_playMenu.SetActive(false);
            m_playerUI.m_pauseMenu.SetActive(true);
            m_playerUI.m_deathMenu.SetActive(false);
            m_playerUI.m_settingsMenu.SetActive(false);
            m_playerUI.m_settingsSoundMenu.SetActive(false);
            m_playerUI.m_settingsGeneralMenu.SetActive(false);
            m_playerUI.m_leaderboardMenu.SetActive(false);
        }
    }

    public void UnpauseGame()
    {
        if (!IsPlayerAlive) return;

        IsGamePaused = false;
        AudioManager.I.UnPauseAllSounds();
        EventManager.I.OnGameUnPaused_Invoke();

        Time.timeScale = 1;

        m_player = GameObject.FindWithTag("Player");
        if (m_player != null)
        {
            if (m_playerUI == null) m_playerUI = m_player.GetComponent<PlayerUI>();

            Cursor.lockState = CursorLockMode.Locked;
            m_player.GetComponent<PlayerControls>().enabled = true;
            m_playerUI.m_playMenu.SetActive(true);
            m_playerUI.m_pauseMenu.SetActive(false);
            m_playerUI.m_deathMenu.SetActive(false);
            m_playerUI.m_settingsMenu.SetActive(false);
            m_playerUI.m_settingsSoundMenu.SetActive(false);
            m_playerUI.m_settingsGeneralMenu.SetActive(false);
            m_playerUI.m_leaderboardMenu.SetActive(false);

            PlayerStats.I.MouseSensitivity = m_player.GetComponent<PlayerUI>().m_sensitivitySlider.value;
            PlayerPrefs.SetFloat("MouseSensitivity", m_player.GetComponent<PlayerUI>().m_sensitivitySlider.value);
            PlayerPrefs.SetFloat(AudioManager.I.Master_Volume, m_playerUI.m_audioSlider.value);
            PlayerPrefs.SetFloat(AudioManager.I.Music_Volume, m_playerUI.m_musicSlider.value);
            PlayerPrefs.SetFloat(AudioManager.I.SFX_Volume, m_playerUI.m_sfxSlider.value);
            PlayerPrefs.Save();
        }
    }

    public void PlayerAlive()
    {
        IsPlayerAlive = true;
    }

    public void PlayerDead()
    {
        IsGamePaused = true;
        Time.timeScale = 0;
        EventManager.I.OnGamePaused_Invoke();

        IsPlayerAlive = false;

        if (!AudioManager.I.Playing("BGM_Death"))
        {
            AudioManager.I.Play("BGM_Death");
        }
        if (AudioManager.I.Playing("BGM_Game"))
        {
            AudioManager.I.Stop("BGM_Game");
        }

        m_player = GameObject.FindWithTag("Player");
        if (m_player != null)
        {
            if (m_playerUI == null) m_playerUI = m_player.GetComponent<PlayerUI>();

            Cursor.lockState = CursorLockMode.None;
            m_playerUI.m_playMenu.SetActive(false);
            m_playerUI.m_pauseMenu.SetActive(false);
            m_playerUI.m_deathMenu.SetActive(true);
        }
    }
}
