using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioClip newMusic;
    public AudioClip originalMusic;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Aucun AudioSource trouvé dans la scène.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
            if (audioSource != null && newMusic != null)
            {
                audioSource.clip = newMusic;
                audioSource.Play();
            }
    }

    void OnTriggerExit(Collider other)
    {
            if (audioSource != null && originalMusic != null)
            {
                audioSource.clip = originalMusic;
                audioSource.Play();
            }
    }
}

