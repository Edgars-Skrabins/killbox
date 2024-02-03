using UnityEngine;

public class UIFollowPlayer : MonoBehaviour
{

    private GameObject m_player;

    private void Start()
    {
        m_player = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (m_player)
        {
            RotateTitle(m_player.transform.position);
        }
    }

    private void RotateTitle(Vector3 pointTo)
    {
        transform.LookAt(pointTo);
    }
}
