using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    #region References

    public static PlayerStats I { get; private set; }

    [SerializeField] private PlayerHealth m_playerHealthCS;
    public PlayerHealth PlayerHealthCS { get => m_playerHealthCS; }

    #endregion

    #region PlayerStats

    [SerializeField] private float m_playerMoveSpeed = 1f;
    public float PlayerMoveSpeed { get => m_playerMoveSpeed; set => m_playerMoveSpeed = value; }


    [SerializeField] private float m_playerJumpHeight;
    public float PlayerJumpHeight { get => m_playerJumpHeight; set => m_playerJumpHeight = value; }


    [SerializeField] private float m_gravity = -9.81f;
    public float Gravity { get => m_gravity; set => m_gravity = value; }


    [SerializeField] private float m_mouseSensitivity = 100f;
    public float MouseSensitivity { get => m_mouseSensitivity; set => m_mouseSensitivity = value; }

    [SerializeField] private float m_masterVolume = 1f;
    public float MasterVolume { get => m_masterVolume; set => m_masterVolume = value; }

    [SerializeField] private int m_playerMaxHealth;
    public int PlayerMaxHealth { get => m_playerMaxHealth; set => m_playerMaxHealth = value; }


    [SerializeField] private int m_playerHealth = 100;
    public int PlayerHealth { get => m_playerHealth; set => m_playerHealth = value; }


    [SerializeField] private int m_playerStartingHealth;


    [SerializeField] private Transform m_playerTF;
    public Transform PlayerTF { get => m_playerTF; }


    [SerializeField] private CharacterController m_playerCharacterController;
    public CharacterController PlayerCharacterController { get => m_playerCharacterController; }


    [SerializeField] private int m_playerScore;
    public int PlayerScore { get => m_playerScore; set => m_playerScore = value; }
    
    
    [SerializeField] private int m_playerHighScore;
    public int PlayerHighScore { get => m_playerHighScore; set { m_playerHighScore = value; PlayerPrefs.SetInt("PlayerHighscore", value); } }


    [SerializeField] private Camera m_playerCamera;
    public Camera PlayerCamera {get => m_playerCamera;}


    #endregion

    private void Awake()
    {

        #region Singleton

        if (I != this && I != null)
        {
            Destroy(this);
        }
        else if (I == null)
        {
            I = this;
        }

        #endregion

        //PlayerPrefs.SetInt("PlayerHighscore", PlayerHighScore);
        //SetFloat("MouseSensitivity", 50);

        PlayerHealth = m_playerStartingHealth;

        m_playerHighScore = PlayerPrefs.GetInt("PlayerHighscore");
        m_mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 50);
        MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 50);
        m_masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume");


    }

    private void Start()
    {
        GameManager.I.PlayerAlive();
        m_playerScore = 0;
        GameManager.I.UnpauseGame();
    }

    private void Update()
    {
        PlayerHealth = Mathf.Clamp(PlayerHealth, 0, PlayerMaxHealth);

        if (PlayerScore > PlayerHighScore)
        {
            PlayerHighScore = PlayerScore;
        }


    }
}
