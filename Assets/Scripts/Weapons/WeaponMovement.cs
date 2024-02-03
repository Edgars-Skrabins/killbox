using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    [SerializeField] private Transform m_weaponCameraTF;
    [SerializeField] private Transform m_weaponTF;

    private void Update()
    {
        if (m_bLookAtAimPoint)
        {
            LookAtAimPoint();
        }

        if (m_weaponSway)
        {
            WeaponSway();
        }
    }

#region LookAtAimPointVariables

    [Header(" ----- LookAtAimPoint Settings ----- ")]
    [Space(5)]

    [SerializeField] private bool m_bLookAtAimPoint;

    [Tooltip("Speed at which the gun rotates towards the new rotation")]
    [SerializeField] private float m_rotationSpeed;

    [Tooltip("Minimum distance required for the LookAtAimPoint to activate")]
    [SerializeField] private float m_lookAtActivationDistance;

    [Tooltip("Layers which will be used to determin what the gun can look at")]
    [SerializeField] private LayerMask m_lookAtLayers;

    private Quaternion m_lookRotation;
    private Vector3 m_direction;

#endregion

    private void LookAtAimPoint()
    {
        if (!Physics.Raycast(m_weaponCameraTF.position, m_weaponCameraTF.forward, out var hit,Mathf.Infinity ,m_lookAtLayers))
            return;

        Vector3 distanceBetweenGunAndHitPoint = hit.point - m_weaponTF.position;
        if (distanceBetweenGunAndHitPoint.magnitude <= m_lookAtActivationDistance)
            return;

        m_direction = (hit.point - m_weaponTF.position).normalized;
        m_lookRotation = Quaternion.LookRotation(m_direction);

        //m_weaponTF.rotation = Quaternion.Slerp(m_weaponTF.rotation, m_lookRotation, Time.deltaTime / m_rotationDurationInSeconds);

        float step = m_rotationSpeed * Time.deltaTime;
        m_weaponTF.rotation = Quaternion.RotateTowards(m_weaponTF.rotation, m_lookRotation, step);
    }

    #region Weapon Sway Variables

    [Space(20)]

    [Header(" ----- Weapon Sway Settings ----- ")]
    [Space(5)]

    [SerializeField] private bool m_weaponSway;

    [SerializeField] private float m_SwayRoughness;
    [SerializeField] private float m_swayMultiplier;

    #endregion

    private void WeaponSway()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * m_swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * m_swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        m_weaponTF.localRotation = Quaternion.Slerp(m_weaponTF.localRotation, targetRotation, m_SwayRoughness * Time.deltaTime);
    }

    #region ShotAnimationVariables

    [Space(20)]

    [Header(" ----- Shot Animation Settings -----")]
    [Space(5)]

    [SerializeField] private Animation m_weaponAnimation;
    [SerializeField] private string m_shotAnimationName;
    [SerializeField] private float m_shotAnimationFadeLength;

    [SerializeField] private bool m_overridePlayingAnimation;
    
    #endregion

    public void PlayShotAnimation()
    {
        if(m_overridePlayingAnimation)
        {
            if(m_weaponAnimation.isPlaying) m_weaponAnimation.Stop();
        }
        
        m_weaponAnimation.CrossFade(m_shotAnimationName, m_shotAnimationFadeLength);
    }
    
#region IdleAnimationVariables

    [Space(20)]

    [Header(" ----- Idle Animation Settings -----")]
    [Space(5)]
    
    [SerializeField] private string m_idleAnimationName;
    [SerializeField] private float m_idleAnimationFadeLength;


#endregion

    public void PlayIdleAnimation()
    {
        m_weaponAnimation.CrossFade(m_idleAnimationName, m_idleAnimationFadeLength);
    }

    private void OnEnable()
    {
        PlayIdleAnimation();
    }
}
