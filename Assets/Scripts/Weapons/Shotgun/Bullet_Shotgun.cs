using MoreMountains.Feedbacks;
using UnityEngine;

public class Bullet_Shotgun : Bullet
{

    [SerializeField] private MMF_Player m_bulletAnimation;

    protected override void Start()
    {
        base.Start();
        m_bulletAnimation.Initialization();
        m_bulletAnimation.PlayFeedbacks();
    }
}
