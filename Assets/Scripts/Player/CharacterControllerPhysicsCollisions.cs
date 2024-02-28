using UnityEngine;

public class CharacterControllerPhysicsCollisions : MonoBehaviour
{
    [SerializeField] private float m_forceMagnitude;

    private void OnControllerColliderHit(ControllerColliderHit _hit)
    {
        HandlePhysicsCollision(_hit);
    }

    private void HandlePhysicsCollision(ControllerColliderHit _hit)
    {
        if (_hit.collider.TryGetComponent(out Rigidbody rigidBody))
        {
            var playerPosition = PlayerStats.I.PlayerTF.position;
            var forceDirection = _hit.gameObject.transform.position - playerPosition;

            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidBody.AddForceAtPosition(forceDirection * m_forceMagnitude, playerPosition, ForceMode.Impulse);
        }
    }
}
