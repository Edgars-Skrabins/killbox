using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    #region References

    [SerializeField] private Transform m_cameraTF;

    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Locks the cursor in the screen 
    }

    private void Update()
    {
        if (GameManager.I.IsGamePaused)
        {
            return;
        }

        MouseControl();
        MovementControl();
    }
    
    private float m_xRotation;
    private void MouseControl()
    {
        var mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * PlayerStats.I.MouseSensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * PlayerStats.I.MouseSensitivity;

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);

        m_cameraTF.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
        PlayerStats.I.PlayerTF.Rotate(Vector3.up * mouseX);

    }
    
    private Vector3 m_Velocity;
    private void MovementControl()
    {

        var movementX = Input.GetAxisRaw("Horizontal");
        var movementZ = Input.GetAxisRaw("Vertical");
        
        Vector3 movementInput = new Vector3(movementX, 0f, movementZ);
        Vector3 movementDirection = PlayerStats.I.PlayerTF.TransformDirection(movementInput).normalized;

        Vector3 m_Velocity = movementDirection * (PlayerStats.I.PlayerMoveSpeed * Time.deltaTime);
        m_Velocity.y = PlayerStats.I.Gravity;
        
        PlayerStats.I.PlayerCharacterController.Move(m_Velocity);
    }
}
