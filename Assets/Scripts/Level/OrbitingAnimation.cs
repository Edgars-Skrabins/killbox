using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f;

    private void LateUpdate()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
