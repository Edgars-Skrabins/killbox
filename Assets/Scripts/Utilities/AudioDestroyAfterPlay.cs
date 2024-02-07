using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroyAfterPlay : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;

    // Update is called once per frame
    void Update()
    {
        if (!m_audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
