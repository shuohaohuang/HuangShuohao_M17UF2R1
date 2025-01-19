using UnityEngine;

public abstract class ACollectables : MonoBehaviour
{
    public AudioClip audioClip;

    public AudioSource audioSource;

    private void Start()
    {
        audioSource.clip = audioClip;
    }
}
