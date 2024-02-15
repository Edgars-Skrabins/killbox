using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsGamePaused {get; set;}

    public bool IsPlayerAlive {get; set;}

    [SerializeField] private float m_timeSinceGameStart;
    public float TimeSinceGameStart {get => m_timeSinceGameStart; set => m_timeSinceGameStart = value;}

    private GameObject m_player;

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

        Time.timeScale = 0;

        m_player = GameObject.FindWithTag("Player");
        if (m_player != null)
        {
            Cursor.lockState = CursorLockMode.None;
            m_player.GetComponent<PlayerControls>().enabled = false;
            m_player.GetComponent<PlayerUI>().m_playMenu.SetActive(false);
            m_player.GetComponent<PlayerUI>().m_pauseMenu.SetActive(true);
            m_player.GetComponent<PlayerUI>().m_deathMenu.SetActive(false);
        }
    }

    public void UnpauseGame()
    {
        if (!IsPlayerAlive) return;
        IsGamePaused = false;
        AudioManager.I.UnPauseAllSounds();

        Time.timeScale = 1;

        m_player = GameObject.FindWithTag("Player");
        if (m_player != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            m_player.GetComponent<PlayerControls>().enabled = true;
            m_player.GetComponent<PlayerUI>().m_playMenu.SetActive(true);
            m_player.GetComponent<PlayerUI>().m_pauseMenu.SetActive(false);
            m_player.GetComponent<PlayerUI>().m_deathMenu.SetActive(false);

            PlayerStats.I.MouseSensitivity = m_player.GetComponent<PlayerUI>().m_sensitivitySlider.value;
            PlayerPrefs.SetFloat("MouseSensitivity", m_player.GetComponent<PlayerUI>().m_sensitivitySlider.value);
            PlayerPrefs.SetFloat("MasterVolume", m_player.GetComponent<PlayerUI>().m_audioSlider.value);
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
            Cursor.lockState = CursorLockMode.None;
            m_player.GetComponent<PlayerUI>().m_playMenu.SetActive(false);
            m_player.GetComponent<PlayerUI>().m_pauseMenu.SetActive(false);
            m_player.GetComponent<PlayerUI>().m_deathMenu.SetActive(true);
        }
    }
}
