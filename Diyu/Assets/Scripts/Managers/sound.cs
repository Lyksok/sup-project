using System.Transactions;
using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioClip newMusic;
    public AudioClip originalMusic;
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = FindObjectOfType<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Aucun AudioSource trouvé dans la scène.");
        }

        _audioSource.clip = originalMusic;
        _audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
            if (_audioSource != null && newMusic != null)
            {
                _audioSource.clip = newMusic;
                _audioSource.Play();
            }
    }

    void OnTriggerExit(Collider other)
    {
            if (_audioSource != null && originalMusic != null)
            {
                _audioSource.clip = originalMusic;
                _audioSource.Play();
            }
    }
}

