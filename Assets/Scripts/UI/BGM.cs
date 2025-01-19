using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    public static Bgm instance;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip BgmClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource.clip = BgmClip;
        audioSource.Play();
    }

    public static void DestroySingleton()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = null;
        }
    }
}
