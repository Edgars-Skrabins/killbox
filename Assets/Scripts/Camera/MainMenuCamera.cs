using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField]
    float Speed;
    
    
    void Update()
    {
        transform.Rotate(0, Speed * Time.deltaTime, 0);
    }
    
}
